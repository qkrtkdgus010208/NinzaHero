using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationHandler : AnimationHandler
{
    private static readonly int IsAttack = Animator.StringToHash("IsAttack");
    private static readonly int AttackX = Animator.StringToHash("AttackX");
    private static readonly int AttackY = Animator.StringToHash("AttackY");

    public override void Attack(Vector2 obj)
    {
        base.Attack(obj);

        Vector2 normalized = obj.normalized;

        animator.SetFloat(AttackX, normalized.x);
        animator.SetFloat(AttackY, normalized.y);
        animator.SetTrigger(IsAttack);
    }
}
