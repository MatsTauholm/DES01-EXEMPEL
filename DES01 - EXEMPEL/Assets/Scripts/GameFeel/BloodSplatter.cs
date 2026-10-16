using System.Collections.Generic;
using UnityEngine;

public class BloodSplatter : MonoBehaviour
{
    [SerializeField] GameObject bloodSplatterPrefab;
    [SerializeField] Transform bloodSplatterHolder;
    ParticleSystem particle;
    private List<ParticleCollisionEvent> bloodSplatterPool; 

    void Start()
    {
        bloodSplatterPool = new List<ParticleCollisionEvent>();
        particle = GetComponent<ParticleSystem>();
    }

    private void OnParticleCollision(GameObject other)
    {
        ParticlePhysicsExtensions.GetCollisionEvents(particle, other, bloodSplatterPool);
        int count = bloodSplatterPool.Count;

        for(int i = 0; i < count; i++)
        {
            Instantiate(bloodSplatterPrefab, bloodSplatterPool[i].intersection, Quaternion.identity, bloodSplatterHolder);
        }
    }
}
