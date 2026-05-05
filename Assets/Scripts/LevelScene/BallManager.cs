using UnityEngine;

public class BallManager : MonoBehaviour
{
    [SerializeField] private ParticleSystem trailParticleSystem;
    [SerializeField] private float particleMinSpeed = 5f;
    [SerializeField] private float particleMaxSpeed = 10f;

    [SerializeField] private float speedModifier = 5f;

    void OnEnable()
    {
        LevelEvents.OnSpeedIncreased += UpdateTheSpeeds;
    }

    void OnDisable()
    {
        LevelEvents.OnSpeedIncreased -= UpdateTheSpeeds;
    }
    void Start()
    {
        ActivateTheTrail();
        UpdateTrailSparkleSpeed(particleMinSpeed,particleMaxSpeed);
    }

    void Update()
    {
        
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
        if(particleMinSpeed == 25 && particleMaxSpeed ==30) return;
        
        particleMinSpeed += speedModifier;
        particleMaxSpeed += speedModifier;

        UpdateTrailSparkleSpeed(particleMinSpeed,particleMaxSpeed);
    }
    private void UpdateTrailSparkleSpeed(float particleMinSpeed,float particleMaxSpeed)
    {
        var mainModule = trailParticleSystem.main;
        
        mainModule.startSpeed = new ParticleSystem.MinMaxCurve(particleMinSpeed,particleMaxSpeed);
    }
}
