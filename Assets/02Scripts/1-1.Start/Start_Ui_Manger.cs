using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Start_Ui_Manger : MonoBehaviour
{
    public GameObject go_CreateMap;
    public void StartBtn()
    {
        
        // 맵 제작 , 맵 정보 세이브
        go_CreateMap.GetComponent<CreateMap>().MapCreate();
        //GameManager.instance.SetData();        

        // 씬 로드
        SceneManager.LoadScene("1-2.MapScene");        
    }

    public void ExitBtn()
    {
        Application.Quit();
        Debug.Log("End Game");
    }
}
