using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameUI : BaseUI
{
  [SerializeField] private TextMeshProUGUI waveText;
  [SerializeField] private SliderJoint2D hpSlider;

  protected override UIState GetUIState()
  {
    return UIState.Game;
  }
}
