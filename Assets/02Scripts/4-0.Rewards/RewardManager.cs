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
    public int Tellent;

    
    // 골드 / 아이템
    public GameObject GoldPannel;
    public GameObject Coments;
    public int Gold;
    public int ItemRate;
    public GameObject GoldPrefab; // 100
    public GameObject ItemPrefab; // 50

    // 결과창
    public GameObject ResultPannel;
    public GameObject[] PlayerObj;
    public Image[] Player_Exp;
    public TextMeshProUGUI[] GainExp;
    public TextMeshProUGUI[] char_Name;
    bool bReadyResul;
    public TextMeshProUGUI resultGold;
    public TextMeshProUGUI resultTellent;




    public TellentsScripts[] tellentArray = new TellentsScripts[3];

    int SelectTellentsCode;
    bool bCheckedTellent;
    public int takeGold;
    public int _itemCode;
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
        // 특성 선택

        // 골드 획득
        // 결과 화면

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 특성 패널 
    void SetTellentArr()
    {
        int[] arr = new int[4] {0,1,2,3};
        System.Random rnd = new System.Random();
        arr = arr.OrderBy(x => rnd.Next()).ToArray();
        
        // tellentArray[0] = GameManager.instance.TellentSeed[0];
        // tellentArray[1] = GameManager.instance.TellentSeed[1];
        // tellentArray[2] = GameManager.instance.TellentSeed[2];

        // for(int i = 0; i < 3; i++)
        // {
        //     Debug.Log(i + "_name : " + tellentArray[i].Tel_Name);
        //     TextMeshProUGUI _Name = Tellents[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        //     _Name.text = tellentArray[i].Tel_Name;
        //     TextMeshProUGUI _Comments = Tellents[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        //     _Comments.text = tellentArray[i].Comments;
        // }

    }

    // Other 패널
    void SetGoldPannel()
    {
        Gold = GameManager.instance.ResultData.Gold;
        GameObject spwGold = Instantiate(GoldPrefab);
        spwGold.transform.SetParent(GoldPannel.transform.GetChild(2));
        spwGold.GetComponent<RectTransform>().anchoredPosition = new Vector2(0,100);
        spwGold.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = ""+Gold;

        ItemRate = GameManager.instance.ResultData.ItemRate;
        int rnd = UnityEngine.Random.Range(1,101);
        Debug.Log("rnd : " + rnd);
        if (ItemRate >= rnd)
        {
            
            GameObject spwItem = Instantiate(ItemPrefab);
            spwItem.transform.SetParent(GoldPannel.transform.GetChild(2));
            spwItem.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -50);
            spwItem.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "연막탄";
        }        

    }

    // 보상 패널
    void SetRewardPannel()
    {
        // 경험치 획득
        Save_Charater_Data SaveData = Save_Charater_Data.instance;
        int gainExp = 150;

        for(int i = 0; i < 3; i++)
        {
            if(i >= SaveData.MyParty.Count)
            {
                PlayerObj[i].SetActive(false);
            }                
        }
        for(int i = 0; i < SaveData.MyParty.Count; i++)
        {
            if(SaveData.MyParty[i].bAlive == true)
            {                
                int curExp = SaveData.MyParty[i].exp;
                int maxExp = 100 + (SaveData.MyParty[i].Level* 50);
                char_Name[i].text = SaveData.MyParty[i].name;
                
                SaveData.MyParty[i].SetEXp(gainExp);
                int _max = SaveData.MyParty[i].Level * 50 +100;
                int _cur = SaveData.MyParty[i].exp;
                StartCoroutine(FillExp(0, gainExp, _max, _cur, i));
            }            
        }


        // 텍스트 출력
        // resultGold.text = ""+takeGold;
        // resultTellent.text = ""+  tellentArray[SelectTellentsCode].Tel_Name;
        // foreach(var exp in GainExp)
        // {
        //     exp.text = "+"+gainExp;
        // }

        // Etel_type teltype = tellentArray[SelectTellentsCode].type;
        // switch (teltype)
        // {
        //     case Etel_type.AfterBattle:
        //         GameManager.instance.ABTelent.Add(tellentArray[SelectTellentsCode]);
        //         GameManager.instance.ABTelent_num.Add(tellentArray[SelectTellentsCode].tel_num);
        //         break;

        //     case Etel_type.beforBattle:
        //         GameManager.instance.BBTellent.Add(tellentArray[SelectTellentsCode]);
        //         GameManager.instance.BBTellent_num.Add(tellentArray[SelectTellentsCode].tel_num);
        //         break;

        //     case Etel_type.beforeTurn:
        //         GameManager.instance.BeforTellents.Add(tellentArray[SelectTellentsCode]);
        //         GameManager.instance.BeforTellents_num.Add(tellentArray[SelectTellentsCode].tel_num);
        //         break;

        //     case Etel_type.getAfter:
        //         GameManager.instance.GetAfterTellents.Add(tellentArray[SelectTellentsCode]);
        //         GameManager.instance.GetAfterTellents_num.Add(tellentArray[SelectTellentsCode].tel_num);
        //         break;
        // }

        // HUDManager.instance.SetTellentTxt();
        // GameManager.instance.TellentSeed.RemoveAt(SelectTellentsCode);

        
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
        if(_cur > _Max)
        {
            _cur = 1; _Max +=50;
        }
        Player_Exp[idx].fillAmount = (float)_cur / ((float)_Max);
        yield return new WaitForSeconds(0.08f);
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
        SelectTellentsCode = n;
        bCheckedTellent = true;
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
        _itemCode = 2;
        bCheckedItem = true;
        for (int i = 0; i < 3; i++)
        {
            if(GameManager.instance.ItemList_num[i] != 0)
            {
                GameManager.instance.ItemList_num[i] = 2;
                break;
            }
        }
        
        HUDManager.instance.SetBoom();
    }


}
