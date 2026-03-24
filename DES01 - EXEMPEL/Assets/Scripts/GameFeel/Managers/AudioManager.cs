using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance; // Static reference
    public AudioSource audioSource;      // Reference to AudioSource

    private void Awake()
    {
        // Singleton pattern: keep only one instance
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

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
