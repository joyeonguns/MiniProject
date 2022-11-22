using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMap : MonoBehaviour
{
    // public GameObject m_Btn;
    // public GameObject m_root;
    public GameObject _SaveData;

    List<List<MapClass>> maps = new List<List<MapClass>>();
    int row = 12;
    int col = 4;

    // 맵 제작
    void CreateMapData()
    {
        // col * row 행렬
        // 6층은 중간 보스, 8층 회복
        // mapData  0 : 전투, 1 : 상점, 2 : 회복, 3 : 특성, 4 : 랜덤, 5 : null, 6 : 중간보스
        for(int i = 0; i < row; i++)
        {
            List<MapClass> mapRow = new List<MapClass>();
            for(int j = 0; j < col; j++)
            {
                MapClass m = new MapClass();
                m.floor = i+1;
                mapRow.Add(m);
                // 맵 상태 결정
                switch (m.floor)
                {
                    case 1:
                        m.mapData = 0;
                        break;
                    case 3:
                        m.mapData = 4;
                        break;
                    case 6:
                        m.mapData = 6;
                        break;
                    case 8:
                        m.mapData = 2;
                        break;
                    default:
                        m.mapData = set_defMapData();
                        break;

                }
            }

            maps.Add(mapRow);
        }

        // Dfs 를 통한 맵
        for (int i = 0; i < col; i++) 
        {		
            int start = i;
            int _row = 0;
            while (_row < row)
            {		
                maps[_row][start].isLife = true;
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
        // 루트
        for (int i = 0; i < col; i++) 
        {
            for (int j = 0; j < row; j++) 
            {
                if (i - 1 >= 0 && maps[j][i-1].roots[2] != 9 && maps[j][i].roots[0] != 9) {
                    maps[j][i-1].roots[1] = i-1;
                    maps[j][i].roots[1] = i;
                    maps[j][i-1].roots[2] = 9;
                    maps[j][i].roots[0] = 9;
                }                    
            }
	    }        
    }

    int set_defMapData()
    {
        int rnd = Random.Range(1,11);
        switch (rnd)
        {
            case 1:
                return 1;
            case 2:
                return 2;
            case 3:
                return 4;;
            case 4:
                return 4;
            default:
                return 0;
        }
    }
    public void MapCreate()
    {  
        // 맵 생성      
        maps.Clear();
        CreateMapData();

        // 맵 정보 저장        
        MapClass cur = new MapClass();
        cur.floor = 0;
        GameManager.instance.SetData(col,row,maps,cur);       
    }

    public void MapLog()
    {
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
    }
}
