using UnityEngine;

public class BallManager : MonoBehaviour
{
    [SerializeField] private ParticleSystem trailParticleSystem;


    void OnEnable()
    {
    }
    void Start()
    {
        ActivateTheTrail();        
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
}
