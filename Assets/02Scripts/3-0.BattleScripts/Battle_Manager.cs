using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Linq;
using UnityEngine.SceneManagement;


public class GameInformation
{
   
    int time;
    int bt_Lvl = 1;   
    public int ItemRate = 30;
    public string Battletype {get; set;}
    public int Golds {get; set;}
    public int TurnCounts {get; set;}
    public int Times {get{return time;} set {if(time > 25) time = 1;}}
}

public class Battle_Manager : MonoBehaviour
{
    public int _time;
    public int _turn;
    // Start is called before the first frame update


    // 배틀씬 기본 설정
    int BtLvl;
    Save_Charater_Class.SD[] Bandits = new Save_Charater_Class.SD[1] {new Save_Charater_Class.SD(Save_Charater_Class.Bandit,e_Class.bandit)};
    //Save_Charater_Class.SD[] Knights = new Save_Charater_Class.SD[1] {new Save_Charater_Class.SD(Save_Charater_Class.Knight,e_Class.Knight)};
    //Save_Charater_Class.SD[] Abominations = new Save_Charater_Class.SD[1] {new Save_Charater_Class.SD(Save_Charater_Class.Abomination,e_Class.abomination)};

    // 캐릭터 적 클래스
    [SerializeField] List<Save_Charater_Class.SD> Character = new List<Save_Charater_Class.SD>();
    public List<Save_Charater_Class.Class_Status> Ch_Status; 
    public Save_Charater_Class.Class_Status volaStatus;
    [SerializeField] List<Save_Charater_Class.SD> Enemy = new List<Save_Charater_Class.SD>();
    public List<Save_Charater_Class.Class_Status> En_Status; 

    // 아군 필드
    // 아군 선택 필드
    public GameObject[] CharacterField = new GameObject[3];
    // 아군 스프라이트
    public Image[] CharacterSprite = new Image[3];
    // 아군 HP / MP
    public Image[] Character_HP = new Image[3];
    public Image[] Character_MP = new Image[3];

    // 적 선택 필드
    public GameObject[] EnemyField = new GameObject[3];
    // 적 스프라이트
    public Image[] EnemySprite = new Image[3];
    // 적 HP / MP
    public Image[] Enemy_HP = new Image[3];
    public Image[] Enemy_MP = new Image[3];

    // Sprites
    public Sprite[] CharacterImages = new Sprite[4];

    // 배틀 패널
    public GameObject SkillPannel;
    public Image[] SkillButton = new Image[4];
    public TextMeshProUGUI AttStatus;
    public TextMeshProUGUI TargetStatus;
    public Image AttackerImage;


    // 공격 순서
    public GameObject[] AttOrder = new GameObject[3];
    public TextMeshProUGUI RoundText;
   
    // 데미지 폰트
    public TextMeshProUGUI Damage;


    // --- 필요 정보 ---
    // 선택한 스킬
    public int selecSkill;
    // 스킬 선택 체크
    public bool bCheckSkill;
    // 타겟
    public int target;
    // 타겟 선택 체크
    public bool bChecktarget;
    // 배틀 스피드
    [SerializeField] List<Tuple<int,int,int>> L_BattleSpeed = new List<Tuple<int, int, int>>();   

    // 현재 공격자
    public int Attacker;

    GameInformation BattleInfo; 
    

