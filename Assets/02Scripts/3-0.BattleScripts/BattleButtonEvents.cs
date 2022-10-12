using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

public class BattleButtonEvents : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Button[] otherButton;
    public bool isMultiTarget;

    void Change_ButtonColor(Button _button, Color _color)
    {
        ColorBlock colorBlock = _button.colors;
        colorBlock.normalColor = _color;
        _button.colors = colorBlock;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(isMultiTarget == true)
        { 
            Change_ButtonColor(otherButton[0], Color.red);
            Change_ButtonColor(otherButton[1], Color.red);
        }        
        // Debug.Log("<color=red>Event:</color> Completed Enter.");
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        Change_ButtonColor(otherButton[0], new Color(0,0,0,0));
        Change_ButtonColor(otherButton[1], new Color(0,0,0,0));

        // Debug.Log("<color=red>Event:</color> Completed Enter.");
    }


}
