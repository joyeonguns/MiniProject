using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System;
using System.Linq;

public class RewardManager : MonoBehaviour
{
    // 특성
    public GameObject TellentsPannel;
    public GameObject[] Tellents;
    public Sprite[] TellentSprite;

    
    // 골드 / 아이템
    public GameObject GoldPannel;
    public GameObject Coments;
    public int Gold;
    public int takeGold = 0;

    public int ItemRate;
    public GameObject GoldPrefab; // 100
    public GameObject ItemPrefab; // 50
    public int itemCode;
    public int takeitemCode = 0;
    public GameObject SpwLoc;

    // 결과창
    public GameObject ResultPannel;
    public GameObject[] PlayerObj;
    public Image[] Player_Exp;
    public TextMeshProUGUI[] GainExp;
    public TextMeshProUGUI[] char_Name;

    bool bReadyResul;
    public GameObject Result_Gettellent;
    public TextMeshProUGUI resultGold;
    public TextMeshProUGUI resultTellent;

    public Sprite[] CharIcon;



    GameManager GM;

    public TellentsScripts[] tellentArray = new TellentsScripts[3];

    int SelectTellentnum;
    bool bCheckedTellent;
    
    public string TellentName;
    
    bool bCheckedItem;



    // Start is called before the first frame update

