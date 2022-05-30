using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMap : MonoBehaviour
{
    public GameObject m_Btn;
    public GameObject m_root;
    public GameManager GM;

    List<List<MapClass>> maps = new List<List<MapClass>>();
    int row = 12;
    int col = 4;

    // 맵 제작
    void CreateMapData()
    {
        for(int i = 0; i < row; i++)
        {
            List<MapClass> mapRow = new List<MapClass>();
            for(int j = 0; j < col; j++)
            {
                MapClass m = new MapClass();
                m.floor = i+1;
                mapRow.Add(m);                 
            }

            maps.Add(mapRow);
        }

        for (int i = 0; i < col; i++) 
        {
		
            int start = i;
            int _row = 0;
            while (_row < row)
            {		
                maps[_row][start].mapData = 0;
                int rnd = Random.Range(-1,2);
                int n = rnd + start;

                if (n > col-1) n = col-1;
                else if (n < 0) n = 0;

                if ((n - start) == 0) {
                    maps[_row][start].roots[1] = n;
                }
                // start -1
                else if ((n - start) < 0) {
                     maps[_row][start].roots[0] = n;
                }
                // start +1
                else {
                     maps[_row][start].roots[2] = n;
                }                

                start = n;
                _row++;		
            }

	    }

        for (int i = 0; i < col; i++) 
        {
            for (int j = 0; j < row; j++) {
                if (i - 1 >= 0 && maps[j][i-1].roots[2] != 9 && maps[j][i].roots[0] != 9) {
                    maps[j][i-1].roots[1] = i-1;
                    maps[j][i].roots[1] = i;
                    maps[j][i-1].roots[2] = 9;
                    maps[j][i].roots[0] = 9;
                }
                    
            }
	    }
        
    }

    // 맵 UI
    void MapUi()
    {
        float sp_X = -400;
        float sp_Y = -1300;
        for(int x = 0; x < row; x++)
        {
           sp_X = -400;
           for(int y = 0; y < col; y++)
           {
               if(maps[x][y].mapData != 5)
               {
                   Vector2 spwVec = new Vector2(sp_X,sp_Y);
                   var sp_MapBTN = Instantiate(m_Btn);

                   sp_MapBTN.transform.parent = GameObject.Find("Map_IMG").transform;
                   //sp_MapBTN.transform.Scale = new Vector2(1.5f,1.5f);
                   sp_MapBTN.GetComponent<RectTransform>().anchoredPosition = spwVec;
                   sp_MapBTN.GetComponent<MapBurron>().SetText(x+1,y);

                   

                   if(maps[x][y].roots[0] != 9)
                   {
                       var sp_Map_root = Instantiate(m_root);
                       sp_Map_root.transform.parent = GameObject.Find("Map_IMG").transform;
                       
                       sp_Map_root.GetComponent<RectTransform>().rotation = Quaternion.EulerAngles(0f, 0f, 45);
                       sp_Map_root.GetComponent<RectTransform>().anchoredPosition = spwVec + new Vector2(-100,100);
                       
                       sp_Map_root.GetComponent<RectTransform>().localScale *= 1.42f;
                   }
                   if(maps[x][y].roots[1] != 9)
                   {
                       var sp_Map_root = Instantiate(m_root);
                       sp_Map_root.transform.parent = GameObject.Find("Map_IMG").transform;
                       
                       sp_Map_root.GetComponent<RectTransform>().anchoredPosition = spwVec + new Vector2(0,100);
                   }
                   if(maps[x][y].roots[2] != 9)
                   {
                       var sp_Map_root = Instantiate(m_root);
                       sp_Map_root.transform.parent = GameObject.Find("Map_IMG").transform;
                       
                       sp_Map_root.GetComponent<RectTransform>().rotation = Quaternion.EulerAngles(0f, 0f, -45);
                       sp_Map_root.GetComponent<RectTransform>().anchoredPosition = spwVec + new Vector2(100,100);
                       
                       sp_Map_root.GetComponent<RectTransform>().localScale *= 1.42f;
                   }                
               }
               sp_X += 200;
           }
           sp_Y += 200;
        }
    }

    public void MapCreateBtn()
    {        
        maps.Clear();
        CreateMapData();
        int i = 1;
        foreach(var mapRow in maps)
        {
            string number = i.ToString("D2"); 
            Debug.Log( number + ":    " + mapRow[0].mapData+"     "+mapRow[1].mapData+"     "+mapRow[2].mapData+"     "+mapRow[3].mapData);
            Debug.Log( number + ": " + mapRow[0].roots[0]+""+ mapRow[0].roots[1]+""+mapRow[0].roots[2]+
            " "+ mapRow[1].roots[0]+""+ mapRow[1].roots[1]+""+mapRow[1].roots[2]+
            " "+ mapRow[2].roots[0]+""+ mapRow[2].roots[1]+""+mapRow[2].roots[2]+
            " "+ mapRow[3].roots[0]+""+ mapRow[3].roots[1]+""+mapRow[3].roots[2]);
            i++;
        }
        i = 1;
        
        GM.SetMapList(maps);

        MapUi();
    }
}
