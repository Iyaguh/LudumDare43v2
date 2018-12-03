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


    public enum GameState { Start, Event, PlayerChoice, ApplyEventResults, ApplySacrifieceResults}
    public GameState gameState = GameState.PlayerChoice;


    [Header("References to the scene")]
    public TextMeshProUGUI cycleText;
    public TextMeshProUGUI populationText;
    public TextMeshProUGUI fearText;
    public GameObject eventPanelPrefab;
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

}