    void Start()
    {       
        BattleInfo = new GameInformation();
        BattleInfo.Times = 12;
        BattleInfo.TurnCounts = 0;
        BattleInfo.Golds = 30;
    }
    // 적 정보 설정
    void SetEnemy()
    {
        BtLvl = GameManager.instance.Battle_Lvl;
        Enemy.Add(new Save_Charater_Class.SD());
        Enemy.Add(new Save_Charater_Class.SD());
        Enemy.Add(new Save_Charater_Class.SD());
        if(GameManager.instance.BattleType == 1)
        {            
            // 1열 전사
            Enemy[0] = new Save_Charater_Class.SD(Save_Charater_Class.Bandit,e_Class.bandit);
            Enemy[0].font = Damage;
            Enemy[0].spwX = EnemyField[0].GetComponent<RectTransform>().anchoredPosition.x +50;
            Enemy[0].spwY = EnemyField[0].GetComponent<RectTransform>().anchoredPosition.y + 300;
            // 2열 전사 / 궁수
            Enemy[1] = new Save_Charater_Class.SD(Save_Charater_Class.Bandit,e_Class.bandit);
            Enemy[1].font = Damage;
            Enemy[1].spwX = EnemyField[1].GetComponent<RectTransform>().anchoredPosition.x + 50;
            Enemy[1].spwY = EnemyField[1].GetComponent<RectTransform>().anchoredPosition.y + 300;
            // 3열 궁수
            Enemy[2] = new Save_Charater_Class.SD(Save_Charater_Class.Bandit,e_Class.bandit);
            Enemy[2].font = Damage;
            Enemy[2].spwX = EnemyField[2].GetComponent<RectTransform>().anchoredPosition.x + 50;
            Enemy[2].spwY = EnemyField[2].GetComponent<RectTransform>().anchoredPosition.y + 300;
        }       

        foreach(var e in Enemy)
        {
            Save_Charater_Class.Class_Status stat = new Save_Charater_Class.Class_Status();
            stat = e.status;
            En_Status.Add(stat);
        }
        
    }
    // 아군 저장
    void SetCharacter()
    {
        Save_Charater_Data Save = GameManager.instance.GetComponent<Save_Charater_Data>();
        Character = Save.S_Character;
        
        for(int i = 0; i< Character.Count; i++)
        {
            Character[i].font = Damage;
            Character[i].spwX = CharacterField[i].GetComponent<RectTransform>().anchoredPosition.x + 50;
            Character[i].spwY = CharacterField[i].GetComponent<RectTransform>().anchoredPosition.y + 300;
            
            Save_Charater_Class.Class_Status stat = Character[i].status;
            Ch_Status.Add(stat);

            CharacterSprite[i].sprite = CharacterImages[(int)Character[i].c_Class];
        }
    }
    void SetStartUI()
    {
        for(int i = 0; i < 3; i++)
        {
            if(i >= Character.Count)
            {
                CharacterField[i].gameObject.SetActive(false);                
            }
        }
    }
    void SetHP_MP()
    {
        for(int i = 0; i < Character.Count; i++)
        {
            Character_HP[i].fillAmount = (float)Character[i].Hp / (float)Character[i].status.s_MaxHp;
            Character_MP[i].fillAmount = (float)Character[i].Mana / 10.0f;
        }
        for(int i = 0; i < Enemy.Count; i++)
        {
            Enemy_HP[i].fillAmount = (float)Enemy[i].Hp / (float)Enemy[i].status.s_MaxHp;
            Enemy_MP[i].fillAmount = (float)Enemy[i].Mana / 10.0f;
        }
        
        
    }

    // 전투시작 특성
    void BBSetTellent()
    {
        Debug.Log("set Tellent");
        //GameManager.instance.SetTellent();
        foreach(var tel in GameManager.instance.BBTellent)
        {
            tel.TellentApply(BattleInfo,Character,0, Ch_Status, volaStatus, Enemy,target);
        }

    }

    // 전투종료 특성
    void ABSetTellent()
    {
        Debug.Log("set Tellent");
        //GameManager.instance.SetTellent();
        foreach(var tel in GameManager.instance.ABTelent)
        {
            volaStatus = tel.TellentApply(BattleInfo,Character,0, Ch_Status, volaStatus, Enemy,target);
        }

    }

