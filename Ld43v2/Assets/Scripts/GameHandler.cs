using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameHandler : MonoBehaviour {


    private int cycle = 0;
    public int Cycle
    {
        set
        {
            cycle = value;
            cycleIndicator.ChangeValue(cycle);
        }
        get
        {
            return cycle;
        }
    }
    private int money = 20;
    public int Money
    {
        set
        {
            money = value;
            moneyIndicator.ChangeValue(Money);
        }
        get
        {
            return money;
        }
    }
    private float fear = 5;
    public float Fear
    {
        set
        {
            fear = value;
            fearIndicator.ChangeValue(Fear);
        }
        get
        {
            return fear;
        }
    }
    private float population = 20;
    public float Population
    {
        set
        {
            population = value;
            populationIndicator.ChangeValue(population);
        }
        get
        {
            return population;
        }
    }
    private float god1 = 20;
    public float God1
    {
        set
        {
            god1 = value;
            god1Indicator.ChangeValue(God1);
        }
        get
        {
            return god1;
        }
    }
    private float god2 = 20;
    public float God2
    {
        set
        {
            god2 = value;
            god2Indicator.ChangeValue(God2);
        }
        get
        {
            return god2;
        }
    }
    private float god3 = 20;
    public float God3
    {
        set
        {
            god3 = value;
            god3Indicator.ChangeValue(God3);
        }
        get
        {
            return god3;
        }
    }


    private GameObject failPanel = null;

    public enum GameState { Start, Event, PlayerChoice, ApplyEventResults, ApplySacrifieceResults}
    public GameState gameState = GameState.PlayerChoice;


    [Header("References to the scene")]
    public TextMeshProUGUI cycleText;
    public TextMeshProUGUI populationText;
    public TextMeshProUGUI fearText;
    public GameObject eventPanelPrefab;
    public GameObject finishPanelPrefab;
    public GameObject currentEventPanel;
    public IndicatorHandler cycleIndicator;
    public IndicatorHandler moneyIndicator;
    public IndicatorHandler fearIndicator;
    public IndicatorHandler populationIndicator;
    public IndicatorHandler god1Indicator;
    public IndicatorHandler god2Indicator;
    public IndicatorHandler god3Indicator;


    [Header("AudioReferences")]
    public AudioClip moneyGain;



    // Use this for initialization
    void Start () {
        gameState = GameState.Start;
        DisplayAllIndicators();
	}


    private void DisplayAllIndicators()
    {
        IndicatorHandler[] allIndicators = FindObjectsOfType<IndicatorHandler>();
        foreach (var indicator in allIndicators)
        {

        }

        Money = Money;
        Cycle = Cycle;
        Population = Population;
        God1 = God1;
        God2 = God2;
        God3 = God3;
        Fear = Fear;

    }

    // Update is called once per frame
    void Update () {
        CheckGameState();        
    }



    public void CheckGameState ()
    {
        switch (gameState)
        {

            case GameState.Start:
                CheckFinState();
                //call event
                EventUI.Instance.StartEvent();
                gameState = GameState.Event;
                Cycle += 1;                
                return;
            case GameState.Event:
                // вызвать функцию ивента
                // деактивировать кнопки
            return;
            case GameState.ApplyEventResults:


                return;

            case GameState.PlayerChoice:
                //активировать кнопки
                return;

            case GameState.ApplySacrifieceResults:

                return;
        }
    }

    public void EventFinished ()
    {
        gameState = GameState.PlayerChoice;
    }

    public void PlayerChoiceMade ()
    {
        gameState = GameState.Start;
    }


    public void ReturnTest ()
    {
        Cycle += 1;
    }


    private void CheckFinState()
    {
        //fail state
        FailStates failedState = isFailed();

        if (failedState != FailStates.None)
        {
            gameState = GameState.Event;

            failPanel = Instantiate(finishPanelPrefab, this.transform);

            TextMeshProUGUI[] textMeshPros = failPanel.GetComponentsInChildren<TextMeshProUGUI>();
            
            foreach (TextMeshProUGUI txt in textMeshPros)
            {
                if (txt.gameObject.name.Contains("EventHeaderText"))
                {
                    txt.text = "You failed!";
                }
                else if (txt.gameObject.name.Contains("EventDescriptionText"))
                {
                    switch (failedState)
                    {
                        case FailStates.PopulationZero:
                            txt.text = "Only sculls and bones at the village. You stay alone with your greedy gods...";
                            break;

                        case FailStates.FearZero:
                            txt.text = "What?! Sacrifice to the gods? Are you kidding me? Go for a walk, old man!";
                            break;

                        case FailStates.FearMax:
                            txt.text = "It's terribly! We want to live. We must...we must burn he, before he sacrifice us! Go together!";
                            break;

                        case FailStates.GodDammed:
                            txt.text = "Wretched slave! How dare you insult us?! You will be buried under the ruins of this pitiful temple!!!";
                            break;
                    }
                }
            }

            Button restartButton = failPanel.GetComponentInChildren<Button>();

            restartButton.GetComponentInChildren<Text>().text = "Restart the game";
            restartButton.onClick.AddListener(RestartGame);
        }
    }
    
    
    /// <summary>
    /// проверка на фейл 
    /// </summary>
    /// <returns></returns>
    private FailStates isFailed()
    {
        if (Population <= 0)
        {
            return FailStates.PopulationZero;
        }
        else if (Fear <= 0)
        {            
            return FailStates.FearZero;
        }
        else if (Fear >= 100)
        {
            return FailStates.FearMax;
        }
        else if (God1 <= 0 || God2 <= 0 || God3 <= 0)
        {
            return FailStates.GodDammed;
        }

        return FailStates.None;        
    }

    private enum FailStates
    {
        None,
        PopulationZero,
        FearMax,
        FearZero,
        GodDammed
    }

    /// <summary>
    /// Проверка на вин
    /// </summary>    
    private bool isWin()
    {

        return false;
    }

    /// <summary>
    /// Перезапуск игры
    /// </summary>
    private void RestartGame()
    {
        Cycle = 0;
        Money = 20;
        Fear = 5;
        Population = 20;
        God1 = 20;
        God2 = 20;
        God3 = 20;

        if (failPanel != null)
        {
            Destroy(failPanel);
            failPanel = null;
        }

        gameState = GameState.Start;
    }

    
}
