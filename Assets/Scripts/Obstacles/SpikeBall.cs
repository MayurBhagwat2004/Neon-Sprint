using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeBall : MonoBehaviour
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
        StartSpinning();

    }

    void FixedUpdate()
    {
    }

    public void StartSpinning()
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
