using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SlotMachineMgr : MonoBehaviour
{
  public GameObject[] SlotSkillObject;
 
  public Button[] Slot;

  public Sprite[] SkillSprite;

  public TextMeshProUGUI[] SkillName;
  public TextMeshProUGUI[] Description;

  public GameObject SkillSlot;//애니메이션으로 쓸 스킬슬롯

  [System.Serializable]
  public class DisplayItemSlot
  {
    public List<Image> SlotSprite = new List<Image>();
  }
  public DisplayItemSlot[] DisplayItemSlots;

  public Skill skillclass;
  public Canvas slotCanvas;


  public List<int> StartList = new List<int>(); 
  public List<int> ResultIndexList = new List<int>();
  int ItemCnt = 4;

  private Button selectedButton = null;

  void Start()
  {




	for(int i =  0; i < ItemCnt * Slot.Length; i++)//12
	{
	  StartList.Add(i);//12
	}
	for(int i = 0 ;  i <Slot.Length; i++)//3
	{
	  for ( int j = 0; j <ItemCnt; j++)//3
	  {
		Slot[i].interactable = false;
		int randomIndex = Random.Range( 0, StartList.Count);
		if (i == 0 && j == 1 || i ==1 && j ==0 || i ==2 && j == 2)
		{
		  ResultIndexList.Add(StartList[randomIndex] );
		}
		DisplayItemSlots[i].SlotSprite[j].sprite = SkillSprite[StartList[randomIndex]]; //DisplayItemSlots[1].SlotSprite[3].sprite = SkillSprite[StartList[randomIndex]]

		if ( j == 0)
		{
		  DisplayItemSlots[i].SlotSprite[ItemCnt].sprite = SkillSprite[StartList[randomIndex]];
		}
		StartList.RemoveAt(randomIndex);
	
	  }
	}
	foreach (Button slot in Slot)
	{
	  slot.onClick.AddListener(() => OnSlotClicked(slot));
	}
	StartCoroutine(SpinSlot(SlotSkillObject[0].GetComponent<RectTransform>(), 58.2289f, 600f));
	StartCoroutine(SpinSlot2(SlotSkillObject[1].GetComponent<RectTransform>(), 58.2289f, 600f));
	StartCoroutine(SpinSlot3(SlotSkillObject[2].GetComponent<RectTransform>(), 58.2289f, 600f));

	

  }
  IEnumerator SpinSlot(RectTransform slotRect, float itemHeight, float speed) //1
  {
	
	float loopDistance = itemHeight * 4;  
	float startY = slotRect.anchoredPosition.y;

	float elapsed = 0f;//코루틴이 시작한 뒤부터 걸리는 시간


	int stopIndex = 3; //4개중 제일 위
	float targetY = startY - (itemHeight * stopIndex);

	bool slowingDown = false; 
	while (true )
	{
	  float step = speed * Time.deltaTime; 
	  slotRect.anchoredPosition -= new Vector2(0, step);
	 
	  elapsed += Time.deltaTime;



	  if (slotRect.anchoredPosition.y <= -27.8F)
	  {
		slotRect.anchoredPosition += new Vector2(0, loopDistance);
		

	  }

	  if (slowingDown && Mathf.Abs(slotRect.anchoredPosition.y - targetY) < 1.5f)
	  {
		slotRect.anchoredPosition = new Vector2(slotRect.anchoredPosition.x, targetY);
		Slot[0].interactable = true;
		break; 
	  }

	  
	  if (elapsed > 1f && !slowingDown)
	  {
		slowingDown = true;
	  }
	
	  yield return null;
	}
  }


  IEnumerator SpinSlot2(RectTransform slotRect, float itemHeight, float speed) //0
  {
	
	float loopDistance = itemHeight * 4;  // 한 바퀴 높이 (58.2 * 5)
	float startY = slotRect.anchoredPosition.y;
	
	int stopIndex = 4;   // 0~4 중 하나에서 멈추기
	float targetY = startY - (itemHeight * stopIndex);

	float elapsed = 0f;//코루틴이 시작한 뒤부터 걸리는 시간

	bool slowingDown = false; // 속도 줄이기 시작했는가

	while (true)
	{
	  float step = speed * Time.deltaTime; // 프레임당 이동 거리
	  slotRect.anchoredPosition -= new Vector2(0, step);

	  elapsed += Time.deltaTime;
	  // 한 칸(58.2px)을 넘어가면 다음 칸으로 정렬


	  // 맨 아래까지 내려가면 위로 되감기
	  if (slotRect.anchoredPosition.y <= -27.8F)
	  {
		slotRect.anchoredPosition += new Vector2(0, loopDistance);
		
	  }

	  if (slowingDown && Mathf.Abs(slotRect.anchoredPosition.y - targetY) < 1.5f)
	  {
		slotRect.anchoredPosition = new Vector2(slotRect.anchoredPosition.x, targetY);
		Slot[1].interactable = true;

		break; // while문 종료 (코루틴 끝)
	  }

	  // 예시: 2초 후 감속 시작
	  if (elapsed > 1f && !slowingDown)
	  {
		slowingDown = true;
	  }

	  yield return null;
	}
  }

  IEnumerator SpinSlot3(RectTransform slotRect, float itemHeight, float speed) //2
  {
	
	float loopDistance = itemHeight * 4;  // 한 바퀴 높이 (58.2 * 5)
	float startY = slotRect.anchoredPosition.y;

	int stopIndex = 2;   // 0~4 중 하나에서 멈추기
	float targetY = startY - (itemHeight * stopIndex);

	float elapsed = 0f;//코루틴이 시작한 뒤부터 걸리는 시간

	bool slowingDown = false; // 속도 줄이기 시작했는가

	while (true)
	{
	  float step = speed * Time.deltaTime; // 프레임당 이동 거리
	  slotRect.anchoredPosition -= new Vector2(0, step);

	  elapsed += Time.deltaTime;
	  // 맨 아래까지 내려가면 위로 되감기
	  if (slotRect.anchoredPosition.y <= -27.8F)
	  {
		slotRect.anchoredPosition += new Vector2(0, loopDistance);
		
	  }

	  if (slowingDown && Mathf.Abs(slotRect.anchoredPosition.y - targetY) < 1.5f)
	  {
		slotRect.anchoredPosition = new Vector2(slotRect.anchoredPosition.x, targetY);
		Slot[2].interactable = true;
		break; // while문 종료 (코루틴 끝)
	  }

	  // 예시: 2초 후 감속 시작
	  if (elapsed > 1f && !slowingDown)
	  {
		slowingDown = true;
	  }

	  yield return null;
	}
  }

  void OnSlotClicked(Button ClickedButton)
  {
	if(selectedButton != null) return;

	selectedButton = ClickedButton;

	foreach(Button btn in Slot)
	{
	  
	  btn.interactable = false;
	}
	Animator SkillAnim = SkillSlot.GetComponent<Animator>();

	//skill이 들어갈 곳

	SkillAnim.SetTrigger("Play");

	Invoke(nameof(HideCanvas), 1f);

	
	

  }


  public void HideCanvas()
  {
	slotCanvas.enabled = false;
  }

  void SkilInput(int i , int j)
  {

	
	




  }



}
