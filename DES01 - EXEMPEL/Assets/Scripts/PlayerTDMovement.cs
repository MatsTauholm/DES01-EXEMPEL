using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerTDMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    private Vector2 moveInput;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void Update()
    {
        Move();
        FlipSprite();
    }

    private void Move()
    {
        Vector2 playerVelocity = moveInput * moveSpeed;
        rb.linearVelocity = playerVelocity;
    }

    private void FlipSprite()
    {
        if (moveInput.x != 0)
        {
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Abs(scale.x) * Mathf.Sign(moveInput.x);
            transform.localScale = scale;
        }
    }

}
