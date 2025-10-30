using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    [SerializeField] protected SpriteRenderer playerSr;

    protected Vector2 moveDirection = Vector2.zero;
    public Vector2 MoveDirection { get { return moveDirection; } }

    protected Vector2 lookDirection = Vector2.zero;
    public Vector2 LookDirection { get { return lookDirection; } }

    private Rigidbody2D _rigidbody;
    private AnimationHandler anime;
    private BoxCollider2D areaBox;
    private float speed = 5.0f;
    private bool isMoving;
    private float throwingSpd = 10f;
    private Vector3 targetPos;
    public GameObject shuriken;
    public Vector2 weaponDir;
    public Shuriken kill;

    protected virtual void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        anime = GetComponent<AnimationHandler>();
        areaBox = GetComponentInChildren<BoxCollider2D>();
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        Action();
        anime.UpdateState(moveDirection);
    }

    protected virtual void FixedUpdate()
    {
        Moving(MoveDirection);
    }

    protected virtual void Action()
    {

    }

    private void Moving(Vector2 direction)
    {
        if (direction == Vector2.zero) isMoving = false;
        else isMoving = true;

        direction = direction * speed;
        _rigidbody.velocity = direction;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isMoving) return;

        if(collision.gameObject.CompareTag("Monster"))
        {
            targetPos = collision.gameObject.transform.position;
            InvokeRepeating("Throwing", 0.0f, 2.0f);
        }
    }

    private void Throwing()
    {
        float posX = transform.position.x;
        float posY = transform.position.y;
        Vector2 targetDir = new Vector2(targetPos.x, targetPos.y);
        weaponDir = new Vector2(posX, posY + 0.5f);

        Instantiate(shuriken, weaponDir, Quaternion.identity);

        anime.Attack((targetDir - weaponDir).normalized);
    }
}
