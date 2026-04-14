using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyPunch : MonoBehaviour
{
    [SerializeField] private Transform player;        // Reference to the player
    [SerializeField] private GameObject fist;        // Reference to the fist

    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 3f;    // Movement speed of the enemy
    [SerializeField] private float stopDistance = 2f; // Distance to stop and punch

    [Header("Punch Settings")]
    [SerializeField] private float punchPower = 0.5f; // Strength of the punch
    [SerializeField] private float punchDuration = 0.3f; // Duration of the punch
    [SerializeField] private float punchElasticity = 0.3f; // Elasticity of the punch
    [SerializeField] private int punchVibrato = 1; // Vibration of the punch

    private Vector3 direction;
    private Rigidbody2D rb;
    private bool isPunching = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (player == null) return; // Exit if player is not assigned

        direction = (player.position - transform.position).normalized; //Find the direction from the player to the enemy
        float distanceToPlayer = Vector2.Distance(transform.position, player.position); //Check distance

        if (distanceToPlayer > stopDistance) //If the player is far enough, follow them
        {
            FollowPlayer();
        }
        else //If close enough, punch
        {
            if (!isPunching)
            {
                StartPunch();
            }
        }
        
    }

    private void FollowPlayer() 
    {
        rb.linearVelocity = moveSpeed * direction;

        // Compare x positions
        if (player.position.x > transform.position.x)
        {
            // Player is to the right: face right
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else
        {
            // Player is to the left: face left
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }

    private void StartPunch()
    {
        isPunching = true;

        // Punch effect using DoTween
        fist.transform.DOPunchPosition(new Vector3(-punchPower, 0f, 0f), punchDuration, punchVibrato, punchElasticity)
            .OnComplete(() => isPunching = false); // Reset punching flag after animation
    }
}
