using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
[RequireComponent(typeof(TilemapCollider2D))]


public class TrapTile : MonoBehaviour
{
    ResourceController resourceController; 

    [SerializeField] int Trapdamage = 50;       //트랩 데미지
    [SerializeField] float hitCooldown = 0.3f;  //적중 후 다음 적중 쿨타임 0.3초
    float lastHitTime = -999f; //최초 트랩 접촉 시 데미지 바로 받게끔 값 할당

    float trapKnockbackPower = 1.5f;
    float trapKnockbackTime = 0.4f;

    void OnTriggerEnter2D(Collider2D other)
    {
        TryHit(other);
        BaseController controller = other.GetComponent<BaseController>();
        if (controller != null)
        {
            Rigidbody2D rb = other.attachedRigidbody;

            Vector2 knockDir;

            // 플레이어가 움직이는 방향의 반대로 튕기게
            if (rb != null && rb.velocity.sqrMagnitude > 0.01f)
                knockDir = -rb.velocity.normalized;
            else
                // 정지 상태일 경우엔 트랩 중심 반대 방향으로 튕김
                knockDir = (other.transform.position - transform.position).normalized;

            controller.ApplyTrapKnockback(knockDir, trapKnockbackPower, trapKnockbackTime);
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        TryHit(other);
    }


    void TryHit(Collider2D other)
    {
        var resourceController = other.GetComponent<ResourceController>();  //플레이어 체력 가져옴

        if (other.gameObject.layer != LayerMask.NameToLayer("Player")) return;
        if (Time.time - lastHitTime < hitCooldown) return;

        lastHitTime = Time.time;

        if (resourceController == null)
        {
            // 혹시 콜라이더가 자식에 있을 수도 있으니까 부모에서도 탐색
            resourceController = other.GetComponentInParent<ResourceController>();
            Debug.LogError("Player 체력 null");
        }

        if (resourceController != null)
        {
            resourceController.ChangeHealth(-Trapdamage);
        
            Debug.Log($"함정 발동, 플레이어 체력: {resourceController.CurrentHealth}");
        }
    }
}