using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Container : MonoBehaviour
{

    private EventBus _eventBus;

    public static Container Instance = null;
    
    
    
    void Awake()
    {
        _eventBus = new EventBus();
        if (Instance != null && Instance != this) 
        {
            Destroy(gameObject);
        }
 
        Instance = this;
    }

    public EventBus GetEventBus()
    {
        return _eventBus;
    }
}
