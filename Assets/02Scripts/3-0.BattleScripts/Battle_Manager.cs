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
    public GameInformation(int gold, int exp, int rate)
    {
        Golds = gold; Exp = exp; ItemRate = rate;
    }
    int bt_Lvl = 1;   
    public int ItemRate = 30;
    public string Battletype {get; set;}
    public int Golds {get; set;}
    public int Exp;
    public int TurnCounts = 0;
}

public class Battle_Manager : MonoBehaviour
{
    public int _time;
    public int _turn;
    // Start is called before the first frame update


    // 배틀씬 기본 설정
    int BtLvl;
    
    // 캐릭터 적 클래스
    [SerializeField]public List<Save.Player> Character = new List<Save.Player>();
    
    [SerializeField]public List<Save.Enemy> Enemy = new List<Save.Enemy>();
    

    // 아군 필드
    // 아군 선택 필드
    public GameObject[] CharacterField = new GameObject[3];
    // 아군 스프라이트
    public Image[] CharacterImages = new Image[3];
    public Sprite[] CharacterSprite = new Sprite[4];
    // 타겟 버튼
    public Button[] CharacterButton = new Button[3];
    // 순서 
    public GameObject[] CharOrder;
    // 아군 HP / MP
    public Image[] Character_HP = new Image[3];
    public Image[] Character_MP = new Image[3];
    public List<GameObject> CharCondition_0;
    public List<GameObject> CharCondition_1;
    public List<GameObject> CharCondition_2;

    // 적 선택 필드
    public GameObject[] EnemyField = new GameObject[3];
    // 적 스프라이트
    public Image[] EnemyImage = new Image[3];
    public Sprite[] EnemySprite = new Sprite[4];
    public Button[] EnemyButton = new Button[3];
    public GameObject[] EnOrder;
    // 적 HP / MP
    public Image[] Enemy_HP = new Image[3];
    public Image[] Enemy_MP = new Image[3];

    public List<GameObject> EnCondition_0;
    public List<GameObject> EnCondition_1;
    public List<GameObject> EnCondition_2;

    // Sprites
    public Sprite TombSprite;

    // 배틀 패널
    public GameObject SkillPannel;    
    public GameObject SkillButtonPannel;
    public Image[] SkillButton = new Image[4];
    public TextMeshProUGUI AttStatus;
    public TextMeshProUGUI TargetStatus;
    public Image AttackerImage;
    public GameObject SkillComment;

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
    // public Sprite[] CharIcon;
    public Sprite[] EnemyIcon;
    public Image[] CoverImage;
   
    // 데미지 폰트
    public GameObject Damage;


    // --- 필요 정보 ---
    // 선택한 스킬
    public int selecSkill;
    // 스킬 선택 체크
    public bool bCheckSkill;

    // 스킬 버프?
    bool bBuffSkill;

    // 타겟
    public int target;
    // 타겟 선택 체크
    public bool bChecktarget;


    // 배틀 스피드 스피드, idx. char
    [SerializeField] public List<Tuple<int,int,int>> L_BattleSpeed = new List<Tuple<int, int, int>>();   

    // 현재 공격자
    public int Attacker;

    GameInformation BattleInfo; 
    

    void Start()
    {       
        StartSetting();
        TurnStart();        
    }

    // START SETTING //

    protected void StartSetting()
    {
        SetBattleInfo();
        SetEnemy();
        SetCharacter();
        SetStartUI();
        for(int i = 0; i < 3; i++)
        {
            if(i < Character.Count)
            {
                SetCharCondition(i);
                SetEnCondition(i);
            }
        }
        HUDManager.instance.players = Character;
        HUDManager.instance.Enemys = Enemy;
    }


    // 턴 진행 //
    protected void TurnStart()
    {
        BattleInfo.TurnCounts++;
        RoundText.text = "R:" + BattleInfo.TurnCounts;

        SetSpeed();

        SetAttacker();
    }

