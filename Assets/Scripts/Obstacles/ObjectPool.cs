using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool SharedInstance;
    public List<GameObject> pooledObjects;
    public List<GameObject> objectToPool;
    public int amountToPool;
    private int currentReturnedObjectIndex;
    private void Awake()
    {
        SharedInstance = this;
    
    pooledObjects = new List<GameObject>();
        GameObject tmp;
        for (int i = 0; i < amountToPool; i++)
        {
            int random = Random.Range(0,objectToPool.Count);
            tmp = Instantiate(objectToPool[random]);
            tmp.SetActive(false);
            pooledObjects.Add(tmp); //Adding the newly created gameobjects to the list
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public GameObject GetPooledObject()
    {

        int randomObj = Random.Range(0, pooledObjects.Count);
        if (!pooledObjects[randomObj].activeInHierarchy)
            return pooledObjects[randomObj];
        // for (int i = currentReturnedObjectIndex; i < pooledObjects.Count; i++)
        // {
        //     if (!pooledObjects[i].activeInHierarchy)
        //     {
        //         currentReturnedObjectIndex = i;
        //         return pooledObjects[i];
        //     }
        //     else
        //         pooledObjects[i].SetActive(false);

        // }
        return null;
    }
    
    public IEnumerator DisablePooledObjects(GameObject pooledObject)
    {
        yield return new WaitForSeconds(2);
        pooledObject.SetActive(false);
    }
    
}
