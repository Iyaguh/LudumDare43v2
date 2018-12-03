using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class ToolTipShower : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    /// <summary>
    /// Задержка перед показом tooltip
    /// </summary>
    public int delay = 0;

    /// <summary>
    /// Текст tooltip
    /// </summary>
    public string tooltipText = "";

    public GameObject tooltipPref;

    private GameObject _curWindow = null;

    private bool _isMouseOver = false;
    private bool _isShowing = false;
    
    private float _timer = 0;

    public void OnPointerEnter(PointerEventData eventData)
    {
        _isMouseOver = true;
        _timer = delay;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _isMouseOver = false;
        HideToolTip();
    }    

    void Update()
    {
        if (_isMouseOver)
        {
            if (_timer > 0f)
            {
                _timer -= Time.deltaTime;

                if (_timer <= 0f)
                    ShowToolTip();
            }            
        }

        if (_isShowing)
        {            
            Tuning();           
        }
        else if (_curWindow != null)
        {
            Destroy(_curWindow);
            _curWindow = null;
        }
    }

    /// <summary>
    /// Показать tooltip
    /// </summary>
    private void ShowToolTip()
    {
        _curWindow = Instantiate(tooltipPref, this.transform);
        TextMeshProUGUI textMeshProUGUI = _curWindow.GetComponentInChildren<TextMeshProUGUI>();

        textMeshProUGUI.text = tooltipText;
        textMeshProUGUI.alignment = TextAlignmentOptions.Top;

        RectTransform rect = _curWindow.GetComponent<RectTransform>();

        _curWindow.transform.position = new Vector3(Input.mousePosition.x + rect.rect.width/2, Input.mousePosition.y + rect.rect.height/2);

        _isShowing = true;
    }

    /// <summary>
    /// Убрать tooltip
    /// </summary>
    private void HideToolTip()
    {
        if (_isShowing)
        {            
            _isShowing = false;
        }
    }

    /// <summary>
    /// Подстройка под мышь
    /// </summary>
    private void Tuning()
    {
        RectTransform rect = _curWindow.GetComponent<RectTransform>();

        _curWindow.transform.position = new Vector3(Input.mousePosition.x + rect.rect.width / 2, Input.mousePosition.y + rect.rect.height / 2);
    }
}
