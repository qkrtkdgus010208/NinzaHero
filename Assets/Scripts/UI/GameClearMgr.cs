using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameClearMgr : MonoBehaviour
{
  [SerializeField] private Button Main;
  [SerializeField] private Button Next;


    void Start()
    {
   
    }
	private void OnEnable()
	{
		Main.onClick.AddListener(StartScene);
		Next.onClick.AddListener(NextStage);
		
	}



	void StartScene()
  {
       SceneManager.LoadScene("StartScene");
		GameManager.Instance.uiManager.isGameClear = true;
		GameManager.Instance.uiManager.GameClear.SetActive(false);
		GameManager.Instance.uiManager.BossHealth.SetActive(false);
	}

  void NextStage()
  {

        SceneManager.LoadScene("GameScene");
		GameManager.Instance.uiManager.isGameClear = true;
		GameManager.Instance.uiManager.GameClear.SetActive(false);
		GameManager.Instance.uiManager.BossHealth.SetActive(false);


	}

  



}
