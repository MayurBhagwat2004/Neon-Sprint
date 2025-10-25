using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Pool;
public class ObjectsSpawner : MonoBehaviour
{
    public int coolDownTime =2;


    void Start()
    {
        StartCoroutine(CreateObstacles());
    }
    IEnumerator CreateObstacles()
    {
        while (true)
        {
            GameObject obstacle = ObjectPool.SharedInstance.GetPooledObject();
            obstacle.SetActive(true);
            obstacle.transform.position = transform.position;

            yield return new WaitForSeconds(coolDownTime);
            DisablePooledObjects(obstacle);
        }
    }

    private void DisablePooledObjects(GameObject pooledObject)
    {
        pooledObject.SetActive(false);
    }
}
