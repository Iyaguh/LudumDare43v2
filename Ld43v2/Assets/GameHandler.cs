using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameHandler : MonoBehaviour {


    public int cycle = 0;
    public int money = 20;
    public float fear = 5;
    public float population = 20;
    public float god1 = 20;
    public float god2 = 20;
    public float god3 = 20;

    private int test = 1;
    public int Test
    {
        get
        {
            return test;
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
