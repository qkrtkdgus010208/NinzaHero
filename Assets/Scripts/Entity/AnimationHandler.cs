using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    private static readonly int IsMove = Animator.StringToHash("IsMove");
    private static readonly int MoveX = Animator.StringToHash("MoveX");
    private static readonly int MoveY = Animator.StringToHash("MoveY");

    protected Animator animator;

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

    public virtual void Attack(Vector2 obj)
    {
    }
}
