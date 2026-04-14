using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed, jumpSpeed;
    [SerializeField] ContactFilter2D groundFlter;

    private bool isGrounded;
    private bool isJumping;
    private Vector2 moveInput;
    private Rigidbody2D rb;
    private Animator ani;
    private KnockBack knockback;

    private void Awake()
    {
        ani = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        knockback = GetComponent<KnockBack>();
    }

    private void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    private void OnJump(InputValue button)
    {
        isJumping = button.isPressed;
    }

    private void Update()
    {
        if(!knockback.isBeingKnockedBack)
        {
            Run();
        }
    }

    private void FixedUpdate()
    {
        isGrounded = rb.IsTouching(groundFlter);

        if (isJumping && !knockback.isBeingKnockedBack)
        {
            rb.linearVelocity += (new Vector2(0f, jumpSpeed));
        }

        if (!isGrounded)
        {
            ani.SetBool("isJumping", true);
        }
        else
        {
            ani.SetBool("isJumping", false);
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
            ani.SetBool("isRunning", true);
        }
        else
        {
            ani.SetBool("isRunning", false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemies"))
        {
            //knockback.CallKnockBack();
        }
    }

}
