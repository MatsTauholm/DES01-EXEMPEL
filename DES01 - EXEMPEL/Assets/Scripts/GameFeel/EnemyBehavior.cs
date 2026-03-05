using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField] private float speed = 2.0f;
    [SerializeField] private int maxHealth = 7;

    private int currentHealth;
    private Rigidbody2D rb;
    private KnockBack knockback;
    private TimeFreezer timeFreezer;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        knockback = GetComponent<KnockBack>();
    }

    void Start()
    {
        currentHealth = maxHealth;
        timeFreezer = FindFirstObjectByType<TimeFreezer>();
    }

    void Update()
    {
        //Only move if not being knocked back
        if (!knockback.isBeingKnockedBack)
        {
            rb.linearVelocity = new Vector2(-speed, rb.linearVelocity.y);
        }
        
    }

    public void TakeDamage(int damage, Vector2 hitDirection)
    {
        currentHealth -= damage;

        // Check if health is depleted
        if (currentHealth <= 0)
        {
            timeFreezer.Freeze(0.05f);
            Destroy(gameObject);
        }

        //Apply knockback
        knockback.CallKnockBack(hitDirection, Vector2.right, 0f);
    }
}
