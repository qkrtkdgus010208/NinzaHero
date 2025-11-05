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
   public AudioSource buttonAudio; 

  public AudioClip Accept5;

  private SpriteRenderer raylight_0;
  private SpriteRenderer raylight_1;
  private SpriteRenderer raylight_2;

  [SerializeField] private float ResetX = -8.46f;
  [SerializeField] private float StartX = 8.32f;
  [SerializeField] private float CloudStartX = 0.95f;



  private void Awake()
  {
	Cloud.transform.localPosition = new Vector3(0.95f,3.22f,0f);
    raylight_0 = RayLight_0.GetComponent<SpriteRenderer>();
	raylight_1 = RayLight_1.GetComponent<SpriteRenderer>();
	raylight_2 = RayLight_2.GetComponent<SpriteRenderer>();

	StartButton.onClick.AddListener(OnClickStartButton);
    
  }

  // Start is called before the first frame update
  void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
	CloudMove();
    raylightMoving(raylight_0);
	raylightMoving(raylight_1);
	raylightMoving(raylight_2);

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

  void raylightMoving(SpriteRenderer i)
  {
    Color j = i.color;
    float speed = 0.1f;

	if (j.a == 1 || j.a > 0.5)
	{
	  j.a -= 1 * speed;
	}

	else if ( j.a <= 0.5)
    {
	  j.a+= 1 * speed;
	}
    
    

    i.color = j;  

  }



public void OnClickStartButton()
  {

    buttonAudio.PlayOneShot(Accept5);

  StartCoroutine(wait());

    
  }

  IEnumerator wait()
  {
    yield return new WaitForSeconds(1f);
	SceneManager.LoadScene("GameScene");
  }






}



