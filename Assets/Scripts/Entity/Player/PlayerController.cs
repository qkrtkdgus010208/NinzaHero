using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.GraphicsBuffer;

public class PlayerController : BaseController
{
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
            return null;
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

public class PlayerController : BaseController
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Action()
    {
        float horizon = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(horizon, vertical);

        if(moveDirection.magnitude != 0f)
        {
            moveDirection = moveDirection.normalized;
        }
        else
        {
            moveDirection = Vector2.zero;
        }
    }
}
