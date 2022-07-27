using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Linq;
using UnityEngine.SceneManagement;

public class BossBtManager : MonoBehaviour
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
    public Image[] CharacterImages = new Image[3];
    public Sprite[] CharacterSprite = new Sprite[4];
    // 아군 HP / MP
    public Image[] Character_HP = new Image[3];
    public Image[] Character_MP = new Image[3];

    // 적 선택 필드
    public GameObject[] EnemyField = new GameObject[3];
    // 적 스프라이트
    public Image[] EnemyImage = new Image[3];
    public Sprite[] EnemySprite = new Sprite[4];
    // 적 HP / MP
    public Image[] Enemy_HP = new Image[3];
    public Image[] Enemy_MP = new Image[3];
    public GameObject BressObj;
    public Image BressImage;
    public TextMeshProUGUI BressText;

    // Sprites
    public Sprite TombSprite;

    // 배틀 패널
    public GameObject SkillPannel;
    public GameObject SkillButtonPannel;
    public Image[] SkillButton = new Image[4];
    public TextMeshProUGUI AttStatus;
    public TextMeshProUGUI TargetStatus;
    public Image AttackerImage;

    // 전투 컷신
    public GameObject AttackObj;
    public TextMeshProUGUI Attack_Text;
    public GameObject Attack;
    public GameObject[] AttackCharacter = new GameObject[3];
    public GameObject[] AttackEnemy = new GameObject[3];
    public GameObject Buff;
    public GameObject[] BuffImg = new GameObject[3];
    public RectTransform HitObj;


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
    
    int pase = 1;
    bool bpaseTrigger = false;
    
   
    void Start()
    {       
        SetBattleInfo();
        SetEnemy();
        SetCharacter();
        SetStartUI();
        AttackObj.SetActive(false);
    }

    // START SETTING //

    void SetBattleInfo()
    {
        BattleInfo = new GameInformation();
        BattleInfo.Times = 0;
        BattleInfo.TurnCounts = 0;
        BattleInfo.Golds = 450;
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
            Enemy[0] = new Save_Charater_Class.SD(Save_Charater_Class.Crystal,e_Class.bandit);
            Enemy[0].font = Damage;
            Enemy[0].spwX = EnemyField[0].GetComponent<RectTransform>().anchoredPosition.x +50;
            Enemy[0].spwY = EnemyField[0].GetComponent<RectTransform>().anchoredPosition.y + 300;
            Enemy[0].MaxMana = 50;
            EnemyImage[0].sprite = EnemySprite[0];

            Enemy[0].MySkill[1] = new BossSkillScripts(4);
            Enemy[0].MySkill[2] = new BossSkillScripts(5);

            // 2열 전사 / 궁수
            Enemy[1] = new Save_Charater_Class.SD(Save_Charater_Class.Witch,e_Class.bandit);
            Enemy[1].font = Damage;
            Enemy[1].spwX = EnemyField[1].GetComponent<RectTransform>().anchoredPosition.x + 50;
            Enemy[1].spwY = EnemyField[1].GetComponent<RectTransform>().anchoredPosition.y + 300;
            Enemy[1].MaxMana = 150;
            EnemyImage[1].sprite = EnemySprite[1];

            Enemy[1].MySkill[1] = new BossSkillScripts(6);
            Enemy[1].MySkill[2] = new BossSkillScripts(7);

            // 3열 궁수
            Enemy[2] = new Save_Charater_Class.SD(Save_Charater_Class.Crystal,e_Class.bandit);
            Enemy[2].font = Damage;
            Enemy[2].spwX = EnemyField[2].GetComponent<RectTransform>().anchoredPosition.x + 50;
            Enemy[2].spwY = EnemyField[2].GetComponent<RectTransform>().anchoredPosition.y + 300;
            EnemyImage[2].sprite = EnemySprite[0];
            EnemyImage[2].color = Color.red;

            Enemy[2].MySkill[1] = new BossSkillScripts(1);
            Enemy[2].MySkill[2] = new BossSkillScripts(2);
            Enemy[2].MySkill[3] = new BossSkillScripts(3);
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

            CharacterImages[i].sprite = CharacterSprite[(int)Character[i].c_Class];
        }
    }

    // 시작 UI 세팅
    void SetStartUI()
    {
        for(int i = 0; i < 3; i++)
        {
            if(i >= Character.Count)
            {
                CharacterField[i].gameObject.SetActive(false);                
            }
        }
        SkillButtonPannel.SetActive(false);
    }

    //                   //
    // START SETTING END //
    //                   //

    void SetHP_MP()
    {
        for(int i = 0; i < Character.Count; i++)
        {
            Character_HP[i].fillAmount = (float)Character[i].Hp / (float)Character[i].status.s_MaxHp;
            Character_MP[i].fillAmount = (float)Character[i].Mana / (float)Character[i].MaxMana;
        }
        for(int i = 0; i < Enemy.Count; i++)
        {
            Enemy_HP[i].fillAmount = (float)Enemy[i].Hp / (float)Enemy[i].status.s_MaxHp;
            Enemy_MP[i].fillAmount = (float)Enemy[i].Mana / (float)Enemy[i].MaxMana;
        }       

        BressText.text = ""+Enemy[0].BressHp;
        int bressScale =  Enemy[0].BressHp / 5;
        if(bressScale < 0) bressScale = 1;
        BressImage.GetComponent<RectTransform>().localScale = new Vector3(bressScale,bressScale,bressScale);
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

    // 전투 중 특성
    void Apply_BTTellent()
    {
        foreach (var tel in GameManager.instance.BeforTellents)
        {
            Debug.Log("tellents name : " + tel.Tel_Name);
            volaStatus = tel.TellentApply(BattleInfo, Character, Attacker, Ch_Status, volaStatus, Enemy, target);
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
                int rnd2 = UnityEngine.Random.Range(1,7);
                Save_Charater_Class.Class_Status CurStatus = En_Status[i];
                L_BattleSpeed.Add(Tuple.Create(CurStatus.s_Speed + rnd2, i, 1));
                Debug.Log("e speed : " + CurStatus.s_Speed);
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

    bool CheckedAlive()
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
            SceneManager.LoadScene("1-0.StartScene");
            Application.Quit();
            //return;
        }
        return false;
    }
    void EnemyTurn(int n)
    {
        // 공격 씬 출력
        int target = Enemy_SetTarget(n);
        int skillIdx = 0;
        
        if((int)(Enemy[n].c_Class) != 8)
        {
            // 발록이 아님
            switch (n)
            {
                case 0:
                if(BattleInfo.TurnCounts != 3)
                    skillIdx = 1;
                else
                    skillIdx = 2;
                break;

                case 1:
                if(BattleInfo.TurnCounts != 7)
                    skillIdx = 1;
                else
                    skillIdx = 2;
                break;

                case 2:
                int rnd = UnityEngine.Random.Range(1,4);
                skillIdx = rnd;
                break;
            }
            
        }
        else
        {
            int turn = BattleInfo.TurnCounts % 7;
            Debug.Log("Barlog turn : " + turn);
            // 발록임
            
            if(turn == 4)
            {
                skillIdx = 2;
                BressObj.SetActive(true);
            }                
            else if(turn == 0)
            {
                skillIdx = 3;
                BressObj.SetActive(false);
            }    
            else
            {
                int rnd = UnityEngine.Random.Range(0,2);
                skillIdx = rnd;
            }                      
        }
        
        Enemy[Attacker].MySkill[skillIdx].UseSkill(Enemy, n, Enemy[n].status, Character, target);
        SpwAttackAnim(Enemy[Attacker].MySkill[skillIdx], false, Enemy[Attacker].MySkill[skillIdx].bmultiTarget, Enemy[Attacker].MySkill[skillIdx].bBuff);
                   
    }
    int Enemy_SetTarget(int n)
    {       
        // 타겟 설정
        int rnd = UnityEngine.Random.Range(1, 11);
        if (rnd >= 5) target = 0;
        else if (rnd >= 3) target = 1;      
        else target = 2;

        Debug.Log("target : " +target);
        // 공격
        if(target >= Character.Count || Character[target].bAlive == false)
        {
            target = (target+1) % 3;
            if (target >= Character.Count || Character[target].bAlive == false)
            {
                target = (target + 1) % 3;
            }
        }

        return target;
    }

    void ChangePase()
    {
        Func<int> Hp = () => {if(Enemy[1].Hp < 0) return 0; return Enemy[1].Hp;}; 
        int BarlogHp = Hp() + Enemy[1].Mana + 3000;
        
        Save_Charater_Class.SD Barlog = new Save_Charater_Class.Barlog_Class();
        Barlog.MySkill[0] = new BossSkillScripts(0);
        Barlog.MySkill[1] = new BossSkillScripts(8);
        Barlog.MySkill[2] = new BossSkillScripts(9);
        Barlog.MySkill[3] = new BossSkillScripts(10);

        Barlog.status.s_MaxHp = BarlogHp;
        Barlog.Hp = BarlogHp;

        Enemy.RemoveAll(x => true);
        Enemy.Add(Barlog);

        EnemyField[1].SetActive(false);
        EnemyField[2].SetActive(false);

        Enemy[0].font = Damage;
        Enemy[0].spwX = EnemyField[0].GetComponent<RectTransform>().anchoredPosition.x + 50;
        Enemy[0].spwY = EnemyField[0].GetComponent<RectTransform>().anchoredPosition.y + 300;
        EnemyImage[0].sprite = EnemySprite[2];
        EnemyImage[0].GetComponent<RectTransform>().sizeDelta = new Vector2(800,800);

        EnemyField[0].transform.GetChild(1).GetComponent<RectTransform>().localScale = new Vector3(4,1,1);
        EnemyField[0].transform.GetChild(1).GetComponent<RectTransform>().anchoredPosition = new Vector3(0,-100,0);
        EnemyField[0].GetComponent<RectTransform>().anchoredPosition = new Vector2(550,150);

        AttackEnemy[1].GetComponent<RectTransform>().sizeDelta = new Vector2(700,700);
        AttackEnemy[1].GetComponent<RectTransform>().anchoredPosition = new Vector2(0,250);
        // 550 150

        L_BattleSpeed.RemoveAll(x => true);
        En_Status[0] = Barlog.status;

        // 선택 초기화
        target = 0;
        bCheckSkill = false;
        bChecktarget = false;
        SetSkillPannel(false, Attacker);
        // 휘발성 스텟 초기화
        volaStatus = new Save_Charater_Class.Class_Status();

        battleState = BattleState.InBattle_SetTurn;

        // 턴 초기화
        BattleInfo.TurnCounts = 0;
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
    
    // 공격 순서
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
        // 전투 단계
        switch (battleState)
        {            
            // 1. 전투 이전
            case BattleState.BeforBattle :
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
                BattleInfo.TurnCounts++;                
                RoundText.text = "R:" + BattleInfo.TurnCounts;

                SetSpeed();
                battleState = BattleState.InBattle_BattleUi;
                break;

            // 2-2-1. 턴 배틀UI
            case BattleState.InBattle_BattleUi :
                // 공격 순서 UI

                SetAttackOrder();
                // 공격자 초기화
                Attacker = L_BattleSpeed[0].Item2;

                // 적 공격
                if(L_BattleSpeed[0].Item3 == 1) 
                {
                    // 죽었으면 다음 씬으로
                    if (Enemy[Attacker].bAlive == false)
                    {
                        battleState = BattleState.InBattle_EndBattle;
                        break;
                    }
                    else
                    {
                        StartCoroutine(WaitAnimate(BattleState.InBattle_Battle_Enemy));
                        battleState = BattleState.InBattle_Battle_Animate;
                    }                    
                }                
                // 캐릭터 공격
                else
                {
                    // 죽었으면 넘어감
                    if (Character[Attacker].bAlive == false)
                    {
                        battleState = BattleState.InBattle_EndBattle;
                        break;
                    }
                    battleState = BattleState.InBattle_Battle_My1;
                }      

                break;

            // 2-2-2 적 공격
            case BattleState.InBattle_Battle_Enemy :                
                AttackerHilight("Enemy",Attacker,true);
                SetTargetStatus(Attacker);
                EnemyTurn(Attacker);
                StartCoroutine(WaitAnimate(BattleState.InBattle_EndBattle));
                battleState = BattleState.InBattle_Battle_Animate;
                
                break;
            // 2-2-3  캐릭터 턴
            case BattleState.InBattle_Battle_My1 :

                Apply_BTTellent();
                
                // 특성 적용
                volaStatus = new Save_Charater_Class.Class_Status();
                volaStatus = Ch_Status[Attacker];
                Debug.Log("vola : " + volaStatus.s_Critical);
            
                
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
                SpwAttackAnim(Character[Attacker].MySkill[selecSkill], true, Character[Attacker].MySkill[selecSkill].bmultiTarget, Character[Attacker].MySkill[selecSkill].bBuff);
                battleState = BattleState.InBattle_Battle_Animate;
                break;
            // 2-2-4. 공격 애니메이션
            case BattleState.InBattle_Battle_Animate :                
                
                break;
            // 2-2-5. 턴종료
            case BattleState.InBattle_EndBattle :          
                // 데드 체크
                CheckDead();

                // 턴종료
                Debug.Log("num : "+ L_BattleSpeed.Count);
                if(L_BattleSpeed.Count > 0)
                    L_BattleSpeed.RemoveAt(0);

                // 선택 초기화
                target = 0;
                bCheckSkill = false;
                bChecktarget = false;                
                SetSkillPannel(false,Attacker);
                // 휘발성 스텟 초기화
                volaStatus = new Save_Charater_Class.Class_Status();

                // 페이즈 변경
                if(pase == 1)
                {
                    if(Enemy[1].pase2 == true || Enemy[1].bAlive == false)
                    {
                        pase = 2;
                        ChangePase();
                        break;
                    }
                    
                }

                // 생사 확인
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

    

    IEnumerator WaitAnimate(BattleState NextState)
    {
        Debug.Log("Animating...");

        yield return new WaitForSeconds(2.5f)   ;
        Debug.Log("Animated...");

        battleState = NextState;
        if (NextState == BattleState.InBattle_EndBattle)
        {
            AttackerHilight("Enemy", L_BattleSpeed[0].Item2, false);
            AttackerHilight("Character", L_BattleSpeed[0].Item2, false);
        }

    }


    void SpwAttackAnim(Skills usingSkill, bool bPlayerAtk, bool bmultiTarget, bool bBuff)
    {
        AttackObj.SetActive(true);
        Attack_Text.text = "[" + usingSkill.skillName + "]";
        Invoke("DeleteAttackAnim",2.0f);

        // 버프
        if(bBuff == true)
        {
            Buff.SetActive(true);
            Attack.SetActive(false);

            Image[] Casters;
            int maxCount;
            if(bPlayerAtk == true)
            {
                Casters = CharacterImages;   
                maxCount = Character.Count;             
            }
            else
            {
                Casters = EnemyImage;
                maxCount = Enemy.Count;
            }

            if (bmultiTarget == false)
            {
                BuffImg[0].SetActive(false);
                BuffImg[1].SetActive(true);
                BuffImg[2].SetActive(false);

                BuffImg[1].GetComponent<Image>().sprite = Casters[Attacker].sprite;
            }
            else
            {
                for (int i = 0; i < 3; i++)
                {
                    BuffImg[i].SetActive(false);  
                    if (i < maxCount)
                    {
                        BuffImg[i].SetActive(true);
                        BuffImg[i].GetComponent<Image>().sprite = Casters[i].sprite;
                        BuffImg[i].GetComponent<Image>().color = Color.white;
                        if (i == Attacker)
                            BuffImg[i].GetComponent<Image>().color = Color.gray;
                    }
                }
            }
        }
        // 공격
        else
        {
            Buff.SetActive(false);
            Attack.SetActive(true);

            // 플레이어 공격
            if(bPlayerAtk == true)
            {
                AttackCharacter[0].SetActive(false);
                AttackCharacter[1].SetActive(true);
                AttackCharacter[2].SetActive(false);

                AttackCharacter[1].GetComponent<Image>().sprite = CharacterImages[Attacker].sprite;

                // 타겟 1명
                if(bmultiTarget == false)
                {
                    AttackEnemy[0].SetActive(false);
                    AttackEnemy[1].SetActive(true);
                    AttackEnemy[2].SetActive(false);

                    AttackEnemy[1].GetComponent<Image>().sprite = EnemyImage[target].sprite; 
                    HitObj.localScale = new Vector3(1.5f,1.5f,1.5f);
                }
                // 타겟 멀티
                else
                {                    
                    for (int i = 0; i < 3; i++)
                    {
                        AttackCharacter[i].SetActive(false);  
                        if(i < Enemy.Count)
                        {
                            AttackEnemy[i].SetActive(true);                        
                            AttackEnemy[i].GetComponent<Image>().sprite = EnemyImage[i].sprite;                            
                        }                        
                    }
                    HitObj.localScale = new Vector3(3,3,3);
                }
                HitObj.anchoredPosition = new Vector3(400,0,0);
            }
            // 적 공격
            else
            {
                AttackEnemy[0].SetActive(false);
                AttackEnemy[1].SetActive(true);
                AttackEnemy[2].SetActive(false);

                AttackEnemy[1].GetComponent<Image>().sprite = EnemyImage[Attacker].sprite;

                // 타겟 1명
                if(bmultiTarget == false)
                {
                    AttackCharacter[0].SetActive(false);
                    AttackCharacter[1].SetActive(true);
                    AttackCharacter[2].SetActive(false);

                    AttackCharacter[1].GetComponent<Image>().sprite = CharacterImages[target].sprite; 
                    HitObj.localScale = new Vector3(1.5f,1.5f,1.5f);
                }
                // 타겟 멀티
                else
                {                    
                    for (int i = 0; i < 3; i++)
                    {
                        AttackCharacter[i].SetActive(false);   
                        if(i < Character.Count)
                        {
                            AttackCharacter[i].SetActive(true);                        
                            AttackCharacter[i].GetComponent<Image>().sprite = CharacterImages[i].sprite;                            
                        }                        
                    }
                    HitObj.localScale = new Vector3(3,3,3);
                }
                HitObj.anchoredPosition = HitObj.anchoredPosition = new Vector3(-400,0,0);;
            }
        }        

    }
    void DeleteAttackAnim()
    {
        AttackObj.SetActive(false);
        Attack.SetActive(false);
        Buff.SetActive(false);
    }

    void CheckDead()
    {
        for (int i = 0; i < 3; i++)
        {
            if(i < Character.Count && Character[i].bAlive == false)
            {
                CharacterImages[i].sprite = TombSprite;
            }
        }

        for (int i = 0; i < 3; i++)
        {
            if(i < Enemy.Count && Enemy[i].bAlive == false)
            {
                EnemyImage[i].sprite = TombSprite;
            }
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
