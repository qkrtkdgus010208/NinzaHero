using UnityEngine;

public class StageManager : MonoBehaviour
{
    private GameManager gameManager;
    private EnemyManager enemyManager;

    [SerializeField] private int[] enemiesPerStage = new int[] { 5, 7, 9 };

    public void Init(GameManager gm)
    {
        gameManager = gm;
        enemyManager = gameManager.GetComponentInChildren<EnemyManager>();
    }

    public void StartStage(int stageCountOrIndex)
    {
        if (gameManager == null)
        {
            Debug.LogError("[StageManager] Init이 먼저 호출되지 않았습니다.");
            return;
        }
        if (enemyManager == null)
        {
            enemyManager = gameManager.GetComponentInChildren<EnemyManager>();
            if (enemyManager == null)
            {
                Debug.LogError("[StageManager] EnemyManager를 찾을 수 없습니다.");
                return;
            }
        }

        int enemyCount;
        if (enemiesPerStage != null &&
            enemiesPerStage.Length > 0 &&
            stageCountOrIndex >= 0 &&
            stageCountOrIndex < enemiesPerStage.Length)
        {
            enemyCount = enemiesPerStage[stageCountOrIndex];
        }
        else
        {
            enemyCount = Mathf.Max(1, stageCountOrIndex);
        }

        enemyManager.StartStage(enemyCount);
    }

    public void EndOfStage()
    {
        gameManager.EndOfStage();
    }
}
