using System.Collections;
using UnityEditor.SearchService;
using UnityEngine;

public class ObjectsSpawner : MonoBehaviour
{
    public static ObjectsSpawner Instance;
    [Header("Spawning Settings")]
    [SerializeField]private float spawnInterval = 2f;
    public float SpawnInterval
    {
        get
        {
            return spawnInterval;
        }
        set
        {
            spawnInterval = value;
        }
    }    
    public Transform[] spawnPoints;
    private Coroutine currentSpawnRoutine;

    public float obstacleSpeed = 5f;
    public float energyBarSpeed = 5f;
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

    void Awake()
    {
        if(Instance != this && Instance != null) Destroy(gameObject);
        else Instance = this;
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
