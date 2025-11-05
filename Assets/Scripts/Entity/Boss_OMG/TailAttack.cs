using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TailAttack : MonoBehaviour
{
    [SerializeField] private LayerMask PlayerCollisionLayer;

    Animator animator;
    
    float damage = 2000f;
    
    bool isEnd = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        animator.SetTrigger("IsActive");
    }

    // Update is called once per frame
    void Update()
    {
        if(isEnd)
        {
            DestroyTail();
        }
    }

    void DestroyTail()
    {
        animator.SetTrigger("IsDestroy");
        Destroy(this.gameObject);
    }

    public void SetDamage(Collider2D collision)
    {
        Debug.Log("플레이어 피격!");
        if(PlayerCollisionLayer.value == (PlayerCollisionLayer.value | (1 << collision.gameObject.layer)))
        {
            ResourceController resource = collision.GetComponent<ResourceController>();
            if(resource != null)
            {
                resource.ChangeHealth(-damage);
            }
        }
    }

    private void OnAnimationEnd()
    {
        isEnd = true;
    }    
}
