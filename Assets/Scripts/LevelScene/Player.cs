using UnityEngine;
using UnityEngine.InputSystem;
public class Player : MonoBehaviour
{
    [SerializeField] private bool amIAlive;
    private bool superSonicAbilityEnabled;
    private Camera mainCamera;
    //Touch inputs variables
    public Vector2 touchPos;

    void Awake()
    {
        mainCamera = Camera.main;
    }

    void Start()
    {
        amIAlive = true;
        superSonicAbilityEnabled = false;

    }

    void Update()
    {
        if(!amIAlive) return;

        if(Pointer.current != null && Pointer.current.press.isPressed)
        {
            touchPos = Pointer.current.position.ReadValue();

            Vector3 worldPosition = mainCamera.ScreenToWorldPoint(touchPos);
            worldPosition.z = transform.position.z;

            transform.position = worldPosition;
        }    
    }
}