    void Awake() 
    {
        bCheckedTellent = false;
        bCheckedItem = false;
        bReadyResul = false;
    }
    void Start()
    {
        GM = GameManager.instance;

        for (int i = 0; i < GM.MyParty.Count; i++)
        {
            if(GM.MyParty[i].bAlive == false)
            {
                GM.MyParty.RemoveAt(i);
            }
        }

        if(GM.ResultData.ResultMode == ResultEnum.Run)
        {
            TellentsPannel.SetActive(false);
            GoldPannel.SetActive(false);
            ResultPannel.SetActive(true);

            SetRewardPannel();
            return;
        }
        // 특성 할당
        SetTellentArr();
        // 골드 패널 할당
        SetGoldPannel();
        //SetRewardPannel();
        // 패널 설정
        TellentsPannel.SetActive(true);
        GoldPannel.SetActive(false);
        ResultPannel.SetActive(false);

        StartCoroutine(ReadyReward());
        GM.GameScoreData.win_Battle++;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 특성 패널 
    void SetTellentArr()
    {
        for (int i = 0; i < 3; i++)
        {
            int rnd = UnityEngine.Random.Range(1,101);
            
            int telCode = 0;
            Etel_Rank rank;
            if(rnd <= 35)
            {
                telCode = UnityEngine.Random.Range(0, 11);
                rank = Etel_Rank.C;
            }
            else if(rnd <= 80)
            {
                telCode = UnityEngine.Random.Range(0, 18);
                rank = Etel_Rank.B;
            }
            else if(rnd <= 99)
            {
                telCode = UnityEngine.Random.Range(0, 5);
                rank = Etel_Rank.A;
            }
            else
            {
                telCode = UnityEngine.Random.Range(0, 2);
                rank = Etel_Rank.S;
            }
            tellentArray[i] = new TellentsScripts(rank,telCode); 
        }
        

        // 이미지 연결
        for(int i = 0; i < 3; i++)
        {
            TextMeshProUGUI _Name = Tellents[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            _Name.text = tellentArray[i].telData.Name;
            TextMeshProUGUI _Comments = Tellents[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>();
            _Comments.text = tellentArray[i].telData.Contents;

            Tellents[i].GetComponent<Image>().sprite = TellentSprite[(int)tellentArray[i].Rank];
        }

    }

    // Other 패널
    void SetGoldPannel()
    {
        Gold = GM.ResultData.Gold;
        GameObject spwGold = Instantiate(GoldPrefab);
        spwGold.transform.SetParent(SpwLoc.transform);
        spwGold.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = ""+Gold;

        ItemRate = GM.ResultData.ItemRate;
        int rnd = UnityEngine.Random.Range(1,101);
        Debug.Log("rnd : " + rnd);
        
        // 아이템 획득
        if (ItemRate >= rnd)
        {   
            itemCode = UnityEngine.Random.Range(1,9);         
            GameObject spwItem = Instantiate(ItemPrefab);

            spwItem.transform.SetParent(SpwLoc.transform);
            spwItem.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = new ItemClass(itemCode).ItemName;

            spwItem.GetComponent<Button>().onClick.AddListener(SetItemBtn);
        }        

    }

    // 보상 패널
    void SetRewardPannel()
    {
        // 경험치 획득
        
        int gainExp = 0;
        if(GM.ResultData.ResultMode != ResultEnum.Run)
        {
            gainExp = GM.ResultData.Exp;

            // 텍스트 출력
            resultGold.text = "" + takeGold;
            resultTellent.text = "" + TellentName;
        }
        else
        {
            gainExp = 0;
            Result_Gettellent.SetActive(false);
            
        }
        

        for(int i = 0; i < 3; i++)
        {
            if(i > GM.MyParty.Count)
            {
                PlayerObj[i].SetActive(false);
            }                
            else
            {
                int roleCode = (int)GM.MyParty[i].Role;
                PlayerObj[i].GetComponent<Image>().sprite = SOManager.GetChar().CharDatas[roleCode].Icon;
            }
        }
        for(int i = 0; i < GM.MyParty.Count; i++)
        {
            if(GM.MyParty[i].bAlive == true)
            {                
                int curExp = GM.MyParty[i].exp;
                int maxExp = 100 + (GM.MyParty[i].Level* 50);
                char_Name[i].text = GM.MyParty[i].name;
                
                
                int _max = GM.MyParty[i].Level * 50 +100;
                int _cur = GM.MyParty[i].exp;
                StartCoroutine(FillExp(0, gainExp, _max, _cur, i));
                GM.MyParty[i].SetEXp(gainExp);
            }            
        }

  
    }

    IEnumerator ReadyReward()
    {
        yield return null;
        if(bReadyResul == false)
            StartCoroutine(ReadyReward());
        else
            SetRewardPannel();
    }

    IEnumerator FillExp(int gained,int _goalExp, int _Max, int _cur, int idx)
    {   
        gained++;
        _cur++;
        GainExp[idx].text = ""+gained;
        if(_cur > _Max)
        {
            _cur = 1; _Max +=50;
        }
        Player_Exp[idx].fillAmount = (float)_cur / ((float)_Max);
        yield return new WaitForSeconds(0.04f);
        if(gained < _goalExp)
            StartCoroutine(FillExp(gained, _goalExp, _Max, _cur, idx));
    }
    

    // 버튼
    public void Next_Tellent_Btn()
    {
        TellentsPannel.SetActive(false);
        GoldPannel.SetActive(true);
    }
    public void Next_Gold_Btn()
    {
        GoldPannel.SetActive(false);
        ResultPannel.SetActive(true);
        bReadyResul = true;
    }
    public void Next_Reward()
    {
        SceneManager.LoadScene("1-2.MapScene");
    }



    public void SetTellBtn(int n)
    {
        SelectTellentnum = n;
        bCheckedTellent = true;

        TellentsScripts GetTellent = tellentArray[n];
        TellentName = GetTellent.name;
        
        GameManager.instance.SetTellent(GetTellent);

        HUDManager.instance.SetTellentTxt();
        HUDManager.instance.GetComponent<TellentCardUI>().SpawnTellentCard(GetTellent);
        Next_Tellent_Btn();
    } 

    public void SetGoldBtn()
    {
        takeGold = Gold;
        GameManager.instance.curGold += takeGold;
        HUDManager.instance.SetGold();
    }
    public void SetItemBtn()
    {
        takeitemCode = itemCode;
        bCheckedItem = true;
        for (int i = 0; i < 3; i++)
        {
            if(GameManager.instance.ItemList_num[i] == 0)
            {
                GameManager.instance.ItemList_num[i] = takeitemCode;
                break;
            }
        }
        
        HUDManager.instance.SetBoom();
    }


}
