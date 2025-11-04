using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hpbar : MonoBehaviour
{
  [SerializeField] private float _health = 2000; //최대체력
  [SerializeField] RectTransform _barRect;
  [SerializeField] private RectMask2D _mask;
  [Range(0f, 100f)][SerializeField] private float DamageHealth = 0;



  private float _maxRightMask;
  private float _initialRightMask;

  private void Update()
  {
	SetValue();
	
  }


  private void Start()
  {
	//x = left, w = top, y = bottom, z = right

	_maxRightMask = _barRect.rect.width - _mask.padding.x - _mask.padding.z; //full width
	_initialRightMask = _mask.padding.z;
  }

  public void SetValue() //newValue = Current hp 
  {
	var targetWidth =  GameManager.Instance.playerResourceController.CurrentHealth / _health * _maxRightMask; //Current hp * full width/full hp = current width
	var newRightMask = _maxRightMask + _initialRightMask - targetWidth; 
	var padding = _mask.padding;  
	  padding.z = newRightMask;
	_mask.padding = padding;
  }


}
