using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class TownManager : MonoBehaviour
{
    public GameManager GM;

    public GameObject HirePannel;


    // 세로운 캐릭터
    public GameObject[] CharacterGO = new GameObject[4];
    public Image[] newCharacterImage = new Image[3];
    public Button[] CharacterBtn = new Button[4];
    public TextMeshProUGUI[] CharNames;
    Save.Player[] NewCharacters = new Save.Player[4];


    // 데이터
    public GameObject DataPannel;
    public Image SelectImage;
    public TextMeshProUGUI data;
    public GameObject SkillPannel;
    public GameObject[] SkillBtn = new GameObject[4];
    public Sprite nullImage;

    // 현재 캐릭터
    public Image[] nowCharacterImage = new Image[3];
    public Button[] nowCharacterBtn = new Button[3];
    List<Save.Player> nowCharacters;
    public Button hireBtn;
    public Button fireBtn;

    // 골드
    public TextMeshProUGUI gold;


    int SelectNum;
    bool bSelect;


    void StartSetting()
    {
        GM = GameManager.instance;

        HirePannel.SetActive(false);
        DataPannel.SetActive(false);

        hireBtn.interactable = false;
        fireBtn.interactable = false;

        nowCharacters = GM.MyParty;
        
    }

    void SetButton()
    {
        // Click new
        CharacterBtn[0].onClick.AddListener(() =>
        {
            ClickCharBtn(0,false);
        });
        CharacterBtn[1].onClick.AddListener(() =>
        {
            ClickCharBtn(1,false);
        });
        CharacterBtn[2].onClick.AddListener(() =>
        {
            ClickCharBtn(2,false);
        });
        CharacterBtn[3].onClick.AddListener(() =>
        {
            ClickCharBtn(3,false);
        });

        // Click now
        nowCharacterBtn[0].onClick.AddListener(() =>
        {
            ClickCharBtn(0,true);
        });
        nowCharacterBtn[1].onClick.AddListener(() =>
        {
            ClickCharBtn(1,true);
        });
        nowCharacterBtn[2].onClick.AddListener(() =>
        {
            ClickCharBtn(2,true);
        });
    }

    // Start is called before the first frame update
    void Start()
    {
        GM = GameManager.instance;

        StartSetting();

        SetCharData();       

        SetButton();

        SetNowCharImage();
    }

    void SetCharData()
    {
        for (int i = 0; i < 4; i++)
        {
            // 레벨 
            int lvl = UnityEngine.Random.Range(0, 3);
            // 캐릭터 생성
            int rnd = UnityEngine.Random.Range(1, 5);

            CharacterDatas charData = SOManager.instance.CharSO.CharDatas[rnd];
            Save.St_Stat newStatus = new Save.St_Stat(charData);
            e_Class newRole = (e_Class)(rnd);
            NewCharacters[i] = new Save.Player(newStatus, newRole);

            newCharacterImage[i].sprite = charData.Icon;

            CharNames[i].text = "" + NewCharacters[i].name + "";

            // 레벨 업
            for(int j = 0; j < lvl; j++)
            {
                NewCharacters[i].LevelUp();
            }
        }
    }

    public void ClickGuild()
    {
        HirePannel.SetActive(true);
    }

    public void ClickRecruit_Exit()
    {
        HirePannel.SetActive(false);
    }


    public void ClickCharBtn(int n, bool Hired)
    {
        Debug.Log("n : " + n);
        Debug.Log("count : " + CharacterBtn.Length);

        // SkillPannel.SetActive(true);
        DataPannel.SetActive(true);

        int rolenum;
        CharacterDatas charData;

        Save.Player curCharacter;

        if(Hired)
        {            
            if(n >= nowCharacters.Count)
            {
                fireBtn.interactable = false;
                return;                
            }
            SetfireBtn();
            curCharacter = nowCharacters[n];            
            hireBtn.interactable = false;
        }
        else
        {
            curCharacter = NewCharacters[n];
            SethireBtn();
            fireBtn.interactable = false;
        }

        // 선택 캐릭터 이미지
        rolenum = (int)curCharacter.Role;
        charData = SOManager.instance.CharSO.CharDatas[rolenum];

        SelectImage.sprite = charData.Illuste;

        int skill_1 = curCharacter.SkillNum[1];
        int skill_2 = curCharacter.SkillNum[2];

        // 캐릭터 정보
        int lvl = curCharacter.Level;
        int exp = curCharacter.exp;
        int Fullexp = curCharacter.Level * 50 + 100;
        string role = Enum.GetName(typeof(e_Class), curCharacter.Role);
        int cost = lvl * 100;

        data.text = $"{lvl} ( {exp} / {Fullexp} ) \n \n \n {role} \n \n {cost} G";


        // 스킬 정보
        SkillBtn[0].GetComponent<Image>().sprite = charData.attack;
        SkillBtn[1].GetComponent<Image>().sprite = charData.skill[skill_1 - 1];
        SkillBtn[2].GetComponent<Image>().sprite = charData.skill[skill_2 - 1];
        SkillBtn[3].GetComponent<Image>().sprite = charData.ulti;


        SkillBtn[0].GetComponent<SkillComment>().skill = curCharacter.MySkill[0];
        SkillBtn[1].GetComponent<SkillComment>().skill = curCharacter.MySkill[1];
        SkillBtn[2].GetComponent<SkillComment>().skill = curCharacter.MySkill[2];
        SkillBtn[3].GetComponent<SkillComment>().skill = curCharacter.MySkill[3];


        SelectNum = n;
        bSelect = true;

        

    }

    public void SethireBtn()
    {
        if (nowCharacters.Count >= 3)
        {
            hireBtn.interactable = false;
        }
        else
        {
            hireBtn.interactable = true;
        }        
    }
    public void SetfireBtn()
    {
        if (nowCharacters.Count > 0)
        {
            fireBtn.interactable = true;
        }
        else
        {
            fireBtn.interactable = false;
        }        
    }

    public void SetNowCharImage()
    {
        for(int i = 0; i < 3; i++)
        {
            if(nowCharacters.Count > i)
            {
                int rolenum = (int)nowCharacters[i].Role;
                CharacterDatas charData = SOManager.instance.CharSO.CharDatas[rolenum];

                nowCharacterImage[i].sprite = charData.Icon;
            }
            else
            {
                nowCharacterImage[i].sprite = nullImage;
            }
        }
    }


    public void ClickRecruit()
    {
        int gold = NewCharacters[SelectNum].Level * 100;
        if (3 > nowCharacters.Count && bSelect == true && GM.curGold >= gold )
        {
            nowCharacters.Add(NewCharacters[SelectNum]);
            CharacterGO[SelectNum].SetActive(false);           

            bSelect = false;
            DataPannel.SetActive(false);

            SetNowCharImage();

            GM.curGold -= gold;

            SetGold();
        }
        else
        {
            Debug.Log("이미 파티원의 수가 풀 입니다.");
        }

        hireBtn.interactable = false;
    }

    public void ClickFire()
    {
        if (GM.MyParty.Count > 0 && bSelect == true)
        {
            nowCharacters.RemoveAt(SelectNum);
            bSelect = false;
            DataPannel.SetActive(false);            

            SetNowCharImage();
        }
        else
        {
            Debug.Log("해고 할 수 없습니다.");
        }

        fireBtn.interactable = false;
    }


    public void SetGold()
    {
        gold.text = "" + GM.curGold;
    }

    public void NextScene()
    {
        SceneManager.LoadScene("1-2.MapScene");
    }

}
