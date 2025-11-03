using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameStartUI : MonoBehaviour
{
  [SerializeField] private GameObject Cloud;
  [SerializeField] private GameObject RayLight_0;
  [SerializeField] private GameObject RayLight_1;
  [SerializeField] private GameObject RayLight_2;
  [Range(0f, 1f)][SerializeField] private float Speed = 0.7f;

  [SerializeField] private Button StartButton;
   private AudioSource buttonAudio; 

  private SpriteRenderer LightSpriteRenderer_1;

  [SerializeField] private float ResetX = -8.46f;
  [SerializeField] private float StartX = 8.32f;
  [SerializeField] private float CloudStartX = 0.95f;



  private void Awake()
  {
	Cloud.transform.localPosition = new Vector3(0.95f,3.22f,0f);
    LightSpriteRenderer_1 = RayLight_0.GetComponent<SpriteRenderer>();
	StartButton.onClick.AddListener(OnClickStartButton);
    buttonAudio = StartButton.GetComponent<AudioSource>();
  }

  // Start is called before the first frame update
  void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
	CloudMove();

	}



  void CloudMove()
  {
    Vector3 pos = Cloud.transform.localPosition;
    pos.x -= Time.deltaTime * Speed;
    Cloud.transform.localPosition = pos;

    if (pos.x < ResetX)
    {
      pos.x = StartX;
      Cloud.transform.localPosition = pos;
    }
  }




public void OnClickStartButton()
  {

    if( buttonAudio != null)
      buttonAudio.Play();
    SceneManager.LoadScene("GameScene");
  }








}



