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
        SpinAndMove();
        GameEvents.OnSpeedIncreased += IncreaseSpeed;

    }

    void OnDisable()
    {
        GameEvents.OnSpeedIncreased -= IncreaseSpeed;

    }
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
   
    private void IncreaseSpeed()
    {
        movementSpeed += 0.2f;
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
