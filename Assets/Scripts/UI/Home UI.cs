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

  public void OnClickStartButton()
  {

  }

  protected override UIState GetUIState()
  {
	throw new System.NotImplementedException();
  }
}
