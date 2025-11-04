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
    Main.onClick.AddListener(StartScene);
    Next.onClick.AddListener(NextStage);
    }

	


	void StartScene()
  {
       SceneManager.LoadScene("StartScene");
  }

  void NextStage()
  {

        GameManager.Instance.StartNextStage();

  }

  



}
