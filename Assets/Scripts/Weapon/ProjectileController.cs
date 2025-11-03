using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField] private LayerMask levelCollisionLayer;
    [SerializeField] private LayerMask bossCollisionLayer;

    private Rigidbody2D rigid;
    private ProjectileManager projectileManager;
    private RangeWeaponHandler rangeWeaponHandler;

    private Transform pivot;
    private Vector2 direction;
    private float currentDuration;
    private bool isReady;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        pivot = transform.GetChild(0);
    }

    private void Update()
    {
        if (!isReady)
        {
            return;
        }

        currentDuration += Time.deltaTime;

        if (currentDuration > rangeWeaponHandler.Duration)
        {
            DestroyProjectile();
        }

        rigid.velocity = direction * rangeWeaponHandler.Speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log( "layer : "+ collision.gameObject.layer);

        if (collision.gameObject.CompareTag("Boss"))
        {
            Debug.Log("공격감지");

            BossController bossHp = collision.GetComponent<BossController>();

            if (bossHp != null)
            {
                bossHp.Damaged(100);
            }
        }
        else if (rangeWeaponHandler.target.value == (rangeWeaponHandler.target.value | (1 << collision.gameObject.layer)))
        {
            ResourceController resourceController = collision.GetComponent<ResourceController>();
            if (resourceController != null)
            {
                resourceController.ChangeHealth(-rangeWeaponHandler.Power);
                if (rangeWeaponHandler.IsOnKnockback)
                {
                    BaseController controller = collision.GetComponent<BaseController>();
                    if (controller != null)
                    {
                        controller.ApplyKnockback(transform, rangeWeaponHandler.KnockbackPower, rangeWeaponHandler.KnockbackTime);
                    }
                }
            }

            DestroyProjectile();
        }
        else if (levelCollisionLayer.value == (levelCollisionLayer.value | (1 << collision.gameObject.layer)))
        {
            DestroyProjectile();
        }
    }

    public void Init(Vector2 direction, RangeWeaponHandler weaponHandler, ProjectileManager projectileManager)
    {
        this.projectileManager = projectileManager;

        rangeWeaponHandler = weaponHandler;

        this.direction = direction;
        currentDuration = 0;
        transform.localScale = Vector3.one * weaponHandler.BulletSize;

        transform.right = this.direction;

        isReady = true;
    }

    private void DestroyProjectile()
    {
        Destroy(this.gameObject);
    }
}
