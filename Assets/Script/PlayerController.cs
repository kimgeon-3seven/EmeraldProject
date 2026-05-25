using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Header("Components")]
    public InputReader inputReader;
    public AnimationController animationController;
    public PlayerCombat playerCombat;

    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float rotationSpeed = 10f;

    [Header("Jump Settings")]
    public float jumpForce = 5f;
    public Transform groundCheck;
    public float groundDistance = 0.2f;
    public LayerMask groundMask;

    private Rigidbody rb;
    private bool isGrounded;
    private bool doJump = false; //FixedUpdate에서 점프를 실행하기 위한 캐싱 변수

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

       
        if (playerCombat != null && playerCombat.IsAttacking)
        {
            doJump = false;
            return;
        }

        if (inputReader != null && inputReader.JumpPressed && isGrounded)
        {
            doJump = true;
        }
    }

    private void FixedUpdate()
    {
        if (playerCombat != null && playerCombat.IsAttacking)
        {
            rb.linearVelocity = new Vector3(0f, rb.linearVelocity.y, 0f); 
            doJump = false; 

            if (animationController != null)
            {
                animationController.UpdateAnimationState(Vector2.zero, isGrounded, rb.linearVelocity.y);
            }
            return;
        }

        
        HandleJump();
        MoveAndRotate();
    }

    private void HandleJump()
    {
        
        if (doJump)
        {
            
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            doJump = false; 
        }
    }

    private void MoveAndRotate()
    {
        if (inputReader == null) return;

        Vector2 input = inputReader.MoveInput;
        Vector3 moveDirection = new Vector3(input.x, 0f, input.y).normalized;

        if (moveDirection.magnitude >= 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);

            Vector3 movePosition = transform.position + moveDirection * moveSpeed * Time.fixedDeltaTime;
            rb.MovePosition(movePosition);
        }

        if (animationController != null)
        {
            animationController.UpdateAnimationState(input, isGrounded, rb.linearVelocity.y);
        }
    }
}