using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using System;
public class Player : MonoBehaviour
{
    public bool canMove;
    private bool superSonicAbilityEnabled;
    private Camera mainCamera;
    //Touch inputs variables
    public Vector2 touchPos;
    private bool isDragging;
    private float moveSpeed = 15f;
    [SerializeField]private float maxY = 3.5f;
    [SerializeField] private float minY = -4.5f;
    void Awake()
    {
        mainCamera = Camera.main;
    }

    void Start()
    {
        if(GameManager.Instance!=null && GameManager.Instance.isGamePaused != true) superSonicAbilityEnabled = false;

    }

    void Update()
    {
        canMove = !GameManager.Instance.isGamePaused;

        if(!canMove) return;

        TakePlayerInput();
            
    }

    public void TakePlayerInput()
    {
        if(Pointer.current!=null)
        {
            if (Pointer.current.press.wasPressedThisFrame)
            {
                if(EventSystem.current.IsPointerOverGameObject()) isDragging = false; //Checking if the player clicks on the ui so that the dragging of the neon ball stops
                else isDragging = true;
            }

            if(Pointer.current.press.wasReleasedThisFrame) isDragging = false; //Checking if the player discontinued touching the screen

            if(Pointer.current.press.isPressed && isDragging)
            {
                touchPos = Pointer.current.position.ReadValue();

                Vector3 worldPosition = mainCamera.ScreenToWorldPoint(touchPos);
                worldPosition.z = transform.position.z;
                worldPosition.y = Mathf.Clamp(worldPosition.y,minY,maxY); //Limiting the movement of the ball in +y and -y axis

                transform.position = Vector3.Lerp(transform.position,worldPosition,moveSpeed * Time.deltaTime);
            }
        }
    }
}
