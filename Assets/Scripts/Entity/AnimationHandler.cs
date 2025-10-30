using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class AnimationHandler : MonoBehaviour
{
    //private static readonly int IsMoving = Animator.StringToHash("IsMove");
    private static readonly int MoveUp = Animator.StringToHash("MoveUp");
    private static readonly int MoveDown = Animator.StringToHash("MoveDown");
    private static readonly int MoveLeft = Animator.StringToHash("MoveLeft");
    private static readonly int MoveRight = Animator.StringToHash("MoveRight");
    //private static readonly int AttackUp = Animator.StringToHash("AttackUp");
    //private static readonly int AttackDown = Animator.StringToHash("AttackDown");
    //private static readonly int AttackLeft = Animator.StringToHash("AttackLeft");
    //private static readonly int AttackRight = Animator.StringToHash("AttackRight");
    private static readonly int Idle = Animator.StringToHash("Idle");

    private Animator animator;

    protected virtual void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void UpdateState(Vector2 direction)
    {
        if(Mathf.Approximately(direction.x, 0) && Mathf.Approximately(direction.y, 0))
        {
            animator.SetBool("IsMove", false);
        }
        else
        {
            animator.SetBool("IsMove", true);
        }

        animator.SetFloat("InputX", direction.x);
        animator.SetFloat("InputY", direction.y);
    }

    public void Attack(Vector2 target)
    { 
        SetDefault();

        animator.SetBool("isLeft", target.x < 0 && target.y >= 0 || target.y < 0);
        animator.SetBool("isRight", target.x > 0 && target.y >= 0 || target.y < 0);
        animator.SetBool("isDown", target.y < 0 && target.x == 0);
        animator.SetBool("isUp", target.y > 0 && target.x == 0);

        animator.SetTrigger("isAttack");
    }

    public void SetDefault()
    {
        animator.SetBool("isLeft", false);
        animator.SetBool("isRight", false);
        animator.SetBool("isDown", false);
        animator.SetBool("isUp",false);
    }
}
