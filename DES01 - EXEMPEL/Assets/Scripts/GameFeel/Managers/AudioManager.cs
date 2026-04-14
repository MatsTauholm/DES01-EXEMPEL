using UnityEngine;
using System.Collections;
public class AudioManager : MonoBehaviour
{
    public static AudioManager instance; // Static reference
    [SerializeField] AudioSource musicSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Static method to play a sound
    public void PlaySFX(AudioClip clip)
    {
        if (clip != null)
        {
            StartCoroutine(PlaySFXCoroutine(clip));  
        }
    }
    private IEnumerator PlaySFXCoroutine(AudioClip clip)
    {
        AudioSource sfxSource = gameObject.AddComponent<AudioSource>();
        sfxSource.clip = clip;

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
