using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Менджер эвентов
/// </summary>
public class EventManager : Singleton<EventManager> {

    public GameHandler gameHandler;

    //доступные эвенты
    private List<Event> availibleEvents;

    //эвенты не доступные из-за требований
    private List<Event> notAvailableEvents;

    private List<EventCooling> eventsInCooling;

    protected override void Awake()
    {
        base.Awake();

        Event[] allEvents = Resources.LoadAll<Event>("Events");

        if (allEvents.Length == 0)
        {
            Debug.LogError("[Event Manager] Can't found any events.");
            return;
        }

        notAvailableEvents = new List<Event>();
        availibleEvents = new List<Event>();
        eventsInCooling = new List<EventCooling>();

        for (int i = 0; i < allEvents.Length; i++)
        {
            notAvailableEvents.Add(allEvents[i]);
        }

        // создаем список доступных ивентов
        for (int i = 0; i < allEvents.Length; i++)
        {
            if (allEvents[i].isActive)
            {
                availibleEvents.Add(allEvents[i]);
            }
        }

        

    }

    /// <summary>
    /// Получить очередной эвент
    /// </summary>    
    public Event GetEvent()
    {
        //актуализация информации о доступных эвентах
        EventAccessUpdate();

        if (availibleEvents.Count == 0)
        {
            Debug.LogWarning("[Event Manager] Can't get any events");
            return null;
        }

        int currEventIdx = Random.Range(0, availibleEvents.Count - 1);

        eventsInCooling.Add(new EventCooling(availibleEvents[currEventIdx], gameHandler.Cycle));
        Event retEvent = availibleEvents[currEventIdx];
        availibleEvents.RemoveAt(currEventIdx);
        return retEvent;

    }

    /// <summary>
    /// Установка эвента в состояние активности/неактивности
    /// </summary>
    /// <param name="targetEvent">Целевой эвент</param>
    /// <param name="isActivate">Сделать активным</param>
    public void SetEventActive(Event targetEvent, bool isActivate)
    {
        if (notAvailableEvents.Contains(targetEvent))
        {
            int idx = notAvailableEvents.IndexOf(targetEvent);

            notAvailableEvents[idx].isActive = isActivate;
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

        //статус
        //TO_DO

        //активируем/деактивируем эвенты
        if (!targetEvent.isRepeat)
        {
            for (int i=0; i<eventsInCooling.Count; i++)
            {
                if (eventsInCooling[i].ItEvent == targetEvent)
                {
                    eventsInCooling.RemoveAt(i);
                    break;
                }
            }

            notAvailableEvents.Add(targetEvent);

            SetEventActive(targetEvent, false);            
        }

        for (int i=0; i < targetEvent.eventToActivate.Length; i++)
        {
            SetEventActive(targetEvent.eventToActivate[i], true);
        }

        for (int i=0; i< eventResult.eventToActivate.Length; i++)
        {
            SetEventActive(eventResult.eventToActivate[i], true);
        }

        //сообщаем EventUI что мы все
        EventUI.Instance.FinishEvent();
    }

    /// <summary>
    /// Актуализация доступности евентов
    /// </summary>
    private void EventAccessUpdate()
    {
        List<Event> eventToDisactivate = new List<Event>();

        // удаляется ивент, который не соответствует требованиям из доступных ивентов.
        for (int i=0; i < availibleEvents.Count; i++)
        {
            if (!IsCorrectEvent(availibleEvents[i]))
            {
                eventToDisactivate.Add(availibleEvents[i]);                               
                availibleEvents.RemoveAt(i);
                i--;
            }
        }

        // возвращает недоступные ивенты в список доступных

        for (int i = 0; i < notAvailableEvents.Count; i++)
        {
            if (!IsCorrectEvent(notAvailableEvents[i]))
            {
                availibleEvents.Add(notAvailableEvents[i]);
                notAvailableEvents.RemoveAt(i);
                i--;
            }
        }

        notAvailableEvents.AddRange(eventToDisactivate);



        for (int i = 0; i < eventsInCooling.Count; i++)
        {
            if (eventsInCooling[i].isCoolDown(gameHandler.Cycle))
            {
                if (IsCorrectEvent(eventsInCooling[i].ItEvent))
                {
                    availibleEvents.Add(eventsInCooling[i].ItEvent);
                }
                else
                {
                    notAvailableEvents.Add(eventsInCooling[i].ItEvent);
                }

                eventsInCooling.RemoveAt(i);
                i--;
            }
        }
    }


    /// <summary>
    /// Проверяет эвент на доступность
    /// </summary>
    /// <param name="targetEvent">Эвент для проверки</param>    
    private bool IsCorrectEvent(Event targetEvent)
    {
        EventRequarments eventRequarments = targetEvent.eventRequarments;

        if (!targetEvent.isActive)
        {
            return false;
        }
        else if (gameHandler.Money < eventRequarments.moneyMin && eventRequarments.moneyMax > 0 ? gameHandler.Money > eventRequarments.moneyMax : true)
        {
            return false;
        }
        else if (gameHandler.Population < eventRequarments.populationMin && eventRequarments.populationMax > 0 ? gameHandler.Population > eventRequarments.populationMax : true)
        {
            return false;
        }
        else if (gameHandler.Fear < eventRequarments.fearMin && eventRequarments.fearMax > 0 ? gameHandler.Fear > eventRequarments.fearMax : true)
        {
            return false;
        }
        else if (gameHandler.God1 < eventRequarments.god1Min && eventRequarments.god1Max > 0 ? gameHandler.God1 > eventRequarments.god1Max : true)
        {
            return false;
        }
        else if (gameHandler.God2 < eventRequarments.god2Min && eventRequarments.god2Max > 0 ? gameHandler.God2 > eventRequarments.god2Max : true)
        {
            return false;
        }
        else if (gameHandler.God3 < eventRequarments.god3Min && eventRequarments.god3Max > 0 ? gameHandler.God3 > eventRequarments.god3Max : true)
        {
            return false;
        }
        

        return true;
    }

    private void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            for (int i = 0; i < availibleEvents.Count; i++)
            {
                Debug.Log(availibleEvents[i].name + " имя доступного ивента");
            }
        }
    }


    private class EventCooling
    {
        public Event ItEvent { get { return _currentEvent; } }
        private Event _currentEvent;
        private int _cycleAtStart;

        public EventCooling(Event currentEvent, int currentCycle)
        {
            _currentEvent = currentEvent;
            _cycleAtStart = currentCycle;
        }

        public bool isCoolDown(int currentCycle)
        {
            return (currentCycle - _cycleAtStart >= _currentEvent.coolDown);
        }
    }
}
