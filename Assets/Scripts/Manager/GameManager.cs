using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    public PlayerController player { get; private set; }
    private ResourceController playerResourceController;

    [SerializeField] private EnemyManager enemyManager;
    [SerializeField] private UIManager uiManager;

    private void Awake()
    {
        instance = this;
        player.Init(this);
    }

    public void GameOver()
    {

    }
}
