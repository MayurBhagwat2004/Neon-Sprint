using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool SharedInstance;
    public List<GameObject> pooledObjects;
    public List<GameObject> objectToPool;
    public int amountToPool;

    private void Awake()
    {
        SharedInstance = this;
    }

    void Start()
    {
        pooledObjects = new List<GameObject>();
        GameObject tmp;
        for (int i = 0; i < amountToPool; i++)
        {
            int random = Random.Range(0,1);
            tmp = Instantiate(objectToPool[random]);
            tmp.SetActive(false);
            pooledObjects.Add(tmp); //Adding the newly created gameobjects to the list
        }
    }

    void Update()
    {

    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
                return pooledObjects[i];
        }
        return null;
    }
    
}
