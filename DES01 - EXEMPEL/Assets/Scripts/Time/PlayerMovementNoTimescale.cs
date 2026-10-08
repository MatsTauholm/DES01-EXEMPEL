using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms;

public class PlayerMovementNoTimescale : MonoBehaviour
{
    [SerializeField] float acceleration = 5f;     // Force when pressing a direction
    [SerializeField] float deceleration = 3f;     // Force when no input, slows down
    [SerializeField] float maxSpeed = 10f;
    [SerializeField] float jumpForce = 5f;
    [SerializeField] ContactFilter2D groundFilter;

    Rigidbody2D rb;
    Animator ani;

    Vector2 moveInput;
    float timeScale;
    bool shouldJump;
    bool isGrounded;

    //Animation states
    const string PLAYER_RUN = "isRunning";
    const string PLAYER_JUMP = "isJumping";


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void OnJump()
    {
        if (isGrounded)
            shouldJump = true;
    }

    void Update()
    {
        Move();
        FlipSprite();
        
    }

    private void Move()
    {
        //Horizontal movement
        if (moveInput.x != 0)
        {
            //Accelerate
            rb.linearVelocity = new Vector2(moveInput.x * acceleration * Time.fixedUnscaledDeltaTime, 0f);
        }
        else
        {
            //Decelerate
            rb.linearVelocity = new Vector2(rb.linearVelocity.x * (1 - deceleration * Time.fixedUnscaledDeltaTime), rb.linearVelocity.y);
        }

        // Clamp max horizontal velocity
        if (Mathf.Abs(rb.linearVelocity.x) > maxSpeed)
        {
            rb.linearVelocity = new Vector2(Mathf.Sign(rb.linearVelocity.x) * maxSpeed, rb.linearVelocity.y);
        }
    }

    private void FlipSprite()
    {
        //Mirror the sprite if moving left
        if (moveInput.x != 0)
        {
            transform.localScale = new Vector2(Mathf.Sign(moveInput.x), transform.localScale.y);
        }

        ani.SetBool(PLAYER_RUN, moveInput != Vector2.zero);
        ani.SetBool(PLAYER_JUMP, !isGrounded);
    }

    void FixedUpdate()
    {
        GroundCheck();
        Jump();
    }

    void GroundCheck() //Ground check
    {
        isGrounded = rb.IsTouching(groundFilter);
    }

    private void Jump()
    {
        if (isGrounded && shouldJump)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
            shouldJump = false;
        }
    }
}