    // 속도 설정
    // 속도에 따른 전투순서
    void SetSpeed()
    {
        for(int i = 0; i < Character.Count; i++)
        {
            if(Character[i].bAlive == true)
            {
                int rnd1 = UnityEngine.Random.Range(1,7);
                Save_Charater_Class.Class_Status CurStatus = Ch_Status[i];
                L_BattleSpeed.Add(Tuple.Create(CurStatus.s_Speed + rnd1, i, 0));
            }            
        }
        for(int i = 0; i < Enemy.Count; i++)
        {
            if(Enemy[i].bAlive == true)
            {
                int rnd2 = UnityEngine.Random.Range(1,4);
                Save_Charater_Class.Class_Status CurStatus = En_Status[i];
                L_BattleSpeed.Add(Tuple.Create(CurStatus.s_Speed + rnd2, i, 1));
            }
            
        }         

        List<Tuple<int,int,int>> sortList = L_BattleSpeed.OrderByDescending(x => x.Item1).ToList();
        L_BattleSpeed = sortList.ToList();
    }

   
    void SetSkillPannel(bool check, int n)
    {
        // 스킬 패널 오픈
        SkillPannel.SetActive(check);
        // 스킬 버튼 설정
        //Assets/Resources/icon/crusader.ability.five.png
        if (check == true)
        {
            SkillButton[0].sprite = Resources.Load<Sprite>("icon/crusader.ability.0") as Sprite;
            int skill_1 = Character[L_BattleSpeed[0].Item2].skill[0];
            int skill_2 = Character[L_BattleSpeed[0].Item2].skill[1];
            SkillButton[1].sprite = Resources.Load<Sprite>("icon/crusader.ability." + skill_1) as Sprite;
            SkillButton[2].sprite = Resources.Load<Sprite>("icon/crusader.ability." + skill_2) as Sprite;
            SkillButton[3].sprite = Resources.Load<Sprite>("icon/crusader.ability.5") as Sprite;

            for(int i = 1; i < 4; i++)
            {
                Character[n].SetSkillClass();
                if(Character[n].MySkill[i].manaCost > Character[n].Mana)
                {
                    SkillButton[i].GetComponent<Button>().interactable = false;
                }
                else
                {
                    SkillButton[i].GetComponent<Button>().interactable = true;
                }
            }
        }
        
               
    }

    void EnemyTurn(int n)
    {  
        int Alive = 0; 
        foreach (var Char in Character)
        {
            if(Char.bAlive == true)
                Alive++;
        }
        if(Alive == 0)
        {
            Debug.Log("All Dead!!");
            Application.Quit();
        }
        
        // 턴 데미지
        // 타겟 설정
        int rnd = UnityEngine.Random.Range(1, 11);
        if (rnd >= 5) target = 0;
        else if (rnd >= 3) target = 1;      
        else target = 2;

        Debug.Log("target : " +target);
        // 공격
        if(target >= Character.Count)
        {
            if (Character.Count == 0)
            {
                Debug.Log("ERROR");
                SceneManager.LoadScene("1-0.StartScene");
            }
            else
                EnemyTurn(n);
        }
        else
            Enemy[n].nomalAttack(Character[target]);
        // 공격 씬 출력
    }

    void SetAttackerStatus(int n, Save_Charater_Class.Class_Status volaStat)
    {
        // 공격자 스텟
        AttStatus.text =
        "Name : " + Character[n].c_Name + ""+n + "\n" +
        "Damage : " + volaStat.s_Damage + "\n" +
        "Armor : " + volaStat.s_Armor + "\n" +
        "Critical : " + volaStat.s_Critical + "\n" +
        "Dodge : " + volaStat.s_Dodge + "\n" +
        "Hp : " + Character[n].Hp;
    }
    void SetTargetStatus(int n)
    {
        // 타겟 스텟
        TargetStatus.text =
        "Name : " + Enemy[n].c_Name + ""+n+ "\n" +
        "Damage : " + Enemy[n].status.s_Damage + "\n" +
        "Armor : " + Enemy[n].status.s_Armor + "\n" +
        "Critical : " + Enemy[n].status.s_Critical + "\n" +
        "Dodge : " + Enemy[n].status.s_Dodge + "\n" +
        "Hp : " + Enemy[n].Hp;
    }
    
