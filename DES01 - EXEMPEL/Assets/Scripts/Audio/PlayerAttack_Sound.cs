using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerAttack_Sound : MonoBehaviour
{  
    [SerializeField] private Animator animator;
    private AudioSource audioSource;
    const string PLAYER_ATTACK = "Attack";

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnAttack(InputValue button)
    {
        animator.SetTrigger(PLAYER_ATTACK);
        audioSource.Play();
    }
}
