using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class ItemBoxManager1 : MonoBehaviour
{
    public TextMeshProUGUI Chatting;
    public GameObject NextButton;
    
    public GameObject SelectPannel;
    public Button SelectBtn_0;
    public Button SelectBtn_1;

    public GameObject GetItemPannel;
    public GameObject spwItem;
    public GameObject spwLoc;

    public GameObject TrapPannel;

    public Dictionary<int, string> Chat_Dic = new Dictionary<int,string>();
    public int ChatNum;

    bool bitem = false;

    void Start() 
    {
        SetNewChar();
        SaveChat();
        SelectPannel.SetActive(false);

        NextBtn();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetNewChar()
    {       
        int idx = UnityEngine.Random.Range(1, 4);
        for (int i = 0; i < idx; i++)
        {
            int itemcode = UnityEngine.Random.Range(1, 9);

            ItemClass item = new ItemClass(itemcode);
            var itemBtn = Instantiate(spwItem);
            itemBtn.transform.SetParent(spwLoc.transform);
            itemBtn.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = item.ItemName;

            itemBtn.GetComponent<Button>().onClick.AddListener(() => GetItem(item, itemBtn));
        }
    }

    void SaveChat()
    {
        Chat_Dic.Add(0, "[ ... 저게뭐지? ]");
        Chat_Dic.Add(1, "[ ... 상자를 발견]");
        Chat_Dic.Add(2, "[ ... 상자를 열어 볼까?]");        
        Chat_Dic.Add(10, "[ 아이템을 발견했다. ]");
        Chat_Dic.Add(11, "[ 아이템을 획득했다. ]");
        Chat_Dic.Add(20, "[ 뱀을 발견했다.!! ]");
        Chat_Dic.Add(21, "[ 피해 <color=red><b> 7 </color></b> ]");
        Chat_Dic.Add(30, "");
    }

    public void NextBtn()
    {
        
        if(ChatNum == 2)
        {
            SelectPannel.SetActive(true);
            NextButton.SetActive(false);
            return;
        }
        else if(ChatNum == 11 || ChatNum == 21)
        {
            SceneManager.LoadScene("1-2.MapScene");
            return;
        }
        Chatting.text = Chat_Dic[ChatNum];
        ChatNum++;
        
    }    


    public void OpenBOx()
    {
        int rnd = UnityEngine.Random.Range(1,11);
        
        // get Item
        if(rnd > 3)
        {   
            ChatNum = 10;
            GetItemPannel.SetActive(true);
        }
        else
        {   
            ChatNum = 20;
            TrapPannel.SetActive(true);
        }
        
        SelectPannel.SetActive(false);
        Chatting.text = Chat_Dic[ChatNum];
    }
    public void CloseBox()
    {
        SceneManager.LoadScene("1-2.MapScene");
    }




    public void GetItem(ItemClass item, GameObject telObj)
    {
        for (int i = 0; i < 3; i++)
        {
            if (GameManager.instance.ItemList_num[i] == 0)
            {
                GameManager.instance.ItemList_num[i] = item.ItemCode;
                telObj.SetActive(false);
                break;
            }
        }
        HUDManager.instance.SetAll();
    }

    public void GetItem_next()
    {
        ChatNum = 11;
        Chatting.text = Chat_Dic[ChatNum];
        GetItemPannel.SetActive(false);
        NextButton.SetActive(true);
    }
    public void SnakeBite()
    {
        ChatNum = 21;
        Chatting.text = Chat_Dic[ChatNum];
        TrapPannel.SetActive(false);
        NextButton.SetActive(true);

        Save_Charater_Data.instance.MyParty[0].Hp -= 7;
    }


}
