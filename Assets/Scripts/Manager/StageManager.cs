// StageManager.cs
using UnityEngine;

public class StageManager : MonoBehaviour
{
    private GameManager gameManager;
    private EnemyManager enemyManager;

    // (있다면 사용, 없다면 없어도 됨) 스테이지별 적 수 배열
    [SerializeField] private int[] enemiesPerStage = new int[] { 5, 7, 9 };

    public void Init(GameManager gm)
    {
        gameManager = gm;
        // EnemyManager 참조 캐시(자식에 붙어있다면)
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

        // --- 핵심: 안전하게 적 수량 계산 ---
        int enemyCount;
        if (enemiesPerStage != null &&
            enemiesPerStage.Length > 0 &&
            stageCountOrIndex >= 0 &&
            stageCountOrIndex < enemiesPerStage.Length)
        {
            // 넘겨진 값을 "인덱스"로 해석
            enemyCount = enemiesPerStage[stageCountOrIndex];
        }
        else
        {
            // 범위를 벗어나면 "그 값 자체를 적 수량"으로 해석
            enemyCount = Mathf.Max(1, stageCountOrIndex);
        }

        enemyManager.StartStage(enemyCount);
    }

    public void EndOfStage()
    {
        gameManager.EndOfStage();
    }
}