    public void SetAttacker()
    {
        SetAttackOrder();
        Attacker = L_BattleSpeed[0].Item2;

        Debug.Log("Speed : " + L_BattleSpeed[0].Item1);
        // 적 차례
        if (L_BattleSpeed[0].Item3 == 1)
        {
            // 죽었으면 턴 종료
            if (Enemy[Attacker].bAlive == false)
            {
                TurnEnd();
            }
            else    // 턴에너미 실행
            {
                AttackerHilight("Enemy", Attacker, true);
                SetTargetStatus(Attacker);
                Enemy[Attacker].StartTurn(Enemy.Cast<Save.Character>().ToList(), Attacker, Character.Cast<Save.Character>().ToList(), 0);

                // 1.5초 후 TurnEnemy() 실행
                Invoke("TurnEnemy",1.5f);                
            }
        }
        // 캐릭터 차례
        else
        {
            // 죽었으면 턴 종료
            if (Character[Attacker].bAlive == false)
            {
                TurnEnd();
            }
            else
            {
                // 공격자 하이라이트
                AttackerHilight("Character", Attacker, true);
                Character[Attacker].StartTurn(Character.Cast<Save.Character>().ToList(), Attacker, Enemy.Cast<Save.Character>().ToList(), 0);

                // 1.5초 후 TurnPlayer() 실행
                Invoke("TurnPlayer",1.5f);  
            }            
        }
    }

    public virtual void TurnEnemy()
    {
        // 생존 + 스턴 X
        if (Enemy[Attacker].bAlive == true && Enemy[Attacker].stunCount == 0)
        {
            // 타겟 선택 And 스킬 선택
            target = Enemy[Attacker].Enemy_SetTarget(Character);
            int skillNum = Enemy[Attacker].SelectSkill();

            Enemy[Attacker].MySkill[skillNum].UseSkill(Enemy.Cast<Save.Character>().ToList(), Attacker, Character.Cast<Save.Character>().ToList(), target);
            // 공격 씬 출력
            SpwAttackAnim(Enemy[Attacker].MySkill[skillNum], false, Enemy[Attacker].MySkill[skillNum].SKill_Data.MultiTarget, Enemy[Attacker].MySkill[skillNum].SKill_Data.Buff);

            Enemy[Attacker].EndTurn();
            Invoke("TurnEnd", 3.0f);
        }
        else    // 스턴
        {
            Enemy[Attacker].EndTurn();
            TurnEnd();
        }          
    }

    void TurnPlayer()
    {
        // 생존 + 스턴 X
        if (Character[Attacker].bAlive == true && Character[Attacker].stunCount == 0)
        {
            // 스킬 패널 on / 이미지 설정
            SetSkillPannel(true, Attacker);
            // 공격자 스텟 표시
            SetAttackerStatus(Attacker);
            
            StartCoroutine(CheckSkillnTarget());
        }
        else
        {
            // 턴 종료
            Character[Attacker].EndTurn();
            TurnEnd();
        }
    }

    IEnumerator CheckSkillnTarget()
    {
          
        yield return null;
        
        if (bChecktarget == true && bCheckSkill == true)
        {
            TurnPlayer2();
        }
        else
        {
            StartCoroutine(CheckSkillnTarget());
        } 
    }

    void TurnPlayer2()
    {
        // 공격
        Character[Attacker].SetSkillClass();
        if (bBuffSkill == false)
        {
            Character[Attacker].MySkill[selecSkill].UseSkill(Character.Cast<Save.Character>().ToList(), Attacker, Enemy.Cast<Save.Character>().ToList(), target);
        }
        else
        {
            Character[Attacker].MySkill[selecSkill].UseSkill(Character.Cast<Save.Character>().ToList(), Attacker, Character.Cast<Save.Character>().ToList(), target);
        }

        SpwAttackAnim(Character[Attacker].MySkill[selecSkill], true, Character[Attacker].MySkill[selecSkill].SKill_Data.MultiTarget, Character[Attacker].MySkill[selecSkill].SKill_Data.Buff);

        if(Character[Attacker].MySkill[selecSkill].SKill_Data.Code == 0)
        {
            GameManager.instance.GameScoreData.Attack_Count++;
        }
        else if(Character[Attacker].MySkill[selecSkill].SKill_Data.Code == 5)
        {
            GameManager.instance.GameScoreData.Ulti_Count++;
        }
        else
        {
            GameManager.instance.GameScoreData.Skill_Count++;
        }
        
        // 턴 종료
        Character[Attacker].EndTurn();
        Invoke("TurnEnd" ,3.0f);
    }

