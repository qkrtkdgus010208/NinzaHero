using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.GraphicsBuffer;

public class PlayerController : BaseController
{
    [SerializeField] BossController boss;
    
    private GameManager gameManager;
    private EnemyManager enemyManager;
    private Transform target;

    public void Init(GameManager gameManager, EnemyManager enemyManager)
    {
        this.gameManager = gameManager;
        this.enemyManager = enemyManager;
    }

    private Transform FindNearestTarget()
    {
        float nearestDistance = float.MaxValue;
        Transform nearestTarget = null;

        if (enemyManager == null || enemyManager.activeEnemies == null || enemyManager.activeEnemies.Count == 0)
        {
            if (boss == null) return null;
            if(boss.isAlive && boss.isActive)
            {
                return boss.thisPos;
            }
            else
            {
                return null;
            }
        }

        foreach (var enemy in enemyManager.activeEnemies)
        {
            if (enemy != null && enemy.transform != null)
            {
                float distance = Vector3.Distance(transform.position, enemy.transform.position);

                if (distance < nearestDistance)
                {
                    nearestDistance = distance;
                    nearestTarget = enemy.transform;
                }
            }
        }

        return nearestTarget;
    }

    protected override void HandleAction()
    {
        base.HandleAction();

        target = FindNearestTarget();

        if (target == null)
        {
            lookDirection = Vector2.zero;
            isAttacking = false;
            return;
        }

        float distance = DistanceToTarget();
        Vector2 direction = DirectionToTarget();

        lookDirection = direction;

        if (distance <= weaponHandler.AttackRange)
        {
            int layerMaskTarget = weaponHandler.target;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, weaponHandler.AttackRange * 1.5f,
                (1 << LayerMask.NameToLayer("Level")) | layerMaskTarget);

            if (hit.collider != null && layerMaskTarget == (layerMaskTarget | (1 << hit.collider.gameObject.layer)))
            {
                isAttacking = true;
            }

            return;
        }
    }

    private float DistanceToTarget()
    {
        return Vector3.Distance(transform.position, target.position);
    }

    private Vector2 DirectionToTarget()
    {
        return (target.position - transform.position).normalized;
    }
    void OnMove(InputValue inputValue)
    {
        movementDirection = inputValue.Get<Vector2>();
        movementDirection = movementDirection.normalized;
    }

    public override void Death()
    {
        base.Death();
        gameManager.GameOver();
    }

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }
}
