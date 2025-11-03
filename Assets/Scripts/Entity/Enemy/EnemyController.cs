using UnityEngine;
using System.Collections;

public class EnemyController : BaseController
{
    private EnemyManager enemyManager;
    private Transform target;

    [Header("Follow")]
    [SerializeField] private float followRange = 15f;

    [Header("Chase Only")]
    [SerializeField] private bool chaseOnly = false;

    [Header("Contact Attack")]
    [SerializeField] private bool useContactAttack = false;
    [SerializeField] private float contactDamage = 8f; 
    [SerializeField] private float contactCooldown = 0.5f;
    [SerializeField] private float contactRange = 0.6f;
    [SerializeField] private bool contactUseKnockback = true;
    [SerializeField] private float contactKnockbackPower = 4f;
    [SerializeField] private float contactKnockbackTime = 0.12f;
    private float nextContactTime = 0f;

    [Header("Charger")]
    [SerializeField] private bool useCharger = false; 
    [SerializeField] private float chargeWindup = 0.5f;
    [SerializeField] private float chargeSpeed = 12f;
    private bool isWindup = false;
    private bool isCharging = false;
    private Vector2 chargeDir = Vector2.zero;
    private float originalSpeed = 0f;

    public void Init(EnemyManager enemyManager, Transform target)
    {
        this.enemyManager = enemyManager;
        this.target = target;
    }
    public void ConfigureContactAttack(bool enable)
    {
        useContactAttack = enable;
        if (enable)
        {
            chaseOnly = false;
            useCharger = false;
        }
        if (weaponHandler != null) weaponHandler.enabled = !enable;
    }

    public void ConfigureCharger(bool enable)
    {
        useCharger = enable;
        if (enable)
        {
            chaseOnly = false;
            useContactAttack = false;
        }
        if (weaponHandler != null) weaponHandler.enabled = !enable;
    }

    public void ConfigureChaseOnly(bool enable)
    {
        chaseOnly = enable;
        if (enable)
        {
            useContactAttack = false;
            useCharger = false;
        }
        if (weaponHandler != null) weaponHandler.enabled = !enable;
    }

    protected override void HandleAction()
    {
        base.HandleAction();

        if (target == null)
        {
            if (!movementDirection.Equals(Vector2.zero)) movementDirection = Vector2.zero;
            return;
        }

        if (useCharger)
        {
            ChargerUpdate();
            return;
        }

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
                        isAttacking = true;
                    }

                    movementDirection = Vector2.zero;
                    return;
                }

                movementDirection = direction;
                return;
            }
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
    private void ChargerUpdate()
    {
        if (isCharging)
        {
            movementDirection = chargeDir;
            lookDirection = chargeDir;
            return;
        }

        if (isWindup)
        {
            movementDirection = Vector2.zero;
            return;
        }
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
        statHandler.Speed = chargeSpeed;
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
        int levelLayer = LayerMask.NameToLayer("Level");
        if (isCharging && col.collider.gameObject.layer == levelLayer)
            StopCharge();

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
