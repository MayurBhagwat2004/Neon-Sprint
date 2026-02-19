using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : MonoBehaviour
{
    public float rotationSpeed = 360f;
    public float movementSpeed = 16f;
    public Rigidbody2D rb;
    void OnEnable()
    {

        if (GameManager.Instance != null)
        {
            movementSpeed = GameManager.Instance.currentGlobalSpeed;
        }

        SpinAndMove();

        GameEvents.OnSpeedIncreased += UpdateSpeed;
    }

    void OnDisable()
    {
        GameEvents.OnSpeedIncreased -= UpdateSpeed;

    }
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }



    private void UpdateSpeed()
    {
        movementSpeed = GameManager.Instance.currentGlobalSpeed;
        rb.velocity = Vector2.left * movementSpeed;

    }

    
    public void SpinAndMove()
    {
        rb.angularVelocity = rotationSpeed;
        rb.velocity = Vector2.left * movementSpeed;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && GameManager.Instance.isPlayerAlive)
            GameManager.Instance.PlayerDied();


        if (collision.gameObject.CompareTag("ScoreWall") && GameManager.Instance.isPlayerAlive)
            GameManager.Instance.IncreaseScore();

    }
}
