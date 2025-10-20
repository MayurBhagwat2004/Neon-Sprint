using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
public class Player : MonoBehaviour
{
    public Rigidbody2D rb;
    public float jumpForce = 10f;
    [SerializeField] private bool onGround;
    public bool canJumpAgain;
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

        DetectInput();

    }

    public void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce);
        

    }

    public void DetectInput()
    {
        if (!GameManager.Instance.canPlayGame) return;

#if UNITY_EDITOR || UNITY_STANDALONE
        if (Input.GetMouseButtonDown(0) && canJumpAgain)
        {
            Jump();
            onGround = false;
            canJumpAgain = false;
        }
        if (Input.GetMouseButtonUp(0))
        {
            
            canJumpAgain = true;
        }

#else
    if(Touchscreen.current!=null)
    {
        var touch = Touchscreen.current.primaryTouch;
        if(touch.press.wasPressedThisFrame && canJumpAgain){
            Jump();
            onGround = false;   
            canJumpAgain = false;
        }
        if (touch.press.wasReleasedThisFrame)
            canJumpAgain = true;
    }


#endif

    }
}
