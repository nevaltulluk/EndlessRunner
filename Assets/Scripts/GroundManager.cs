using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundManager : MonoBehaviour
{

    [SerializeField] private float speed;
    [SerializeField] private Transform[] roads;
    
    
    public static float offScreenPositionZ = -50;
    void Start()
    {
        
    }

    void Update()
    {
        foreach (Transform road in roads)
        {
            road.position += Vector3.back * speed * Time.deltaTime;
            if (road.position.z <= offScreenPositionZ)
            {
                road.position += Vector3.forward * roads.Length * road.localScale.z;
            }
        }
        

        
    }
}
