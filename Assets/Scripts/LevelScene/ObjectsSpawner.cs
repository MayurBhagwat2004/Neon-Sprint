using System.Collections;
using UnityEngine;

public class ObjectsSpawner : MonoBehaviour
{
    [Header("Spawning Settings")]
    public float spawnInterval = 2f;    
    public Transform[] spawnPoints;
    private Coroutine currentSpawnRoutine;
    void OnEnable()
    {
        LevelEvents.OnGameStarted += StartSpawning;
        LevelEvents.OnGameOver += StopSpawningRoutine;
    }

    void OnDisable()
    {
        LevelEvents.OnGameStarted -= StartSpawning;
        LevelEvents.OnGameOver -= StopSpawningRoutine;

        
    }

    void Update()
    {
        
    }

    public void StartSpawning()
    {
        currentSpawnRoutine = StartCoroutine(StartSpawningRoutine());
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

    private void StopSpawningRoutine()
    {
        if(currentSpawnRoutine != null)
        {
            StopCoroutine(currentSpawnRoutine);
            currentSpawnRoutine = null;
        }
    }
}
