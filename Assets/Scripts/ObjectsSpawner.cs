using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ObjectsSpawner : MonoBehaviour
{
    public List<GameObject> obstaclePrefabs;
    public int poolSize = 10;
    private List<GameObject> pool;

    public float spawnInterval = 2f;
    public Transform spawnPoint;
    public float timer;
    void Start()
    {
        pool = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            int randomIndex = Random.Range(0, obstaclePrefabs.Count);
            GameObject obj = Instantiate(obstaclePrefabs[randomIndex]);
            obj.SetActive(false);
            pool.Add(obj);
        }
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnObstacle();
            timer = 0f;
        }
    }
    
    void SpawnObstacle()
    {
        GameObject obstacle = GetPooledObject();

        if(obstacle != null)
        {
            Vector3 spawnPos = spawnPoint.position;
            obstacle.transform.position = spawnPos;
            obstacle.SetActive(true);
        }
    }

    public GameObject GetPooledObject()
    {
        foreach (var obj in pool)
        {
            if (!obj.activeInHierarchy)
                return obj;
        }

        int randomIndex = Random.Range(0, obstaclePrefabs.Count);
        GameObject newObj = Instantiate(obstaclePrefabs[randomIndex]);
        newObj.SetActive(false);
        pool.Add(newObj);
        return newObj;
        {
            
        }
    }

}
