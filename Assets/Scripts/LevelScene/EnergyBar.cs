using System.Collections;
using UnityEngine;

public class EnergyBar : MonoBehaviour
{
    private SpriteRenderer obstacleSprite;
    public ParticleSystem obstacleParticleSys;
    public float particleEffectDuration = 0.5f;
    public float particleDimSpeed = 5f;
    public float moveSpeed = 5f;
    private Vector3 moveDirection = Vector3.left;
    Color defaultColor = Color.white;

    void OnDisable()
    {
    }
    void Start()
    {
        if(transform.GetComponent<ParticleSystem>() != null)
        {
            obstacleParticleSys = transform.GetComponent<ParticleSystem>();
        }

        obstacleSprite = transform.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        StartMoving();
    }

    private void StartMoving()
    {
        if(GameManager.Instance.isGamePaused) return;

        float currentSpeed = GameManager.Instance.DistanceCoveringSpeed * moveSpeed;

        transform.Translate(moveDirection * currentSpeed * Time.deltaTime,Space.World);

    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            CallDestroyObstacle();
        }
        else if (collision.CompareTag("Wall"))
        {
            DisableEnergy();
        }
    }

    private void DisableEnergy()
    {
        gameObject.SetActive(false);
        obstacleSprite.color = defaultColor;

    }
    public void CallDestroyObstacle()
    {
        StartCoroutine(DestroyObstacle());

        StartCoroutine(SlowlyFadeObstacle());
    }
    private IEnumerator DestroyObstacle()
    {
        if(obstacleParticleSys != null) obstacleParticleSys.Play();


        yield return null;

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

        gameObject.SetActive(false);

        
        
    }
}
