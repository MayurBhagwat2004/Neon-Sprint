using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool SharedInstance;
    [SerializeField] private List<GameObject> prefabsToPool;
    [SerializeField] private int amountToPool = 20;

    private List<GameObject> pooledObjects;
    private void Awake()
    {
        SharedInstance = this;
        pooledObjects = new List<GameObject>();

        if (prefabsToPool == null || prefabsToPool.Count == 0) return;

        for (int i = 0; i < amountToPool; i++)
        {
            GameObject obj = Instantiate(prefabsToPool[Random.Range(0, prefabsToPool.Count)]);
            obj.SetActive(false);
            pooledObjects.Add(obj); //Adding the newly created gameobjects to the list
        }
    }


    public GameObject GetPooledObject()
    {
        int startIndex = Random.Range(0, pooledObjects.Count);

        // 2. Loop through the list once, starting from that random index
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            // Use modulo (%) to wrap around to the start of the list if we hit the end
            int index = (startIndex + i) % pooledObjects.Count;

            if (!pooledObjects[index].activeInHierarchy)
            {
                return pooledObjects[index];
            }
        }

        return null;
    }

    public IEnumerator DisablePooledObjects(GameObject pooledObject)
    {
        yield return new WaitForSeconds(2);
        pooledObject.SetActive(false);
    }

}
