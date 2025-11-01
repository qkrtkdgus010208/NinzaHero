using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TailAttack : MonoBehaviour
{
    bool isActive = false;
    float duration = 0f;
    float maxExist = 2.0f;
    float damage = 2000f;

    private void Start()
    {
        isActive = true;
    }
    // Update is called once per frame
    void Update()
    {
        if(isActive)
        {
            duration += Time.deltaTime;
            
            if(maxExist < duration)
            {
                DestroyTail();
            }
        }
    }

    void DestroyTail()
    {
        Destroy(this.gameObject);
        isActive = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            ResourceController resource = collision.GetComponent<ResourceController>();
            if(resource != null)
            {
                resource.ChangeHealth(-damage);
            }
        }
        else
        {
            DestroyTail();
        }    
    }
}
