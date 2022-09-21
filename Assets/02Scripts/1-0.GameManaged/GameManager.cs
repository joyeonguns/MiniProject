using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using System.Linq;






public class GameManager : MonoBehaviour
{
    // 싱클턴 클래스
    public static GameManager instance = null;


    // 현재 맵 정보   
    MapClass cur_Map;
    public MapClass _CurMap{ get{return cur_Map;} set{cur_Map = value;}}
    
    // 전체 맵 정보
    public List<List<MapClass>> maps = new List<List<MapClass>>();
    public int row, col,floor;


    // 특성 리스트
    public List<List<TellentsScripts>> Tellents = new List<List<TellentsScripts>>();

    // 보상
    public ResultClass ResultData = new ResultClass();
    // 골드
    public int curGold = 10000;
    // 아이템
    public int[] ItemList_num = {0,0,0};

    // 파티
    public List<Save.Player> MyParty = new List<Save.Player>();
    
    // 싱글턴 인스턴스
    private void Awake() 
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject); 
            this.gameObject.SetActive(true);
        }
        else if(instance != this)
        {
            //instance = null;
            Destroy(this.gameObject);
        }  
    }


    // Start is called before the first frame update
    void Start()
    {
        ItemList_num[0] = 1;
        ItemList_num[1] = 2;

        // TellentSeed 설정

        Tellents.Add(new List<TellentsScripts>());
        Tellents.Add(new List<TellentsScripts>());
        Tellents.Add(new List<TellentsScripts>());
        Tellents.Add(new List<TellentsScripts>());

        Tellents[0].Add(new TellentsScripts(Etel_Rank.C,1));
        Tellents[0].Add(new TellentsScripts(Etel_Rank.C,10));
        Tellents[1].Add(new TellentsScripts(Etel_Rank.B,14));
        Tellents[2].Add(new TellentsScripts(Etel_Rank.A,1));
     
    }    
    
    // Update is called once per frame
    void Update()
    {

    }    
    public void SetTellent(TellentsScripts newTellent)
    {
       Tellents[(int)newTellent.Rank].Add(newTellent);
    }

    // 맵 정보
    public void SetData(int _col, int _row, List<List<MapClass>> _maps, MapClass _curMaps)
    {              
        maps = _maps;
        row = _row;
        col = _col;
        cur_Map = _curMaps.CopyMapClass();
    }

    public void SetCurMap(int a, int b)
    {
        cur_Map = maps[a][b];
        floor = cur_Map.floor;
    }
    
}
