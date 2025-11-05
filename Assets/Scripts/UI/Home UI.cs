using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeUI : BaseUI
{
  [SerializeField] private Button StartButton;

  public override void Init(UIManager uiManager)
  {
	base.Init(uiManager);

	StartButton.onClick.AddListener(OnClickStartButton);

  }


  private void Update()
  {
	


  }


  public void OnClickStartButton()
  {
	GameManager.Instance.StartGame();
  }

  
}
