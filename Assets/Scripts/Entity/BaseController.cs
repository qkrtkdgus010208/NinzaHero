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

    protected virtual void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        anime = GetComponent<AnimationHandler>();
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        Action();
        Look(lookDirection);
    }

    protected virtual void FixedUpdate()
    {
        Moving(moveDirection);
    }

    protected virtual void Action()
    {

    }

    private void Moving(Vector2 direction)
    {
        direction *= 5f;

        _rigidbody.velocity = direction;
        // 애니메이션 핸들러 호출
    }

    private void Look(Vector2 direction)
    {
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bool isLeft = Mathf.Abs(rotZ) > 135f && Mathf.Abs(rotZ) <= 270f;
        bool isUp = Mathf.Abs(rotZ) > 90f && Mathf.Abs(rotZ) <= 180f;
        bool isRight = Mathf.Abs(rotZ) > 45f && Mathf.Abs(rotZ) <= 90;


        if(isLeft)
        {

        }
        else if(isUp)
        {

        }
        else if(isRight)
        {

        }
        else
        {

        }
    }
}
