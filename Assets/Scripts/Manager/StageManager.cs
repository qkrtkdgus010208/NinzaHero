using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] private StageController[] stages;

    public StageController ActiveStage { get; private set; }
    
    private GameManager gameManager;

    public void Init(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }

    public void StartStage(int stageCount)
    {
        foreach (StageController stage in stages)
        {
            stage.gameObject.SetActive(false);
        }

        if (stageCount == 1)
        {
            gameManager.stageIndex = Random.Range(0, 3);
        }
        else if (stageCount == 2)
        {
            gameManager.stageIndex = Random.Range(3, 5);
        }
        else
        {
            gameManager.stageIndex = 5;
        }

        ActiveStage = stages[gameManager.stageIndex];
        ActiveStage.gameObject.SetActive(true);
    }
}
