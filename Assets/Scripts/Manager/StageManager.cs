using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] GameObject[] stages;

    private int stageIndex;

    public void StartStage(int stageCount)
    {
        foreach (GameObject stage in stages)
        {
            stage.SetActive(false);
        }

        if (stageCount == 1)
        {
            stageIndex = Random.Range(0, 3);
        }
        else if (stageCount == 2)
        {
            stageIndex = Random.Range(3, 5);
        }
        else
        {
            stageIndex = 5;
        }

        stages[stageIndex].SetActive(true);
    }
}
