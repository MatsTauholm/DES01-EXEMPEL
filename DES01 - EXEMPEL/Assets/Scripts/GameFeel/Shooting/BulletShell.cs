using System;
using Unity.Mathematics;
using UnityEngine;

public class BulletShell : MonoBehaviour
{
    [SerializeField] private float MaxEjectForceX = 3;
    [SerializeField] private float MinEjectForceX = 3;
    [SerializeField] private float MaxEjectForceY = -3;
    [SerializeField] private float MinEjectForceY = -3;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        AddForce();
    }

    private void AddForce()
    {
        // Apply random eject force within specified ranges, then add impulse force to the rigidbody
        var _ejectForce = new Vector2(UnityEngine.Random.Range(MinEjectForceX, MaxEjectForceX), UnityEngine.Random.Range(MinEjectForceY, MaxEjectForceY));
        rb.AddForce(_ejectForce, ForceMode2D.Impulse);
    }

    private void Update()
    {
        RotateWithVelocity();
    }

    void RotateWithVelocity()
    {
        // Only rotate if velocity is significant
        if (rb.linearVelocity.sqrMagnitude > 0.01f)
        {
            float angle = Mathf.Atan2(rb.linearVelocity.y, rb.linearVelocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }
    }
}
