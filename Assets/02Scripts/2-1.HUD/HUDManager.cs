using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Linq;

public class HUDManager : MonoBehaviour
{
    public static HUDManager instance = null;

    
    public TextMeshProUGUI GoldText;
    public GameObject[] BoomObj = new GameObject[3];
    public TextMeshProUGUI FloorText;      
    
    // 아이템
    public GameObject Boom;
    public TextMeshProUGUI BoomText;
    public GameObject Boom_inBattle;
    public Button UseBoom_Btn;
    public Button CancleBoom_Btn;

    // 텔런트 카드  
    public GameObject TellentCard;
    public GameObject BigCard;
    public TextMeshProUGUI TellentText;
    bool bOpenTell = false;

    // 인포
    bool bOpenInfo = false;
    public GameObject InfoUI;


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
        SetAll();
    }
    public void SetAll()
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
            BoomObj[boomIdx].GetComponentInChildren<Image>().color = new Color(1,1,1,(100.0f/255.0f)); 
            if(num != 0)
            {
                BoomObj[boomIdx].GetComponentInChildren<Image>().color = new Color(1,1,1,1);                
            }
            boomIdx++;
        }
    }

    public void SetFloor()
    {
        FloorText.text = GameManager.instance._CurMap.floor + "F";
    }


    public void ClickTellents()
    {

    }

    public void SetTellentTxt()
    {
        int num = GameManager.instance.Tellents[0].Count + GameManager.instance.Tellents[1].Count + GameManager.instance.Tellents[2].Count + GameManager.instance.Tellents[3].Count;

        TellentText.text = ""+num; 
    }




    int SelectIdx;
    int itemCode;
    public void Click_Boom(int i)
    {
        SelectIdx = i;
        itemCode = GameManager.instance.ItemList_num[i];
        if(itemCode == 0)
            return;
        Boom.SetActive(true);        

        ItemClass Item = new ItemClass(itemCode);
        if(SceneManager.GetActiveScene().name == "2-0.BattleScene" || SceneManager.GetActiveScene().name == "4-0.BossScene")
        {   
            BoomText.text = "["+Item.ItemName +"]";

            Boom_inBattle.SetActive(true);           
            UseBoom_Btn.onClick.RemoveAllListeners();
            UseBoom_Btn.onClick.AddListener(Click_UseBoon);
            CancleBoom_Btn.onClick.AddListener(Click_Cancle);
        }
        else
        {
            Boom_inBattle.SetActive(false);
            BoomText.text = "["+Item.ItemName +"]"+"\n 비 전투중...";
            Invoke("DeleteBoomText",2.0f);
        }
    }
    void DeleteBoomText()
    {
        Boom.SetActive(false);        
    }

    public List<Save.Player> players;
    public List<Save.Enemy> Enemys;
    public void Click_UseBoon()
    {
        Debug.Log("Click_UseBoon");
        ItemClass Item = new ItemClass(itemCode);
        Item.UseItem(players,0,Enemys,0);
        GameManager.instance.ItemList_num[SelectIdx] = 0;
        SetBoom();
        Click_Cancle();
    }
    public void Click_Cancle()
    {
        DeleteBoomText();
    }

    

    public void OpenTellentUI()
    {
        InfoUI.SetActive(false);
        bOpenInfo = false;
        BigCard.SetActive(false);

        if(bOpenTell == false)
        {
            TellentCard.SetActive(true);
            bOpenTell = true;
        }
        else
        {
            TellentCard.SetActive(false);
            bOpenTell = false;
        }        
    }

    public void OpenInfoUI()
    {
        TellentCard.SetActive(false);
        bOpenTell = false;

        if(bOpenInfo == false)
        {
            InfoUI.SetActive(true);
            bOpenInfo = true;
        }
        else
        {
            InfoUI.SetActive(false);
            bOpenInfo = false;
        }        
    }

}
