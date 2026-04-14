using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private GameObject hitEffectPrefab;
    [SerializeField] private float bulletSpeed = 5f;
    [SerializeField] private float spreadAngle = 0f;
    [SerializeField] private int damageAmount;

    private Vector2 spreadDirection;
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {     
        // Apply random spread angle
        float randomSpread = Random.Range(-spreadAngle, spreadAngle);
        Quaternion spreadRotation = Quaternion.Euler(0f, 0f, randomSpread);
        spreadDirection = spreadRotation * transform.right;
    }

    void Update()
    {
        rb.linearVelocity = spreadDirection * bulletSpeed;

        Vector2 dir = rb.linearVelocity;

        if (dir != Vector2.zero)
            transform.right = dir;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent<EnemyBehavior>(out EnemyBehavior enemyBehavior))
        {
            enemyBehavior.TakeDamage(damageAmount, transform.right);
        }
        Instantiate(hitEffectPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
