using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;


public class Textanimation : MonoBehaviour
{
    public ScoreData scoreData;
    public TextMeshProUGUI Tmp;

    public GameObject ScrollView;
    public GameObject ScrollView_content;
    public GameObject[] contents;

    public RectTransform contentRect;
    public float MaxHeight;
    public float MinHeight;

    public string Score = "000000";

    public char[] newScore;
    public float times;

    public Button NextScene;



    // Start is called before the first frame update
    void Start()
    {
        contentRect = ScrollView_content.GetComponent<RectTransform>();
        SetScoreData();
        Set_ContentsText();

        for(int i = 0; i < contents.Length; i++)
        {
            ContentsState(i,false);
        }

        ScrollView.GetComponent<ScrollRect>().vertical = false;
        StartCoroutine(ContentsSpw(0));

        newScore = new char[] {'0','0','0','0','0','0'};

        
    }

    IEnumerator SetText_1(int i)
    {      
        if (times >= 1.0f)
        {
            newScore[i] = Score[i];
            i--;
            times = 0;
        }
        else
        {
            int rnd = UnityEngine.Random.Range(0,10);
            string s = ""+rnd;
            newScore[i] = s[0];
        }

        string str = new string(newScore);

        Tmp.text = str;

        yield return new WaitForSeconds(0.05f);

        times += 0.1f;

        if(i >= 0)
        {
            StartCoroutine(SetText_1(i));
        }       
       
    }

    void ContentsState(int i, bool _b)
    {
        contents[i].SetActive(_b);        
    }
    IEnumerator ContentsSpw(int i)
    {
        contentRect.sizeDelta = new Vector2(0, (i+1)*100);
        if(i > 6)
        {
            contentRect.anchoredPosition += new Vector2(0,100);
        }

        contents[i].SetActive(true);
        i++;

        yield return new WaitForSeconds(1.0f);
        if(i >= contents.Length)
        {
            Debug.Log("SPW end");
            ScrollView.GetComponent<ScrollRect>().vertical = true;
            StartCoroutine(SetText_1(5));
        }
        else
        {            
            StartCoroutine(ContentsSpw(i));
        }
    }

    void SetScoreData()
    {
        int score_int = 0;

        scoreData.floor = 12;
        scoreData.win_Battle = 5;
        scoreData.win_Boss = 1;
        scoreData.C_Rank = 3;
        scoreData.B_Rank = 2;
        scoreData.A_Rank = 1;
        scoreData.S_Rank = 3;
        scoreData.Attack_Count = 10;
        scoreData.Skill_Count = 20;
        scoreData.Ulti_Count = 5;
        scoreData.Gold = 700;
        scoreData.All_Lvl = 10;
        scoreData.Used_Itme = 4;
        scoreData.Member_1 = 0;
        scoreData.Member_3 = 1;
        scoreData.Death_Count = 0;
        scoreData.Critical_Count = 10;
        scoreData.Dodge_Count = 8;

        score_int = 
        scoreData.floor * 11111 +
        scoreData.win_Battle * 33333 +
        scoreData.win_Boss * 151515 +
        scoreData.C_Rank * 1515 +
        scoreData.B_Rank * 55555 +
        scoreData.A_Rank * 122222 +
        scoreData.S_Rank * 355555 +
        scoreData.Attack_Count * 555 +
        scoreData.Skill_Count * 1111 +
        scoreData.Ulti_Count * 11111 +
        scoreData.Gold * 111 +
        scoreData.All_Lvl * 9999 +
        scoreData.Used_Itme * 99999 +
        scoreData.Member_1 * 3000000 +
        scoreData.Member_3 * 1000000 +
        scoreData.Death_Count * 50000 +
        scoreData.Critical_Count * 3333 +
        scoreData.Dodge_Count * 5555;

        Score = ""+score_int;
        Debug.Log("Score : " + score_int);
    }

    void Set_ContentsText()
    {
        contents[0].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = ""+scoreData.floor;
        contents[1].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = ""+scoreData.win_Battle;
        contents[2].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = ""+scoreData.win_Boss;
        contents[3].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = ""+scoreData.C_Rank;
        contents[4].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = ""+scoreData.B_Rank;
        contents[5].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = ""+scoreData.A_Rank;
        contents[6].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = ""+scoreData.S_Rank;
        contents[7].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = ""+scoreData.Attack_Count;
        contents[8].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = ""+scoreData.Skill_Count;
        contents[9].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = ""+scoreData.Ulti_Count;
        contents[10].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = ""+scoreData.Gold;
        contents[11].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = ""+scoreData.All_Lvl;
        contents[12].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = ""+scoreData.Used_Itme;
        contents[13].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = ""+3;
        contents[14].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = ""+scoreData.Death_Count;
        contents[15].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = ""+scoreData.Critical_Count;
        contents[16].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = ""+scoreData.Dodge_Count;
    }


    void Update()
    {
        // if(contentRect.anchoredPosition.y > MaxHeight)
        // {
        //     contentRect.anchoredPosition = new Vector2(0, MaxHeight);
        // }
        // else if(contentRect.anchoredPosition.y < MinHeight)
        // {
        //     contentRect.anchoredPosition = new Vector2(0, MinHeight);
        // }
    }

    void RegistRankingScene()
    {
        SceneManager.LoadScene(13);
    }

    void firstScene()
    {
        SceneManager.LoadScene(0);
    }
}
