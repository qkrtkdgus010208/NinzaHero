using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class BossController : MonoBehaviour
{
    [Range(0, 2000)][SerializeField] private int hp = 2000;
    public int Hp
    {
        get => hp;
        set => Mathf.Clamp(value, 0, 2000);
    }

    public bool ignorePhase;
    public bool isActive = true;

    [SerializeField] public Transform thisPos;
    [SerializeField] private Transform energyBallSpawn;
    [SerializeField] private GameObject tail;
    [SerializeField] private GameObject born;
    

    private Animator animator;
    private Vector2 direction;

    private int phase;
    private float delay = 4.0f;
    private float time;

    private bool attacking = true;
    private bool attackable = false;
    public bool isAlive = true;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        isActive = true;   
    }

    Vector3 target;
    public void Init(Transform target)
    {
        this.target = target.position;
    }

    public void Update()
    {
        if (isAlive)
        {
            if (!attacking && attackable)
            {
                if (target != null)
                {
                    Attack();
                    ShootEnergyBall();

                    attacking = true;
                }
            }
            else
            {
                time += Time.deltaTime;

                if (time > delay)
                {
                    attacking = false;
                    time = 0f;
                }
            }

            isAlive = hp > 0 ? true : false;
        }
        else
        {
            DragonHasFallen();
        }
    }

    public void Attack()
    {
        if (attacking) return; 

        if (hp > 1500) phase = 1;
        else if (hp > 1000) phase = 2;
        else if (hp > 500) phase = 3;
        else if (hp > 0) phase = 4;

        animator.SetTrigger("IsAttack");

        for(int i = 0; i < phase; i++)
        {
            Debug.Log("즉사기 생성");
            float tailX = target.x - (Random.Range(-2.0f, 2.0f));
            float tailY = target.y - (Random.Range(-2.0f, 2.0f));

            Vector2 InstPos = new Vector2(tailX, tailY);

            Instantiate(tail, InstPos, Quaternion.identity);
        }
    }  
    private void ShootEnergyBall()
    {
        if (!ignorePhase)
        {
            if (phase < 2) return;
        }

        if (target == null) return;
        
        direction = (target - energyBallSpawn.position).normalized;
        animator.SetTrigger("IsAttack");

        for (int i = 0; i < phase; i++)
        {
            Instantiate(born, energyBallSpawn.position, Quaternion.identity);
            EnergyBallController energyBall = FindAnyObjectByType<EnergyBallController>();
            energyBall.Init(direction);
        }
    }

    public void Damaged(int damage)
    {
        hp -= damage;

        Debug.Log(hp);

        if (hp <= 0) isAlive = false;
    }
    public void Detected(bool onTarget, Transform pos)
    {
        if (pos == null) return;
        attackable = onTarget;

        UpdateTargetPos(pos);
    }
    public void UpdateTargetPos(Transform pos)
    {
        target = pos.position;
    }

    private void DragonHasFallen()
    {
        isActive = false;
        Destroy(this.gameObject);
    }
}
