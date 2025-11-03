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
  [SerializeField] private GameObject Player;
  int CurrentHealth;



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

	Debug.Log($"플레이어의 현재 체력 : {GameManager.Instance.playerResourceController.CurrentHealth}");
  } //newValue로 체력값을 받아와서 패딩을 줄이거나 늘릴량을 계산하는 코드
  //이 코드를 체력이 있는 플레이어 스크립트에서 체력을 받아와서 캐릭터가 공격을 받을때마다 실행하거나 게임이 실행되는 순간 update에서 실행되야한다고생각한다.


}
