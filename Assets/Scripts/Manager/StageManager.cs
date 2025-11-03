using UnityEngine;

public class StageManager : MonoBehaviour
{
    private GameManager gameManager;
    private EnemyManager enemyManager;

    [Header("Stage Config")]
    [Tooltip("스테이지별 적 수(인덱스로 접근). 비워두면 StartStage 인자를 '수량'으로 해석합니다.")]
    [SerializeField] private int[] enemiesPerStage = new int[] { 5, 7, 9 };

    /// <summary>
    /// 현재(또는 마지막으로 시작한) 스테이지 인덱스. 0부터 시작.
    /// UI나 로깅에서 참조할 수 있도록 공개(Get 전용).
    /// </summary>
    public int ActiveStage { get; private set; } = 0;

    public void Init(GameManager gm)
    {
        gameManager = gm;
        // 자식이 비활성일 수도 있으니 true 사용
        enemyManager = (gameManager != null) ? gameManager.GetComponentInChildren<EnemyManager>(true) : null;
        if (enemyManager == null)
            enemyManager = FindAnyObjectByType<EnemyManager>();
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
            enemyManager = gameManager.GetComponentInChildren<EnemyManager>(true);
            if (enemyManager == null)
            {
                Debug.LogError("[StageManager] EnemyManager를 찾을 수 없습니다.");
                return;
            }
        }

        int enemyCount;

        // enemiesPerStage가 설정되어 있고, 인덱스 범위 안이면 "인덱스"로 해석
        bool treatAsIndex =
            enemiesPerStage != null &&
            enemiesPerStage.Length > 0 &&
            stageCountOrIndex >= 0 &&
            stageCountOrIndex < enemiesPerStage.Length;

        if (treatAsIndex)
        {
            ActiveStage = stageCountOrIndex;                 // 현재 스테이지 인덱스 기록
            enemyCount = Mathf.Max(1, enemiesPerStage[ActiveStage]);
        }
        else
        {
            // 범위를 벗어나면 "그 값 자체를 적 수량"으로 해석
            enemyCount = Mathf.Max(1, stageCountOrIndex);
            // 인자로 '수량'이 들어온 경우엔 ActiveStage를 변경하지 않음 (GameManager가 관리)
        }

        enemyManager.StartStage(enemyCount);
    }

    public void EndOfStage()
    {
        gameManager?.EndOfStage();
    }
}