    public virtual void TurnEnd()
    {
        for(int i = 0; i < 3; i++)
        {
            if(i < Character.Count && i < Enemy.Count)
            {
                SetCharCondition(i);
                SetEnCondition(i);
            }            
        }
        // 셋팅 초기화 
        ResetSetting();
        // 라이브 체크
        if (LiveCheck_Character(Character.Cast<Save.Character>().ToList()) == false)
        {
            Debug.Log("패배");
            HUDManager.instance.Dead();
            
        }
        else if (LiveCheck_Enemy(Enemy.Cast<Save.Character>().ToList()) == false)
        {
            Debug.Log("승리");
            EndBattle(10);
        }
        else
        {
            // 다음 차례
            if (L_BattleSpeed.Count > 0)
            {
                L_BattleSpeed.RemoveAt(0);
            }
            else    // 턴 초기화
            {
                Debug.LogError("L_BattleSpeed error");
            }

            // 다음 공격자 있음
            if (L_BattleSpeed.Count != 0)
            {
                SetAttacker();
            }
            else
            {
                TurnStart();
            }
        }
            
    }

    public void ResetSetting()
    {
        // 데드 체크 - 무덤
        CheckDead();

        // 턴종료 셋팅 초기화
        AttackerHilight("Enemy", L_BattleSpeed[0].Item2, false);
        AttackerHilight("Character", L_BattleSpeed[0].Item2, false);
        
        // 플레이어 턴 앤드
        if(L_BattleSpeed[0].Item3 == 0)
        {
            SetSkillPannel(false, Attacker);
        }
        
        target = 0;
        bCheckSkill = false;
        bChecktarget = false;
        bBuffSkill = false;

    }

    public virtual void EndBattle(int num)
    {
        Debug.Log("승리");

        GameManager.instance.ResultData.ResultMode = ResultEnum.NomalBattle;
        GameManager.instance.ResultData.Gold = BattleInfo.Golds;
        GameManager.instance.ResultData.ItemRate = BattleInfo.ItemRate;
        GameManager.instance.ResultData.Exp = BattleInfo.Exp;

        BattleEndTellent();

        SceneManager.LoadScene(num);
        //battleState = BattleState.waiting;
    }

    // 턴 진행 //

    protected void SetBattleInfo()
    {
        BattleInfo = new GameInformation(80, 80, 30);
    }

    // 적 정보 설정
    public virtual void SetEnemy()
    {
        // 레벨 설정
        int level = GameManager.instance.floor / 2 + UnityEngine.Random.Range(0, 1);   
        
        Enemy.Add(new Save.Enemy());
        Enemy.Add(new Save.Enemy());
        Enemy.Add(new Save.Enemy());

        if(level < 3)
        {
            SpawnBandit(level);
        }
        else if(level < 5)
        {
            SpawnKnight(level);
        }
        else
        {
            SpawnAbomination(level);
        }
             
    }

    void SpawnBandit(int level)
    {
        for(int i = 0; i < Enemy.Count; i ++)
        {
            Enemy[i] = new Save.Enemy(Save.Bandit, e_Class.Bandit);
            Enemy[i].PoolSave = GetComponent<DamagePool>();
            Enemy[i].spwLoc = EnemyField[i].GetComponent<RectTransform>().anchoredPosition + new Vector2(50,300);
            EnemyImage[i].sprite = EnemySprite[0];
            for(int j = 0; j < level; j++)
            {
                Enemy[i].LevelUp();
            }
        }
    }
    void SpawnKnight(int level)
    {
        for(int i = 0; i < Enemy.Count; i ++)
        {
            Enemy[i] = new Save.Enemy(Save.Knight, e_Class.Knight);
            Enemy[i].PoolSave = GetComponent<DamagePool>();
            Enemy[i].spwLoc = EnemyField[i].GetComponent<RectTransform>().anchoredPosition + new Vector2(50,300);
            EnemyImage[i].sprite = EnemySprite[1];
            for(int j = 0; j < level; j++)
            {
                Enemy[i].LevelUp();
            }
        }
    }
    void SpawnAbomination(int level)
    {
        for(int i = 0; i < Enemy.Count; i ++)
        {
            Enemy[i] = new Save.Enemy(Save.Abomination, e_Class.Abomination);
            Enemy[i].PoolSave = GetComponent<DamagePool>();
            Enemy[i].spwLoc = EnemyField[i].GetComponent<RectTransform>().anchoredPosition + new Vector2(50,300);
            EnemyImage[i].sprite = EnemySprite[2];
            for(int j = 0; j < level; j++)
            {
                Enemy[i].LevelUp();
            }
        }
    }

