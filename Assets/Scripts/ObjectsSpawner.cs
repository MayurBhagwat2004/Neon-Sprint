using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;
public class ObjectsSpawner : MonoBehaviour
{
    public static ObjectsSpawner instance;

    [Header("Spawn Settings")]
    [SerializeField] private float initialCoolDown = 2f;
    [SerializeField] private float minCoolDown = 0.5f;
    [SerializeField] private float coolDownReduceAmount = 0.1f;

    private float currentCoolDown;
    private bool isSpawning = false;
    public bool canIncreaseSpeed;
    public bool started;

    private void OnEnable() => GameEvents.OnSpeedIncreased += HandleSpeedIncrease;
    private void OnDisable() => GameEvents.OnSpeedIncreased -= HandleSpeedIncrease;

    void Start()
    {
        currentCoolDown = initialCoolDown;
    }
    void Awake()
    {
        if (instance != this && instance != null) Destroy(this);
        else instance = this;
    }

    private void Update()
    {
        if (GameManager.Instance.canPlayGame && GameManager.Instance.isPlayerAlive && !isSpawning)
        {
            isSpawning = true;
            StartCoroutine(SpawnRoutine());
        }

    }

    private void HandleSpeedIncrease()
    {
        currentCoolDown = Mathf.Max(minCoolDown,currentCoolDown - coolDownReduceAmount);
    }

    IEnumerator SpawnRoutine()
    {
        while (GameManager.Instance.isPlayerAlive)
        {
            GameObject obstacle = ObjectPool.SharedInstance.GetPooledObject();

            if(obstacle != null)
            {
                obstacle.transform.position = transform.position;
                obstacle.SetActive(true);
            }

            yield return new WaitForSeconds(currentCoolDown);
        }
        isSpawning = false;
    }



}


