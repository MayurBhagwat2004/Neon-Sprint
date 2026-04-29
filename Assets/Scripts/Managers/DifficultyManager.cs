using System.Collections;
using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    [Header("Speed logic variables")]
    [SerializeField] private Player playerScript;
    private float playerSpeedIncrease = 1f;
    private float maxPlayerSpeed = 25f;
    [SerializeField] private float minSpawnInterval = 0.2f;
    [SerializeField] private float spawnReductionAmount = 0.1f;
    private float speedIncreasingFactor;

    [Header("Progression Threshold")]
    [SerializeField] private float distanceInterval = 100f;
    [SerializeField]private float nextDistanceMilestone;
    private float timeElapsed;
    float currentDist;
    void OnEnable()
    {
        
    }

    void OnDisable()
    {
        
    }
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

        if(currentDist >= nextDistanceMilestone)
        {
            IncreaseDifficulty();
            
            LevelEvents.OnShouldIncreaseSpeedDecision();

            nextDistanceMilestone += distanceInterval;
        }    
    }

    private void IncreaseDifficulty()
    {
        float previousInterval = ObjectsSpawner.Instance.SpawnInterval;

        ObjectsSpawner.Instance.SpawnInterval = Mathf.Max(minSpawnInterval,ObjectsSpawner.Instance.SpawnInterval - spawnReductionAmount);

        if(ObjectsSpawner.Instance.SpawnInterval < previousInterval)
        {
            ObjectsSpawner.Instance.obstacleSpeed += speedIncreasingFactor;
            ObjectsSpawner.Instance.energyBarSpeed += speedIncreasingFactor;

            if(playerScript != null)
            {
                playerScript.MoveSpeed = Mathf.Min(playerScript.MoveSpeed + playerSpeedIncrease,maxPlayerSpeed);
            }


            LevelEvents.TriggerStatusUpdate(GameStatusTexts.SpeedIncreased);

        }
    }

    


    
}
