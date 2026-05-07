using System.Collections;
using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    [Header("Speed logic variables")]
    [SerializeField] private Player playerScript;
    [SerializeField] private float minSpawnInterval = 0.2f;
    [SerializeField] private float spawnIntervalReductionAmount = 0.1f;
    [SerializeField] private float distanceCalculatingSpeedFactor = .2f;
    [SerializeField] private float speedIncreasingFactor;

    [Header("Progression Threshold")]
    [SerializeField] private float distanceInterval = 100f; //Var for adding a factor to calculate next speed increasing distance
    [SerializeField]private float nextDistanceMilestone; //Var for the updated speed increasing score
    private float timeElapsed;
    float currentDist;
    private bool maxSpawnSpeedLimitReached;
    private bool maxSpeedReachedMessageDisplayed;

    void Start()
    {
        nextDistanceMilestone = Random.Range(distanceInterval,nextDistanceMilestone);
        speedIncreasingFactor = 2f;
       
    }

    void Update()
    {
        if(GameManager.Instance.gameEnded || GameManager.Instance.isGamePaused) return;

        timeElapsed += Time.deltaTime;
        currentDist = GameManager.Instance.distanceCovered;

        if(currentDist >= nextDistanceMilestone && !maxSpawnSpeedLimitReached)
        {
            IncreaseDifficulty();
            
            LevelEvents.OnShouldIncreaseSpeedDecision();

            nextDistanceMilestone += Random.Range(distanceInterval,nextDistanceMilestone);
        }
        else if(maxSpawnSpeedLimitReached && !maxSpeedReachedMessageDisplayed)
        {
            maxSpeedReachedMessageDisplayed = true;
            LevelEvents.OnStatusUpdate(GameStatusTexts.MaxLimit);
        }
    }

    private void IncreaseDifficulty()
    {
        float previousInterval = ObjectsSpawner.Instance.SpawnInterval;

        if(previousInterval <= 1.2f)
        {
            maxSpawnSpeedLimitReached = true;
        }
        else
        {
            if(GameManager.Instance != null)
            {
                GameManager.Instance.DistanceCoveringSpeed += distanceCalculatingSpeedFactor;
            }

            ObjectsSpawner.Instance.SpawnInterval = Mathf.Max(minSpawnInterval,ObjectsSpawner.Instance.SpawnInterval - spawnIntervalReductionAmount);
            LevelEvents.TriggerStatusUpdate(GameStatusTexts.SpeedIncreased);
        }
        
        if(ObjectsSpawner.Instance.obstacleSpeed <= 20f)
        {
            ObjectsSpawner.Instance.obstacleSpeed += speedIncreasingFactor;
            ObjectsSpawner.Instance.energyBarSpeed += speedIncreasingFactor;
            
        }

        
    }
    
}