    void SetAttackOrder()
    {
        for(int i = 0; i < 3; i++)
        {
            if(i >= L_BattleSpeed.Count)
            {
                AttOrder[i].SetActive(false);
                return;
            }
            AttOrder[i].SetActive(true);
            TextMeshProUGUI AttText = AttOrder[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>(); 
            
            string first = "E";
            if(L_BattleSpeed[i].Item3 == 0) first = "C";
            AttText.text = first +":"+ L_BattleSpeed[i].Item2;
        }
    }

    
    public BattleState battleState = BattleState.BeforBattle;
    // Update is called once per frame
    void Update()
    {
        _time = BattleInfo.Times;
        _turn = BattleInfo.TurnCounts;   
        // 전투 단계
        switch (battleState)
        {            
            // 1. 전투 이전
            case BattleState.BeforBattle :
                BeforeTheBattle();
                battleState = BattleState.InBattle;
                break;

            // 2. 전투 중 
            case BattleState.InBattle :
                battleState = BattleState.InBattle_Tellent;
                break;
            // 2-1. 전투 전 특성 처리
            case BattleState.InBattle_Tellent :
                BBSetTellent();      
                battleState = BattleState.InBattle_SetTurn;
                break;

            // 2-2. 턴 할당
            case BattleState.InBattle_SetTurn :
                BattleInfo.Times++;
                BattleInfo.TurnCounts++;                
                RoundText.text = "R:" + BattleInfo.TurnCounts;

                SetSpeed();
                battleState = BattleState.InBattle_BattleUi;
                break;

            // 2-2-1. 턴 배틀UI
            case BattleState.InBattle_BattleUi :
                // 공격 순서 UI
                if(L_BattleSpeed[0].Item3 == 1) 
                {
                    StartCoroutine(WaitAnimate(BattleState.InBattle_Battle_Enemy));
                    battleState = BattleState.InBattle_Battle_Animate;
                }                
                else battleState = BattleState.InBattle_Battle_My1;              

                SetAttackOrder();
                Attacker = L_BattleSpeed[0].Item2;
                break;

            // 2-2-2 적 공격
            case BattleState.InBattle_Battle_Enemy :
                if(Enemy[Attacker].bAlive == false)
                {
                    battleState = BattleState.InBattle_EndBattle;
                    break;
                }
                AttackerHilight("Enemy",Attacker,true);
                SetTargetStatus(Attacker);
                EnemyTurn(Attacker);
                StartCoroutine(WaitAnimate(BattleState.InBattle_EndBattle));
                battleState = BattleState.InBattle_Battle_Animate;
                break;
            // 2-2-3  캐릭터 턴
            case BattleState.InBattle_Battle_My1 :

                if(Character[Attacker].bAlive == false)
                {
                    battleState = BattleState.InBattle_EndBattle;
                    break;
                }
                // 특성 적용
                volaStatus = new Save_Charater_Class.Class_Status();
                volaStatus = Ch_Status[Attacker];
                Debug.Log("vola : " + volaStatus.s_Critical);
            
                foreach(var tel in GameManager.instance.BeforTellents)
                {
                    Debug.Log("tellents name : " + tel.Tel_Name);
                    volaStatus = tel.TellentApply(BattleInfo,Character, Attacker, Ch_Status, volaStatus, Enemy,target);
                }
                // 공격자 하이라이트
                AttackerHilight("Character", Attacker,true);
                // 스킬 패널 on / 이미지 설정
                SetSkillPannel(true,Attacker);
                // 공격자 스텟 표시
                SetAttackerStatus(Attacker,volaStatus);

                battleState = BattleState.InBattle_Battle_Waiting;
                break;
            // 2-2-3-1 스킬 및 타겟 선택
            case BattleState.InBattle_Battle_Waiting :
                if(bChecktarget == true && bCheckSkill == true)
                    battleState = BattleState.InBattle_Battle_My2;
                break;
            // 2-2-3-2 My 공격
            case BattleState.InBattle_Battle_My2 :                
                
                // 공격
                Character[Attacker].SetSkillClass();                

                Character[Attacker].MySkill[selecSkill].UseSkill(Character,Attacker, volaStatus, Enemy,target);
                StartCoroutine(WaitAnimate(BattleState.InBattle_EndBattle));
                battleState = BattleState.InBattle_Battle_Animate;
                break;
            // 2-2-4. 공격 애니메이션
            case BattleState.InBattle_Battle_Animate :                
                
                break;
            // 2-2-5. 턴종료
            case BattleState.InBattle_EndBattle :                
                // 턴종료
                Debug.Log("num : "+ L_BattleSpeed.Count);
                L_BattleSpeed.RemoveAt(0);
                target = 0;
                bCheckSkill = false;
                bChecktarget = false;                
                SetSkillPannel(false,Attacker);

                // 휘발성 스텟 초기화
                volaStatus = new Save_Charater_Class.Class_Status();

                int charAlive = 0;
                foreach(var Char in Character)
                {
                    if(Char.bAlive == true)
                        charAlive++;
                }
                if(charAlive == 0)
                {
                    Debug.Log("패배");
                    Application.Quit();
                    battleState = BattleState.EndBattle;
                    break;
                }

                int eneAlive = 0;
                foreach(var Ene in Enemy)
                {
                    if(Ene.bAlive == true)
                        eneAlive++;
                }
                if(eneAlive == 0)
                {
                    Debug.Log("승리");
                    battleState = BattleState.EndBattle;
                    break;
                }
                // 다음 공격자 있음
                if(L_BattleSpeed.Count != 0)
                {
                    battleState = BattleState.InBattle_BattleUi;
                }
                else
                {
                    battleState = BattleState.InBattle_SetTurn;
                }
                break;
            // (1~2 반복)
            // 3. 전투 종료
            case BattleState.EndBattle :
                 Debug.Log("승리");
                 ABSetTellent();
                 GameManager.instance.gold = BattleInfo.Golds;
                 GameManager.instance.ItemRate = BattleInfo.ItemRate;
                 Debug.Log(" 골드 : " + BattleInfo.Golds +"\n"+" 아이템 : " + BattleInfo.ItemRate +"\n" );
                 SceneManager.LoadScene("2-4.GiftScene");
                 battleState = BattleState.waiting;
                 //SceneManager.LoadScene("SampleScene");
                break;
            default :
                break;
        }       
        SetHP_MP();
    }
    void BeforeTheBattle()
    {
        SetEnemy();
        SetCharacter();        
        SetStartUI();
    }

    

    IEnumerator WaitAnimate(BattleState NextState)
    {
        Debug.Log("Animating...");
        yield return new WaitForSeconds(1.5f)   ;
        Debug.Log("Animated...");
        battleState = NextState;
        if (NextState == BattleState.InBattle_EndBattle)
        {
            AttackerHilight("Enemy", L_BattleSpeed[0].Item2, false);
            AttackerHilight("Character", L_BattleSpeed[0].Item2, false);
        }
    }
    public void InputTargetBTN(int n)
    {
        // 타겟 스텟
        SetTargetStatus(target);
        if(bCheckSkill == true)
        {
            bChecktarget = true;
            target = n;
        }            
    }
    public void InputSkillBTN(int n)
    {
        bCheckSkill = true;
        selecSkill = n;         
    }
    
    void AttackerHilight(string str, int idx, bool b)
    {
        if(str == "Enemy")
        {
            Image img =  EnemyField[idx].transform.GetChild(1).GetComponent<Image>();
            if(b == true) img.color = Color.black;
            else img.color = Color.white;
        }
        else
        {
            Image img =  CharacterField[idx].transform.GetChild(1).GetComponent<Image>();
            if(b == true) img.color = Color.black;
            else img.color = Color.white;
        }
    }
    void SpwDamage(Save_Charater_Class.SD[] hitted)
    {

    }
}

// 배틀 단계
    public enum BattleState {BeforBattle, InBattle, InBattle_Tellent, 
    InBattle_SetTurn, InBattle_BattleUi,InBattle_Battle_Enemy,
    InBattle_Battle_My1, InBattle_Battle_Waiting, InBattle_Battle_My2, InBattle_Battle_Animate,
    InBattle_EndBattle, EndBattle,waiting};
    