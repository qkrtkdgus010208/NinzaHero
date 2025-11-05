using UnityEngine;
using System.Collections;

public class EnemyController : BaseController
{
    private GameManager gameManager;
    private EnemyManager enemyManager;
    private BaseController target;

    [Header("Follow")]
    [SerializeField] private float followRange = 15f;


    public void Init(EnemyManager enemyManager, GameManager gameManager)
    {
        this.gameManager = gameManager;
        this.enemyManager = enemyManager;
        this.target = gameManager.player;
    }
    protected override void HandleAction()
    {
        base.HandleAction();

        // 적이 범위 내에 없을 때
        if (target == null || weaponHandler == null)
        {
            movementDirection = Vector2.zero;
            return;
        }

        float distance = DistanceToTarget();
        Vector2 direction = DirectionToTarget();

        isAttacking = false;

        if (distance <= followRange)
        {
            // 바라 보게 하기
            lookDirection = direction;
            movementDirection = direction;

            // 공격 무기에 대한 준비가 되어 있는지
            if (weaponHandler != null && weaponHandler.enabled)
            {
                // 공격이 시작 될 때
                if (distance <= weaponHandler.AttackRange)
                {
                    isAttacking = true;
                    return;
                }
            }
        }
        else
            movementDirection = Vector2.zero;
    }

    private float DistanceToTarget() => Vector3.Distance(transform.position, target.transform.position);
    private Vector2 DirectionToTarget() => (target.transform.position - transform.position).normalized;

    public override void Death()
    {
        enemyManager.DeathOfEnemy(this);
        base.Death();
    }


#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        //if (useContactAttack)
        //{
        //    Gizmos.color = new Color(1f, 0.4f, 0f, 0.4f);
        //    Gizmos.DrawWireSphere(transform.position, contactRange);
        //}
    }
#endif
}
