using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using Cinemachine;

public class EnergyBallController : MonoBehaviour
{
    [Range(1, 100)][SerializeField] private float damage;
    public float Damage
    {
        get => damage;
        set => Mathf.Clamp(value, 0f, 100f);
    }

    [SerializeField] private LayerMask levelCollisionLayer;
    [SerializeField] private LayerMask bounceSpotCollisionLayer;
    [SerializeField] private LayerMask PlayerCollisionLayer;

    private Vector2 direction;
    private Vector2 reflect;

    private float reflectAngle = 45f;
    private float MaxDuration = 10.0f;
    private float inAirDuration = 0f;
    private float speed = 2f;

    private int bounceCount = 0;
    private int maxBounce = 3;

    private bool inAir = false;
    private bool isReflect = false;

    public void Init(Vector2 direction)
    {
        this.direction = direction * speed;
        inAir = true;
    }

    public void Update()
    {
        if (inAir)
        {
            if(isReflect)
            {
                direction = -reflect;
            }

            transform.Translate(direction * speed * Time.deltaTime, Space.World);
            inAirDuration += Time.deltaTime;

            if (inAirDuration >= MaxDuration)
            {
                DestroyBorn();
                inAir = false;
                inAirDuration = 0f;
            }
        }
    }

    private void DestroyBorn()
    {
        isReflect = false;
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (PlayerCollisionLayer.value == (PlayerCollisionLayer.value | (1 << collision.gameObject.layer)))
        {
            DestroyBorn();
            ResourceController resource = collision.GetComponent<ResourceController>();
            if (resource != null)
            {
                resource.ChangeHealth(-damage);
            }
        }
        else if (levelCollisionLayer.value == (levelCollisionLayer.value | (1 << collision.gameObject.layer))
            || bounceSpotCollisionLayer.value == (bounceSpotCollisionLayer.value | (1 << collision.gameObject.layer)))
        {
            Bounce();
        }
    }

    public void Bounce()
    {
        isReflect = true;
        float angle = Random.value > 0.5f ? reflectAngle : -reflectAngle;
        Vector2 reflectDir = Quaternion.Euler(0, 0, angle) * direction;
        reflect = reflectDir;
        bounceCount++;

        if (bounceCount > maxBounce)
        {
            DestroyBorn();
        }
    }
}
