using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StageMgr : MonoBehaviour
{
  public GameObject[] Stage;
  public TextMeshProUGUI[] StageNumber;
  public List<Image> FightIcon = new List<Image>();
  public GameObject BossStage;
 



  void Start()
  {




  }

  void ImageMove()
  {
  }

  void BossStageImage()
  {
    Transform BossImage =BossStage.transform.Find("Image/BossIcon");
    Image bossIcon = BossImage.GetComponent<Image>();

    //스테이지에서 boss이름이랑 스프라이트를 찾아와서 
    //if(스테이지스택 => 4 && LastBoss.Dead == true)
    //{
          

    //}
  }
  
  void CleatStage()
  {






  }


}

    
