using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapClass : MonoBehaviour
{
    public MapClass(){
        mapData = 5;
        isLife = false;
    }

    public void SetMapData(int a){
        mapData = a;
    }

    public void SetNextMap(int a, int b, int c){
        roots[0] = a;
        roots[1] = b;
        roots[2] = c;
    }

    public MapClass CopyMapClass()
    {
        MapClass _m = new MapClass();
        _m.mapData = this.mapData;
        _m.roots = (int[])this.roots.Clone();
        _m.floor = this.floor;
        _m.isLife = this.isLife;

        return _m;
    }
    


    // 0 : 전투, 1 : 상점, 2 : 회복, 3 : 특성, 4 : 랜덤, 5 : null, 6 : 중간보스
    public int mapData;
    public int[] roots = new int[3] {9,9,9};
    public int floor = 0;
    public bool isLife;

    
}
