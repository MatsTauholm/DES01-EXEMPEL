using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;

public class EvilHead : MonoBehaviour
{
    [SerializeField] private Transform[] ninjaStars;
    [SerializeField] private Vector2 aimPosition;
    [SerializeField] private Transform playerPosition;
    [SerializeField] private float minMoveTime, maxMoveTime;

    [Header("Jump Settings")]
    [SerializeField] private float jumpPower, jumpDuration;
    [SerializeField] private int numberOfJumps;
    [SerializeField] private DG.Tweening.Ease linearEase;

    void Start()
    {
        ShootStars();
    }

    private void ShootStars()
    {
        //Create a sequence
        var sequence = DOTween.Sequence();

        //Append move-tween to the sequence for each ninja star
        foreach (var ninjaStar in ninjaStars)
        {
            sequence.Append(ninjaStar.DOMove(aimPosition, Random.Range(minMoveTime, maxMoveTime)));
            aimPosition.y += 3;
        }

        //Append jump-tween to the sequence for each ninja star
        foreach (var ninjaStar in ninjaStars)
        {
            sequence.Append(ninjaStar.DOJump(playerPosition.position, jumpPower, numberOfJumps, jumpDuration)
               .SetEase(Ease.Linear));
        }

        //OnComplete callback to fade the stars
        sequence.OnComplete(Fade);
    }

    private void Fade()
    {
        //Shake then fade out each ninja star
        foreach (var ninjaStar in ninjaStars)
        {
            ninjaStar.DOShakeScale(0.5f, 10, 10, 90, true);
            ninjaStar.DOScale(Vector3.zero, 0.5f)
                .SetDelay(1f)
                .SetEase(Ease.InFlash);
        }
    }
}

