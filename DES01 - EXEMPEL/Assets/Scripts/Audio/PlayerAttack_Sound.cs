using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerAttack_Sound : MonoBehaviour
{  
    [SerializeField] private float attackTime;
    [SerializeField] private SpriteRenderer swordAttackSprite;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnAttack(InputValue button)
    {
        StartCoroutine(Attack());
    }

    IEnumerator Attack()
    {
        swordAttackSprite.enabled = true;
        audioSource.Play();
        yield return new WaitForSeconds(attackTime);
        swordAttackSprite.enabled = false;
        yield return null;
    }
}
