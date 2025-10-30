using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class AnimationHandler : MonoBehaviour
{
    private static readonly int IsLeft = Animator.StringToHash("isLeft");
    private static readonly int IsFront = Animator.StringToHash("isFront");
    private static readonly int IsBack = Animator.StringToHash("isBack");
    private static readonly int IsAttack = Animator.StringToHash("isAttack");
    private static readonly int WalkFront = Animator.StringToHash("WalkFront");
    private static readonly int WalkBack = Animator.StringToHash("WalkBack");
    private static readonly int WalkLeft = Animator.StringToHash("WalkLeft");

    protected Animator animator;

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
