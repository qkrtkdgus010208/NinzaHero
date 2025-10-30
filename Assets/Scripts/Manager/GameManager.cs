using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    public PlayerController player { get; private set; }
    private ResourceController playerResourceController;

    private EnemyManager enemyManager;

    [SerializeField] private int currentStageIndex = 0;

    public static bool isFirstLoading = true;

    private void Awake()
    {
        instance = this;

        enemyManager = GetComponentInChildren<EnemyManager>();
        enemyManager.Init(this);

        player = FindAnyObjectByType<PlayerController>();
        player.Init(this, enemyManager);
    }

    private void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
        StartNextStage();
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // 어플리케이션 종료
#endif
    }

    private void StartNextStage()
    {
        player.transform.position = new Vector3(0f, -7f, 0f);
        currentStageIndex += 1;
        enemyManager.StartStage(1 + currentStageIndex);
    }

    public void EndOfStage()
    {
        StartNextStage();
    }

    public void GameOver()
    {
        enemyManager.StopStage();
    }
}
