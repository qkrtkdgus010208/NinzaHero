using System.Collections.Generic;
using UnityEngine;

public class StageController : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints;

    GameManager gameManager;
    EnemyManager enemyManager;
    StageManager stageManager;

    bool isRunning = false;
    public bool IsRunning {  get { return isRunning; } }

    bool isClear = false;
    public bool IsClear { get { return isClear; } }

    // TODO:: 몬스터 스폰 정도 수정 필요
    [SerializeField] List<GameObject> enemyPrefabs;
    [SerializeField] List<int> spawnCount;
    public PolygonCollider2D polygonCollider;

    public Transform GetRandomSpawnPoint()
    {
        Transform point = spawnPoints[Random.Range(0, spawnPoints.Length)];
        return point;
    }

    public void SetActive(bool isActive)
    {
        gameObject.SetActive(isActive);

        if (enemyPrefabs.Count != spawnCount.Count) 
        { 
            Debug.Log($"{gameObject.name} 오브젝트 셋팅 에러");
            isClear = true;
            return;
        }

        isRunning = false;
        isClear = false;
    }

    public void StartPhase(StageManager stageManager, GameManager gameManager, EnemyManager enemyManager)
    {
        this.stageManager = stageManager;
        this.gameManager = gameManager;
        this.enemyManager = enemyManager;

        if (enemyPrefabs.Count <= 0)
        {
            Debug.Log($"{gameObject.name} 셋팅 안함");
            isClear = true;
            return;
        }

        // 페이즈 준비
        for (int i = 0; i < enemyPrefabs.Count; i++)
        {
            GameObject prefab = enemyPrefabs[i];
            int count = spawnCount[i];
            enemyManager.Spawn(this, prefab, count);
        }

        // 페이즈 실행
        isRunning = true;
    }

    public void IsOver()
    {
        if(isRunning)
        {
            if(enemyManager.IsEmpty())
            {
                isRunning = false;
                isClear = true;
            }
        }
    }


}
