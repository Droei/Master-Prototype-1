using UnityEngine;

public class PlayerAnimationHandler : MonoBehaviour
{
    Animator animator;

    static readonly int MoveXHash = Animator.StringToHash("MoveX");
    static readonly int MoveYHash = Animator.StringToHash("MoveY");
    static readonly int SpeedHash = Animator.StringToHash("LocomotionSpeed");

    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void UpdateMovement(Vector2 moveInput, bool isSprinting)
    {
        float speed = 0f;

        if (moveInput.sqrMagnitude > 0.01f)
            speed = isSprinting ? 1f : 0.5f;

        animator.SetFloat(MoveXHash, moveInput.x);
        animator.SetFloat(MoveYHash, moveInput.y);
        animator.SetFloat(SpeedHash, speed);
    }
}