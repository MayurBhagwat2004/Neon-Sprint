using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool SharedInstance;
    [SerializeField] private List<GameObject> prefabsToPool;
    [SerializeField] private int amountToPool = 20;

    private Queue<GameObject> poolQueue;
    private void Awake()
    {
        SharedInstance = this;
        poolQueue = new Queue<GameObject>();

        if (prefabsToPool == null || prefabsToPool.Count == 0) return;

        List<GameObject> tempLoader = new List<GameObject>();
        for (int i = 0; i < amountToPool; i++)
        {
            GameObject obj = Instantiate(prefabsToPool[Random.Range(0, prefabsToPool.Count)]);
            obj.SetActive(false);
            tempLoader.Add(obj); //Adding the newly created gameobjects to the list
        }

        foreach (GameObject obj in tempLoader)
        {
            poolQueue.Enqueue(obj);
        }
    }


    public GameObject GetPooledObject()
    {
        GameObject obj = poolQueue.Dequeue();
        
        poolQueue.Enqueue(obj);

        if(!obj.activeInHierarchy) return obj;
        return null;
    }

  

}
