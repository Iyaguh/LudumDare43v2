using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TempleIndicators : MonoBehaviour {

    public Button smallSacrifiece;
    public int realtionshipWithGodChange;
    public EventManager eventManager;

    public EventResult sacrifieceResult;

    public GameObject arrowButton1;
    public GameObject arrowButton2;
    public GameObject arrowButton3;

    public SacrifieceButton sacrifieceButton1;
    public SacrifieceButton sacrifieceButton2;
    public SacrifieceButton sacrifieceButton3;





    // Use this for initialization
    void Start () {
        sacrifieceResult = new EventResult();		
	}



	
	// Update is called once per frame
	void Update () {
		
	}

    public void CancelSelectionSacrifieceButtons()
    {
        sacrifieceButton1.isChosen = false;
        sacrifieceButton2.isChosen = false;
        sacrifieceButton3.isChosen = false;
    }


    public void ActivateArrowButtons(bool activate)
    {
        arrowButton1.SetActive(activate);
        arrowButton2.SetActive(activate);
        arrowButton3.SetActive(activate);
    }

    public void Click1()
    {
        sacrifieceResult.god1Change = realtionshipWithGodChange;
        eventManager.DoEventResult(sacrifieceResult);
        ActivateArrowButtons(false);
        CancelSelectionSacrifieceButtons();

    }

    public void Click2()
    {
        sacrifieceResult.god2Change = realtionshipWithGodChange;
        eventManager.DoEventResult(sacrifieceResult);
        ActivateArrowButtons(false);
        CancelSelectionSacrifieceButtons();
    }

    public void Click3()
    {
        sacrifieceResult.god3Change = realtionshipWithGodChange;
        eventManager.DoEventResult(sacrifieceResult);
        ActivateArrowButtons(false);
        CancelSelectionSacrifieceButtons();
    }



}
