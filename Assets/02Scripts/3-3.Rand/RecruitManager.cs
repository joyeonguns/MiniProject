using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class RecruitManager : MonoBehaviour
{
    public TextMeshProUGUI Chatting;
    public GameObject NextButton;
    
    public GameObject SelectPannel;
    public Button SelectBtn_0;
    public Button SelectBtn_1;

    public Dictionary<int, string> Chat_Dic = new Dictionary<int,string>();
    public int ChatNum;

    Save.Player newChar;

    public Action ResulfAction;

    void Start() 
    {
        SetNewChar();
        SaveChat();

        if(GameManager.instance.MyParty.Count == 3)
        {
            SelectBtn_0.interactable = false;
        }
        SelectPannel.SetActive(false);

        NextBtn();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetNewChar()
    {
        // 레벨
        int lvl = 0;
        int rnd = UnityEngine.Random.Range(1,11);
        if(rnd > 5) lvl = 2;
        else if(rnd > 1) lvl = 3;
        else if(rnd == 1) lvl = 4;       
        
        

        // 캐릭터 생성
        rnd = UnityEngine.Random.Range(0,4);

        e_Class newClass = (e_Class)(rnd + 1);

        CharacterDatas charData = SOManager.instance.CharSO.CharDatas[rnd + 1];
        Save.St_Stat newStatus = new Save.St_Stat(charData);
        

        newChar = new Save.Player(newStatus,newClass);

        for(int i = 0; i < lvl; i++)
        {
            newChar.LevelUp();
        }
    }

    void SaveChat()
    {
        Chat_Dic.Add(0,"[ 거수자 발견 ]");
        Chat_Dic.Add(1," ??? \n 잠깐 멈춰!");
        Chat_Dic.Add(2," ??? \n 난 " + newChar.name +" 이야 \n 길을 잃어서 그런대 마차 좀 같이 탈 수 있을까? ");
        Chat_Dic.Add(3,"");
        Chat_Dic.Add(10, " "+newChar.name + "\n 으윽 150골드면 거의 전 재산이잔아?");
        Chat_Dic.Add(11,"[ 그래서 안탈거야? ]");
        Chat_Dic.Add(12, " "+newChar.name + "\n 어쩔수 없지 자 받아");
        Chat_Dic.Add(20, " "+newChar.name + "\n 뭐? 갑자기?");
        Chat_Dic.Add(21,"[ 마침 파티원을 구하고 있어서 ]");
        Chat_Dic.Add(22, " "+newChar.name + "\n 음.. 마침 일도 없으니 좋아");
    }

    public void NextBtn()
    {
        if(ChatNum == 3)
        {
            SelectPannel.SetActive(true);
            NextButton.SetActive(false);
            return;
        }  
        else if(ChatNum == 12 || ChatNum == 22)
        {
            ResulfAction();
        }
        else if(ChatNum == 13 || ChatNum == 23)
        {
            SceneManager.LoadScene("1-2.MapScene");
            return;
        }
        Chatting.text = Chat_Dic[ChatNum];
        ChatNum++;
        
    }

    void GetGold()
    {
        GameManager.instance.curGold += 150;
        HUDManager.instance.SetAll();
    }

    void Recruit()
    {
        if(GameManager.instance.MyParty.Count < 3)
        {
            GameManager.instance.MyParty.Add(newChar);
        }
    }

    public void ClickRecruit()
    {        
        Debug.Log("Recruit");
        NextButton.SetActive(true);
        ChatNum = 20;
        ResulfAction = Recruit;
        SelectPannel.SetActive(false);
        NextBtn();
    }
    public void ClickGetGold()
    {
        Debug.Log("GetGold");
        NextButton.SetActive(true);     
        ChatNum = 10;   
        ResulfAction = GetGold;
        SelectPannel.SetActive(false);
        NextBtn();
    }

}
