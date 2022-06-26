using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class RewardManager : MonoBehaviour
{
    public GameObject TellentsPannel;
    public GameObject[] Tellents;
    public int Tellent;

    
    public GameObject GoldPannel;
    public GameObject Coments;
    public int Gold;
    public int Item;

    public GameObject ResultPannel;
    public GameObject[] Player;
    public Image[] Player_Exp;
    public TextMeshProUGUI GainExp;



    // Start is called before the first frame update
    void Start()
    {
        TellentsPannel.SetActive(true);
        GoldPannel.SetActive(false);
        ResultPannel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

    // 버튼
    public void Next_Tellent_Btn()
    {
        TellentsPannel.SetActive(false);
        GoldPannel.SetActive(true);
    }
    public void Next_Gold_Btn()
    {
        GoldPannel.SetActive(false);
        ResultPannel.SetActive(true);
    }
    public void Next_Scene()
    {
        TellentsPannel.SetActive(false);
        GoldPannel.SetActive(true);
    }


}
