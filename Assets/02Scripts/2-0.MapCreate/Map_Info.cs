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
    
    Save_Charater_Data Save;
    public int p_Count;

    // 스텟 패널
    public TextMeshProUGUI stat_Name;
    public TextMeshProUGUI stat_Comments;

    // 스킬 패널
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
    public GameObject InfoUI;

    // 조작
    bool bOpenInfo = false;
    bool bOpenTellent = false;    
    

    void Start() 
    {
        SetStartSetting();
        SetTellentBtn();
    }

    void SetStartSetting()
    {
        Debug.Log("SetStartSetting");
        Save = GameManager.instance.GetComponent<Save_Charater_Data>();
        p_Count = Save.S_Character.Count;

        bCheckedTarget_0 = false;
        bCheckedTarget_1 = false;
        stat_Name.text = "";
        stat_Comments.text = "";
        
        SetCharImage();
    }

    void SetCharImage()
    {
        Debug.Log("SetCharImage");
        Save = GameManager.instance.GetComponent<Save_Charater_Data>();   
        p_Count = Save.S_Character.Count;
        for(int i = 0; i < 3; i++)
        {
            if(i < p_Count)
            {
                Char_Name[i].text = Save.S_Character[i].c_Name;
                
                switch (Save.S_Character[i].c_Class)
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
                Debug.Log("p_Count : " + Save.S_Character.Count);
                Debug.Log("i : " + i);
            }
        }
    }

    void SetStatus_Text(int n)
    {
        Save = GameManager.instance.GetComponent<Save_Charater_Data>();
        stat_Name.text = "" + Save.S_Character[n].c_Name;
        stat_Comments.text = 
        Save.S_Character[n].Level + "\n" +
        "(" + Save.S_Character[n].exp + " / " + (Save.S_Character[n].Level * 50  + 100) + ")" + "\n" +
        "\n" +
        Enum.GetName(typeof(e_Class),Save.S_Character[n].c_Class)  + "\n" +
        "\n" +
        Save.S_Character[n].CurHp + " / " + Save.S_Character[n].status.s_MaxHp + "\n" +
        "\n" +
        Save.S_Character[n].Mana +" / " + "10" + "\n" +
        "\n" +
        Save.S_Character[n].status.s_Damage + "\n" +
        "\n" +
        Save.S_Character[n].status.s_Armor * 100 + " %"+ "\n" +
        "\n" +
        Save.S_Character[n].status.s_Critical + " %"+ "\n" +
        "\n" +
        Save.S_Character[n].status.s_Dodge + " %" + "\n" +
        "\n" +
        Save.S_Character[n].status.s_Speed + "\n" +
        "\n";
    }

    void SetSkillBtn(int n)
    {
        if(n < 0)
        {
            foreach (var sk in skill)
            {
                sk.SetActive(false);
            }
            return;
        }
        int i = 1;
        foreach (var sk in skill)
        {
            sk.SetActive(true);
            TextMeshProUGUI sk_Text = sk.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            Save.S_Character[n].SetSkillClass();
            sk_Text.text = "" + Save.S_Character[n].MySkill[i].skillName;
            i++;
        }
        
    }

    public void SetTargetBtn(int n)
    {
        // SetStartSetting(); 
        // 버튼 색상 할당
        ColorBlock clb_green = CharacterBtn[n].colors;
        clb_green.normalColor = Color.green;
        clb_green.selectedColor = Color.green;

        ColorBlock clb_white = CharacterBtn[n].colors;
        clb_white.normalColor = Color.white;
        clb_white.selectedColor = Color.white;

        CharacterBtn[n].colors = clb_green;

        if (SceneManager.GetActiveScene().name != "1-2.MapScene")
        {
            bCheckedTarget_0 = false;
            CharacterBtn[Target_0].colors = clb_white;
            Target_0 = n;
        }
        
        else if (bCheckedTarget_0 == false)
        {
            bCheckedTarget_0 = true;
            Target_0 = n;
        }

        else
        {
            
            Target_1 = n;

            // 버튼 색상 해제
            

            CharacterBtn[Target_0].colors = clb_white;
            CharacterBtn[Target_1].colors = clb_white;

            if(Target_0 == Target_1)
            {
                bCheckedTarget_0 = false;
                return;
            }

            Save_Charater_Class.SD temp = Save.S_Character[Target_0];
            Save.S_Character[Target_0] = Save.S_Character[Target_1];
            Save.S_Character[Target_1] = temp;
            bCheckedTarget_0 = false;

            SetCharImage();

                     
                     
        }
        SetStatus_Text(n);
        SetSkillBtn(n);
    }

    void SetTellentBtn()
    {
        telnum = 0;
        float x = -400, y = 35; 
        foreach(int btt in GameManager.instance.BeforTellents_num)
        {
            telnum++;
            if(telnum == 5)
            {
                x = -400;
                y = -35;
            }
            var spwTel = Instantiate(TellentPrefabs);
            spwTel.transform.SetParent(SpwArea.transform);
            spwTel.GetComponent<RectTransform>().anchoredPosition = new Vector2(x,y);
            x += 200;
            spwTel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "BTT " +btt;
        }
        foreach(var bbt in GameManager.instance.BBTellent_num)
        {
            telnum++;
            if(telnum == 5)
            {
                x = -400;
                y = -35;
            }
            var spwTel = Instantiate(TellentPrefabs);
            spwTel.transform.SetParent(SpwArea.transform);
            spwTel.GetComponent<RectTransform>().anchoredPosition = new Vector2(x,y);
            x += 200;
            spwTel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "BBT " + bbt;
        }
        foreach(var gat in GameManager.instance.GetAfterTellents_num)
        {
            telnum++;
            if(telnum == 5)
            {
                x = -400;
                y = -35;
            }
            var spwTel = Instantiate(TellentPrefabs);
            spwTel.transform.SetParent(SpwArea.transform);
            spwTel.GetComponent<RectTransform>().anchoredPosition = new Vector2(x,y);
            x += 200;
            spwTel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "GAT " + gat; 
        }
        foreach(var abt in GameManager.instance.ABTelent_num)
        {
            telnum++;
            if(telnum == 5)
            {
                x = -400;
                y = -35;
            }
            var spwTel = Instantiate(TellentPrefabs);
            spwTel.transform.SetParent(SpwArea.transform);
            spwTel.GetComponent<RectTransform>().anchoredPosition = new Vector2(x,y);
            x += 200;
            spwTel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "ABT " + abt; 
        }
        
    }

    public void OpenInfoUI()
    {
        if(bOpenInfo == false)
        {
            InfoUI.SetActive(true);
            bOpenInfo = true;
        }
        else
        {
            InfoUI.SetActive(false);
            bOpenInfo = false;
        }        
    }


}
