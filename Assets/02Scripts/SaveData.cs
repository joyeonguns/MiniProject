using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SaveData : MonoBehaviour
{
    public List<List<MapClass>> maps;
    public int col, row, floor;

    //[Monitor]
    MapClass curMaps = new MapClass();
    public string mapName;

   
    public MapClass getCurMaps()
    {
        return curMaps;
    }
    public void SaveMap(int _col, int _row, List<List<MapClass>> _maps, MapClass _curMaps)
    {
        col = _col;
        row = _row;
        maps = _maps;


        curMaps = _curMaps.CopyMapClass();
        if(!this.curMaps)
        {
            Debug.Log("curMaps : " + curMaps.floor );
            Debug.Log("!sb.curMaps");
        }
  
        
    }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);    
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       if( Input.GetKeyDown(KeyCode.Q))
       {
           curMaps = new MapClass();
       }
       if(this.curMaps)
        {
            Debug.Log("sb.curMaps");
        }
    }
}
