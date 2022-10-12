using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Linq;
using System.IO;


public class Start_Ui_Manger : MonoBehaviour
{
    public GameObject go_CreateMap;

    public GameObject ReStartBtn;

    private void Start()
    {
        if (File.Exists(SaveDataManager.instance.path))
        {
            ReStartBtn.SetActive(true);            
        }
        else
        {
            ReStartBtn.SetActive(false);
        }
    }

    public void OnClickReStart()
    {        
        
        SaveDataManager.instance.LoadData();
        // 맵 로드
        List<MapClass> rowData = new List<MapClass>();

        for(int i = 0; i < 48; i++)
        {
            int col = i % 4;
            if( i != 0 && col == 0)
            {
                GameManager.instance.maps.Add(rowData);
                rowData = new List<MapClass>();
                rowData.Add(SaveDataManager.instance.nowSave.mapsJson[i]);
            }
            else
            {
                rowData.Add(SaveDataManager.instance.nowSave.mapsJson[i]);
            }                     
        }
        GameManager.instance.maps.Add(rowData);
        GameManager.instance.row = 12;
        GameManager.instance.col = 4;
        GameManager.instance._CurMap = SaveDataManager.instance.nowSave.nowMap;

        // 파티 로드
        GameManager.instance.MyParty = SaveDataManager.instance.nowSave.character;
        foreach(var pl in GameManager.instance.MyParty)
        {
            pl.SetSkillClass();
        }

        // 특성 로드
        GameManager.instance.Tellents[0].Clear();
        GameManager.instance.Tellents[1].Clear();
        GameManager.instance.Tellents[2].Clear();
        GameManager.instance.Tellents[3].Clear();

        foreach (var tel in SaveDataManager.instance.nowSave.tellent_C)
        {
            GameManager.instance.Tellents[0].Add(new TellentsScripts(tel.Rank,tel.Code));
        }
        foreach (var tel in SaveDataManager.instance.nowSave.tellent_B)
        {
            GameManager.instance.Tellents[1].Add(new TellentsScripts(tel.Rank,tel.Code));
        }
        foreach (var tel in SaveDataManager.instance.nowSave.tellent_A)
        {
            GameManager.instance.Tellents[2].Add(new TellentsScripts(tel.Rank,tel.Code));
        }
        foreach (var tel in SaveDataManager.instance.nowSave.tellent_S)
        {
            GameManager.instance.Tellents[3].Add(new TellentsScripts(tel.Rank,tel.Code));
        }

        // 아이템 로드
        GameManager.instance.ItemList_num = SaveDataManager.instance.nowSave.itemCodes;

        
        
        Debug.Log($"tel_0 num : { GameManager.instance.Tellents[0][0].Rank}, {GameManager.instance.Tellents[0][0].Code}");

        for(int i = 0; i < GameManager.instance.MyParty.Count; i++)
        {
            

            GameManager.instance.MyParty[i].ApplyGetTellent((GameManager.instance.MyParty).Cast<Save.Character>().ToList(), i, GameManager.instance.MyParty.Cast<Save.Character>().ToList(), 0);
        }

        // 씬로드          
        SceneManager.LoadScene(2);  
    }
    
    public void StartBtn()
    {
        
        // 맵 제작 , 맵 정보 세이브
        go_CreateMap.GetComponent<CreateMap>().MapCreate();
        //GameManager.instance.SetData();        

        

        // 1-1.TownScene

        for(int i = 0; i < GameManager.instance.MyParty.Count; i++)
        {
            GameManager.instance.MyParty[i].ApplyGetTellent((GameManager.instance.MyParty).Cast<Save.Character>().ToList(), i, GameManager.instance.MyParty.Cast<Save.Character>().ToList(), 0);
        }

        // 씬 로드
        SceneManager.LoadScene("1-1.TownScene");        
    }

    public void ExitBtn()
    {
        Application.Quit();
        Debug.Log("End Game");
    }
}
