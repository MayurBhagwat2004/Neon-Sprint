using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerTouchInput : MonoBehaviour
{
    public bool playerTouched;
    void Start()
    {
        
    }

    void Update()
    {
        TakeTouchInput();
    }

    private void TakeTouchInput()
    {
        if(Pointer.current != null)
        {
            if (Pointer.current.press.wasPressedThisFrame)
            {
                playerTouched = true;
                CameraSlomo.Instance.ActivateSloMo();

            }

            if (Pointer.current.press.wasReleasedThisFrame)
            {
                playerTouched = false;
            }
        }
    }
}
