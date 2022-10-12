using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class CampManager : MonoBehaviour
{
    public GameObject BackGround;
    public GameObject SelectPannel;

    public GameObject Select;
    public GameObject Select_In;
    public GameObject Select_Out;

    public Button SpecialRoom;
    public Button SpecialTrainning;

    public GameObject ResultPannel;
    public GameObject[] Character = new GameObject[3];
    public Image[] Hp = new Image[3];
    public Image[] Mp = new Image[3];
    public Image[] Exp = new Image[3];
    public TextMeshProUGUI[] UpText = new TextMeshProUGUI[3];

    public int SpecialRoom_Price = 50;
    public int Char_Count;
    int[] GetHp = new int[3];
    int GetMp;
    int GetExp;

    GameManager GM; 
    void Start() 
    {
        BackGround.GetComponent<Image>().material.SetFloat("_Size",0);
        GM = GameManager.instance;

        // 선택지 패널
        SelectPannel.SetActive(true);
        Select.SetActive(true);
        Select_In.SetActive(false);
        Select_Out.SetActive(false);

        // 특별실
        if(GameManager.instance.curGold < SpecialRoom_Price)
            SpecialRoom.interactable = false;
        else SpecialRoom.interactable = true;

        // 특훈
        SpecialTrainning.interactable = CheckingHp();

        // 결과
        ResultPannel.SetActive(false);
        Char_Count = GM.MyParty.Count;
        for(int i = 0; i < 3; i++)
        {
            if(i >= Char_Count) Character[i].SetActive(false);
            else
            {
                Character[i].SetActive(true);
                
                int roleCOde = (int)GM.MyParty[i].Role;
                Character[i].GetComponent<Image>().sprite = SOManager.GetChar().CharDatas[roleCOde].Icon;
            } 
        }       

    }

    bool CheckingHp()
    {
        bool Check = true;
        foreach(var Char in GM.MyParty)
        {
            if(Char.Hp < 2) Check = false;
        }
        return Check;
    }

    void SetResult() 
    {
        ResultPannel.SetActive(true);
        if(ResultPannel.activeSelf == true)
        {
            for(int i = 0; i < Char_Count; i++)
            {



                double curHp = GM.MyParty[i].Hp;
                double maxHp = GM.MyParty[i].status.MaxHp;

                int curExp = GM.MyParty[i].exp;
                int maxExp = GM.MyParty[i].Level*50 + 100;

                int curMana = GM.MyParty[i].Mana;


                GM.MyParty[i].Hp += GetHp[i];
                GM.MyParty[i].Mana += GetMp;
                GM.MyParty[i].SetEXp(GetExp);

                int sign = 1;
                string str_sign = "+";
                if(GetHp[i] < 0) {sign = -1;  str_sign = ""; }; 

                StartCoroutine(FillAmount_Hp(i, 0, Mathf.Abs(GetHp[i]), curHp, maxHp, sign));
                StartCoroutine(FillAmount_Mp(i, 0, GetMp, curMana, 10));
                StartCoroutine(FillAmount_Exp(i, 0, GetExp, curExp, maxExp));


                
                UpText[i].text = 
                "<color=red>"+ str_sign + GetHp[i] +"</color> \n"+
                "<color=blue>+"+ GetMp +"</color> \n"+
                "<color=green>+"+ GetExp +"</color>";
            }
        }
    }
    
    IEnumerator FillAmount_Hp(int idx, int _get, int Gets, double cur, double _max, int sign)
    {
        Debug.Log("_get : "+ _get);

        _get++;
        cur += sign;        

        Hp[idx].fillAmount = (float)(cur / _max);

        yield return new WaitForSeconds(0.3f);
        if(_get < Gets)  
            StartCoroutine(FillAmount_Hp(idx, _get, Gets, cur, _max,sign));
        else{   Debug.Log(_get + " : " + Gets); }
    }
    IEnumerator FillAmount_Mp(int idx, int _get, int Gets, int cur, int _max)
    {
        _get++;
        cur++;
        
        Mp[idx].fillAmount = (float)cur / (float)_max;
        
        yield return new WaitForSeconds(1.2f);
        if(_get < Gets)  
            StartCoroutine(FillAmount_Mp(idx, _get, Gets, cur, _max));
    }
    IEnumerator FillAmount_Exp(int idx, int _get, int Gets, int cur, int _max)
    {
        _get++;
        cur++;        

        if(cur > _max)
        {
            cur = 1;
            _max += 50;
        }
        Exp[idx].fillAmount =  (float)cur / (float)_max;
        
        yield return new WaitForSeconds(0.01f);
        if(_get < Gets)  
            StartCoroutine(FillAmount_Exp(idx, _get, Gets, cur, _max));
    }



    public void Select_Btn(int n)
    {
        if(n == 0)
            Select_In.SetActive(true);
        else Select_Out.SetActive(true);
        Select.SetActive(false);
    }

    public void NomalRoom_Btn()
    {
        Debug.Log("select : NomalRoom_Btn");
        GetMp = 1;
        for (int i = 0; i < Char_Count; i++)
        {
            GetHp[i] = (int)((float)GM.MyParty[i].Hp * 0.25f);
        }

        SelectPannel.SetActive(false);
        Invoke("SetResult",3.0f);
        StartCoroutine(Blurs(0));
    }
    public void SpecialRoom_Btn()
    {
        Debug.Log("select : SpecialRoom_Btn");
        GameManager.instance.curGold -= 100;
        HUDManager.instance.SetGold();

        GetMp = 3;
        for (int i = 0; i < Char_Count; i++)
        {
            GetHp[i] = (int)((float)GM.MyParty[i].Hp * 0.5f);
        }

        SelectPannel.SetActive(false);
        Invoke("SetResult",3.0f);
        StartCoroutine(Blurs(0));
    }

    public void Trainning_Btn()
    {
        Debug.Log("select : Trainning_Btn");
        GetExp = 100;

        SelectPannel.SetActive(false);
        Invoke("SetResult",3.0f);
        StartCoroutine(Blurs(0));
    }
    public void SpecialTrainning_Btn()
    {        
        Debug.Log("select : SpecialTrainning_Btn");
        GetExp = 200;;
        for (int i = 0; i < Char_Count; i++)
        {
            GetHp[i] = (-1)*(int)((float)GM.MyParty[i].Hp * 0.3f);
        }

        SelectPannel.SetActive(false);
        Invoke("SetResult",3.0f);
        StartCoroutine(Blurs(0));
    }

    public void ExitBtn()
    {
        SceneManager.LoadScene("1-2.MapScene");
    }

    IEnumerator Blurs(float n)
    {
        n += 0.1f;
        BackGround.GetComponent<Image>().material.SetFloat("_Size",n);
        yield return new WaitForSeconds(0.1f);
        if(n < 3.3f)
            StartCoroutine(Blurs(n));
    }
}
