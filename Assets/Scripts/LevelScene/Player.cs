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
    private bool isDragging;
    [SerializeField] private float moveSpeed = 5f;
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

    private float maxY = 3.5f;
    private float minY = -4.5f;
    private float maxX = 8f;
    private float minX = -8f;
    private Vector3 lastTouchWorldPos;
    private Vector3 virtualTargetPos;

    public ParticleSystem damageParticleEffect;
    [SerializeField] private TrailRenderer swipeFeedbackRenderer;


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
                if (EventSystem.current.IsPointerOverGameObject())
                {
                    isDragging = false; //Checking if the player clicks on the ui so that the dragging of the neon ball stops
                }
                else
                {
                    lastTouchWorldPos = GetWorldPosition(Pointer.current.position.ReadValue());
                    isDragging = true;
                }
            }
            if (Pointer.current.press.wasReleasedThisFrame)
            {
                GameManager.Instance.PlayerLiftedFinger = true;
                isDragging = false; //Checking if the player discontinued touching the screen
            }

            if (Pointer.current.press.isPressed && isDragging)
            {
                MoveTheBall();
            }
        }
    }

    void FixedUpdate()
    {
        if (GameManager.Instance.gameEnded) return;

    }
    public void PlayAffectedEffect()
    {
        damageParticleEffect.Play();
    }

    private void MoveTheBall()
    {
        var pointer = Pointer.current;
 
        if (pointer == null) return;

        Vector3 currentTouchedWorldPos = GetWorldPosition(pointer.position.ReadValue()); //Player touch position

        Vector3 delta = currentTouchedWorldPos - lastTouchWorldPos;

        virtualTargetPos += delta;

        virtualTargetPos.x = Mathf.Clamp(virtualTargetPos.x, minX, maxX);
        virtualTargetPos.y = Mathf.Clamp(virtualTargetPos.y, minY, maxY);

        transform.position = Vector3.Lerp(transform.position, virtualTargetPos, moveSpeed * Time.deltaTime);

        lastTouchWorldPos = currentTouchedWorldPos;



    }

    private Vector3 GetWorldPosition(Vector2 screenPos)
    {
        Vector3 screenPosWithDepth = new Vector3(screenPos.x, screenPos.y, 10f);
            return mainCamera.ScreenToWorldPoint(screenPosWithDepth);
    }

}