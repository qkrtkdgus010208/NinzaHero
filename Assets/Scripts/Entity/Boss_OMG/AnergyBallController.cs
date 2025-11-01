using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnergyBallController : MonoBehaviour
{
    Rigidbody2D rigidbody;
    CircleCollider2D collider;
    PlayerController player;

    Vector2 Direction;

    private float damage = 100f;
    private float MaxDuration = 2.0f;
    private float inAirDuration = 0f;
    private bool inAir = false;

    public void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        collider = GetComponent<CircleCollider2D>();
        player = FindAnyObjectByType<PlayerController>();
    }
    public void Init(Transform target, Vector2 direction)
    {
        Direction = direction;
        inAir = true;
    }

    public void Update()
    { 
        if(inAir)
        {
            inAirDuration += Time.deltaTime;

            if(inAirDuration >= MaxDuration)
            {
                DestroyBorn();
                inAir = false;
            }
        }
    }

    private void FixedUpdate()
    {
        AirTime(Direction);
    }

    private void AirTime(Vector2 direction)
    {
        rigidbody.velocity += direction;
    }

    private void DestroyBorn()
    {
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            DestroyBorn();
            ResourceController resource = collision.GetComponent<ResourceController>();
            if(resource != null)
            {
                resource.ChangeHealth(-damage);
            }    
        }
        else
        {
            DestroyBorn();
        }
    }
}