    // 아군 저장
    void SetCharacter()
    {
        Character = GameManager.instance.MyParty;
        
        for(int i = 0; i< Character.Count; i++)
        {
            Character[i].PoolSave = GetComponent<DamagePool>();
            Character[i].spwLoc = CharacterField[i].GetComponent<RectTransform>().anchoredPosition + new Vector2(50,300);            

            CharacterImages[i].sprite = CharacterSprite[(int)Character[i].Role];
            Character[i].Battlestatus = Character[i].status;
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

    public virtual void SetHP_MP()
    {
        for(int i = 0; i < Character.Count; i++)
        {
            Character_HP[i].fillAmount = (float)Character[i].Hp / (float)Character[i].status.MaxHp;
            Character_MP[i].fillAmount = (float)Character[i].Mana /  (float)Character[i].MaxMana;
        }
        for(int i = 0; i < Enemy.Count; i++)
        {
            Enemy_HP[i].fillAmount = (float)Enemy[i].Hp / (float)Enemy[i].status.MaxHp;
            Enemy_MP[i].fillAmount = (float)Enemy[i].Mana / 10.0f;
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
                int rndSpeed = Character[i].Battlestatus.Speed;
                L_BattleSpeed.Add(Tuple.Create(rndSpeed + rnd1, i, 0));
            }            
        }
        for(int i = 0; i < Enemy.Count; i++)
        {
            if(Enemy[i].bAlive == true)
            {
                int rnd2 = UnityEngine.Random.Range(1,7);
                int rndSpeed = Enemy[i].Battlestatus.Speed;
                L_BattleSpeed.Add(Tuple.Create(rndSpeed + rnd2, i, 1));
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
        int rolenum = (int)Character[n].Role;
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
        
        if (check == true)
        {
            SkillButton[0].sprite = charData.attack;
            int skill_1 = Character[L_BattleSpeed[0].Item2].SkillNum[1];
            int skill_2 = Character[L_BattleSpeed[0].Item2].SkillNum[2];
            SkillButton[1].sprite = charData.skill[skill_1 - 1];
            SkillButton[2].sprite = charData.skill[skill_2 - 1];
            SkillButton[3].sprite = charData.ulti;

            SkillButton[0].GetComponent<SkillComment>().skill = Character[n].MySkill[0];
            SkillButton[1].GetComponent<SkillComment>().skill = Character[n].MySkill[1];
            SkillButton[2].GetComponent<SkillComment>().skill = Character[n].MySkill[2];
            SkillButton[3].GetComponent<SkillComment>().skill = Character[n].MySkill[3];

            for(int i = 1; i < 4; i++)
            {
                Character[n].SetSkillClass();
                if(Character[n].MySkill[i].SKill_Data.Cost > Character[n].Mana)
                {
                    SkillButton[i].GetComponent<Button>().interactable = false;
                }
                else
                {
                    SkillButton[i].GetComponent<Button>().interactable = true;
                }
            }
        } 
        else{
            if(SkillComment.transform.childCount != 0)
            {
                Destroy(SkillComment.transform.GetChild(0).gameObject);
            }
        }         
    }

    void SetAttackerStatus(int n)
    {
        // 공격자 스텟
        AttStatus.text =
        "Name : " + Character[n].name + ""+n + "\n" +
        "Damage : " + Character[n].Battlestatus.Damage + "\n" +
        "Armor : " + Character[n].Battlestatus.Armor + "\n" +
        "Critical : " + Character[n].Battlestatus.Critical + "\n" +
        "Dodge : " + Character[n].Battlestatus.Dodge + "\n" +
        "Hp : " + Character[n].Hp;
    }
    void SetTargetStatus(int n)
    {
        // 타겟 스텟
        TargetStatus.text =
        "Name : " + Enemy[n].name + ""+n+ "\n" +
        "Damage : " + Enemy[n].Battlestatus.Damage + "\n" +
        "Armor : " + Enemy[n].Battlestatus.Armor + "\n" +
        "Critical : " + Enemy[n].Battlestatus.Critical + "\n" +
        "Dodge : " + Enemy[n].Battlestatus.Dodge + "\n" +
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
                CoverImage[i].color = Color.white;
                return;
            }
            AttOrder[i].SetActive(true);
            Image icon =  AttOrder[i].GetComponent<Image>();
            
            // 캐릭터, 적
            if(L_BattleSpeed[i].Item3 == 0) 
            {
                int roleCode = (int)Character[L_BattleSpeed[i].Item2].Role;
                icon.sprite = SOManager.GetChar().CharDatas[roleCode].Icon;
                CoverImage[i].color = Color.green;
            }
            else
            {
                if(Enemy[L_BattleSpeed[i].Item2].Role == e_Class.Barlog || Enemy[L_BattleSpeed[i].Item2].Role == e_Class.Witch)
                {
                    icon.sprite = EnemyIcon[1];
                }
                else
                {
                    icon.sprite = EnemyIcon[0];
                }
                CoverImage[i].color = Color.red;
            }
            
        }
        
        // 순서표시 오브젝트 비활성화
        CharOrder[0].SetActive(false);
        CharOrder[1].SetActive(false);
        CharOrder[2].SetActive(false);

        EnOrder[0].SetActive(false);
        EnOrder[1].SetActive(false);
        EnOrder[2].SetActive(false);

        // 타겟 버튼 비활성화
        CharacterButton[0].gameObject.SetActive(false); 
        CharacterButton[1].gameObject.SetActive(false); 
        CharacterButton[2].gameObject.SetActive(false);

        EnemyButton[0].gameObject.SetActive(false); 
        EnemyButton[1].gameObject.SetActive(false); 
        EnemyButton[2].gameObject.SetActive(false);

    }

    void Update()
    {        
        SetHP_MP();
    }

    public bool LiveCheck_Character(List<Save.Character> characters)
    {
        // 생사 확인
        bool charAlive = true;
        foreach (var Char in characters)
        {
            if (Char.Main == true && Char.bAlive == false)
                charAlive = false;
        }

        return charAlive;
    }
    public bool LiveCheck_Enemy(List<Save.Character> enemy)
    {
        // 생사 확인
        bool charAlive = false;
        foreach (var en in enemy)
        {
            if (en.bAlive == true)
                charAlive = true;
        }

        return charAlive;
    }
 

    void SpwAttackAnim(BaseSkill usingSkill, bool bPlayerAtk, bool bmultiTarget, bool bBuff)
    {
        AttackObj.SetActive(true);
        Attack_Text.text = "[" + usingSkill.SKill_Data.Name + "]";
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
                        AttackEnemy[i].SetActive(false);  
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

    public void BattleEndTellent()
    {
        foreach (var tel in GameManager.instance.Tellents[1])
        {
            if(tel.type == Etel_type.End)
            {
                tel.TellentApply(Character.Cast<Save.Character>().ToList(), 0, Enemy.Cast<Save.Character>().ToList(), 0);
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
        
        
        if(Character[Attacker].MySkill[n].SKill_Data.Buff == true)
        {
            bBuffSkill = true;    
            
            for(int i = 0; i < 3; i++)
            {
                CharacterButton[i].gameObject.SetActive(true); 
                EnemyButton[i].gameObject.SetActive(false); 

                if(Character[Attacker].MySkill[n].SKill_Data.MultiTarget == true)
                {
                    CharacterButton[i].GetComponent<BattleButtonEvents>().isMultiTarget = true;
                }
                else
                {
                    CharacterButton[i].GetComponent<BattleButtonEvents>().isMultiTarget = false;
                }
            }                  
        }
        else    // 공격
        {
            bBuffSkill = false;

            for(int i = 0; i < 3; i++)
            {
                CharacterButton[i].gameObject.SetActive(false); 
                EnemyButton[i].gameObject.SetActive(true); 

                if(Character[Attacker].MySkill[n].SKill_Data.MultiTarget == true)
                {
                    EnemyButton[i].GetComponent<BattleButtonEvents>().isMultiTarget = true;
                }
                else
                {
                    EnemyButton[i].GetComponent<BattleButtonEvents>().isMultiTarget = false;
                }
            }
        }

  
    }
    
    void AttackerHilight(string str, int idx, bool b)
    {
        if(str == "Enemy")
        {
            if(b == true) 
            {
                EnOrder[idx].SetActive(true);
            }
            else 
            {
                EnOrder[idx].SetActive(false);
            }
        }
        else
        {
            if(b == true) 
            {
                CharOrder[idx].SetActive(true);
            }
            else 
            {
                CharOrder[idx].SetActive(false);
            }
        }
    }
    
    public void SetCharCondition(int _Attacker)
    {
        List<List<GameObject>> Conditions = new List<List<GameObject>>(){CharCondition_0,CharCondition_1,CharCondition_2};

        // 출혈
        if(Character[_Attacker].bleedCount != 0)
        {
            Conditions[_Attacker][0].SetActive(true);
        }
        else
        {
            Conditions[_Attacker][0].SetActive(false);
        }
        // 화상
        if(Character[_Attacker].bleedCount != 0)
        {
            Conditions[_Attacker][1].SetActive(true);
        }
        else
        {
            Conditions[_Attacker][1].SetActive(false);
        }
        // 부식
        if(Character[_Attacker].corrotionCount != 0)
        {
            Conditions[_Attacker][2].SetActive(true);
        }
        else
        {
            Conditions[_Attacker][2].SetActive(false);
        }
        // 강화
        if(Character[_Attacker].enHanceCount != 0)
        {
            Conditions[_Attacker][3].SetActive(true);
        }
        else
        {
            Conditions[_Attacker][3].SetActive(false);
        }
        // 둔화
        if(Character[_Attacker].frostCount != 0)
        {
            Conditions[_Attacker][4].SetActive(true);
        }
        else
        {
            Conditions[_Attacker][4].SetActive(false);
        }
        // 속도
        if(Character[_Attacker].rapidCount != 0)
        {
            Conditions[_Attacker][5].SetActive(true);
        }
        else
        {
            Conditions[_Attacker][5].SetActive(false);
        }
        // 리젠
        if(Character[_Attacker].regenCount != 0)
        {
            Conditions[_Attacker][6].SetActive(true);
        }
        else
        {
            Conditions[_Attacker][6].SetActive(false);
        }
        // 스턴
        if(Character[_Attacker].stunCount != 0)
        {
            Conditions[_Attacker][7].SetActive(true);
        }
        else
        {
            Conditions[_Attacker][7].SetActive(false);
        }
    }

    public void SetEnCondition(int _Attacker)
    {
        List<List<GameObject>> Conditions = new List<List<GameObject>>(){EnCondition_0,EnCondition_1,EnCondition_2};

        // 출혈
        if(Enemy[_Attacker].bleedCount != 0)
        {
            Conditions[_Attacker][0].SetActive(true);
        }
        else
        {
            Conditions[_Attacker][0].SetActive(false);
        }
        // 화상
        if(Enemy[_Attacker].bleedCount != 0)
        {
            Conditions[_Attacker][1].SetActive(true);
        }
        else
        {
            Conditions[_Attacker][1].SetActive(false);
        }
        // 부식
        if(Enemy[_Attacker].corrotionCount != 0)
        {
            Conditions[_Attacker][2].SetActive(true);
        }
        else
        {
            Conditions[_Attacker][2].SetActive(false);
        }
        // 강화
        if(Enemy[_Attacker].enHanceCount != 0)
        {
            Conditions[_Attacker][3].SetActive(true);
        }
        else
        {
            Conditions[_Attacker][3].SetActive(false);
        }
        // 둔화
        if(Enemy[_Attacker].frostCount != 0)
        {
            Conditions[_Attacker][4].SetActive(true);
        }
        else
        {
            Conditions[_Attacker][4].SetActive(false);
        }
        // 속도
        if(Enemy[_Attacker].rapidCount != 0)
        {
            Conditions[_Attacker][5].SetActive(true);
        }
        else
        {
            Conditions[_Attacker][5].SetActive(false);
        }
        // 리젠
        if(Enemy[_Attacker].regenCount != 0)
        {
            Conditions[_Attacker][6].SetActive(true);
        }
        else
        {
            Conditions[_Attacker][6].SetActive(false);
        }
        // 스턴
        if(Enemy[_Attacker].stunCount != 0)
        {
            Conditions[_Attacker][7].SetActive(true);
        }
        else
        {
            Conditions[_Attacker][7].SetActive(false);
        }
    }
}

    