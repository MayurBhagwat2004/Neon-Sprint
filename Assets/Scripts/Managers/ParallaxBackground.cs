using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    [Range(0f,1f)]
    public float parallaxAmount;
    public float globalMoveSpeed = 5f;
    [SerializeField] private float speedIncreasingFactor = 0.05f;
    private float width;

    void OnEnable()
    {
        LevelEvents.OnShouldIncreaseSpeed += IncreaseTheSpeed;
    }

    void OnDisable()
    {
        LevelEvents.OnShouldIncreaseSpeed -= IncreaseTheSpeed;
    }
    void Start()
    {
        width = GetComponent<SpriteRenderer>().sprite.bounds.size.x;
    }

    void Update()
    {
        if(GameManager.Instance.GameStarted && !GameManager.Instance.gameEnded)
        {
            float movement = globalMoveSpeed * parallaxAmount * Time.deltaTime;
            transform.Translate(Vector2.left * movement);
            
            if(transform.position.x <= -width)
            {
                transform.position += new Vector3(width * 2,0,0);
            }
        
        }
    }

    private void IncreaseTheSpeed()
    {
        if(parallaxAmount == 1f) return; //Return if reached maximum speed
        parallaxAmount = Mathf.Clamp(parallaxAmount,parallaxAmount+speedIncreasingFactor,.5f);
    }
}
