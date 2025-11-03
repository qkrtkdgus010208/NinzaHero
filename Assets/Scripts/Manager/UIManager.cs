using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UIManager : MonoBehaviour
{
  public GameObject GameOver;
  public GameObject HpBarUI;
  public GameObject StageMove;
  public GameObject SkillSlot;
  public GameObject GameClear;
  



  private void Start()
  {
	GameOver.SetActive(false);
  }


  public void ShowGameOver()
  {
	GameOver.SetActive(true);
  }

  public void ShowStageMove()
  {
	StageMove.SetActive(true);
  }

  public void ShowSkillSlot()
  {
	SkillSlot.SetActive(true);
  }

  public void ShowGameClear()
  {
	GameClear.SetActive(true);
  }

}
