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

    private Button[] _chooses;

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

        _chooses = _eventPanel.GetComponentsInChildren<Button>();

        foreach (Button button in _chooses)
        {            
            //button.gameObject.SetActive(false);
        }

        //при начале эвента
        //_chooses[0].onClick.AddListener(Choose_1);

        _chooses[1].onClick.AddListener(Choose_2);
        _chooses[2].onClick.AddListener(Choose_3);

        _eventPanel.SetActive(false);

    }

    /// <summary>
    /// Генерация эвента
    /// </summary>
    public void StartEvent()
    {
        _chooses[0].onClick.AddListener(Choose_1);

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
            if (i >= _chooses.Length)
                break;

            _chooses[i].gameObject.SetActive(true);
            Text txt = _chooses[i].gameObject.GetComponentInChildren<Text>();
            txt.text = _currentEvent.eventVariants[i].text;           
        }

        _eventPanel.SetActive(true);
    }

    // Обработка выборов
    void Choose_1()
    {
        _currentResult = _currentEvent.eventVariants[0].eventResult;
        EventManager.Instance.DoEventResult(_currentEvent , _currentResult);        
    }

    void Choose_2()
    {
        _currentResult = _currentEvent.eventVariants[1].eventResult;
        EventManager.Instance.DoEventResult(_currentEvent, _currentResult);        
    }

    void Choose_3()
    {
        _currentResult = _currentEvent.eventVariants[2].eventResult;
        EventManager.Instance.DoEventResult(_currentEvent, _currentResult);        
    }

    /// <summary>
    /// Окончание эвента
    /// </summary>
    public void FinishEvent()
    {
        foreach (Button button in _chooses)
        {
            button.gameObject.SetActive(false);
        }

        Text txt = _chooses[0].gameObject.GetComponentInChildren<Text>();

        txt.text = "Продолжить";

        _eventDescription.text += _currentResult.resultText;

        _chooses[0].onClick.RemoveListener(Choose_1);
        _chooses[0].onClick.AddListener(ExitEventDisplay);

    }

    public void ExitEventDisplay()
    {
        _chooses[0].onClick.RemoveListener(ExitEventDisplay);

        foreach (Button button in _chooses)
        {
            button.gameObject.SetActive(false);
        }

        _eventPanel.SetActive(false);

        gameHandler.EventFinished();
    }
}
