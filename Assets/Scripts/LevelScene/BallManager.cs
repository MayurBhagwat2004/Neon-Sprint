using UnityEngine;

public class BallManager : MonoBehaviour
{
    [SerializeField] private ParticleSystem trailParticleSystem;
    [SerializeField] private float minSpeed = 10f;
    [SerializeField] private float maxSpeed = 15f;
    [SerializeField] private float speedModifier = 5f;
    public bool shouldIncreaseSpeed = false;

    void OnEnable()
    {
        LevelEvents.OnSpeedIncreased += UpdateTheSpeeds;
    }
    void Start()
    {
        ActivateTheTrail();
        UpdateTrailSparkleSpeed(minSpeed,maxSpeed);
    }

    void Update()
    {
        if (shouldIncreaseSpeed)
        {
            Debug.Log("Increasing the trail speed");
            LevelEvents.InvokeTheSpeedModifier();
            shouldIncreaseSpeed = false;
        }
    }

    private void ActivateTheTrail()
    {
        if(trailParticleSystem == null) return;
         
        var mainModule = trailParticleSystem.main;

        mainModule.loop = true; //Keep the sparks flowing in a continously
        mainModule.prewarm = true; //Load the sparks before the game starts
        
        trailParticleSystem.Play(); //Start the trail
    }

    public void UpdateTheSpeeds()
    {
        minSpeed += speedModifier;
        maxSpeed += speedModifier;

        UpdateTrailSparkleSpeed(minSpeed,maxSpeed);
    }
    private void UpdateTrailSparkleSpeed(float minSpeed,float maxSpeed)
    {
        var mainModule = trailParticleSystem.main;
        
        mainModule.startSpeed = new ParticleSystem.MinMaxCurve(minSpeed,maxSpeed);
    }
}
