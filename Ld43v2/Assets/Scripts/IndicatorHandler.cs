using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class IndicatorHandler : MonoBehaviour, IPointerEnterHandler
{

    [SerializeField]
    TextMeshProUGUI numericalValueText;


    public Slider targetSlider;
    public AudioClip thisIndicatorAudioClip;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ChangeValue (float indicatorValue)
    {
        if (numericalValueText!=null)
        {
            numericalValueText.text = indicatorValue.ToString();
            //play sound
            //play animation
        }


        if (targetSlider != null)
        {
            indicatorValue = Mathf.Clamp(indicatorValue, 0f, 100f);
            targetSlider.value = indicatorValue;
        }
    }

    private void OnMouseOver()
    {
        Debug.Log("mouse is over element");
        print("hi");
    }

    public void ChangeValue(int indicatorValue)
    {
        if (numericalValueText != null)
        {
            numericalValueText.text = indicatorValue.ToString();
            //play sound
            //play animation
        }

    }

    public void SetSlider(float targetValue)
    {

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("entered");
    }
}
