using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundManager : MonoBehaviour
{

    [SerializeField] private float speed;
    [SerializeField] private Transform[] roads;
    
    
    private float _offScreenPositionZ;
    void Start()
    {
        _offScreenPositionZ = -1 * (roads[0].localScale.z / 2) - 10;
    }

    void Update()
    {
        foreach (Transform road in roads)
        {
            road.position += Vector3.back * speed * Time.deltaTime;
            if (road.position.z <= _offScreenPositionZ)
            {
                road.position += Vector3.forward * roads.Length * road.localScale.z;
            }
        }
    }
}
