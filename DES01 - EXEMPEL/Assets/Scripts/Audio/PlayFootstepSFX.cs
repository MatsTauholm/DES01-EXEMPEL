using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class PlayFootstepSFX : MonoBehaviour
{
    [SerializeField] private AudioResource footSteps;
    [SerializeField] private float footstepInterval = 0.35f; // Interval between footsteps
    private PlayerWithForce playerMovement;
    private Rigidbody2D rb;

    void Start()
    {
        playerMovement = GetComponent<PlayerWithForce>();
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(PlayFootstepSound());
    }

    IEnumerator PlayFootstepSound()
    {
        while (true)
        {
            if (rb.linearVelocityX != 0 && playerMovement.isGrounded)
            {
                StartCoroutine(AudioManager.instance.PlayAudioResourceCoroutine(footSteps, footstepInterval));
            }
            yield return new WaitForSeconds(footstepInterval); // Adjust the interval as needed
        }
    }
}

