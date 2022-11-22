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
    public GameObject[] Booms;
    public TextMeshProUGUI BoomText;
    public GameObject Boom_inBattle;
    public GameObject Boom_outBattle;
    public bool useItem;

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

    // Dead
    public GameObject DeadUI;

    // Setting
    public GameObject SettingUI;


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
        UIActiveFalse();
    }
    public void UIActiveFalse()
    {
        InfoUI.SetActive(false);
        TellentCard.SetActive(false);
        BigCard.SetActive(false);
        DeadUI.SetActive(false);
        SettingUI.SetActive(false);
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
        GoldText.text = "" + GameManager.instance.curGold;        
    }

    public void SetBoom()
    {
        int boomIdx = 0;
        foreach (var num in GameManager.instance.ItemList_num)
        {
            ItemData item = SOManager.GetItem().itemDatas[num];

            BoomObj[boomIdx].GetComponentInChildren<Image>().color = new Color(1,1,1,(100.0f/255.0f)); 
            BoomObj[boomIdx].GetComponent<Image>().sprite = item.ItemImage;
            // BoomObj[boomIdx].GetComponent<RectTransform>().localScale = Vector3.one;    
            if(num != 0)
            {
                BoomObj[boomIdx].GetComponentInChildren<Image>().color = new Color(1,1,1,1);       
                // BoomObj[boomIdx].GetComponent<RectTransform>().localScale = Vector3.one * 0.7f;         
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
        {
            return; 
        }
        else
        {

            if (SceneManager.GetActiveScene().name == "2-0.BattleScene" || SceneManager.GetActiveScene().name == "4-0.BossScene")
            {
                if (useItem == true)
                {

                    Boom_inBattle.transform.position = Booms[i].transform.position;

                    ItemClass Item = new ItemClass(itemCode);
                    Boom_inBattle.SetActive(true);
                    Boom_outBattle.SetActive(false);

                    BoomText.text = "[" + Item.ItemName + "]";

                    UseBoom_Btn.onClick.RemoveAllListeners();
                    UseBoom_Btn.onClick.AddListener(() => Click_UseBoon(Item));
                    CancleBoom_Btn.onClick.AddListener(Click_Cancle);   
                }
            }
            else
            {                
                Boom_outBattle.transform.position = Booms[i].transform.position;
                Boom_inBattle.SetActive(false);
                Boom_outBattle.SetActive(true);
                Invoke("DeleteBoomText", 1.5f);
            }
        }
                 

        
    }
    void DeleteBoomText()
    {
        Boom_outBattle.SetActive(false);        
        Boom_inBattle.SetActive(false); 
    }

    public List<Player> players;
    public List<Enemy> Enemys;
    public void Click_UseBoon(ItemClass Item)
    {
        Debug.Log("Click_UseBoon");
        Item.UseItem(players,0,Enemys,0);

        GameManager.instance.ItemList_num[SelectIdx] = 0;
        SetBoom();
        Click_Cancle();

        GameManager.instance.GameScoreData.Used_Itme++;
    }
    public void Click_Cancle()
    {
        DeleteBoomText();
    }

    

    public void OpenTellentUI()
    {
        UIActiveFalse();
        bOpenInfo = false;

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
        UIActiveFalse();
        bOpenTell = false;

        if(bOpenInfo == false)
        {
            GetComponent<Map_Info>().SetStartSetting();
            InfoUI.SetActive(true);
            //InfoUI.GetComponent<Map_Info>().SetCharImage();
            bOpenInfo = true;            
        }
        else
        {
            InfoUI.SetActive(false);
            bOpenInfo = false;
        }        
    }

    public void Dead()
    {
        UIActiveFalse();
        DeadUI.SetActive(true);
    }

    public void OpenSetting()
    {
        UIActiveFalse();
        SettingUI.SetActive(true);
        //GetComponent<SettingManager>().OpenSetting();
    }


    public void GotoMainBtn()
    {
        DeadUI.SetActive(false);        
        SceneManager.LoadScene("1-0.StartScene");
        Destroy(GameManager.instance.gameObject);
        Destroy(this.gameObject);        
    }
    public void DestroyHUD()
    {
        
    }

    public void QuitBtn()
    {        
        SceneManager.LoadScene(12);
    }

    
}
