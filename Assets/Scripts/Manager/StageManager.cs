using System;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;
using Random = UnityEngine.Random;
public class StageManager : MonoBehaviour
{
    private GameManager gameManager;
    private EnemyManager enemyManager;

    [SerializeField] private StageController[] stages;
    [SerializeField] private Transform group_01_03; 
    [SerializeField] private Transform group_04_05;
    [SerializeField] private Transform group_Boss;

    public int ActiveStage { get; private set; } = 0;

    public StageController ActiveStageController;

    public StageController[] stageControllers;

    public void Init(GameManager gm)
    {
        gameManager = gm;
        // 자식이 비활성일 수도 있으니 true 사용
        enemyManager = gameManager.EnemyManager;

        stageControllers = GetComponentsInChildren<StageController>(true);
	}

    private StageController ActivateRandom(Transform group)
    {
        //그룹의 자식들 중 하나를 랜덤으로 선택
        Transform map =  group.GetChild(Random.Range(0, group.childCount));
        StageController sc1 =  map.GetComponent<StageController>();
        sc1.SetActive(true);
        return sc1;

        //var stage = new List<StageController>();
        //for (int i = 0; i < group.childCount; i++)
        //{
        //    var child = group.GetChild(i);
        //    var sc = child.GetComponent<StageController>();
        //    child.gameObject.SetActive(false);
        //    if (sc != null) stage.Add(sc);
        //}

        //var pick = stage[Random.Range(0, stage.Count)];
        //pick.SetActive(true);
        //return pick;
    }
    public void StartStage()
    {
        if (ActiveStage == 0)
            ActiveStageController = ActivateRandom(group_01_03);
        else if (ActiveStage == 1)
            ActiveStageController = ActivateRandom(group_04_05);
        else
            ActiveStageController = ActivateRandom(group_Boss);

        ActiveStageController?.StartPhase(this, gameManager, enemyManager);
    }

    public void DeathOfEnemy(EnemyController enemy)
    {
        ActiveStageController.IsOver();
    }

    public void NextStage()
    {
        ActiveStageController.SetActive(false);

        ActiveStage += 1;
        gameManager.StartNextStage();
    }

    internal bool OnExit()
    {
        if (ActiveStageController != null && ActiveStageController.IsClear)
        {
            NextStage();
            return true;
        }
        return false;
    }
}
