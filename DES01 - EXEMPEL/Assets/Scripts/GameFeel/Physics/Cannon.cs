using UnityEngine;
using UnityEngine.InputSystem;

public class Cannon : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject miniBulletPrefab;

    [Header("Rotation")]
    [SerializeField] private float rotationSpeed = 90f;

    [Header("Shooting")]
    [SerializeField] private float fireRate = 0.05f;
    [SerializeField] private float spreadAngle = 30f;
    [SerializeField] private float bulletSpeed = 10f;

    private float fireTimer;

    private void Update()
    {
        RotateCannon();
        HandleShooting();
    }

    private void RotateCannon()
    {
        transform.Rotate(0f,0f,rotationSpeed * Time.deltaTime);
    }

    private void HandleShooting()
    {
        if (Mouse.current.leftButton.isPressed)
        {
            fireTimer -= Time.deltaTime;

            if (fireTimer <= 0f)
            {
                Fire();

                fireTimer = fireRate;
            }
        }
        else
        {
            // Reset the timer when the player releases the button
            fireTimer = 0f;
        }
    }

    private void Fire()
    {
        float randomAngle = Random.Range(
            -spreadAngle / 2f,
            spreadAngle / 2f
        );

        Vector2 direction = Quaternion.Euler(0f, 0f, randomAngle) * firePoint.up;

        GameObject bullet = Instantiate(miniBulletPrefab,firePoint.position, Quaternion.identity);

        MiniBullet miniBullet = bullet.GetComponent<MiniBullet>();

        miniBullet.Launch(direction, bulletSpeed);
    }
}