using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using System.Drawing;
public class Player : MonoBehaviour
{
    private Camera mainCamera;
    
    #region  Player Swipe Settings
    [Header("Player Swipe Settings")]
    public bool canMove;
    public Vector2 touchPos;
    [SerializeField] private bool isDragging;
    private float moveSpeed = 5f;
    public float MoveSpeed
    {
        get
        {
            return moveSpeed;
        }
        set
        {
            moveSpeed = value;
        }
    }
    
    [SerializeField] private float maxY = 3.5f;
    [SerializeField] private float minY = -4.5f;
    [SerializeField] private float maxX = 5f;
    [SerializeField] private float minX = 5f;

    public ParticleSystem damageParticleEffect;
    [SerializeField] private TrailRenderer swipeFeedbackRenderer;

    public float offset = 2f;

    #endregion
    void Awake()
    {
        mainCamera = Camera.main;
    }

    void Start()
    {
        if (swipeFeedbackRenderer != null)
        {
            swipeFeedbackRenderer.emitting = true;
        }
        if (damageParticleEffect != null)
        {
            var main = damageParticleEffect.main;
            main.loop = false;
            damageParticleEffect.Clear();
            damageParticleEffect.Stop();
        }

    }

    void Update()
    {
        if (GameManager.Instance.gameEnded) return;

        canMove = !GameManager.Instance.isGamePaused;

        if (!canMove) return;

        TakeplayerTouchVal(); //Take the input provided by the user via mobile,pc.

    }
    public void TakeplayerTouchVal()
    {

        if (Pointer.current != null)
        {
            if (Pointer.current.press.wasPressedThisFrame)
            {
                if (EventSystem.current.IsPointerOverGameObject()) isDragging = false; //Checking if the player clicks on the ui so that the dragging of the neon ball stops
                else isDragging = true;
            }

            if (Pointer.current.press.wasReleasedThisFrame) isDragging = false; //Checking if the player discontinued touching the screen

            if (Pointer.current.press.isPressed && isDragging)
            {
                MoveTheBall();
            }

            if (Pointer.current.press.wasReleasedThisFrame)
            {
                GameManager.Instance.PlayerLiftedFinger = true;
            }

            if (!GameManager.Instance.gameEnded && Pointer.current.press.wasPressedThisFrame)
            {
                if (isDragging)
                {
                    GameManager.Instance.PlayerLiftedFinger = false;
                }
            }
        }
    }
    public void PlayAffectedEffect()
    {
        damageParticleEffect.Play();
    }

    private void MoveTheBall()
    {
        Vector2 screenTouchPos = Pointer.current.position.ReadValue(); //Player touch position
        Vector3 worldTouchPos = mainCamera.ScreenToWorldPoint(screenTouchPos);//Player touch position converted to real world coordinates

        float targetX = worldTouchPos.x + offset;
        float targetY = worldTouchPos.y + offset;

        //Restricting the values to not let the player drag the ball of the screen
        targetY = Mathf.Clamp(targetY,minY,maxY);
        targetX = Mathf.Clamp(targetX,minX,maxX);

        Vector2 targetPos = new Vector2(targetX,targetY);

        transform.position = Vector2.Lerp(transform.position,targetPos,moveSpeed * Time.deltaTime);


    }

}