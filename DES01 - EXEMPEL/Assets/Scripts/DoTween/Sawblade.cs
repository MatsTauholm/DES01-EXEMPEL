using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Sawblade : MonoBehaviour
{
    [SerializeField] private Transform sawblade;
    [SerializeField] private float moveTime = 2;
    [SerializeField] private float rotateTime = 2;
    [SerializeField] private DG.Tweening.Ease linearEase;
    [SerializeField] private DG.Tweening.Ease angularEase;

    void Start()
    {
        // Start the sawblade movement and rotation
        sawblade.transform.DOLocalMove(new Vector3(5,0,0),moveTime)
            .SetEase(linearEase)
            .SetLoops(-1, LoopType.Yoyo);

        sawblade.transform.DOLocalRotate(new Vector3(0,0,360),rotateTime,RotateMode.FastBeyond360)
            .SetEase(angularEase)
            .SetLoops(-1, LoopType.Restart);    
    }
}
