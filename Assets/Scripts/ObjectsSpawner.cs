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
            if (obstacle != null)
            {
                obstacle.transform.position = transform.position;
                obstacle.SetActive(true);
                StartCoroutine(ObjectPool.SharedInstance.DisablePooledObjects(obstacle));
            }
            yield return new WaitForSeconds(coolDownTime);

        }
    }

    
}
