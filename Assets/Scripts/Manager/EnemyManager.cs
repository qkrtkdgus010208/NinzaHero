using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;
using static UnityEditor.PlayerSettings;

public class EnemyManager : MonoBehaviour
{
    [Header("Prefabs & Spawn")]
    [SerializeField] private List<GameObject> enemyPrefabs;

    public List<EnemyController> activeEnemies = new List<EnemyController>();
    private GameManager gameManager;
    private StageManager stageManager;

    public void Init(GameManager gameManager)
    {
        this.gameManager = gameManager;
        stageManager = gameManager.StageManager;
    }

    public void Spawn(StageController stageController, GameObject prefab, int count)
    {
        for (int i = 0; i < count; i++)
        {
            Transform point = stageController.GetRandomSpawnPoint();

            Vector3 pos = point.position;
            GameObject go = Instantiate(prefab, pos, Quaternion.identity);
            var ec = go.GetComponent<EnemyController>();
            ec.Init(this, gameManager);
            activeEnemies.Add(ec);
        }
    }


    public void DeathOfEnemy(EnemyController enemy)
    {
        // 몬스터 죽음 통보
        activeEnemies.Remove(enemy);
        stageManager.DeathOfEnemy(enemy);
    }

    public bool IsEmpty()
    {
        return activeEnemies.Count == 0;
    }
}
