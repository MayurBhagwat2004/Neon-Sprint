using System.Collections;
using UnityEngine;

public class CameraSlomo : MonoBehaviour
{
    public static CameraSlomo Instance;
    private Camera cam;
    [SerializeField] private float slomoDuration = 0.5f;
    [Range(0f,1f)]
    [SerializeField] private float slomoSpeed;
    [SerializeField] private float targetCamSize;
    [SerializeField] private float originalSize;
    private Vector3 originalPos;
    [SerializeField]private bool readyAgain;

    void Awake()
    {
        if(Instance != this && Instance!=null) Destroy(this);
        else Instance = this;
    }
    void Start()
    {
        cam = transform.GetComponent<Camera>(); //Getting the reference to the slomo camera
        originalSize = cam.orthographicSize;
        originalPos = transform.position;

    }

    void Update()
    {

    }

    public void ActivateSloMo()
    {
        StartCoroutine(ActivateSlowMoRoutine());
    }

    private IEnumerator ActivateSlowMoRoutine()
    {

        Time.timeScale = slomoSpeed;

        float currentCameraSize = originalSize;
        float currentDuration = 0f;

        while (currentDuration < slomoDuration)
        {
            float t = currentDuration / slomoDuration;

            cam.orthographicSize = Mathf.Lerp(currentCameraSize, targetCamSize, t);
            
            currentDuration += Time.unscaledDeltaTime;
            yield return null;

        }

        cam.orthographicSize = targetCamSize; 

        yield return new WaitForSecondsRealtime(slomoDuration);

        currentDuration = 0f;

        while (currentDuration < slomoDuration)
        {
            float t = currentDuration / slomoDuration;

            cam.orthographicSize = Mathf.Lerp(targetCamSize, originalSize, t);
            currentDuration += Time.unscaledDeltaTime;

            yield return null;

        }

        Time.timeScale = 1f;
        cam.orthographicSize = originalSize;

    }
}
