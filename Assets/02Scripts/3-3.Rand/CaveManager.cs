using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class CaveManager : MonoBehaviour
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

    public Dictionary<int, string> Chat_Dic = new Dictionary<int,string>();
    public int ChatNum;

    bool bitem = false;

    void Start() 
    {
        SaveChat();
        SelectPannel.SetActive(false);

        NextButton.GetComponent<Button>().onClick.AddListener(NextBtn);
        //NextBtn();
        Chatting.text = Chat_Dic[ChatNum];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SaveChat()
    {
        Chat_Dic.Add(0, "[ 수상한 동굴을 발견했다. ]");
        Chat_Dic.Add(1, "[ ... 들어가 볼까? ]");    

        Chat_Dic.Add(10, "[ 동굴안으로 들어왔다. ]");
        Chat_Dic.Add(11, "[ 수상한 책을 발견했다. ]");
        
        Chat_Dic.Add(12, "[ 수상한 기운이 몸을 감싼다. ]");
        Chat_Dic.Add(13, "[ 깨닳음을 얻었다. <color=green><b> Levl +1 </b></color>]");

        Chat_Dic.Add(14, "[ 보아선 안될것을 보았다. ]");
        Chat_Dic.Add(15, "[ 피해를 <color=red><b> 10 </color></b> 입었다. ]");

        Chat_Dic.Add(20, "[ 책이 손에 붙어서 떨어지지 않는다. ] \n[ 피해를 <color=red> 3 </color>입었습니다. ]");
        Chat_Dic.Add(21, "[ 책을 다시 내려 놓았다. ] ");

        Chat_Dic.Add(22, "[ 동굴에 다른 것들은 보이지 않는다.] \n[ 이만 동굴을 나가야 겠다. ]");
        Chat_Dic.Add(30, "[ ...그냥 지나가자 ]");
    }

    public void NextBtn()
    {
        
        
        switch (ChatNum)
        {
            case 1 :
            SetBtn(InCave,"동굴안으로 들어간다.",OutCave,"동굴을 무시한다.");
            SelectPannel.SetActive(true);
            NextButton.SetActive(false);
            return;

            case 11 :     
            Contents.SetActive(true);
            SetBtn(OpenBook, "책을 읽어본다.", CloseBook, "책을 내려 놓는다.");
            SelectPannel.SetActive(true);
            NextButton.SetActive(false);
            return;

            case 13 :
            ChatNum = 22;
            Chatting.text = Chat_Dic[ChatNum];
            return;
            case 15 :
            ChatNum = 22;
            Chatting.text = Chat_Dic[ChatNum];
            return;

            case 20 :
            Contents.SetActive(true);
            SetBtn(OpenBook, "책을 읽어본다.", CloseBook, "책을 내려 놓는다.");
            SelectPannel.SetActive(true);
            NextButton.SetActive(false);
            return;

            case 22 :
            SceneManager.LoadScene("1-2.MapScene");
            return;

            case 30 :
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
        SelectBtn_0.onClick.RemoveAllListeners();
        SelectBtn_0.onClick.AddListener(() => fuction0());
        SelectBtn_0.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = str0;
        SelectBtn_1.onClick.RemoveAllListeners();
        SelectBtn_1.onClick.AddListener(() => fuction1());
        SelectBtn_1.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = str1;
    }


    public void InCave()
    {
        ChatNum = 10;
        Chatting.text = Chat_Dic[ChatNum];

        BackGroundImage.sprite = BackSprite[1];
        
        Contents.GetComponent<Image>().sprite = Contentsprite[1];
        Contents.GetComponent<RectTransform>().localScale = new Vector3(1,1.3f,1);
        Contents.SetActive(false);
        
        Chatting.text = Chat_Dic[ChatNum];
        SelectPannel.SetActive(false);
        NextButton.SetActive(true);
    }
    public void OutCave()
    {
        Contents.SetActive(false);
        NextButton.SetActive(true);
        SelectPannel.SetActive(false);
        
        ChatNum = 30;
        Chatting.text = Chat_Dic[ChatNum];
    }




    public void OpenBook()
    {
        int rnd = UnityEngine.Random.Range(0,2);
        
        // 레벨업
        if(rnd == 1)
        {
            ChatNum = 12;
            foreach (var Char in GameManager.instance.MyParty)
            {
                Char.LevelUp();
            }
        }
        else {
            foreach (var Char in GameManager.instance.MyParty)
            {
                Char.Hp -= 10;
            }
            ChatNum = 14;
        }

        NextButton.SetActive(true);
        SelectPannel.SetActive(false);
        Chatting.text = Chat_Dic[ChatNum];
    }

    public void CloseBook()
    { 
        int rnd = UnityEngine.Random.Range(0,2);        
        
        // 실패
        if(rnd == 1)
        {
            ChatNum = 20;
            foreach (var Char in GameManager.instance.MyParty)
            {
                Char.Hp -= 3;
            }
        }
        else {
            ChatNum = 21;
        }

        NextButton.SetActive(true);
        SelectPannel.SetActive(false);
        Chatting.text = Chat_Dic[ChatNum];
    }


}
