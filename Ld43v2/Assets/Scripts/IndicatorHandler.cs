using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IndicatorHandler : MonoBehaviour {

    [SerializeField]
    TextMeshProUGUI numericalValueText;
    public AudioClip thisIndicatorAudioClip;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ChangeValue (float indicatorChange)
    {
        numericalValueText.text = indicatorChange.ToString();
        //play sound
        //play animation
    }

    private void OnMouseOver()
    {
        Debug.Log("mouse is over element");
    }

    public void ChangeValue (int indicatorChange)
    {
        numericalValueText.text = indicatorChange.ToString();
    }
}
