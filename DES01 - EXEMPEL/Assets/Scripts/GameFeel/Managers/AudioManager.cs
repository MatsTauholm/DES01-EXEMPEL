using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance; // Static reference
    public AudioSource audioSource;      // Reference to AudioSource

    // Static method to play a sound
    public static void PlaySound(AudioClip clip)
    {
        if (Instance != null && clip != null)
        {
            Instance.audioSource.PlayOneShot(clip);
        }
    }

    // Static method to play a sound
    public static void StopSound()
    {
         Instance.audioSource.Stop();
    }

}
