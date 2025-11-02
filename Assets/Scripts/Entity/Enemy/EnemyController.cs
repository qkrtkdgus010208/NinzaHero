using UnityEngine;
using System.Collections;

public class EnemyController : BaseController
{
    private EnemyManager enemyManager;
    private Transform target;

    [Header("Follow")]
    [SerializeField] private float followRange = 15f;

    // ===== 추격만 모드 =====
    [Header("Chase Only")]
    [SerializeField] private bool chaseOnly = false;   // true면 공격 없이 추격만

    // ===== 접촉(근접) 공격 옵션 =====
    [Header("Contact Attack")]
    [SerializeField] private bool useContactAttack = false;   // true면: 닿으면 피해
    [SerializeField] private float contactDamage = 8f;        // 1회 피해량
    [SerializeField] private float contactCooldown = 0.5f;    // 같은 대상 연속타격 간격
    [SerializeField] private float contactRange = 0.6f;       // 거의 붙었다고 보는 거리
    [SerializeField] private bool contactUseKnockback = true;
    [SerializeField] private float contactKnockbackPower = 4f;
    [SerializeField] private float contactKnockbackTime = 0.12f;
    private float nextContactTime = 0f;

    // ===== 돌진(Charger) 옵션 =====
    [Header("Charger")]
    [SerializeField] private bool useCharger = false;         // true면: 0.5초 멈췄다가 벽에 부딪칠 때까지 돌진
    [SerializeField] private float chargeWindup = 0.5f;       // 정지(윈드업) 시간
    [SerializeField] private float chargeSpeed = 12f;         // 돌진 중 속도(절대값)
    private bool isWindup = false;
    private bool isCharging = false;
    private Vector2 chargeDir = Vector2.zero;
    private float originalSpeed = 0f;

    public void Init(EnemyManager enemyManager, Transform target)
    {
        this.enemyManager = enemyManager;
        this.target = target;
    }

    // === 외부에서 타입 토글할 수 있도록 공개 API ===
    public void ConfigureContactAttack(bool enable)
    {
        useContactAttack = enable;
        if (enable)
        {
            chaseOnly = false;
            useCharger = false;
        }
        if (weaponHandler != null) weaponHandler.enabled = !enable; // 근접만이면 원거리 무기 비활성
    }

    public void ConfigureCharger(bool enable)
    {
        useCharger = enable;
        if (enable)
        {
            chaseOnly = false;
            useContactAttack = false; // 기본은 '돌진만'
        }
        if (weaponHandler != null) weaponHandler.enabled = !enable; // 돌진형이면 원거리 무기 비활성
    }

    public void ConfigureChaseOnly(bool enable)
    {
        chaseOnly = enable;
        if (enable)
        {
            useContactAttack = false;
            useCharger = false;
        }
        if (weaponHandler != null) weaponHandler.enabled = !enable; // 추격만이면 원거리 무기 비활성
    }

    protected override void HandleAction()
    {
        base.HandleAction();

        if (target == null)
        {
            if (!movementDirection.Equals(Vector2.zero)) movementDirection = Vector2.zero;
            return;
        }

        // 1) 돌진형이 켜져 있으면 우선
        if (useCharger)
        {
            ChargerUpdate();
            return;
        }

        // 2) 추격만 모드
        if (chaseOnly)
        {
            float d = DistanceToTarget();
            Vector2 dir = DirectionToTarget();
            if (d <= followRange)
            {
                lookDirection = dir;
                movementDirection = dir;
            }
            else
            {
                movementDirection = Vector2.zero;
            }
            return;
        }

        // 3) 일반(원거리/접촉형)
        if ((weaponHandler == null) && !useContactAttack)
        {
            if (!movementDirection.Equals(Vector2.zero)) movementDirection = Vector2.zero;
            return;
        }

        float distance = DistanceToTarget();
        Vector2 direction = DirectionToTarget();
        isAttacking = false;

        if (distance <= followRange)
        {
            lookDirection = direction;

            // (A) 원거리/무기 공격
            if (!useContactAttack && weaponHandler != null && weaponHandler.enabled)
            {
                if (distance <= weaponHandler.AttackRange)
                {
                    int layerMaskTarget = weaponHandler.target;
                    RaycastHit2D hit = Physics2D.Raycast(
                        transform.position,
                        direction,
                        weaponHandler.AttackRange * 1.5f,
                        (1 << LayerMask.NameToLayer("Level")) | layerMaskTarget
                    );

                    if (hit.collider != null &&
                        layerMaskTarget == (layerMaskTarget | (1 << hit.collider.gameObject.layer)))
                    {
                        isAttacking = true; // BaseController가 Delay 체크 후 weaponHandler.Attack() 호출
                    }

                    movementDirection = Vector2.zero;
                    return;
                }

                movementDirection = direction;
                return;
            }

            // (B) 접촉형: 계속 추적, 거의 붙으면 멈춰서 밀착 유지
            if (useContactAttack)
            {
                movementDirection = (distance <= contactRange * 0.9f) ? Vector2.zero : direction;
                return;
            }
        }
        else
        {
            if (!movementDirection.Equals(Vector2.zero)) movementDirection = Vector2.zero;
        }
    }

