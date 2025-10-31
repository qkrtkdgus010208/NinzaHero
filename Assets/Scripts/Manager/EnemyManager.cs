using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private Coroutine stageRoutine;

    [SerializeField]
    private List<GameObject> enemyPrefabs;

    [SerializeField]
    private List<Rect> spawnAreas;

    [SerializeField]
    private Color gizmoColor = new Color(1, 0, 0, 0.3f); // 기즈모 색상

    public List<EnemyController> activeEnemies = new List<EnemyController>();

    private bool enemySpawnComplite;

    private GameManager gameManager;
    private StageManager stageManager;

    public void Init(GameManager gameManager, StageManager stageManager)
    {
        this.gameManager = gameManager;
        this.stageManager = stageManager;
    }

    public void StartStage(int stageCount)
    {
        if (stageCount <= 0)
        {
            gameManager.EndOfStage();
            return;
        }

        if (stageRoutine != null)
            StopCoroutine(stageRoutine);
        stageRoutine = StartCoroutine(SpawnStage(stageCount));
    }

    public void StopStage()
    {
        StopAllCoroutines();
    }

    private IEnumerator SpawnStage(int stageCount)
    {
        enemySpawnComplite = false;

        for (int i = 0; i < stageCount; i++)
        {
            yield return null;
            SpawnRandomEnemy();
        }

        enemySpawnComplite = true;
    }

    private void SpawnRandomEnemy()
    {
        if (enemyPrefabs.Count == 0 || stageManager.ActiveStage == null)
        {
            Debug.LogWarning("Enemy Prefabs 또는 ActiveStage가 설정되지 않았습니다.");
            return;
        }

        GameObject randomPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];

        Transform spawnPoint = stageManager.ActiveStage.spawnPoints[Random.Range(0, stageManager.ActiveStage.spawnPoints.Length)];

        // 적 생성 및 리스트에 추가
        GameObject spawnedEnemy = Instantiate(randomPrefab, spawnPoint.position, Quaternion.identity);
        EnemyController enemyController = spawnedEnemy.GetComponent<EnemyController>();
        enemyController.Init(this, gameManager.player.transform);

        activeEnemies.Add(enemyController);
    }

    public void RemoveEnemyOnDeath(EnemyController enemy)
    {
        activeEnemies.Remove(enemy);
        if (enemySpawnComplite && activeEnemies.Count == 0)
            gameManager.EndOfStage();
    }
}
