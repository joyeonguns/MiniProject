using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class map_Ui_Manager : MonoBehaviour
{

    // 지도 이미지
    public Image Map_IMG;

    int minHight = -1600;
    int maxHight = 1300;

    // 지도 스크롤 속도
    public float scroll_Speed = 200;
    // Start is called before the first frame update
    void Start()
    {                
        //SaveData sb = GameObject.Find("GameManager").GetComponent<SaveData>(); 

        GetComponent<MapDrowing>().MapDrow();
        Map_IMG.rectTransform.anchoredPosition += GameManager.instance.floor * new Vector2(0,-200);
    }

    // Update is called once per frame
    void Update()
    {
        MapControll();
    }

     void MapControll()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel") * scroll_Speed;
        
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
