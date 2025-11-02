using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    private static readonly int IsMove = Animator.StringToHash("IsMove");
    private static readonly int MoveX = Animator.StringToHash("MoveX");
    private static readonly int MoveY = Animator.StringToHash("MoveY");

    private static readonly int IsAttack = Animator.StringToHash("IsAttack");
    private static readonly int AttackX = Animator.StringToHash("AttackX");
    private static readonly int AttackY = Animator.StringToHash("AttackY");

    private Animator animator;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void Move(Vector2 obj)
    {
        bool isMove = obj.magnitude > 0.5f;
        Vector2 normalized = obj.normalized;

        animator.SetFloat(MoveX, normalized.x);
        animator.SetFloat(MoveY, normalized.y);
        animator.SetBool(IsMove, isMove);
    }

    public void Attack(Vector2 obj)
    {
        Vector2 normalized = obj.normalized;

        animator.SetFloat(AttackX, normalized.x);
        animator.SetFloat(AttackY, normalized.y);
        animator.SetTrigger(IsAttack);
    }
}
