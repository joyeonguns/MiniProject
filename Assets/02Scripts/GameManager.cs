using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameManager : MonoBehaviour
{
    // 싱클턴 클래스
    public static GameManager instance = null;

    // 세이브 데이터    
    SaveData I_SaveData;


    MapClass cur_Map = new MapClass() ;
    public MapClass getCurMap() {return cur_Map;}
    
    public List<List<MapClass>> maps = new List<List<MapClass>>();
    public int row, col;

    // 맵그리기
    // public GameObject go_mapDrow;

    private void Awake() 
    {
        instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        
        
    }    
    public void SetData()
    {        
        //Debug.Log("SetData" + GetComponent<SaveData>().curMaps.floor);
        maps = GetComponent<SaveData>().maps;
        row = GetComponent<SaveData>().row;
        col = GetComponent<SaveData>().col;
        cur_Map = new MapClass();
        cur_Map = GetComponent<SaveData>().getCurMaps();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetMapList(List<List<MapClass>> _maps)
    {
        maps = _maps;
    }
    public void SetCurMap(int a, int b)
    {
        cur_Map = maps[a][b];
        Debug.Log("GM : " + cur_Map.floor);
    }


    
}
