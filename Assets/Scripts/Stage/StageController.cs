using UnityEngine;

public class StageController : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints;

    GameManager gameManager;
    EnemyManager enemyManager;
    StageManager stageManager;

    bool isRunning = false;

    public Transform GetRandomSpawnPoint()
    {
        Transform point = spawnPoints[Random.Range(0, spawnPoints.Length)];
        return point;
    }

    public void SetActive(bool isActive)
    {
        gameObject.SetActive(isActive);
    }

    public void StartPhase(StageManager stageManager, GameManager gameManager, EnemyManager enemyManager)
    {
        this.stageManager = stageManager;
        this.gameManager = gameManager;
        this.enemyManager = enemyManager;

        isRunning = true;

        // 적 생성
        enemyManager.RandomSpawn(this);
    }


}
