using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    public GameObject enemyPrefab;
    public Transform spawnPoint;
    
    [Header("Wave Timing Settings")]
    public float minSpawnTime = 1.5f; // Minimum seconds between enemies
    public float maxSpawnTime = 4f;   // Maximum seconds between enemies

    void Start()
    {
        // Start the continuous spawning loop
        StartCoroutine(SpawnEnemyWave());
    }

    IEnumerator SpawnEnemyWave()
    {
        // The while(true) creates an infinite loop for the game
        while (true) 
        {
            // 1. Calculate a random delay time
            float randomDelay = Random.Range(minSpawnTime, maxSpawnTime);
            
            // 2. Wait for that random amount of time
            yield return new WaitForSeconds(randomDelay);

            // 3. Spawn the enemy at the spawner's position
            Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        }
    }
}