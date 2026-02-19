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
    private AudioSource audioSource;
    public Animator animator;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
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
        if(PlayerPrefs.GetInt("MusicEnabled") == 1)
            audioSource.Play();
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    public void DetectInput()
    {
        if (!GameManager.Instance.canPlayGame || GameManager.Instance.IsPointerOverUI()) return;

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

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            animator.SetBool("Hit",true);
            animator.SetBool("Run",false);
            animator.SetBool("Jump",false);
            
        }
    }
}
