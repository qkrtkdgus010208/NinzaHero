using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    public PlayerController player { get; private set; }
    public ResourceController playerResourceController;
    //public BossController boss { get; private set; }

    private EnemyManager enemyManager;
    private StageManager stageManager;
  private UIManager uiManager;

    [SerializeField] private int currentStageIndex = 0; // 1층, 2층, 3층
    public int stageIndex; // 1층 : 0, 1, 2 / 2층 : 3, 4 / 3층 : 5

    public static bool isFirstLoading = true;

    [SerializeField] private CameraConfinerSetter cameraConfinerSetter;

    private void Awake()
    {
        instance = this;

        stageManager = GetComponentInChildren<StageManager>();
        stageManager.Init(this);

        enemyManager = GetComponentInChildren<EnemyManager>();
        enemyManager.Init(this);

        player = FindAnyObjectByType<PlayerController>();
        player.Init(this, enemyManager);
	    uiManager = GetComponentInChildren<UIManager>();

	//boss = FindAnyObjectByType<BossController>();
	//boss.Init(player.transform);
  }

    private void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
        StartNextStage();
	    uiManager.ShowSkillSlot();
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
        stageManager.StartStage(currentStageIndex);

        cameraConfinerSetter.SetConfinerBoundingShape();
    }

    public void EndOfStage()
    {
        StartNextStage();
    }

    public void GameOver()
    {
        enemyManager.StopStage();
        uiManager.ShowGameOver();
    }
}
