using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using System;
using UnityEngine.SceneManagement;

public class Map_Info : MonoBehaviour
{
    // 캐릭터 패널
    public Button[] CharacterBtn;
    public TextMeshProUGUI[] Char_Name;
    public Image[] Char_Image;
    public GameObject[] Captain;

    public int Target_0;
    public int Target_1;
    public bool bCheckedTarget_0;
    public bool bCheckedTarget_1;

    public GameObject firstCharacter;
    public GameObject SecondCharacter;
    public Vector3 firstLoc;
    public Vector3 secondLoc;

    
    GameManager GM;
    public int p_Count;

    // 스텟 패널
    public TextMeshProUGUI stat_Name;
    public TextMeshProUGUI stat_Comments;

    // 스킬 패널
    public GameObject skillPannel;
    public GameObject[] skill;

    // tellent 패널
    public GameObject TellentPrefabs;
    public GameObject SpwArea;
    public int telnum;

    // 캐릭터 이미지
    public Sprite[] CharacterImage;

    // 맵
    // public GameObject MapUI;
    // 인포
    

    
    bool bOpenTellent = false;    
    
    

    void Start() 
    {
        GM = GameManager.instance;
        SetStartSetting();
        SetTellentBtn();
        
    }

    public void SetStartSetting()
    {
        Debug.Log("SetStartSetting");
        p_Count = GM.MyParty.Count;

        bCheckedTarget_0 = false;
        bCheckedTarget_1 = false;
        stat_Name.text = "";
        stat_Comments.text = "";

        skillPannel.SetActive(false);
        
        SetCharImage();
    }

    public void SetCharImage()
    {
        Debug.Log("SetCharImage");
        p_Count = GM.MyParty.Count;
        for(int i = 0; i < 3; i++)
        {
            if(i < p_Count)
            {
                CharacterBtn[i].gameObject.SetActive(true);
                Char_Name[i].text = GM.MyParty[i].name;
                
                Captain[i].SetActive(GM.MyParty[i].Main);

                switch (GM.MyParty[i].Role)
                {
                    case e_Class.Warrior :
                        Char_Image[i].sprite = CharacterImage[0];

                        break;
                    case e_Class.Magicion :
                        Char_Image[i].sprite = CharacterImage[1];
                        break;
                    case e_Class.Supporter :
                        Char_Image[i].sprite = CharacterImage[2];
                        break;
                    case e_Class.Assassin :
                        Char_Image[i].sprite = CharacterImage[3];
                        break;
                }
            }
            else
            {
                CharacterBtn[i].gameObject.SetActive(false);
                Debug.Log("p_Count : " + GM.MyParty.Count);
                Debug.Log("i : " + i);
            }
        }
    }

    public void SetStatus_Text(int n)
    {
        stat_Name.text = "" + GM.MyParty[n].name;
        stat_Comments.text = 
        GM.MyParty[n].Level + "\n" +
        "(" + GM.MyParty[n].exp + " / " + (GM.MyParty[n].Level * 50  + 100) + ")" + "\n" +
        "\n" +
        Enum.GetName(typeof(e_Class),GM.MyParty[n].Role)  + "\n" +
        "\n" +
        GM.MyParty[n].Hp + " / " + GM.MyParty[n].status.MaxHp + "\n" +
        "\n" +
        GM.MyParty[n].Mana +" / " + "10" + "\n" +
        "\n" +
        GM.MyParty[n].status.Damage + "\n" +
        "\n" +
        GM.MyParty[n].status.Armor * 100 + " %"+ "\n" +
        "\n" +
        GM.MyParty[n].status.Critical + " %"+ "\n" +
        "\n" +
        GM.MyParty[n].status.Dodge + " %" + "\n" +
        "\n" +
        GM.MyParty[n].status.Speed + "\n" +
        "\n";
    }

    public void SetSkillBtn(int n)
    {       
        skillPannel.SetActive(true);
        int rolenum = (int)GM.MyParty[n].Role;
        // string root ="";
        // switch (rolenum)
        // {
        //     case 1 :
        //     root = "icon/Worrier/";
        //     break;
        //     case 2 :
        //     root = "icon/Magition/";
        //     break;
        //     case 3 :
        //     root = "icon/Healer/";
        //     break;
        //     case 4 :
        //     root = "icon/Assassin/";
        //     break;
        // }

        CharacterDatas charData = SOManager.instance.CharSO.CharDatas[rolenum];

        int skill_1 = GM.MyParty[n].SkillNum[1];
        int skill_2 = GM.MyParty[n].SkillNum[2];
        skill[0].GetComponent<Image>().sprite = charData.skill[skill_1 - 1];
        skill[1].GetComponent<Image>().sprite = charData.skill[skill_2 - 1];
        skill[2].GetComponent<Image>().sprite = charData.ulti;        

        skill[0].GetComponent<SkillComment>().skill = GM.MyParty[n].MySkill[1];
        skill[1].GetComponent<SkillComment>().skill = GM.MyParty[n].MySkill[2];
        skill[2].GetComponent<SkillComment>().skill = GM.MyParty[n].MySkill[3];
        
    }

    public void SetTargetBtn(int n)
    {
        
    }

    void SetTellentBtn()
    {
        telnum = 0;       
        
    }    

    public void ChangeCharacterLocation()
    {
        if (SceneManager.GetActiveScene().name != "1-2.MapScene")
        {
            bCheckedTarget_0 = false;
        }
        else
        {
            // 위치 초기화
            firstCharacter.transform.position = firstLoc;
            firstCharacter.GetComponent<Info_Character>().num = Target_1;
            SecondCharacter.transform.position = secondLoc;      
            SecondCharacter.GetComponent<Info_Character>().num = Target_0;



            // 캐릭터 위치변경
            Save.Player temp = GM.MyParty[Target_0];
            GM.MyParty[Target_0] = GM.MyParty[Target_1];
            GM.MyParty[Target_1] = temp;              

            SetCharImage();
                     
        }
    }
}
