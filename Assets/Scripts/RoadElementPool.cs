using System;
using System.Collections.Generic;
using UnityEngine;


public class RoadElementPool : MonoBehaviour
{
    [SerializeField] private RoadElement[] roadElements;

    public int initialPoolSize;
    public Dictionary<RoadElementType, RoadElement> roadElementDict;
    private Dictionary<RoadElementType, Queue<RoadElement>> pool;
    private void Start()
    {
        pool = new Dictionary<RoadElementType, Queue<RoadElement>>();
        roadElementDict = new Dictionary<RoadElementType, RoadElement>();
        
        InitializeRoadElementDict();
        InitializePoolDict();
        Populate();
    }

    private void InitializePoolDict()
    {
        foreach (RoadElementType type in Enum.GetValues(typeof(RoadElementType)))
        {
            pool.Add(type, new Queue<RoadElement>());
        }
    }

    private void InitializeRoadElementDict()
    {
        foreach (RoadElement roadElement in roadElements)
        {
            roadElementDict.Add(roadElement.type, roadElement);
        }
    }

    private void Populate()
    {
        foreach (var pair in roadElementDict)
        {
            for (int i = 0; i < initialPoolSize; i++)
            {
                CreateNewRoadElement(pair.Key);
            }
        }
    }

    public RoadElement Get(RoadElementType type)
    {
        if (pool[type].IsEmpty())
        {
            CreateNewRoadElement(type);
        }

        RoadElement current = pool[type].Dequeue();
        current.gameObject.SetActive(true);
        return current;
    }

    public void Inactivate(RoadElement current)
    {
        current.gameObject.SetActive(false);
        pool[current.type].Enqueue(current);
    }

    private void CreateNewRoadElement(RoadElementType type)
    {
        RoadElement curr = Instantiate(roadElementDict[type], transform);
        pool[curr.type].Enqueue(curr);
        curr.gameObject.SetActive(false);
    }
}

