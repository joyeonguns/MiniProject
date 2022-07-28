using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MapIcon : MonoBehaviour
{
    public TextMeshProUGUI Mapname;
    GameManager GM;
    Button _btn;
    Image img;

    public int x = 1, y = 2;
    public MapClass map;

    public int floor;

    void Awake()
    {
        img = GetComponent<Image>();
        _btn = GetComponent<Button>();
    }

    // Start is called before the first frame update
    void Start()
    {
        GM = GameManager.instance;
        floor = GM._CurMap.floor;

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
        if (GM._CurMap.floor == x - 1)
        {
            // 연결된 길이면 활성화
            if (GM._CurMap.roots[0] == y || GM._CurMap.roots[1] == y || GM._CurMap.roots[2] == y)
            {
                _btn.interactable = true;
            }
            else if (x == 1)
            {
               // 1 층이면 활성화
                _btn.interactable = true;
            }
            else if (x == 13)
            {
               // 13 층이면 활성화
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

        string str = "";
        if (map.mapData == 0) str = "2-0.BattleScene";
        else if (map.mapData == 1) str = "2-2.ShpScene";
        else if (map.mapData == 2) str = "2-1.CampScene";
        else if (map.mapData == 3) str = "2-4.GiftScene";
        else if (map.mapData == 4) str = "2-3.RandomScene";
        else if (map.mapData == 6) str = "2-0.BattleScene";


        //str = "4-0.BossScene";
        HUDManager.instance.SetFloor();
        SceneManager.LoadScene(str);
    }
}
