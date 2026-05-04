using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    [Range(0f,1f)]
    public float parallaxAmount;    
    public float globalMoveSpeed = 5f;
    private float width;
    public bool playGame;
    void Start()
    {
        width = GetComponent<SpriteRenderer>().sprite.bounds.size.x;
    }

    void Update()
    {
        if(playGame)
        {
            float movement = globalMoveSpeed * parallaxAmount * Time.deltaTime;
            transform.Translate(Vector2.left * movement);
            
            if(transform.position.x <= -width)
            {
                transform.position += new Vector3(width * 2,0,0);
            }
        
        }
    }
}
