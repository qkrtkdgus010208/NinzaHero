using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer playerSr;

    private Vector2 moveDirection = Vector2.zero;
    public Vector2 MoveDirection { get { return moveDirection; } }

    private Vector2 lookDirection = Vector2.zero;
    public Vector2 LookDirection { get { return lookDirection; } }

    private Rigidbody2D _rigidbody;
    private AnimaionHandler anime;

    protected virtual void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        anime = GetComponent<AnimaionHandler>();
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }

    protected virtual void FixedUpdate()
    {
        
    }

    protected virtual void Action()
    {

    }

    private void Moving(Vector2 direction)
    {

    }
}
