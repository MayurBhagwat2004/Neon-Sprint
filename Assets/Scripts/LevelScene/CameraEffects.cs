using System.Collections;
using JetBrains.Annotations;
using UnityEngine;

public class CameraEffects : MonoBehaviour
{
    private Camera cam;
    private float originalSize;
    private Vector3 originalPosition;

    public Transform playerTransform;

    [Header("Zoom Settings")]
    public float targetZoomSize = 3.5f;
    public float zoomTransitionTime = 0.2f;

    [Header("Slow-Mo Settings")]
    public float slowMoSpeed = 0.2f;
    public float slowMoDuration = 0.5f;
    private Coroutine currentZoomRoutine;

    void Awake()
    {
        cam = GetComponent<Camera>();
        originalSize = cam.orthographicSize;

        originalPosition = transform.position;
    }

    void OnEnable()
    {
        LevelEvents.OnObstacleHit += TriggerHitEffect;
    }

    void OnDisable()
    {
        LevelEvents.OnObstacleHit -= TriggerHitEffect;
        
    }
    void Start()
    {
        
    }

    public void TriggerHitEffect()
    {
        if(currentZoomRoutine != null)
        {
            StopCoroutine(currentZoomRoutine);
        }

        currentZoomRoutine = StartCoroutine(HitStopRoutine());
    }

    private IEnumerator HitStopRoutine()
    {
        Time.timeScale = slowMoSpeed;

        Vector3 targetPosition = new Vector3(playerTransform.position.x, playerTransform.position.y, originalPosition.z);

        float elapsedTime = 0f;
        while(elapsedTime < zoomTransitionTime)
        {
            float t = elapsedTime/zoomTransitionTime;
            
            cam.orthographicSize = Mathf.Lerp(originalSize,targetZoomSize,t);

            transform.position = Vector3.Lerp(originalPosition,targetPosition,t);

            elapsedTime += Time.unscaledDeltaTime;
            yield return null;
        }

        cam.orthographicSize = targetZoomSize;
        transform.position = targetPosition;

        yield return new WaitForSecondsRealtime(slowMoDuration);

        elapsedTime = 0f;
        while(elapsedTime < zoomTransitionTime)
        {
            float t = elapsedTime / zoomTransitionTime;
            
            cam.orthographicSize = Mathf.Lerp(targetZoomSize,originalSize,t);
            transform.position = Vector3.Lerp(targetPosition,originalPosition,t);


            elapsedTime += Time.unscaledDeltaTime;
            yield return null;
        }

        cam.orthographicSize = originalSize;
        transform.position = originalPosition;

        Time.timeScale = 1f;

        currentZoomRoutine = null;
    }
}
