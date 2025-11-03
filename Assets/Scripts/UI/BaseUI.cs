using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseUI : MonoBehaviour
{
   protected UIManager uiManager;

  public virtual void Init(UIManager uiManager)
  {
	this.uiManager = uiManager;
  }

 







}
