using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;


public enum HudEnum {아이템, 현재층, 스킬카드, 파티원, 설정};
 
public class HudComment : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public GameObject CommentsParents;
    public GameObject Comments;
    public HudEnum hudEnum;


    private void Start() {
    }

    // Start is called before the first frame update
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("OnPointerEnter");
        var comment = Instantiate(Comments);
        comment.transform.SetParent(CommentsParents.transform);  
        comment.transform.position = this.transform.position;    
        comment.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = ""+hudEnum;  
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("OnPointerExit");
        Destroy(CommentsParents.transform.GetChild(0).gameObject);
    }
}
