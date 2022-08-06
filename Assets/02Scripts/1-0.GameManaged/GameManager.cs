using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using System.Linq;

public enum ResultEnum {NomalBattle, EliteBattle, BossBattle, Run}

// 전투 보상
public class ResultClass
{
    public ResultEnum ResultMode = ResultEnum.NomalBattle;
    public int Gold;
    public int Exp;
    public int ItemRate;
    public string TellentRank;
}


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

    // 현재 전투 난이도
    public int Battle_Lvl = 1;
    public int BattleType = 1; 

    // 특성 리스트
    public List<List<TellentsScripts>> Tellents = new List<List<TellentsScripts>>();
    // public List<TellentsScripts> Tellents_C = new List<TellentsScripts>();
    // public List<TellentsScripts> Tellents_B = new List<TellentsScripts>();
    // public List<TellentsScripts> Tellents_A = new List<TellentsScripts>();
    // public List<TellentsScripts> Tellents_S = new List<TellentsScripts>();

    // 보상
    public ResultClass ResultData = new ResultClass();
    // 골드
    public int curGold = 10000;
    // 아이템
    public int[] ItemList_num = {0,0,0};
    
    // 싱글턴 인스턴스
    private void Awake() 
    {
        instance = this;
        //cur_Map = new MapClass();
        DontDestroyOnLoad(this.gameObject); 
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

        // Tellents_C.Add(new TellentsScripts(Etel_Rank.C,1));
        // Tellents_C.Add(new TellentsScripts(Etel_Rank.C,10));
        // Tellents_B.Add(new TellentsScripts(Etel_Rank.B,14));
        // Tellents_A.Add(new TellentsScripts(Etel_Rank.A,1));
     
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
        Debug.Log("GM : " + cur_Map.floor);
    }
    
}
