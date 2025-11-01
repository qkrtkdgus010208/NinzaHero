using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
[RequireComponent(typeof(TilemapCollider2D))]
public class TrapTile : MonoBehaviour
{
    [SerializeField] int damage = 50;       //트랩 데미지
    [SerializeField] float hitCooldown = 1f;  //적중 후 다음 적중 쿨타임 1초
    float lastHitTime = -999f; //최초 트랩 접촉 시 데미지 바로 받게끔 값 할당

    void OnTriggerEnter2D(Collider2D other)
    {
        TryHit(other);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        TryHit(other);
    }

    //문제점 : 가만히 서 있으면 작동 안 함. 무조건 움직여야 데미지 적용. 넉백로직을 적용할지 고민

    void TryHit(Collider2D other)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer("Player")) return;
        if (Time.time - lastHitTime < hitCooldown) return;

        lastHitTime = Time.time;
        //var hp = other.GetComponent<statHandler.Health>(); // player 체력 컴포넌트, player체력이 깎이도록
        //if (hp) hp.TakeDamage(damage);  // hp에서 데미지 받는 함수
        Debug.Log("함정 발동");
    }
}