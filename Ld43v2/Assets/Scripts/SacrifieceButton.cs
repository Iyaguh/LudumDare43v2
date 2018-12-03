using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SacrifieceButton : MonoBehaviour {

    public bool isChosen = false;
    public TempleIndicators templeIndicator;
    public int moneyChange;
    public int relationshipWithGodChange;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}



    private void OnMouseOver()
    {
        Debug.Log("mouse is over element");
    }

    public void Clicked ()
    {
        templeIndicator.realtionshipWithGodChange = relationshipWithGodChange;
        templeIndicator.sacrifieceResult.moneyChange = moneyChange;
        templeIndicator.ActivateArrowButtons(true);
    }
}
