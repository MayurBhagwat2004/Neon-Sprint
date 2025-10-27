using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

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
        else
            pooledObjects[randomObj].SetActive(false);
        
        return null;
    }
    
    public IEnumerator DisablePooledObjects(GameObject pooledObject)
    {
        yield return new WaitForSeconds(2);
        pooledObject.SetActive(false);
    }
    
}
