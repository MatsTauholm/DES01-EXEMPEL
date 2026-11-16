using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;


public class PlayerAttack_Sound : MonoBehaviour
{  
    [SerializeField] private Animator animator;
    [SerializeField] private AudioResource swordSounds;
    [SerializeField] private float swordSwingInterval = 0.5f; // Interval between Sword Swing sounds
    private AudioSource audioSource;
    const string PLAYER_ATTACK = "Attack";

    void OnAttack(InputValue button)
    {
        animator.SetTrigger(PLAYER_ATTACK);
        StartCoroutine(AudioManager.instance.PlayAudioResourceCoroutine(swordSounds, swordSwingInterval));
    }
}
