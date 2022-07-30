using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TownManager : MonoBehaviour
{
    public GameObject BackGround;
    public GameObject[] Characters = new GameObject[4];
    public Button[] CharacterBtn = new Button[4];
    public GameObject SkillPannel;
    public GameObject[] SkillText = new GameObject[4];
    public Sprite[] CharImage = new Sprite[4];

    Save_Charater_Data SaveData;
    Save.Player[] NewCharacters = new Save.Player[4];
    int SelectNum;
    bool bSelect;


    // Start is called before the first frame update
    void Start()
    {
        CharacterBtn[0].onClick.AddListener(() => {
            ClickCharBtn(0);
        });
        CharacterBtn[1].onClick.AddListener(() => {
            ClickCharBtn(1);
        });
        CharacterBtn[2].onClick.AddListener(() => {
            ClickCharBtn(2);
        });
        CharacterBtn[3].onClick.AddListener(() => {
            ClickCharBtn(3);
        });        


        BackGround.SetActive(false);
        SkillPannel.SetActive(false);
        SaveData = GameManager.instance.GetComponent<Save_Charater_Data>();

        SetCharData();

    }

    void SetCharData()
    {
        for(int i = 0; i < 4; i++)
        {
            e_Class newClass = e_Class.worrier;
            Save.St_Stat newStatus = Save.Worrier;
            int rnd = Random.Range(0,4);
            
            switch (rnd)
            {
                case 0:
                newClass = e_Class.worrier;
                newStatus = Save.Worrier;
                Characters[i].GetComponent<Image>().sprite = CharImage[rnd];
                break;

                case 1:
                newClass = e_Class.magicion;
                newStatus = Save.Magicion;
                Characters[i].GetComponent<Image>().sprite = CharImage[rnd];
                break;
                
                case 2:
                newClass = e_Class.supporter;
                newStatus = Save.Supporter;
                Characters[i].GetComponent<Image>().sprite = CharImage[rnd];
                break;
                
                case 3:
                newClass = e_Class.assassin;
                newStatus = Save.Assassin;
                Characters[i].GetComponent<Image>().sprite = CharImage[rnd];
                break;                
            }
            NewCharacters[i] = new Save.Player(newStatus,newClass);
            var charName = Characters[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>();
            charName.text = "[" + NewCharacters[i].name + "]";
        }
    }

    public void ClickGuild()
    {
        BackGround.SetActive(true);        
    }

    public void ClickRecruit_Exit()
    {
        BackGround.SetActive(false);
    }


    public void ClickCharBtn(int n)
    {        
        foreach (var Btn in CharacterBtn)
        {            
            Btn.GetComponent<Image>().color = Color.white;            
        }
        Debug.Log("n : " + n);
        Debug.Log("count : " + CharacterBtn.Length);
        CharacterBtn[n].GetComponent<Image>().color = Color.black;     

        SkillPannel.SetActive(true);

        SkillText[0].GetComponent<TextMeshProUGUI>().text = "Nomal";
        SkillText[1].GetComponent<TextMeshProUGUI>().text = "Sk_"+ NewCharacters[n].SkillNum[1];
        SkillText[2].GetComponent<TextMeshProUGUI>().text = "Sk_"+ NewCharacters[n].SkillNum[2];
        SkillText[3].GetComponent<TextMeshProUGUI>().text = "Ulti";
        

        SelectNum = n;
        bSelect = true;           
    }

    public void ClickRecruit()
    {
       if(3 > SaveData.MyParty.Count && bSelect == true)
       {
            SaveData.MyParty.Add(NewCharacters[SelectNum]);
            Characters[SelectNum].SetActive(false);
            bSelect = false;
            SkillPannel.SetActive(false);

       } 
    }



}
