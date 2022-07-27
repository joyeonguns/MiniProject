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

    Save_Charater_Data Save;
    Save_Charater_Class.SD[] NewCharacters = new Save_Charater_Class.SD[4];
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
        Save = GameManager.instance.GetComponent<Save_Charater_Data>();

        SetCharData();

    }

    void SetCharData()
    {
        for(int i = 0; i < 4; i++)
        {
            e_Class newClass = e_Class.worrier;
            Save_Charater_Class.Class_Status newStatus = Save_Charater_Class.Worrier;
            int rnd = Random.Range(0,4);
            
            switch (rnd)
            {
                case 0:
                newClass = e_Class.worrier;
                newStatus = Save_Charater_Class.Worrier;
                Characters[i].GetComponent<Image>().sprite = CharImage[rnd];
                break;

                case 1:
                newClass = e_Class.magicion;
                newStatus = Save_Charater_Class.Magicion;
                Characters[i].GetComponent<Image>().sprite = CharImage[rnd];
                break;
                
                case 2:
                newClass = e_Class.supporter;
                newStatus = Save_Charater_Class.Supporter;
                Characters[i].GetComponent<Image>().sprite = CharImage[rnd];
                break;
                
                case 3:
                newClass = e_Class.assassin;
                newStatus = Save_Charater_Class.Assassin;
                Characters[i].GetComponent<Image>().sprite = CharImage[rnd];
                break;                
            }
            NewCharacters[i] = new Save_Charater_Class.SD(newStatus,newClass);
            var charName = Characters[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>();
            charName.text = "[" + NewCharacters[i].c_Name + "]";
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
        SkillText[1].GetComponent<TextMeshProUGUI>().text = "Sk_"+ NewCharacters[n].skill[0];
        SkillText[2].GetComponent<TextMeshProUGUI>().text = "Sk_"+ NewCharacters[n].skill[1];
        SkillText[3].GetComponent<TextMeshProUGUI>().text = "Ulti";
        

        SelectNum = n;
        bSelect = true;           
    }

    public void ClickRecruit()
    {
       if(3 > Save.S_Character.Count && bSelect == true)
       {
            Save.S_Character.Add(NewCharacters[SelectNum]);
            Characters[SelectNum].SetActive(false);
            bSelect = false;
            SkillPannel.SetActive(false);

       } 
    }



}
