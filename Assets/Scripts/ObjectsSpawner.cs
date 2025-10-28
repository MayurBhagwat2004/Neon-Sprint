using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;
public class ObjectsSpawner : MonoBehaviour
{
    public int coolDownTime =2;
    public bool started;
    void Start()
    {
    }

    private void Update() {
        if (GameManager.Instance.canPlayGame && !started)
        {
            started = true;
            StartCoroutine(CreateObstacles());
        }
    }
    IEnumerator CreateObstacles()
    {

        while (true && GameManager.Instance.canPlayGame)
        {
            yield return new WaitForSeconds(coolDownTime);
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
