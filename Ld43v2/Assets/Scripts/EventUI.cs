using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EventUI : Singleton<EventUI>
{
    public GameObject eventPanelPrefab;
    public GameHandler gameHandler;

    private GameObject _eventPanel;

    private TextMeshProUGUI _eventHeader;

    private TextMeshProUGUI _eventDescription;

    private Button[] _choices;

    private Event _currentEvent;
    private EventResult _currentResult;

    override protected void Awake()
    {
        base.Awake();

        _eventPanel = Instantiate(eventPanelPrefab, this.transform);


        //получаем реф на элементы панели
        TextMeshProUGUI[] texts = _eventPanel.GetComponentsInChildren<TextMeshProUGUI>();

        foreach (TextMeshProUGUI txt in texts)
        {
            if (txt.gameObject.name.Contains("EventHeaderText"))
            {
                _eventHeader = txt;
            }
            else if (txt.gameObject.name.Contains("EventDescriptionText"))
            {
                _eventDescription = txt;
            }
        }

        _choices = _eventPanel.GetComponentsInChildren<Button>();

        foreach (Button button in _choices)
        {            
            button.gameObject.SetActive(false);
        }

        //при начале эвента
        //_chooses[0].onClick.AddListener(Choose_1);

        _choices[1].onClick.AddListener(Choice_2);
        _choices[2].onClick.AddListener(Choice_3);

        _eventPanel.SetActive(false);

    }

    /// <summary>
    /// Генерация эвента
    /// </summary>
    public void StartEvent()
    {
        _choices[0].onClick.AddListener(Choice_1);

        _currentEvent = EventManager.Instance.GetEvent();

        if (_currentEvent == null)
        {
            gameHandler.EventFinished();
            return;
        }

        _eventHeader.text = _currentEvent.Name;
        _eventDescription.text = _currentEvent.Description;

        for (int i = 0; i < _currentEvent.eventVariants.Length; i++)
        {
            if (i >= _choices.Length)
                break;

            _choices[i].gameObject.SetActive(true);
            Text txt = _choices[i].gameObject.GetComponentInChildren<Text>();
            txt.text = _currentEvent.eventVariants[i].text;           
        }

        _eventPanel.SetActive(true);
    }

    // Обработка выборов
    void Choice_1()
    {
        _currentResult = _currentEvent.eventVariants[0].eventResult;
        EventManager.Instance.DoEventResult(_currentEvent , _currentResult);        
    }

    void Choice_2()
    {
        _currentResult = _currentEvent.eventVariants[1].eventResult;
        EventManager.Instance.DoEventResult(_currentEvent, _currentResult);        
    }

    void Choice_3()
    {
        _currentResult = _currentEvent.eventVariants[2].eventResult;
        EventManager.Instance.DoEventResult(_currentEvent, _currentResult);        
    }

    /// <summary>
    /// Окончание эвента
    /// </summary>
    public void FinishEvent()
    {
        foreach (Button button in _choices)
        {
            button.gameObject.SetActive(false);
        }

        Text txt = _choices[0].gameObject.GetComponentInChildren<Text>();

        txt.text = "Продолжить";

        _choices[0].gameObject.SetActive(true);

        _eventDescription.text += _currentResult.resultText;

        _choices[0].onClick.RemoveListener(Choice_1);
        _choices[0].onClick.AddListener(ExitEventDisplay);

    }

    public void ExitEventDisplay()
    {
        _choices[0].onClick.RemoveListener(ExitEventDisplay);

        foreach (Button button in _choices)
        {
            button.gameObject.SetActive(false);
        }

        _eventPanel.SetActive(false);

        gameHandler.EventFinished();
    }
}
