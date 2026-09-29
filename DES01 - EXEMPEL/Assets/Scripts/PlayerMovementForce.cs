using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Rendering.Universal;

public class PlayerMovementForce : MonoBehaviour
{
    [SerializeField] float acceleration = 50f;     // Force when pressing a direction
    [SerializeField] float deceleration = 30f;     // Force when no input, slows down
    [SerializeField] float maxSpeed = 10f;
    [SerializeField] float jumpForce = 5f;
    [SerializeField] ContactFilter2D groundFilter;

    Rigidbody2D rb;

    Vector2 moveInput;
    bool shouldJump;
    bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
        //Mirror the sprite if moving left
        if (moveInput.x != 0)
        {
            transform.localScale = new Vector2(Mathf.Sign(moveInput.x), transform.localScale.y);
        }
    }

    void FixedUpdate()
    {
        //Ground check
        isGrounded = rb.IsTouching(groundFilter);

        //Jump function
        if (isGrounded && shouldJump)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
            shouldJump = false;
        }
        
        //Horizontal movement
        if (moveInput.x != 0)
        {
            //Accelerate
            rb.AddForce(new Vector2(moveInput.x * acceleration, 0f), ForceMode2D.Force);
        }
        else
        {
            //Decelerate
            rb.AddForce(new Vector2(-rb.linearVelocity.x * deceleration, 0f), ForceMode2D.Force);
        }

           // Clamp max horizontal velocity
        if (Mathf.Abs(rb.linearVelocity.x) > maxSpeed)
        {
            rb.linearVelocity = new Vector2(Mathf.Sign(rb.linearVelocity.x) * maxSpeed, rb.linearVelocity.y);
        }
    }
}
