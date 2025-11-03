using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndUI : MonoBehaviour
{


  [SerializeField] private Button Main;
  [SerializeField] private Button Restart;
    // Start is called before the first frame update
    void Start()
    {

    Main.onClick.AddListener(StartScene);
    Restart.onClick.AddListener(RestartGameScene);


        //게임이 over되면 setactive
    }

  void StartScene()
  {
     SceneManager.LoadScene ("StartScene");


  }

  void RestartGameScene()
  {
    SceneManager.LoadScene ("GameScene");
  }

    
}
