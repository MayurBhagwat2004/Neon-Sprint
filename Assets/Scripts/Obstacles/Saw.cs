using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : MonoBehaviour
{
    public float rotationSpeed = 360f;
    public float movementSpeed = 6f;
    public Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        SpinAndMove();

    }

    void FixedUpdate()
    {
    }

    public void SpinAndMove()
    {
        rb.angularVelocity = rotationSpeed;
        rb.velocity = Vector2.left * movementSpeed;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("DestroyWall"))
            Destroy(gameObject);
    }
}
