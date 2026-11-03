using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Audio;

public class ChangeAudioMixer : MonoBehaviour
{
    public AudioMixerSnapshot normal;
    public AudioMixerSnapshot messedUp;

    public float fadeDuration = 2f;

    void Update()
    {
        if(Keyboard.current.uKey.wasPressedThisFrame)
        {
            EnterUnderwater();
        }
        else if (Keyboard.current.iKey.wasPressedThisFrame)
        {
            ExitUnderwater();
        }
    }

    void EnterUnderwater()
    {
        Debug.Log("Underwater");
        messedUp.TransitionTo(fadeDuration);
    }

    void ExitUnderwater()
    {
        Debug.Log("Exiting underwater");
        normal.TransitionTo(fadeDuration);
    }
}