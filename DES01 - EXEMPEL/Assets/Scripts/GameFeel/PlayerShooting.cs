using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour
{
    [Header("Gun Settings")]
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject bulletShell;
    [SerializeField] private GameObject gun;
    [SerializeField] private GameObject muzzleFlash; 
    [SerializeField] private float fireRate = 0.2f;
    [SerializeField] private AudioClip shoot;

    private float gunAngle;
    private float nextFireTime = 0f;
    private bool isFiring;

    private SpriteRenderer muzzleFlashSprite;
    private GunKickback gunKickback;
    private PlayerInput playerInput;
    private InputAction fireAction;

    void Awake()
    {
        gunKickback = FindFirstObjectByType<GunKickback>();
        playerInput = GetComponent<PlayerInput>();
        muzzleFlashSprite = muzzleFlash.GetComponent<SpriteRenderer>();
        fireAction = playerInput.actions["Attack"];
    }

    void OnEnable()
    {
        fireAction.performed += StartFiring;
        fireAction.canceled += StopFiring;
    }

    void OnDisable()
    {
        fireAction.performed -= StartFiring;
        fireAction.canceled -= StopFiring;
    }

    private void StartFiring(InputAction.CallbackContext context)
    {
        isFiring = true;
    }

    private void StopFiring(InputAction.CallbackContext context)
    {
        isFiring = false;
    }

    void Update()
    {
        if (isFiring && Time.time >= nextFireTime)
        {
            Fire();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Fire()
    {
        if (isFiring)
        {
            StartCoroutine(MuzzleFlash());
            gunKickback.PlayKickback();
            AudioManager.PlaySound(shoot);
            GameObject newBullet = Instantiate(bullet, muzzleFlash.transform.position, Quaternion.Euler(0, 0, transform.localScale.x >= 0 ? 0 : 180));
            GameObject newbulletShell = Instantiate(bulletShell, gun.transform.position, gun.transform.rotation);
        }           
    }
    private IEnumerator MuzzleFlash()
    {
        muzzleFlashSprite.enabled = true;
        yield return new WaitForSeconds(0.05f);
        muzzleFlashSprite.enabled = false;
    }
}
