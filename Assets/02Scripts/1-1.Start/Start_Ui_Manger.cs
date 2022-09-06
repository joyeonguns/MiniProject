using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Linq;

public class Start_Ui_Manger : MonoBehaviour
{
    public GameObject go_CreateMap;
    public void StartBtn()
    {
        
        // 맵 제작 , 맵 정보 세이브
        go_CreateMap.GetComponent<CreateMap>().MapCreate();
        //GameManager.instance.SetData();        

        // 씬 로드
        SceneManager.LoadScene("1-1.TownScene");        

        // 1-1.TownScene

        for(int i = 0; i < GameManager.instance.MyParty.Count; i++)
        {
            GameManager.instance.MyParty[i].ApplyGetTellent((GameManager.instance.MyParty).Cast<Save.Character>().ToList(), i, GameManager.instance.MyParty.Cast<Save.Character>().ToList(), 0);
        }
    }

    public void ExitBtn()
    {
        Application.Quit();
        Debug.Log("End Game");
    }
}
