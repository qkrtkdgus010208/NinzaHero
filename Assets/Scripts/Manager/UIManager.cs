using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UIManager : MonoBehaviour
{
	public GameObject HpBarUI; //캔버스 안에 UI들 배치해놓는 순서
	
	public GameObject SkillSlot;
	public GameObject GameClear;
	public GameObject GameOver;
	public GameObject BossHealth;



	private void Awake()
	{
		GameOver.SetActive(false);
		GameClear.SetActive(false);
		SkillSlot.SetActive(false);
		HpBarUI.SetActive(true);
		BossHealth.SetActive(false);
	}

	private void Update()
	{
		if(GameManager.Instance.isBossStage)
		{
			BossHealth.SetActive(true);
		}
		if(BossController.instance != null && BossController.instance.Hp == 0)
		{
			StartCoroutine(WaitClear());
		}
	}

	IEnumerator WaitClear()
	{
		yield return new WaitForSeconds(1f);
		GameClear.SetActive(true);
	}

	public void ShowGameOver()
	{
		GameOver.SetActive(true);
	}
	public void ShowSkillSlot()
	{
		SkillSlot.SetActive(true);
	}
	

	public void ShowGameClear()
	{
		GameClear.SetActive(true);
	}
	public void ShowHpBarUI()
	{
		HpBarUI.SetActive(true);
	}
}
