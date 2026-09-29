using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;
using Unity.Cinemachine;

public class SceneStart : MonoBehaviour
{
    [Header("Level Move To Start Position")]
    [SerializeField] private float tweenTime;
    [SerializeField] private float xMinRange;
    [SerializeField] private float xMaxRange;
    [SerializeField] private float yMinRange;
    [SerializeField] private float yMaxRange;

    [SerializeField] private GameObject player;

    private CinemachineImpulseSource impulseSource;
    string objectTag = "Resetable";

    // A dictionary to store the objects and their start positions
    private Dictionary<GameObject, Vector3> startPositions = new Dictionary<GameObject, Vector3>();

    private void Awake()
    {
        EffectManager effectManager = FindAnyObjectByType<EffectManager>();
        if (effectManager.LevelTween == false)
        {
            this.enabled = false;
        }
        else
        {
            this.enabled = true;
        }
    }

    void Start()
    {
        //Find all the objects, save their startpos, then move them to a new random pos 
        GameObject[] objectsToReset = GameObject.FindGameObjectsWithTag(objectTag);
        foreach (var obj in objectsToReset)
        {
            startPositions[obj] = obj.transform.position;
        }
        foreach (var obj in objectsToReset)
        {
            obj.transform.position = new Vector2(Random.Range(xMinRange, xMaxRange), Random.Range(yMinRange, yMaxRange));
        }

        impulseSource = GetComponent<CinemachineImpulseSource>();

        // Reset positions using DoTween
        ResetToStartPositionsSequentially();
    }

    void ResetToStartPositionsSequentially()
    {
        // Create a sequence
        Sequence sequence = DOTween.Sequence();

        //Move level objects to start position 
        foreach (var kvp in startPositions)
        {
            GameObject obj = kvp.Key;
            Vector2 startPosition = kvp.Value;
            if (startPositions.ContainsKey(obj))
            {
                // Add a tween to the sequence for each object
                sequence.Append(obj.transform.DOMove(startPositions[obj], 0.3f).SetEase(Ease.InOutQuad));
            }
        }
          
        //Spawn Player on complete
        sequence.OnComplete(SpawnPlayer);
    }
    
    private void SpawnPlayer()
    {
        player.SetActive(true);
        CameraShakeManager.instance.CameraShake(impulseSource);  
    }
}
