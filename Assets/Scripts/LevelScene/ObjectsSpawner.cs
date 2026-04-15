using System.Collections;
using UnityEngine;

public class ObjectsSpawner : MonoBehaviour
{
    [Header("Spawning Settings")]
    public float spawnInterval = 2f;    
    public Transform[] spawnPoints;
    void OnEnable()
    {
        LevelEvents.OnGameStarted += StartSpawning;
    }

    void OnDisable()
    {
        LevelEvents.OnGameStarted -= StartSpawning;
        
    }

    void Update()
    {
        
    }

    public void StartSpawning()
    {
        StartCoroutine(StartSpawningRoutine());
    }

    private IEnumerator StartSpawningRoutine()
    {
        while (true)
        {
            if (!GameManager.Instance.isGamePaused)
            {
                Transform randomSpawnPoint = spawnPoints[Random.Range(0,spawnPoints.Length)];
            
                string objectTag = Random.value > 0.5f ? "Obstacle" : "Energy";
                ObjectPooler.Instance.SpawnFromPool(objectTag,randomSpawnPoint.position,randomSpawnPoint.rotation);
            }



            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
