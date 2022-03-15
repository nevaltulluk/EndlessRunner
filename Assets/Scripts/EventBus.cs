
using System;
using System.Collections.Generic;
using UnityEngine;

public class EventBus
{
    private Dictionary<Type, Action> eventList = new Dictionary<Type, Action>();

    public void Subscribe<T>(Action action) where T : Event
    {
        Type type = typeof(T);
        if (eventList.ContainsKey(type))
        {
            eventList[type] += action;
        }
        else
        {
            eventList.Add(type,action);
        }
    }

    public void Fire<T>(T args) where T : Event
    {
        Type type = typeof(T);
        if (eventList.ContainsKey(type))
        {
            eventList[type]?.Invoke();
        }
        else
        {
            Debug.LogWarning("No subs");
        }
    }
}

public class Event
{
    
}
