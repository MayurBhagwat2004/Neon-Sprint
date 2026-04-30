using System.Collections;
using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    [Header("Speed logic variables")]
    [SerializeField] private Player playerScript;
    [SerializeField] private float minSpawnInterval = 0.2f;
    [SerializeField] private float spawnReductionAmount = 0.1f;
    private float distanceCalculatingSpeedFactor = .2f;
    private float speedIncreasingFactor;

    [Header("Progression Threshold")]
    [SerializeField] private float distanceInterval = 100f;
    [SerializeField]private float nextDistanceMilestone;
    private float timeElapsed;
    float currentDist;
    private bool maxSpawnSpeedLimitReached;
    private bool maxSpeedReachedMessageDisplayed;
    void Start()
    {
        nextDistanceMilestone = distanceInterval;
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

            nextDistanceMilestone += distanceInterval;
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

        if(previousInterval <= 1f)
        {
            maxSpawnSpeedLimitReached = true;
        }
        else
        {
            if(GameManager.Instance != null)
            {
                GameManager.Instance.DistanceCoveringSpeed += distanceCalculatingSpeedFactor;
            }

            ObjectsSpawner.Instance.SpawnInterval = Mathf.Max(minSpawnInterval,ObjectsSpawner.Instance.SpawnInterval - spawnReductionAmount);
            LevelEvents.TriggerStatusUpdate(GameStatusTexts.SpeedIncreased);
        }
        
        if(ObjectsSpawner.Instance.obstacleSpeed <= 20f)
        {
            ObjectsSpawner.Instance.obstacleSpeed += speedIncreasingFactor;
            ObjectsSpawner.Instance.energyBarSpeed += speedIncreasingFactor;

            // if(playerScript != null)
            // {
            //     playerScript.MoveSpeed = Mathf.Min(playerScript.MoveSpeed + playerSpeedIncrease,maxPlayerSpeed);
            // }

            
        }

        
    }
    
}
