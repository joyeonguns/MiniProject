using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapDrowing : MonoBehaviour
{
    // 맵 아이콘
    public GameObject m_Icon;
    // 맵 길
    public GameObject m_root;
    // 게임 매니져
    public GameManager GM;
    public Sprite[] RoomIcon;
    public GameObject RoomParents;
    public GameObject RootParents;

    // GM 에서 가져올 맵 정보
    int row, col;
    List<List<MapClass>> maps = new List<List<MapClass>>();


    void saveMapData()
    {
        maps = GM.maps;
        row = GM.row;
        col = GM.col;
    }

    // 맵 UI
    public void MapDrow()
    {
        GM = GameManager.instance;
        saveMapData();
        float sp_X = -300;
        float sp_Y = -1300;
        for (int x = 0; x < row; x++)
        {
            sp_X = -300;
            for (int y = 0; y < col; y++)
            {
                if (maps[x][y].isLife == true)
                {
                    Vector2 spwVec = new Vector2(sp_X, sp_Y);
                    var sp_MapBTN = Instantiate(m_Icon);

                    sp_MapBTN.transform.SetParent(RoomParents.transform);
                    //sp_MapBTN.transform.Scale = new Vector2(1.5f,1.5f);
                    sp_MapBTN.GetComponent<RectTransform>().anchoredPosition = spwVec;
                    sp_MapBTN.GetComponent<MapIcon>().SetText(x + 1, y);
                    sp_MapBTN.GetComponent<MapIcon>().map = maps[x][y];
                    sp_MapBTN.GetComponent<Image>().sprite = RoomIcon[maps[x][y].mapData];

                    if (x != row - 1)
                    {
                        if (maps[x][y].roots[0] != 9)
                        {
                            var sp_Map_root = Instantiate(m_root);
                            sp_Map_root.transform.SetParent(RootParents.transform);

                            sp_Map_root.GetComponent<RectTransform>().rotation = Quaternion.Euler(0f, 0f, 45);
                            sp_Map_root.GetComponent<RectTransform>().anchoredPosition = spwVec + new Vector2(-100, 100);

                            sp_Map_root.GetComponent<RectTransform>().localScale *= 1.42f;
                        }
                        if (maps[x][y].roots[1] != 9)
                        {
                            var sp_Map_root = Instantiate(m_root);
                            sp_Map_root.transform.SetParent(RootParents.transform);

                            sp_Map_root.GetComponent<RectTransform>().anchoredPosition = spwVec + new Vector2(0, 100);
                        }
                        if (maps[x][y].roots[2] != 9)
                        {
                            var sp_Map_root = Instantiate(m_root);
                            sp_Map_root.transform.SetParent(RootParents.transform);

                            sp_Map_root.GetComponent<RectTransform>().rotation = Quaternion.Euler(0f, 0f, -45f);
                            sp_Map_root.GetComponent<RectTransform>().anchoredPosition = spwVec + new Vector2(100, 100);

                            sp_Map_root.GetComponent<RectTransform>().localScale *= 1.42f;
                        }
                    }
                    else
                    {
                        var sp_Map_root = Instantiate(m_root);
                        sp_Map_root.transform.SetParent(RootParents.transform);

                        sp_Map_root.GetComponent<RectTransform>().anchoredPosition = spwVec + new Vector2(0, 100);
                    }

                }
                sp_X += 200;
            }
            sp_Y += 200;
        }
    }


    // Update is called once per frame
    void Update()
    {

    }
}
