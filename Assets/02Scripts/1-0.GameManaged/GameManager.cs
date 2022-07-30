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
    // BeforTellents
    public List<TellentsScripts> BeforTellents = new List<TellentsScripts>();
    public List<int> BeforTellents_num;

    // GetAfter
    public List<TellentsScripts> GetAfterTellents = new List<TellentsScripts>();
    public List<int> GetAfterTellents_num;

    // BBTellent
    public List<TellentsScripts> BBTellent = new List<TellentsScripts>();
    public List<int> BBTellent_num;

    // ABTelent
    public List<TellentsScripts> ABTelent = new List<TellentsScripts>();
    public List<int> ABTelent_num;


    public List<TellentsScripts> TellentSeed = new List<TellentsScripts>();

    


    // 보상
    public ResultClass ResultData = new ResultClass();
    // 골드
    public int curGold = 10000;
    // 아이템
    public List<int> ItemList_num;
    
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
        // TellentSeed 설정
        for(int i = 0; i < 4; i++)
        {
            TellentSeed.Add(new TellentsScripts(Etel_type.AfterBattle,i));
            TellentSeed.Add(new TellentsScripts(Etel_type.beforBattle,i));
            TellentSeed.Add(new TellentsScripts(Etel_type.beforeTurn,i));
            TellentSeed.Add(new TellentsScripts(Etel_type.getAfter,i));
        }
        System.Random rnd = new System.Random();

        TellentSeed = TellentSeed.OrderBy(item => rnd.Next()).ToList<TellentsScripts>();


        // 특성계수 할당
        for(int i = 0; i < 4; i++)
        {
            Etel_type teltype = TellentSeed[0].type;
            switch (teltype)
            {
                case Etel_type.AfterBattle : 
                ABTelent.Add(TellentSeed[0]);
                ABTelent_num.Add(TellentSeed[0].tel_num);
                break;

                case Etel_type.beforBattle : 
                BBTellent.Add(TellentSeed[0]);
                BBTellent_num.Add(TellentSeed[0].tel_num);
                break;

                case Etel_type.beforeTurn : 
                BeforTellents.Add(TellentSeed[0]);
                BeforTellents_num.Add(TellentSeed[0].tel_num);
                break;

                case Etel_type.getAfter : 
                GetAfterTellents.Add(TellentSeed[0]);
                GetAfterTellents_num.Add(TellentSeed[0].tel_num);
                break;
            }
            TellentSeed.RemoveAt(0);
        }       
    }    
    
    // Update is called once per frame
    void Update()
    {

    }

    // 특성계수에 따른 할당 
    public void SetTellent()
    {
        if(BeforTellents.Count != 0) BeforTellents.Clear();
        if(BBTellent.Count != 0) BBTellent.Clear();
        if(ABTelent.Count != 0) ABTelent.Clear();
        for(int i = 0; i < 4; i++)
        {
            BeforTellents.Add(new TellentsScripts(Etel_type.beforeTurn,i));
            //BBTellent.Add(new TellentsScripts(Etel_type.beforBattle,i));
            //ABTelent.Add(new TellentsScripts(Etel_type.AfterBattle,i));
        }        
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
