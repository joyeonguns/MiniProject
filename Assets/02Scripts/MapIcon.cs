using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MapIcon : MonoBehaviour
{
    public TextMeshProUGUI Mapname;
    GameManager GM;
    Button _btn;
    Image img;

    public int x = 1, y = 2;
    public MapClass map;



    void Awake()
    {

        img = GetComponent<Image>();
        _btn = GetComponent<Button>();
    }

    // Start is called before the first frame update
    void Start()
    {
        GM = GameManager.instance;

        // 아이콘 텍스트;
        if (x < 13)
        {
            SetText(map.mapData);
            SetImage();
        }

        // 버튼 활성화 false
        _btn.interactable = false;
    }
    public void SetText(int a, int b)
    {
        x = a;
        y = b;
        Mapname.text = "" + x + " , " + y;
    }
    public void SetText(int a)
    {
        string str = "";
        if (a == 0) str = " M ";
        else if (a == 1) str = "Shop";
        else if (a == 2) str = "Camp";
        else if (a == 3) str = "Tellent";
        else if (a == 4) str = " ? ";
        else if (a == 6) str = "Named";

        Mapname.text = str;
    }
    void SetImage()
    {
        int m = map.mapData;
        if (m == 0) img.color = Color.white;
        else if (m == 1) img.color = Color.blue;
        else if (m == 2) img.color = Color.green;
        else if (m == 3) img.color = Color.yellow;
        else if (m == 4) img.color = Color.black;
        else if (m == 6) img.color = Color.red;
    }

    // Update is called once per frame
    void Update()
    {

        // 버튼 활성화
        if (GM.getCurMap().floor == x - 1)
        {
            // 왼쪽 길
            if (GM.getCurMap().roots[0] == y || GM.getCurMap().roots[1] == y || GM.getCurMap().roots[2] == y)
            {
                _btn.interactable = true;
            }
            else if (x == 1)
            {
               // 가운대
                _btn.interactable = true;
            }
            else
            {
               // 오른쪽
                _btn.interactable = false;
            }
        }
        else
        {
           
            _btn.interactable = false;
        }
    }



    public void ClickBTN()
    {

        Debug.Log("set : " + x + " " + y);
        GM.SetCurMap(x - 1, y);

    }
}
