using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    private static readonly int IsMoving = Animator.StringToHash("IsMove");
    private static readonly int MoveX = Animator.StringToHash("MoveX");
    private static readonly int MoveY = Animator.StringToHash("MoveY");

    private Animator animator;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void Move(Vector2 obj)
    {
        bool isMoving = obj.magnitude > 0.5f;
        Vector2 normalized = obj.normalized;

        animator.SetBool(IsMoving, isMoving);
        animator.SetFloat(MoveX, normalized.x);
        animator.SetFloat(MoveY, normalized.y);
    }
}
