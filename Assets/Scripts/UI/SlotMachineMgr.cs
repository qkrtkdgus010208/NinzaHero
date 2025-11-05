using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SlotMachineMgr : MonoBehaviour
{
	public GameObject[] SlotSkillObject;// 두루마리같은 오브젝트 3개

	public Button[] Slot;

	public Sprite[] SkillSprite;

	public TextMeshProUGUI[] SkillName;
	public TextMeshProUGUI[] Description;
	public GameObject[] skillNameOb;
	public GameObject[] DescriptionOb;

	public Skill[] Skills;

	public GameObject SkillSlots;//ui에 있는거 두루마리랑 텍스트 그런것들 백그라운드는 포함안되고 그런것들 다 한번에 내려갔다가 위로올라가는거 만드는거임

	public GameObject SkillSlot; //얘는 이거 다 담고있는거 그냥 이거 그 ui이 자체를 껐다킬때 사용하는거임 버튼에 이미지 할당 끝나면 버튼에 onclick에 
								 // addAddListener 달아서 OnSlotClicked 함수 넣어서 이거 실행되면 그 ui 이거 내려갔다 올라가는 애니메이션 실행되게하고
								 //그다음에 실행 끝나면 invoke로 1초있다가 이거 ui전체 끌려고 오브젝트 만들어둔거

	[System.Serializable]
	public class DisplayItemSlot
	{
		public List<Image> SlotSprite = new List<Image>();
	}
	public DisplayItemSlot[] DisplayItemSlots;



	public List<int> StartList = new List<int>();
	public List<int> ResultIndexList = new List<int>();
	int ItemCnt = 2;

	private Button selectedButton = null;

	void Start()
	{



	}
	private void OnEnable()
	{
		StartCoroutine(WaitSlot());
		ResultIndexList.Clear();
	}
	IEnumerator WaitSlot()
	{
		yield return new WaitForSeconds(0.05f);
		SlotStart();
	}


	void SlotStart()
	{
		for (int i = 0; i < ItemCnt * Slot.Length; i++)//6
		{
			if (GameManager.Instance.StageManager.ActiveStage == 0 && i ==2)
			{
				

			    StartList.Add(3);
				
			}
			else
			{
				StartList.Add(i);
			}
				
		}
		for (int i = 0; i < Slot.Length; i++)//slot1 slot2 slot3    
		{
			for (int j = 0; j < ItemCnt; j++)//image1 image2   22222222222
			{
				Slot[i].interactable = false;
				int randomIndex = Random.Range(0, StartList.Count);
				if(i == 0 && j == 0 || i == 1 && j == 0 || i == 2 && j == 0)
				{
					ResultIndexList.Add(StartList[randomIndex]);
				}

				DisplayItemSlots[i].SlotSprite[j].sprite = SkillSprite[StartList[randomIndex]]; //DisplayItemSlots[1].SlotSprite[3].sprite = SkillSprite[StartList[randomIndex]]

				


				StartList.RemoveAt(randomIndex);

			}

			
		}
		foreach (Skill k in Skills)
		{
			for (int Y = 0; Y < ResultIndexList.Count; Y++)
			{

				if (SkillSprite[ResultIndexList[Y]] == k.data.skillIcon)
				{
					Slot[Y].onClick.AddListener(k.OnClick);
					SkillName[Y].text = k.data.skillName;
					Description[Y].text = k.data.skillDesc;

					foreach (GameObject q in skillNameOb)
					{
						q.SetActive(false);
					}
					foreach (GameObject e in DescriptionOb)
					{
						e.SetActive(false);
					}
				}
			}
		}

		StartCoroutine(StartSlot1());
		StartCoroutine(StartSlot2());
		StartCoroutine(StartSlot3());

		foreach (GameObject q in skillNameOb)
		{
			q.SetActive(true);
		}
		foreach (GameObject e in DescriptionOb)
		{
			e.SetActive(true);
		}


		foreach (Button slot in Slot)
		{
			slot.onClick.AddListener(() => OnSlotClicked(slot));
		}

	
	}


	IEnumerator StartSlot1()
	{

		RectTransform gg  = SlotSkillObject[0].GetComponent<RectTransform>();
		
		for (int i = 0; i < 12*16; i++)
		{
			gg.anchoredPosition -= new Vector2( 0, 3.63930625f);
			if (SlotSkillObject[0].transform.localPosition.y < -29.1289f)
			{
				SlotSkillObject[0].transform.localPosition += new Vector3 (0, 58.2289f, 0);

			}
			yield return null;
		}
		Slot[0].interactable = true;


	}


	IEnumerator StartSlot2()
	{

		RectTransform gg = SlotSkillObject[1].GetComponent<RectTransform>();

		for (int i = 0; i < 12*16; i++)
		{
			gg.anchoredPosition -= new Vector2(0, 3.63930625f);
			if (SlotSkillObject[1].transform.localPosition.y <= -29.1289f)
			{
				SlotSkillObject[1].transform.localPosition += new Vector3(0, 58.2289f, 0);

			}
			yield return null;
		}
		Slot[1].interactable = true;


	}

	IEnumerator StartSlot3()
	{

		RectTransform gg = SlotSkillObject[2].GetComponent<RectTransform>();

		for (int i = 0; i < 12*16 ; i++)
		{
			gg.anchoredPosition -= new Vector2(0, 3.63930625f);
			if (SlotSkillObject[2].transform.localPosition.y < -29.1289f)
			{
				SlotSkillObject[2].transform.localPosition += new Vector3(0, 58.2289f, 0);

			}
			yield return null;
		}
		Slot[2].interactable = true;


	}









	void OnSlotClicked(Button ClickedButton)
	{
		if (selectedButton != null) return;

		selectedButton = ClickedButton;

		foreach (Button btn in Slot)
		{

			btn.interactable = false;
		}
		Animator SkillAnim = SkillSlots.GetComponent<Animator>();

		//skill이 들어갈 곳

		SkillAnim.SetTrigger("Play");
		selectedButton = null;
		Invoke(nameof(HideCanvas), 1f);
	}


	public void HideCanvas()
	{
		SkillSlot.SetActive(false);

	}




}
