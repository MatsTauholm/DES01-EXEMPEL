using UnityEngine;

public class PlayMusic : MonoBehaviour
{
    [SerializeField] private AudioClip musicClip;

    void Start()
    {
        AudioManager.PlayMusic(musicClip);
    }
}
