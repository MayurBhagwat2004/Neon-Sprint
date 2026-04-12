using System.Collections;
using UnityEngine;

public class EnergyBar : MonoBehaviour
{
    private SpriteRenderer obstacleSprite;
    public ParticleSystem obstacleParticleSys;
    public float particleEffectDuration = 0.5f;
    public float particleDimSpeed = 5f;

    void Start()
    {
        if(transform.GetComponent<ParticleSystem>() != null)
        {
            obstacleParticleSys = transform.GetComponent<ParticleSystem>();
        }

        obstacleSprite = transform.GetComponent<SpriteRenderer>();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            CallDestroyObstacle();
        }
    }
    public void CallDestroyObstacle()
    {
        StartCoroutine(DestroyObstacle());
    }
    private IEnumerator DestroyObstacle()
    {
        if(obstacleParticleSys != null) obstacleParticleSys.Play();

        yield return StartCoroutine(SlowlyFadeObstacle());

    }
    private IEnumerator SlowlyFadeObstacle()
    {
        Color spriteCol = obstacleSprite.color;
        float startAlpha = spriteCol.a;
        float currentDuration = 0f;
        
        while(currentDuration < particleEffectDuration)
        {
            currentDuration += Time.deltaTime;
            
            float percentCompleted = currentDuration/particleEffectDuration;
            
            spriteCol.a = Mathf.Lerp(startAlpha,0f,percentCompleted);
            obstacleSprite.color = spriteCol;

            yield return null;
        }

        Destroy(gameObject);
        
        
    }
}
