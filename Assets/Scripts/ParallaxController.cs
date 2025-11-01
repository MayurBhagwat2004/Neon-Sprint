using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class ParallaxController : MonoBehaviour
{
    [SerializeField]private Camera cam;
    [SerializeField] private Transform camPos;
    private GameObject[] backGrounds;
    private Material[] backgroundMaterials;
    private float[] backgroundSpeed;
    private int backgroundCounts;
    private float farthestBack;
    [Range(.5f,2f)]
    [SerializeField]private float parallaxSpeed;

    void Awake()
    {
        cam = Camera.main;
        camPos.position = cam.transform.position; //Getting current camera position

        backgroundCounts = transform.childCount; //Getting the number of childrens of current objects


        backgroundMaterials = new Material[backgroundCounts]; //Initializing the number of background materials array
        backGrounds = new GameObject[backgroundCounts]; //Initializing the number of background gameobject array
        backgroundSpeed = new float[backgroundCounts]; //Initializing the number of speeds values


        for (int i = 0; i < backgroundCounts; i++)
        {
            backGrounds[i] = transform.GetChild(i).gameObject;
            backgroundMaterials[i] = backGrounds[i].GetComponent<Renderer>().material;

        }

        for (int i = 0; i < backgroundCounts; i++)
        {
        }

    }
    

    public void CalculateBackgroundSpeed()
    {
        for (int i = 0; i < backgroundCounts; i++)
        {
            if((backGrounds[i].transform.position.z - camPos.position.z) > farthestBack)
            {
                farthestBack = backGrounds[i].transform.position.z - camPos.position.z;
            }
        }

        for (int i = 0; i < backgroundCounts; i++)
        {
            backgroundSpeed[i] = 1 - (backGrounds[i].transform.position.z - camPos.position.z)/farthestBack;

        }
    }

    void LateUpdate()
    {
        for (int i = 0; i < backgroundCounts; i++)
        {
            float speed = backgroundSpeed[i] * parallaxSpeed;
            backgroundMaterials[i].SetTextureOffset("_MainTex", new Vector2(parallaxSpeed, 0) * Time.time);
        }
    }
}
