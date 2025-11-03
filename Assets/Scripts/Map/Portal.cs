using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
[RequireComponent(typeof(TilemapCollider2D))]

public class Portal : MonoBehaviour
{
    GameManager gameManager;
    StageManager stageManager;

    private void Start()
    {
        gameManager = GameManager.Instance;
        stageManager = gameManager.StageManager;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (stageManager.OnExit())
        {
            Debug.Log("다음스테이지로");
        }
    }
}
