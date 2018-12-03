using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Менджер эвентов
/// </summary>
public class EventManager : Singleton<EventManager> {

    public GameHandler gameHandler;

    //Все события
    private Event [] allEvents;

    //
    private List<Event> poolOfEvents;

//    private List<EventCooling> eventsInCooling;

    protected override void Awake()
    {
        base.Awake();

        allEvents = Resources.LoadAll<Event>("Events");

        if (allEvents.Length == 0)
        {
            Debug.LogError("[Event Manager] Can't found any events.");
            return;
        }

        poolOfEvents = new List<Event>();
    }



    /// <summary>
    /// Получить очередной эвент
    /// </summary>    
    public Event GetEvent()
    {

        for (int i = 0; i < allEvents.Length; i++)
        {
            if (IsCoolDownOver(allEvents[i]))
            {
                allEvents[i].isActive = true;
                allEvents[i].isOnCoolDown = false;
            }              
        }


        for (int i = 0; i < allEvents.Length; i++)
        {
            if (allEvents[i].isActive)
            {
                poolOfEvents.Add(allEvents[i]);
            }
        }

        Event eventToReturn;

        eventToReturn = poolOfEvents[Random.Range(0, poolOfEvents.Count)];

        if (!eventToReturn.isRepeat)
        {
            eventToReturn.isActive = false;
        }
        else if (eventToReturn.isRepeat & eventToReturn.coolDown !=0)
        {
            eventToReturn.isActive = false;
            eventToReturn.isOnCoolDown = true;
            eventToReturn.cycleWhenCoolDownStarted = gameHandler.Cycle;
        }

        poolOfEvents.Clear();

        return eventToReturn;
       
    }

    public bool IsCoolDownOver (Event targetEvent)
    {
        if (gameHandler.Cycle - targetEvent.cycleWhenCoolDownStarted >= targetEvent.coolDown)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Применяем выбранный результат
    /// </summary>
    /// <param name="targetEvent">Эвент, чей результат</param>
    /// <param name="eventResult">Сам результат</param>
    public void DoEventResult(Event targetEvent, EventResult eventResult)
    {
        //мгновенный результат
        gameHandler.Money += eventResult.moneyChange;
        gameHandler.Population += eventResult.populationChange;
        gameHandler.Fear += eventResult.fearChange;
        gameHandler.God1 += eventResult.god1Change;
        gameHandler.God2 += eventResult.god2Change;
        gameHandler.God3 += eventResult.god3Change;

        EventUI.Instance.FinishEvent();
    }

    /// <summary>
    /// Проверяет эвент на доступность
    /// </summary>
    /// <param name="targetEvent">Эвент для проверки</param>    
    private bool AreRequirementsForEventMet(Event targetEvent)
    {
        EventRequarments eventRequirements = targetEvent.eventRequarments;

        if (!targetEvent.isActive)
        {
            return false;
        }
        else if (gameHandler.Money < eventRequirements.moneyMin || eventRequirements.moneyMax > 0 ? gameHandler.Money > eventRequirements.moneyMax : false)
        {
            return false;
        }
        else if (gameHandler.Population < eventRequirements.populationMin || eventRequirements.populationMax > 0 ? gameHandler.Population > eventRequirements.populationMax : false)
        {
            return false;
        }
        else if (gameHandler.Fear < eventRequirements.fearMin || eventRequirements.fearMax > 0 ? gameHandler.Fear > eventRequirements.fearMax : false)
        {
            return false;
        }
        else if (gameHandler.God1 < eventRequirements.god1Min || eventRequirements.god1Max > 0 ? gameHandler.God1 > eventRequirements.god1Max : false)
        {
            return false;
        }
        else if (gameHandler.God2 < eventRequirements.god2Min || eventRequirements.god2Max > 0 ? gameHandler.God2 > eventRequirements.god2Max : false)
        {
            return false;
        }
        else if (gameHandler.God3 < eventRequirements.god3Min || eventRequirements.god3Max > 0 ? gameHandler.God3 > eventRequirements.god3Max : false)
        {
            return false;
        }        
        return true;
    }

    private void Update()
    {

    }
}
