using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class BossController : MonoBehaviour
{
    Transform boss;

    private Animator animator;
    private CapsuleCollider2D area;
    [SerializeField] public GameObject tail;
    [SerializeField] public GameObject born;
    private float throwSpeed = 10f;
    private int phase;

    private Vector2 direction;
    private bool Attacking = false;
    private float delay = 2.0f;
    private float time;

    private void Awake()
    {
        boss = this.transform;
        animator = GetComponent<Animator>();
        area = GetComponentInChildren<CapsuleCollider2D>();
    }

    [Range(0, 2000)][SerializeField] private int hp = 2000;
    public int Hp
    {
        get => hp;
        set => Mathf.Clamp(value, 0, 2000);
    }

    Transform target;
    public void Init(Transform target)
    {
        this.target = target;
    }

    public void Update()
    {
        if (!Attacking)
        {
            if (target != null)
            {
                time += Time.deltaTime;

                Attack();
                ShootEnergyBall();
                if(time > delay)
                {

                }
            }
        }
    }

    public void Attack()
    {
        if (hp > 1500) phase = 1;
        else if (hp > 1000) phase = 2;
        else if (hp > 500) phase = 3;
        else if (hp > 0) phase = 4;

        float tailX = Random.Range(-2.0f, 2.0f);
        float tailY = Random.Range(-2.0f, 2.0f);

        target.position = new Vector3(tailX, tailY);

        animator.SetTrigger("IsAttack");

        for(int i = 0; i < phase; i++)
        {
            Instantiate(tail, target.position, Quaternion.identity);
        }
    }  
    private void ShootEnergyBall()
    {
        if (phase < 2) return;
        if (target == null) return;

        direction = (boss.position - target.position).normalized;
        direction *= throwSpeed;

        for(int i = 0; i < phase; i++)
        {
            Instantiate(born, boss.position, Quaternion.identity);
        }
    }
}
