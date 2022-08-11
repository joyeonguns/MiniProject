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
    public int Target_0;
    public int Target_1;
    public bool bCheckedTarget_0;
    public bool bCheckedTarget_1;

    public GameObject firstCharacter;
    public GameObject SecondCharacter;
    public Vector3 firstLoc;
    public Vector3 secondLoc;

    
    Save_Charater_Data SaveData;
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
        SetStartSetting();
        SetTellentBtn();
        
    }

    public void SetStartSetting()
    {
        Debug.Log("SetStartSetting");
        SaveData = Save_Charater_Data.instance;
        p_Count = SaveData.MyParty.Count;

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
        SaveData = Save_Charater_Data.instance;
        p_Count = SaveData.MyParty.Count;
        for(int i = 0; i < 3; i++)
        {
            if(i < p_Count)
            {
                CharacterBtn[i].gameObject.SetActive(true);
                Char_Name[i].text = SaveData.MyParty[i].name;
                
                switch (SaveData.MyParty[i].Role)
                {
                    case e_Class.worrier :
                        Char_Image[i].sprite = CharacterImage[0];

                        break;
                    case e_Class.magicion :
                        Char_Image[i].sprite = CharacterImage[1];
                        break;
                    case e_Class.supporter :
                        Char_Image[i].sprite = CharacterImage[2];
                        break;
                    case e_Class.assassin :
                        Char_Image[i].sprite = CharacterImage[3];
                        break;
                }
            }
            else
            {
                CharacterBtn[i].gameObject.SetActive(false);
                Debug.Log("p_Count : " + SaveData.MyParty.Count);
                Debug.Log("i : " + i);
            }
        }
    }

    public void SetStatus_Text(int n)
    {
        SaveData = Save_Charater_Data.instance;
        stat_Name.text = "" + SaveData.MyParty[n].name;
        stat_Comments.text = 
        SaveData.MyParty[n].Level + "\n" +
        "(" + SaveData.MyParty[n].exp + " / " + (SaveData.MyParty[n].Level * 50  + 100) + ")" + "\n" +
        "\n" +
        Enum.GetName(typeof(e_Class),SaveData.MyParty[n].Role)  + "\n" +
        "\n" +
        SaveData.MyParty[n].Hp + " / " + SaveData.MyParty[n].status.MaxHp + "\n" +
        "\n" +
        SaveData.MyParty[n].Mana +" / " + "10" + "\n" +
        "\n" +
        SaveData.MyParty[n].status.Damage + "\n" +
        "\n" +
        SaveData.MyParty[n].status.Armor * 100 + " %"+ "\n" +
        "\n" +
        SaveData.MyParty[n].status.Critical + " %"+ "\n" +
        "\n" +
        SaveData.MyParty[n].status.Dodge + " %" + "\n" +
        "\n" +
        SaveData.MyParty[n].status.Speed + "\n" +
        "\n";
    }

    public void SetSkillBtn(int n)
    {       
        skillPannel.SetActive(true);
        int rolenum = (int)SaveData.MyParty[n].Role;
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
            root = "icon/Hiller/";
            break;
            case 4 :
            root = "icon/Assassin/";
            break;
        }
        int skill_1 = SaveData.MyParty[n].SkillNum[1];
        int skill_2 = SaveData.MyParty[n].SkillNum[2];
        skill[0].GetComponent<Image>().sprite = Resources.Load<Sprite>(root + skill_1) as Sprite;
        skill[1].GetComponent<Image>().sprite = Resources.Load<Sprite>(root + skill_2) as Sprite;
        skill[2].GetComponent<Image>().sprite = Resources.Load<Sprite>(root + "5") as Sprite;        

        skill[0].GetComponent<SkillComment>().skill = SaveData.MyParty[n].MySkill[1];
        skill[1].GetComponent<SkillComment>().skill = SaveData.MyParty[n].MySkill[2];
        skill[2].GetComponent<SkillComment>().skill = SaveData.MyParty[n].MySkill[3];
        
    }

    public void SetTargetBtn(int n)
    {
        // // SetStartSetting(); 
        // // 버튼 색상 할당
        // ColorBlock clb_green = CharacterBtn[n].colors;
        // clb_green.normalColor = Color.green;
        // clb_green.selectedColor = Color.green;

        // ColorBlock clb_white = CharacterBtn[n].colors;
        // clb_white.normalColor = Color.white;
        // clb_white.selectedColor = Color.white;

        // CharacterBtn[n].colors = clb_green;

        // if (SceneManager.GetActiveScene().name != "1-2.MapScene")
        // {
        //     bCheckedTarget_0 = false;
        //     CharacterBtn[Target_0].colors = clb_white;
        //     Target_0 = n;
        // }
        
        // else if (bCheckedTarget_0 == false)
        // {
        //     bCheckedTarget_0 = true;
        //     Target_0 = n;
        // }

        // else
        // {
            
        //     Target_1 = n;

        //     // 버튼 색상 해제
            

        //     CharacterBtn[Target_0].colors = clb_white;
        //     CharacterBtn[Target_1].colors = clb_white;

        //     if(Target_0 == Target_1)
        //     {
        //         bCheckedTarget_0 = false;
        //         return;
        //     }

        //     Save.Player temp = SaveData.MyParty[Target_0];
        //     SaveData.MyParty[Target_0] = SaveData.MyParty[Target_1];
        //     SaveData.MyParty[Target_1] = temp;
        //     bCheckedTarget_0 = false;

        //     SetCharImage();

                     
                     
        // }
        // SetStatus_Text(n);
        // SetSkillBtn(n);
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
            firstCharacter.transform.position = secondLoc;
            firstCharacter.GetComponent<Info_Character>().num = Target_1;
            SecondCharacter.transform.position = firstLoc;      
            SecondCharacter.GetComponent<Info_Character>().num = Target_0;

            Save.Player temp = SaveData.MyParty[Target_0];
            SaveData.MyParty[Target_0] = SaveData.MyParty[Target_1];
            SaveData.MyParty[Target_1] = temp;              
                     
        }
    }
}
