using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class HUDComments : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject CommentsParents;
    public GameObject Comments;

    //public string commentstring = "";


    // Start is called before the first frame update
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("OnPointerEnter");
        var comment = Instantiate(Comments);
        comment.transform.SetParent(CommentsParents.transform);  
        //comment.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = commentstring;  
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("OnPointerExit");
        Destroy(CommentsParents.transform.GetChild(0).gameObject);
    }
}
