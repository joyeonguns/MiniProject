using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;


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
    public List<TellentsScripts> BeforTellents;
    public List<int> BeforTellents_num;

    // GetAfter
    public List<TellentsScripts> GetAfterTellents;
    public List<int> GetAfterTellents_num;

    // BBTellent
    public List<TellentsScripts> BBTellent;
    public List<int> BBTellent_num;

    // ABTelent
    public List<TellentsScripts> ABTelent;
    public List<int> ABTelent_num;


    // 보상
    public int gold;
    public int ItemRate;
    
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
        // 특성계수 할당
        for(int i = 0; i < 4; i++)
        {
            BeforTellents_num.Add(i);
            GetAfterTellents_num.Add(i);
            BBTellent_num.Add(i);
            ABTelent_num.Add(i);
        }       
    }    
    
    // Update is called once per frame
    void Update()
    {
        if(cur_Map)
            floor = cur_Map.floor;
    }

    // 특성계수에 따른 할당 
    public void SetTellent()
    {
        BeforTellents.Clear();
        BBTellent.Clear();
        ABTelent.Clear();
        for(int i = 0; i < 4; i++)
        {
            BeforTellents.Add(new TellentsScripts(Etel_type.beforeTurn,i));
            BBTellent.Add(new TellentsScripts(Etel_type.beforBattle,i));
            ABTelent.Add(new TellentsScripts(Etel_type.AfterBattle,i));
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
