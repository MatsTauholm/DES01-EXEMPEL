using UnityEngine;
using System.Collections;

public class BossSpawner : MonoBehaviour
{
    [Header("Enemy Settings")]
    public int enemiesAlive;

    [Header("Boss Spawn Settings")]
    public GameObject bossPrefab;
    public Transform bossSpawnPosition;

    [Header("Spawn Effect")]
    public GameObject spawnEffectPrefab;
    public Transform effectPosition;
    public float spawnDelay = 3f;

    private bool bossSpawned = false;

    private void Start()
    {
        StartCoroutine(SpawnBossRoutine());
    }


    public void EnemyKilled()
    {
        enemiesAlive--;

        if (enemiesAlive <= 0 && !bossSpawned)
        {
            bossSpawned = true;
            StartCoroutine(SpawnBossRoutine());
        }
    }

    IEnumerator SpawnBossRoutine()
    {
        yield return new WaitForSeconds(spawnDelay);
        // Spawn particle effect
        GameObject effect = Instantiate(
            spawnEffectPrefab,
            effectPosition.position,
            Quaternion.identity
        );

        // Wait before spawning the boss
        yield return new WaitForSeconds(spawnDelay);

        // Destroy particle effect
        effect.GetComponent<ParticleSystemForceField>().enabled = false;
        effect.GetComponent<ParticleSystem>().Stop();

        // Spawn boss
        Instantiate(
            bossPrefab,
            bossSpawnPosition.position,
            bossSpawnPosition.rotation
        );
    }
}
