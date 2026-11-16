using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
public class AudioManager : MonoBehaviour
{
    public static AudioManager instance; // Static reference
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource sfxSource;
    [SerializeField] AudioClip[] musicClips;

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


    public IEnumerator PlaySFXCoroutine(AudioClip clip, float delay)
    {
        AudioSource sfxSource = gameObject.AddComponent<AudioSource>();
        sfxSource.clip = clip;
        sfxSource.Play();
        yield return new WaitForSeconds(delay);
        Destroy(sfxSource);
    }


    public IEnumerator PlayAudioResourceCoroutine(AudioResource resource, float delay)
    {
        AudioSource sfxSource = gameObject.AddComponent<AudioSource>();
        sfxSource.resource = resource;
        sfxSource.Play();
        yield return new WaitForSeconds(delay);
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
