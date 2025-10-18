using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
    }

    void Update()
    {
        if (!GameManager.Instance.CanStartGame()) return;
        
        if (GameManager.Instance.CanStartGame())
            rb.gravityScale = 1;

    }
    
    public void DetectTouch()
    {
    }
}
