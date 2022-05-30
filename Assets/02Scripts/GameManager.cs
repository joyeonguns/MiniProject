using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameManager : MonoBehaviour
{
    public Image Map_IMG;
    public float scroll_Speed = 200;


    public MapClass cur_Map;
    
    List<List<MapClass>> maps = new List<List<MapClass>>();


    private void Awake() 
    {
        cur_Map = new MapClass();
        cur_Map.floor = 0;    
    }

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        MapControll();
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


    void MapControll()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel") * scroll_Speed;
        int minHight = -1200;
        int maxHight = 1300;
        Vector2 Map_vec = new Vector2();
        Map_vec = Map_IMG.GetComponent<RectTransform>().anchoredPosition;

        if(Map_vec.y < minHight) Map_vec.y = minHight;
        else if(Map_vec.y > maxHight) Map_vec.y = maxHight;
        
        // 위로 
        if( scroll_Speed > 0)
        {            
            Map_vec = new Vector2(Map_vec.x,Map_vec.y-scroll);

            Map_IMG.GetComponent<RectTransform>().anchoredPosition = Map_vec;
        }
        // 아래로
        if( scroll_Speed < 0)
        {
            Map_vec = new Vector2(Map_vec.x,Map_vec.y-scroll);

            Map_IMG.GetComponent<RectTransform>().anchoredPosition = Map_vec;
        }
    }
}
