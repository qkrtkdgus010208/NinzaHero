using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
[RequireComponent(typeof(TilemapCollider2D))]

public class Portal : MonoBehaviour
{
    bool fired;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (fired) return; 
        if (other.gameObject.layer != LayerMask.NameToLayer("Player")) return;
        // if문으로 몬스터가 남아 있다면 return하도록 해야 합니다.
        fired = true;
        //GameManager.Instance.StartNextStage();  //GameManager의 startnextstage함수를 public으로 바꿔주면 정상동작
        Debug.Log("다음스테이지로");
    }
}
