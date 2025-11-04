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
    [SerializeField] public Transform thisPos;
    [SerializeField] private Transform[] energyBallSpawn;
    [SerializeField] private GameObject tail;
    [SerializeField] private GameObject born;
    private static BossController bossController;
    public static BossController instance { get { return bossController; } }

    private Animator animator;
    private Vector2 direction;

    private int phase;
    private int energySpawnPosCount = 0;
    private int touchDamage = 200;
    private float delay = 4.0f;
    private float time;

    private bool attacking = true;
    private bool attackable = false;
    public bool isAlive = true;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        if (bossController == null)
        {
            bossController = this;
        }
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
        if (hp > 1500) phase = 1;
        else if (hp > 1000) phase = 2;
        else if (hp > 500) phase = 3;
        else if (hp > 0) phase = 4;

        animator.SetTrigger("IsAttack");

        for (int i = 0; i < phase; i++)
        {
            float tailX = target.x - (Random.Range(-2.0f, 2.0f));
            float tailY = target.y - (Random.Range(-2.0f, 2.0f));

            Vector2 InstPos = new Vector2(tailX, tailY);

            Instantiate(tail, InstPos, Quaternion.identity);
        }
    }
    private void ShootEnergyBall()
    {
        if (phase < 2) return;
        else
        {
            if (phase == 2) energySpawnPosCount = 1;
            else if (phase == 3) energySpawnPosCount = 2;
            else energySpawnPosCount = 3;
        }

        if (target == null) return;

        for (int i = 0; i < energySpawnPosCount; i++)
        {
            direction = (target - energyBallSpawn[i].position).normalized;
            animator.SetTrigger("IsAttack");

            Instantiate(born, energyBallSpawn[i].position, Quaternion.identity);
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

        phase = 0;
        Destroy(this.gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ResourceController resource = collision.GetComponent<ResourceController>();
            if (resource != null)
            {
                resource.ChangeHealth(-touchDamage);
            }
        }
    }

}
