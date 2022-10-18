using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class SkillComment : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject CommentsParents;
    public GameObject Comments;

    public BaseSkill skill = new Healer_Skill(0);

    private void Start() {
    }

    // Start is called before the first frame update
    public void OnPointerEnter(PointerEventData eventData)
    {
        // Debug.Log("OnPointerEnter");
        var comment = Instantiate(Comments);
        comment.transform.SetParent(CommentsParents.transform);  
        comment.transform.position = this.transform.position + new Vector3(110,80,0);    

        string multitarget = (skill.SKill_Data.MultiTarget == true) ? "MultiTarget" : "SingleTarget";
        comment.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = skill.SKill_Data.Name + "\n" + "[ " + multitarget + " ]" + "\n" + skill.SKill_Data.Contents;  
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Debug.Log("OnPointerExit");
        Destroy(CommentsParents.transform.GetChild(0).gameObject);
    }
}
