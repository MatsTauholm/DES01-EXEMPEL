using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Rendering.Universal;
using System;

public class PlayerWithForce : MonoBehaviour
{
    [SerializeField] private float acceleration = 50f;     // Force when pressing a direction
    [SerializeField] private float deceleration = 30f;     // Force when no input, slows down
    [SerializeField] private float maxSpeed = 10f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private ContactFilter2D groundFilter;

    private Rigidbody2D rb;
    private Animator ani;

    private Vector2 moveInput;
    private bool shouldJump;
    private bool isGrounded;

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
        MirrorSprite();
        Animate();
    }

    private void MirrorSprite() //Mirror the sprite if moving left
    {
        if (moveInput.x != 0)
        {
            transform.localScale = new Vector2(Mathf.Sign(moveInput.x), transform.localScale.y);
        }
    }

    private void Animate() //Animations for running and jumping
    {
        ani.SetBool(PLAYER_RUN, moveInput != Vector2.zero);
        ani.SetBool(PLAYER_JUMP, !isGrounded);
    }

    void FixedUpdate()
    {
        GroundCheck();
        Move();
        Jump();
    }

    private void GroundCheck()
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

    private void Move()
    {
        
        if (moveInput.x != 0) //Horizontal movement
        {
            rb.AddForce(new Vector2(moveInput.x * acceleration, 0f), ForceMode2D.Force); //Accelerate
        }
        else 
        {
            rb.AddForce(new Vector2(-rb.linearVelocity.x * deceleration, 0f), ForceMode2D.Force); //Decelerate
        }

        if (Mathf.Abs(rb.linearVelocity.x) > maxSpeed) // Clamp max horizontal velocity
        {
            rb.linearVelocity = new Vector2(Mathf.Sign(rb.linearVelocity.x) * maxSpeed, rb.linearVelocity.y);
        }
    }
}
