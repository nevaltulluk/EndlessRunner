using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Container : MonoBehaviour
{
    public static Container Instance = null;
    
    public EventBus EventBus;
    public GameStateService GameStateService;
    public DataService DataService;
    
    void Awake()
    {
        if (Instance != null && Instance != this) 
        {
            Destroy(gameObject);
        }
 
        Instance = this;
        
        EventBus = new EventBus();
        GameStateService = new GameStateService();
        DataService = new DataService();
    }
}
