using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    public static HUDManager instance = null;

    
    public TextMeshProUGUI GoldText;
    public GameObject[] BoomObj = new GameObject[3];
    public TextMeshProUGUI FloorText;    
    public Button TellentButton;
    public TextMeshProUGUI TellentText;



    private void Awake() {

        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject); 
            this.gameObject.SetActive(true);
        }
        else if(instance != this)
        {
            //instance = null;
            Destroy(this.gameObject);
        }       
    }
    // Start is called before the first frame update
    void Start()
    {
        SetGold();
        SetBoom();
        SetFloor();
        SetTellentTxt();
    }

    public void SetGold()
    {
        GoldText.text = "Gold : " + GameManager.instance.curGold;        
    }

    public void SetBoom()
    {
        int boomIdx = 0;
        foreach (var num in GameManager.instance.ItemList_num)
        {
            BoomObj[boomIdx].GetComponentInChildren<Image>().color = new Color(1,1,1,1);
            boomIdx++;
        }
    }

    public void SetFloor()
    {
        FloorText.text = GameManager.instance._CurMap.floor + "F";
    }

    public void SetTellentTxt()
    {
        int num = GameManager.instance.BBTellent_num.Count + GameManager.instance.BeforTellents_num.Count + GameManager.instance.GetAfterTellents.Count + GameManager.instance.ABTelent_num.Count;
        
        TellentText.text = ""+num;
    }
}
