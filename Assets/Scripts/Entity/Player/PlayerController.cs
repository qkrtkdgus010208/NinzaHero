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
}
