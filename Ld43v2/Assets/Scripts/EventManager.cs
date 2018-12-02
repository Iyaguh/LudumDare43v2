using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Менджер эвентов
/// </summary>
public class EventManager : Singleton<EventManager> {
    
    //доступные эвенты
    private Event[] availibleEvents;

    //активные эвенты не доступные из-за требований
    private List<Event> activeEventPool;

    //неактивные эвенты
    private List<Event> disactivedEventPool;

    protected override void Awake()
    {
        base.Awake();

        Event[] allEvents = Resources.LoadAll<Event>("Events");

        if (allEvents.Length == 0)
        {
            Debug.LogError("[Event Manager] Can't found any events.");
            return;
        }

        for (int i = 0; i < allEvents.Length; i++)
        {
            if (allEvents[i].isActive)
            {
                activeEventPool.Add(allEvents[i]);
            }
            else
            {
                disactivedEventPool.Add(allEvents[i]);
            }
        }
    }    
}
