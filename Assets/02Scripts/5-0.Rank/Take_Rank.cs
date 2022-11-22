using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class RankData
{
    public int Score;
    public string Name;
}

public class Take_Rank : MonoBehaviour
{
    const string url = "https://script.google.com/macros/s/AKfycbyTGzX7OG5bGoMid1V4N5hSYR7CYLTr4KM3LRx42bCapTseOVgnc2rS-H_V8_Ox9xbZTg/exec";   
    
    string[] jsonData;

    public List<RankData> RankData = new List<RankData>();
    public List<GameObject> RankText;
    public TextMeshProUGUI NameText;

    public GameObject Loading;
    
    public void GetData()
    {
        StartCoroutine(Get());
    }
    IEnumerator Get()
    {
        Loading.SetActive(true);

        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        string data = www.downloadHandler.text;
        
        jsonData = data.Split(',');
        
        RankData = new List<RankData>();
        for(int i = 0; i < jsonData.Length-1; i+=2)
        {
            // print(jsonData[i] + " : " + jsonData[i+1]);
            RankData.Add(new RankData(){Score = int.Parse(jsonData[i]), Name = jsonData[i+1]});
        }

        // print(data);

        RefreshText();

        Loading.SetActive(false);

    }    

    public void Register(int _score, string _name)
    {
        WWWForm form = new WWWForm();
        form.AddField("names",_name);
        form.AddField("score", ""+_score);
        StartCoroutine(Post(form));
        
    }

    IEnumerator Post(WWWForm _form)
    {
        using(UnityWebRequest www = UnityWebRequest.Post(url, _form))
        {
            
            yield return www.SendWebRequest();

            if(www.isDone)
            {
                print("성공");
            }
            else
            {
                print("응답 없음...");
            }

        }
    }


    public void RefreshText()
    {
        for(int i = 0; i < RankText.Count; i++)
        {
            TextMeshProUGUI ranktxt = RankText[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI nametxt = RankText[i].transform.GetChild(2).GetComponent<TextMeshProUGUI>();

            ranktxt.text = "" + RankData[i].Score;
            nametxt.text = RankData[i].Name;            
        }
        // ScoreText.text = "";
        // NameText.text = "";
        // foreach(var rk in RankData)
        // {
        //     string scoretxt = rk.Score.ToString("D8");
            
        //     ScoreText.text += (scoretxt + "\n" + "\n");

        //     NameText.text += (rk.Name + "\n" + "\n");
        // }

    }

    
}
