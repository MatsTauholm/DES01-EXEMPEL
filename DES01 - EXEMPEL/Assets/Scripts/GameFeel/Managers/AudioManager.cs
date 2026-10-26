using UnityEngine;
using System.Collections;
public class AudioManager : MonoBehaviour
{
    public static AudioManager instance; // Static reference
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource sfxSource;

    void Awake()
    {
        if (instance == null)
        {
            instance = this; // Assign the static reference
            DontDestroyOnLoad(gameObject); // Optional: Persist across scenes
        }
        else
        {
            Destroy(gameObject); // Ensure only one instance exists
        }
    }

    // Static method to play a sound
    public void PlaySFX(AudioClip clip)
    {
        sfxSource.clip = clip;
        sfxSource.Play();
        //if (clip != null)
        {
            PlaySFXCoroutine(clip);
        }
    }

    private IEnumerator PlaySFXCoroutine(AudioClip clip)
    {
        AudioSource sfxSource = gameObject.AddComponent<AudioSource>();
        sfxSource.clip = clip;
        sfxSource.Play();
        yield return new WaitForSeconds(clip.length);
        Destroy(sfxSource);
    }

    public static void PlayMusic(AudioClip clip)
    {
        if (clip != null)
        {
            instance.musicSource.clip = clip;
            instance.musicSource.Play();
        }
    }
}
