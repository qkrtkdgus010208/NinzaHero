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

    public void Init(GameManager gameManager) => this.gameManager = gameManager;

    public void RandomSpawn(StageController stageController)
    {
        // 지금은 랜덤
        GameObject prefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];

        Transform point = stageController.GetRandomSpawnPoint();

        Vector3 pos = point.position;
        GameObject go = Instantiate(prefab, pos, Quaternion.identity);
        var ec = go.GetComponent<EnemyController>();
        ec.Init(this, gameManager);
        activeEnemies.Add(ec);
    }


    public void RemoveEnemyOnDeath(EnemyController enemy)
    {
        activeEnemies.Remove(enemy);
    }
}