    // ===== Charger 전용 업데이트 =====
    private void ChargerUpdate()
    {
        // 돌진 중이면 계속 전진
        if (isCharging)
        {
            movementDirection = chargeDir;
            lookDirection = chargeDir;
            return;
        }

        // 윈드업 중이면 멈춤 유지
        if (isWindup)
        {
            movementDirection = Vector2.zero;
            return;
        }

        // 플레이어 발견 시(거리 조건): 윈드업 → 돌진 시작
        float distance = DistanceToTarget();
        if (distance <= followRange)
        {
            Vector2 dir = DirectionToTarget();
            lookDirection = dir;
            StartCoroutine(ChargeRoutine(dir));
        }
        else
        {
            movementDirection = Vector2.zero;
        }
    }

    private IEnumerator ChargeRoutine(Vector2 dir)
    {
        isWindup = true;
        movementDirection = Vector2.zero;
        lookDirection = dir;
        yield return new WaitForSeconds(chargeWindup);

        isWindup = false;
        isCharging = true;

        chargeDir = dir.sqrMagnitude > 0.0001f ? dir.normalized : Vector2.right;
        originalSpeed = statHandler.Speed;
        statHandler.Speed = chargeSpeed;   // 돌진 동안 속도 고정

        // 멈추는 타이밍은 OnCollisionEnter2D에서 'Level'에 부딪힐 때 처리
    }

    private void StopCharge()
    {
        if (!isCharging) return;
        isCharging = false;
        statHandler.Speed = originalSpeed;
        movementDirection = Vector2.zero;
    }

    private float DistanceToTarget() => Vector3.Distance(transform.position, target.position);
    private Vector2 DirectionToTarget() => (target.position - transform.position).normalized;

    public override void Death()
    {
        base.Death();
        enemyManager.RemoveEnemyOnDeath(this);
    }

    // ===== 접촉 피해 처리(접촉형/돌진형에서 함께 사용 가능) =====
    private bool IsPlayerObject(Transform other)
    {
        if (target == null || other == null) return false;
        return (other == target) || other.IsChildOf(target);
    }

    private void TryContactDamage(Vector2 hitPoint)
    {
        if (!useContactAttack || target == null) return;
        if (Time.time < nextContactTime) return;

        var rc = target.GetComponent<ResourceController>();
        if (rc != null)
        {
            rc.ChangeHealth(-contactDamage);

            if (contactUseKnockback)
            {
                var bc = target.GetComponent<BaseController>();
                if (bc != null)
                    bc.ApplyKnockback(this.transform, contactKnockbackPower, contactKnockbackTime);
            }
        }

        nextContactTime = Time.time + contactCooldown;
    }

    // Trigger/Collision 양쪽 지원
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (IsPlayerObject(other.transform)) TryContactDamage(other.ClosestPoint(transform.position));
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (IsPlayerObject(other.transform)) TryContactDamage(other.ClosestPoint(transform.position));
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        // 벽(Level)에 부딪히면 돌진 종료
        int levelLayer = LayerMask.NameToLayer("Level");
        if (isCharging && col.collider.gameObject.layer == levelLayer)
            StopCharge();

        // 플레이어에 닿으면 접촉 피해(옵션)
        if (IsPlayerObject(col.collider.transform))
            TryContactDamage(col.GetContact(0).point);
    }
    private void OnCollisionStay2D(Collision2D col)
    {
        if (IsPlayerObject(col.collider.transform))
            TryContactDamage(col.GetContact(0).point);
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        if (useContactAttack)
        {
            Gizmos.color = new Color(1f, 0.4f, 0f, 0.4f);
            Gizmos.DrawWireSphere(transform.position, contactRange);
        }
    }
#endif
}
