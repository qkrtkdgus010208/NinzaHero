using System;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    private GameManager gameManager;
    private EnemyManager enemyManager;

    [SerializeField] private StageController[] stages;

    public int ActiveStage { get; private set; } = 0;

    public StageController ActiveStageController;

    public StageController[] stageControllers;

    public void Init(GameManager gm)
    {
        gameManager = gm;
        // 자식이 비활성일 수도 있으니 true 사용
        enemyManager = gameManager.EnemyManager;

        stageControllers = GetComponentsInChildren<StageController>(true);
		ActiveStageController= stageControllers[0];
	}

    public void StartStage()
    {
		ActiveStageController.SetActive(false);
		ActiveStageController = stageControllers[ActiveStage];

        
		ActiveStageController.SetActive(true);
		ActiveStageController.StartPhase(this, gameManager, enemyManager);
		ActiveStage += 1;
		
        
      
	}

    public void DeathOfEnemy(EnemyController enemy)
    {
        ActiveStageController.IsOver();
    }

    private void NextStage()
    {
        ActiveStageController.SetActive(false);

        ActiveStage += 1;
        StartStage();
    }

    internal bool OnExit()
    {
        if(ActiveStageController.IsClear)
        {
            NextStage();
            return true;
        }
        return false;
    }
}
