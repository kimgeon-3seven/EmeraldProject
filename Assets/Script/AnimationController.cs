using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationController : MonoBehaviour
{
    private Animator animator;

    private int moveXHash;
    private int moveZHash;
    private int isGroundedHash;
    private int verticalVelocityHash;
    private int attackHash;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        moveXHash = Animator.StringToHash("MoveX");
        moveZHash = Animator.StringToHash("MoveZ");
        isGroundedHash = Animator.StringToHash("IsGrounded");
        verticalVelocityHash = Animator.StringToHash("VerticalVelocity");
        attackHash = Animator.StringToHash("Attack");
    }

    public void UpdateAnimationState(Vector2 moveInput, bool isGrounded, float verticalVelocity)
    {
       
        animator.SetFloat(moveXHash, moveInput.x);
        animator.SetFloat(moveZHash, moveInput.y);

       
        animator.SetBool(isGroundedHash, isGrounded);             
        animator.SetFloat(verticalVelocityHash, verticalVelocity); 
    }

    public void PlayAttackAnimation()
    {
       animator.SetTrigger(attackHash);
    }
}