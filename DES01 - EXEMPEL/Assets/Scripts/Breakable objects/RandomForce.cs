using UnityEngine;

public class RandomForce : MonoBehaviour
{
    public float minForce = 5f;
    public float maxForce = 10f;

    private Rigidbody2D rb;
    private float rotationSpeed = 5f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        ApplyRandomForce();
        RotateTowardsMovement();
    }

    private void Update()
    {
        RotateTowardsMovement();
    }

    void ApplyRandomForce()
    {
        // Random horizontal direction (-1 to 1)
        float randomX = Random.Range(-1f, 1f);

        // Always upward (positive Y)
        float upwardY = 1f;

        // Create direction vector and normalize it
        Vector2 direction = new Vector2(randomX, upwardY).normalized;

        // Random force magnitude
        float forceAmount = Random.Range(minForce, maxForce);

        // Apply force
        rb.AddForce(direction * forceAmount, ForceMode2D.Impulse);
    }

    void RotateTowardsMovement()
    {
        if (rb.linearVelocity.magnitude > 0.1f) // Avoid jitter when nearly still
        {
            float angle = Mathf.Atan2(rb.linearVelocity.y, rb.linearVelocity.x) * Mathf.Rad2Deg;

            // Adjust by -90 if your sprite faces "up" instead of "right"
            Quaternion targetRotation = Quaternion.Euler(0f, 0f, angle);

            transform.rotation = Quaternion.Lerp(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
        }
    }
}
