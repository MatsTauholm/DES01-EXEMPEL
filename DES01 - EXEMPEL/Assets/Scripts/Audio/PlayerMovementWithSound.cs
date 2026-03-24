using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementWithSound : MonoBehaviour
{
    [SerializeField] float moveSpeed, jumpSpeed;
    [SerializeField] AudioClip footSteps;
    [SerializeField] ContactFilter2D groundFilter;

    private bool isGrounded;
    private bool isJumping;
    private Vector2 moveInput;
    private Rigidbody2D rb;
    private bool shouldJump;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    private void OnJump(InputValue button)
    {
        shouldJump = button.isPressed;
    }

    private void Update()
    {
         Run();
    }

    private void FixedUpdate()
    {
        isGrounded = rb.IsTouching(groundFilter);

        if (shouldJump && isGrounded)
        {
            rb.linearVelocity += (new Vector2(0f, jumpSpeed));
            shouldJump = false;
        }
    }

    private void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * moveSpeed, rb.linearVelocity.y);
        rb.linearVelocity = playerVelocity;
       
        if (moveInput.x != 0)
        {
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Abs(scale.x) * Mathf.Sign(moveInput.x);
            transform.localScale = scale;
           // AudioManager.PlaySound(footSteps);
        }

    }
}
