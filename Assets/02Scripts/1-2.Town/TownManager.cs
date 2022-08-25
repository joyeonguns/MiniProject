using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class TownManager : MonoBehaviour
{
    public GameObject BackGround;
    public GameObject[] Characters = new GameObject[4];
    public Button[] CharacterBtn = new Button[4];
    public GameObject SkillPannel;
    public GameObject[] SkillBtn = new GameObject[4];
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
            int rnd = Random.Range(0,4);

            CharacterDatas charData = Save_Charater_Data.instance.characterData[rnd+1];
            Save.St_Stat newStatus = new Save.St_Stat(charData);
            
            e_Class newRole = (e_Class)(rnd + 1);
            Characters[i].GetComponent<Image>().sprite = CharImage[rnd];

            NewCharacters[i] = new Save.Player(newStatus,newRole);
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

        int rolenum = (int)NewCharacters[n].Role;
        string root ="";
        switch (rolenum)
        {
            case 1 :
            root = "icon/Worrier/";
            break;
            case 2 :
            root = "icon/Magition/";
            break;
            case 3 :
            root = "icon/Healer/";
            break;
            case 4 :
            root = "icon/Assassin/";
            break;
        }
        int skill_1 = NewCharacters[n].SkillNum[1];
        int skill_2 = NewCharacters[n].SkillNum[2];

        SkillBtn[0].GetComponent<Image>().sprite = Resources.Load<Sprite>(root + 0) as Sprite;
        SkillBtn[1].GetComponent<Image>().sprite = Resources.Load<Sprite>(root + skill_1) as Sprite;
        SkillBtn[2].GetComponent<Image>().sprite = Resources.Load<Sprite>(root + skill_2) as Sprite;
        SkillBtn[3].GetComponent<Image>().sprite = Resources.Load<Sprite>(root + 5) as Sprite;

        SkillBtn[0].GetComponent<SkillComment>().skill = NewCharacters[n].MySkill[0];
        SkillBtn[1].GetComponent<SkillComment>().skill = NewCharacters[n].MySkill[1];
        SkillBtn[2].GetComponent<SkillComment>().skill = NewCharacters[n].MySkill[2];
        SkillBtn[3].GetComponent<SkillComment>().skill = NewCharacters[n].MySkill[3];
        

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
       else
       {
            Debug.Log("이미 파티원의 수가 풀 입니다.");
       }
    }


    public void NextScene()
    {
        SceneManager.LoadScene("1-2.MapScene");     
    }

}
