using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Info_Character : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    public GameObject Gam;
    Map_Info map_Info;
    public int num;

    void Start() 
    {
        map_Info = Gam.GetComponent<Map_Info>();
    } 

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            map_Info.SetSkillBtn(num);
            map_Info.SetStatus_Text(num);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Debug.Log("BeginDrag!!");
        GetComponent<Image>().raycastTarget = false;

        map_Info.Target_0 = num;
        map_Info.firstCharacter = this.gameObject;
        map_Info.firstLoc = transform.position;        
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Debug.Log("Drag!!");
        
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Debug.Log("EndDrag!!");
        if (map_Info.SecondCharacter == null)
        {
            transform.position = map_Info.firstLoc;            
        }
        GetComponent<Image>().raycastTarget = true;
        map_Info.firstCharacter = null;
        map_Info.SecondCharacter = null;
            
    }

    public void OnDrop(PointerEventData eventData)
    {
        // Debug.Log("Drop!!");
        map_Info.SecondCharacter = this.gameObject;
        map_Info.secondLoc = transform.position;

        map_Info.Target_1 = num;
        map_Info.ChangeCharacterLocation();
    }

}
