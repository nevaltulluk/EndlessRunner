
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GroundManager : MonoBehaviour
{

    [SerializeField] private float speed;
    [SerializeField] private Road[] roads;

    private List<Vector3> _initialRoadPositions;
    private EventBus _eventBus;
    private float _offScreenPositionZ;
    private bool _isPlaying;
    private bool _shouldReset;
    void Start()
    {
        _eventBus = Container.Instance.GetEventBus();
        _initialRoadPositions = new List<Vector3>();
        _offScreenPositionZ = -1 * (roads[0].transform.localScale.z / 2) - 10;
        _isPlaying = false;
        foreach (var road in roads)
        {
            _initialRoadPositions.Add(road.transform.localPosition);
        }
        
        roads[0].Initialize(30);
        roads[1].Initialize(0);
        
        _eventBus.Subscribe<GameStartedEvent>(OnGameStarted);
        _eventBus.Subscribe<GameOverEvent>(OnGameOver);
    }

    private void OnGameStarted()
    {
        if (_shouldReset)
        {
            ResetRoads();
        }
        _isPlaying = true;
    }

    private void OnGameOver()
    {
        _isPlaying = false;
        _shouldReset = true;
    }
    
    private void Update()
    {
        if (_isPlaying)
        {
            Move();
        }
    }

    private void Move()
    {
        foreach (Road road in roads)
        {
            road.transform.position += Vector3.back * speed * Time.deltaTime;
            if (road.transform.position.z <= _offScreenPositionZ)
            {
                road.ClearRoad();
                road.Initialize(0);
                road.transform.position += Vector3.forward * roads.Length * road.transform.localScale.z;
            }
        }
    }

    private void ResetRoads()
    {
        for (int index = 0; index < roads.Length; index++)
        {
            Road road = roads[index];
            road.transform.localPosition = _initialRoadPositions[index];
            road.ClearRoad();
        }
        
        roads[0].Initialize(30);
        roads[1].Initialize(0);
    }
}
