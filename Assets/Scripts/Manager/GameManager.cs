using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public PlayerController player { get; private set; }
    public ResourceController playerResourceController;
    //public BossController boss { get; private set; }

    private EnemyManager enemyManager;
    private StageManager stageManager;
    private UIManager uiManager;
    public EnemyManager EnemyManager { get { return enemyManager; } }

    private StageManager stageManager;
    public StageManager StageManager { get { return stageManager; } }

    public int stageIndex = 0;

    public static bool isFirstLoading = true;

    [SerializeField] private CameraConfinerSetter cameraConfinerSetter;

    protected override void Awake()
    {
        base.Awake();

        enemyManager = GetComponentInChildren<EnemyManager>();
        stageManager = GetComponentInChildren<StageManager>();


        enemyManager.Init(this);
        stageManager.Init(this);



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
        stageManager.StartStage();

        cameraConfinerSetter.SetConfinerBoundingShape(stageManager.ActiveStageController.polygonCollider);
    }

    public void EndOfStage()
    {
       // StartNextStage();
    }

    public void GameOver()
    {
        enemyManager.StopStage();
        uiManager.ShowGameOver();
    }
}
