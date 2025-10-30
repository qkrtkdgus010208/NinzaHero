using UnityEngine;

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

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void Move(Vector2 obj)
    {
        bool isMoving = obj.magnitude > 0.5f;

        if (isMoving)
        {
            if (obj.x > 0)
            {
                animator.Play(MoveRight);
            }
            else if (obj.x < 0)
            {
                animator.Play(MoveLeft);
            }
            else
            {
                if (obj.y > 0)
                {
                    animator.Play(MoveUp);
                }
                else if (obj.y < 0)
                {
                    animator.Play(MoveDown);
                }
            }
        }
        else
        {
            animator.Play(Idle);
        }
    }
}
