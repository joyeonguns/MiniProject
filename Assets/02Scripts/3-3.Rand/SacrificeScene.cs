using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;


public class SacrificeScene : MonoBehaviour
{
    public Image BackGroundImage;
    public Sprite[] BackSprite;
    
    public GameObject Contents;
    public Sprite[] Contentsprite;

    public TextMeshProUGUI Chatting;
    public GameObject NextButton;
    
    public GameObject SelectPannel;
    public Button SelectBtn_0;
    public Button SelectBtn_1;
    public Button SelectBtn_2;

    public Dictionary<int, string> Chat_Dic = new Dictionary<int,string>();
    public int ChatNum;

    int idx = 0;
    int lostHp = 0;
    bool Tryout = false;

    void Start() 
    {
        SaveChat();
        SelectPannel.SetActive(false);

        NextButton.GetComponent<Button>().onClick.AddListener(NextBtn);

        BackGroundImage.sprite = BackSprite[0];

        Chatting.text = Chat_Dic[ChatNum];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SaveChat()
    {
        // 유적 발견
        Chat_Dic.Add(0, "[ 수상한 유적을 발견했다. ]");
        Chat_Dic.Add(1, "[ ... 들어가 볼까? ]");    
 
        Chat_Dic.Add(10, "[ 유적안으로 들어왔다. ]");
        Chat_Dic.Add(11, "[ 계단으로 이어져 있다. ]");
        // 유적 탈출
        Chat_Dic.Add(12, "[ 문이 잠겨있어 나갈수가 없다. ]");        
        Chat_Dic.Add(13, "[ 최상층에 도착했다. ]");
        Chat_Dic.Add(14, "[ 닫혀있는 문을 발견하였다. ]");
        Chat_Dic.Add(15, "[ 방안으로 들어갔다. ]");
        Chat_Dic.Add(16, "[ 방 가운대 수상한 구조물이 보인다. ]");
        Chat_Dic.Add(17, "[ 구조물에는 '희생의 제단' 이라 쓰여있다. ]");
        Chat_Dic.Add(18, "[ 제단에 다가가자 제단의 사용방법이 머리에 들어온다. ]");
        Chat_Dic.Add(19, "[ 제단을 사용한다. ]");
        Chat_Dic.Add(20, "[ 제단을 한번 더 사용한다. ]");
        
        // 돈을 바침
        Chat_Dic.Add(30, "[ 검은 기운이 몸을 감싼다. ]");
        Chat_Dic.Add(31, "[ 체력과 마나가 가득 찬다. 경험치 +300 ]");
        

        // 파티원을 바침

        
        Chat_Dic.Add(40, "[ 나가는 문을 발견하였다. ]");
    }

    public void NextBtn()
    {
                
        switch (ChatNum)
        {
            case 1 :
            Chatting.text = Chat_Dic[ChatNum];
            SetBtn(InRuin,"유적안으로 들어간다.",NoRuin,"유적에 들어가지 않는다.");

            SelectPannel.SetActive(true);
            NextButton.SetActive(false);
            return;

            case 11 :     
            Chatting.text = Chat_Dic[ChatNum];
            SetBtn(Climbstairs, "계단을 따라 올라간다.", TryOutRuin, "유적 밖으로 나간다.");
            if(Tryout == false)
                SelectBtn_1.interactable = true;
            else
                SelectBtn_1.interactable = false;

            SelectPannel.SetActive(true);
            NextButton.SetActive(false);
            return;

            case 12 :
            Chatting.text = Chat_Dic[ChatNum];
            SetBtn(Climbstairs, "계단을 따라 올라간다.", TryOutRuin, "유적 밖으로 나간다.");
            if(Tryout == false)
                SelectBtn_1.interactable = true;
            else
                SelectBtn_1.interactable = false;
                
            SelectPannel.SetActive(true);
            NextButton.SetActive(false);
            return;

            case 16 :           
            Contents.SetActive(true);    
            ChatNum++;    
            Chatting.text = Chat_Dic[ChatNum];      
            return;

            case 19 :
            Chatting.text = Chat_Dic[ChatNum];
            idx++;
            Contents.GetComponent<Image>().sprite = Contentsprite[1];
            SetBtn(SacrificeGold, $"돈을 바친다. ({idx*200}골드)", SacrificeTeam, "팀원을 바친다.(HP 30% 감소)", giveupSacrifice, "제단에서 떨어진다.");
            if(GameManager.instance.curGold >= idx*200)
                SelectBtn_0.interactable = true;
            else
                SelectBtn_0.interactable = false;

            if(GameManager.instance.MyParty.Count > 1)
                SelectBtn_1.interactable = true;
            else
                SelectBtn_1.interactable = false;
            
            SelectPannel.SetActive(true);
            NextButton.SetActive(false);            
            return;
            
            case 20 :
            Chatting.text = Chat_Dic[ChatNum];
            idx++;
            Contents.GetComponent<Image>().sprite = Contentsprite[1];
            SetBtn(SacrificeGold, $"돈을 바친다. ({idx*200}골드)", SacrificeTeam, "팀원을 바친다.(HP 30% 감소)", giveupSacrifice, "제단에서 떨어진다.");
            if(GameManager.instance.curGold >= idx*200)
                SelectBtn_0.interactable = true;
            else
                SelectBtn_0.interactable = false;

            if(GameManager.instance.MyParty.Count > 1)
                SelectBtn_1.interactable = true;
            else
                SelectBtn_1.interactable = false;
            
            SelectPannel.SetActive(true);
            NextButton.SetActive(false);            
            return;

            case 40 :
            Chatting.text = Chat_Dic[ChatNum];
            SetBtn(ReturnSacrifice, "재단으로 다시 돌아간다.", OutRuin, "문을 열고 나간다.");
            SelectBtn_0.interactable = true;
            SelectBtn_1.interactable = true;
            SelectPannel.SetActive(true);
            NextButton.SetActive(false);
            return;


            case 50 :
            SceneManager.LoadScene("1-2.MapScene");
            return;

            default :       
            ChatNum++;    
            Chatting.text = Chat_Dic[ChatNum];
            break;
        }       
        
    }    

    public void SetBtn(Action fuction0, string str0, Action fuction1, string str1)
    {
        SelectBtn_0.gameObject.SetActive(true);
        SelectBtn_0.onClick.RemoveAllListeners();
        SelectBtn_0.onClick.AddListener(() => fuction0());
        SelectBtn_0.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = str0;

        SelectBtn_1.gameObject.SetActive(true);
        SelectBtn_1.onClick.RemoveAllListeners();
        SelectBtn_1.onClick.AddListener(() => fuction1());
        SelectBtn_1.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = str1;

        SelectBtn_2.gameObject.SetActive(false);
    }
    public void SetBtn(Action fuction0, string str0, Action fuction1, string str1, Action fuction2, string str2)
    {
        SelectBtn_0.gameObject.SetActive(true);
        SelectBtn_0.onClick.RemoveAllListeners();
        SelectBtn_0.onClick.AddListener(() => fuction0());
        SelectBtn_0.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = str0;

        SelectBtn_1.gameObject.SetActive(true);
        SelectBtn_1.onClick.RemoveAllListeners();
        SelectBtn_1.onClick.AddListener(() => fuction1());
        SelectBtn_1.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = str1;

        SelectBtn_2.gameObject.SetActive(true);
        SelectBtn_2.onClick.RemoveAllListeners();
        SelectBtn_2.onClick.AddListener(() => fuction2());
        SelectBtn_2.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = str2;
    }


    public void InRuin()
    {
        ChatNum = 10;
        Chatting.text = Chat_Dic[ChatNum];

        BackGroundImage.sprite = BackSprite[1];
        BackGroundImage.GetComponent<RectTransform>().sizeDelta = new Vector2(1920,1080);
        
        Contents.GetComponent<Image>().sprite = Contentsprite[1];
        Contents.GetComponent<RectTransform>().localScale = new Vector3(2,2,2);
        Contents.GetComponent<RectTransform>().sizeDelta = new Vector2(369,492);
         Contents.GetComponent<RectTransform>().anchoredPosition = new Vector2(540,70);
        
        Contents.SetActive(false);
        SelectPannel.SetActive(false);
        NextButton.SetActive(true);

    }



    public void Climbstairs()
    {
        ChatNum = 13;
        Chatting.text = Chat_Dic[ChatNum];

        SelectPannel.SetActive(false);
        NextButton.SetActive(true);
    }

    public void TryOutRuin()
    {
        ChatNum = 12;
        Chatting.text = Chat_Dic[ChatNum];

        ChatNum = 11;
        Tryout = true;
        SelectPannel.SetActive(false);
        NextButton.SetActive(true);
    }
        
    public void NoRuin()
    {         
        ChatNum = 50;
        Chatting.text = "[ 뒤를 돌아보자 유적은 연기처럼 사라졌다.]";
        SelectPannel.SetActive(false);
        NextButton.SetActive(true);
        Contents.SetActive(false);
    }

    public void SacrificeGold()
    {
        ChatNum = 20;
        int cost = idx * 200;
        GameManager.instance.curGold -= cost;
        HUDManager.instance.SetAll();

        foreach (var team in GameManager.instance.MyParty)
        {
            team.Mana = 10;
            team.Hp += 100;
            team.SetEXp(cost);
        }

        Chatting.text = $"[ 검은 기운이 몸을 감싼다. ] \n[ 체력과 마나가 가득 찼습니다. ]\n[ EXP + <color=green>{cost}</color> ]";
        SelectPannel.SetActive(false);
        NextButton.SetActive(true);
    }

    public void SacrificeTeam()
    {
        ChatNum = 20;
        int i = 0;
        foreach (var team in GameManager.instance.MyParty)
        {
            if(team.Main == false)
            {
                GameManager.instance.MyParty.RemoveAt(i);
                break;
            }
            i++;
        }

        foreach (var team in GameManager.instance.MyParty)
        {
            team.Hp -= (team.Hp * 0.3f);
            team.SetEXp(400);
        }

        Chatting.text = $"[ 붉은 기운이 몸을 감싼다. ]\n[ 전투로 인해 HP <color=red>30%</color> 감소 ] \n[ EXP + <color=green>{1000}</color>]";
        SelectPannel.SetActive(false);
        NextButton.SetActive(true);
    }

    public void giveupSacrifice()
    {   
        Contents.SetActive(false);     
        ChatNum = 40;
        Chatting.text = $"[ 주위를 둘러본다. ]";
        SelectPannel.SetActive(false);
        NextButton.SetActive(true);
    }

    public void ReturnSacrifice()
    {        
        ChatNum = 19;
        Chatting.text = $"[ 제단에 다가가간다. ]";
        SelectPannel.SetActive(false);
        NextButton.SetActive(true);
    }

    public void OutRuin()
    {
        ChatNum = 50;
        foreach (var team in GameManager.instance.MyParty)
        {
            if(team.Main == true)
            {
                lostHp = (int)(team.curHp * 0.3f);
            }
        }

        if(idx > 1)
            Chatting.text = $"[ 유적 밖으로 나오자 유적은 연기처럼 사라졌다. ]";
        else
        {
            foreach (var team in GameManager.instance.MyParty)
            {
                team.curHp -= lostHp;
            }
            Chatting.text = $"[ 소름 끼치는 환영을 보았다. 피해를 <color=red> -{lostHp}</color> 입었습니다.]";
        }            

        SelectPannel.SetActive(false);
        NextButton.SetActive(true);
    }

}
