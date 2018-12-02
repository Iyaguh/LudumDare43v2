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
        }
        get
        {
            return god3;
        }
    }



    [Header("References to the scene")]
    public TextMeshProUGUI cycleText;




	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ReturnTest ()
    {
        cycleText.text = Test.ToString();
    }

}
