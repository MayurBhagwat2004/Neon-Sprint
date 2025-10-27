using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
public class Player : MonoBehaviour
{
    public static Player Instance;
    public Rigidbody2D rb;
    public float jumpForce = 10f;
    [SerializeField] private bool onGround;
    public bool canJumpAgain;

    public Animator animator;   
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        rb.gravityScale = 0;
    }

    void Update()
    {
        if (!GameManager.Instance.CanStartGame()) return;

        if (GameManager.Instance.CanStartGame())

        DetectInput();

    }

    public void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    public void DetectInput()
    {
        if (!GameManager.Instance.canPlayGame) return;

        if (onGround)
            rb.gravityScale = 1f;
        else
            rb.gravityScale = 9.8f;


#if UNITY_EDITOR || UNITY_STANDALONE
        if (Input.GetMouseButtonDown(0) && canJumpAgain)
            Jump();


#else
    if(Touchscreen.current!=null)
    {
        var touch = Touchscreen.current.primaryTouch;
        if(touch.press.wasPressedThisFrame && canJumpAgain)
            Jump();
      
    }

#endif

    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            onGround = true;
            canJumpAgain = true;
            animator.SetBool("Run", true);
            animator.SetBool("Jump", false);

        }
        
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            onGround = false;
            canJumpAgain = false;
            animator.SetBool("Jump",true);
            animator.SetBool("Run", false);
        }
    }

    
}
