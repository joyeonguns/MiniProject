using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Linq;
using UnityEngine.SceneManagement;

public class BossBtManager : Battle_Manager
{
    // Start is called before the first frame update


    // 브래스
    public GameObject BossObj;
    public Image BossHP;
    public List<GameObject> BossCondition;
    public Button BossBtn;
    public Image BossIMG;
    public GameObject BressObj;
    public Image BressImage;
    public TextMeshProUGUI BressText;


    int pase = 1;
    bool bpaseTrigger = false;


    void Start()
    {
        StartSetting();
        TurnStart();

        BossObj.SetActive(false);

        AttackObj.SetActive(false);
        HUDManager.instance.players = Character;
        HUDManager.instance.Enemys = Enemy;
    }


    public override void TurnEnd()
    {
        ResetSetting();
        
        for(int i = 0; i < 3; i++)
        {
            if(i < Character.Count && i < Enemy.Count)
            {
                SetCharCondition(i);
                SetEnCondition(i);
            }
            
        }

        // 페이즈 변경
        if (Enemy.Count > 1 && Enemy[1].Role == e_Class.Witch && ((Witch)Enemy[1]).pase == true)
        {
            pase = 2;
            ChangePase();
            TurnStart();
            
        }
        else
        {
            if (LiveCheck_Character(Character.Cast<Character>().ToList()) == false)
            {
                Debug.Log("패배");
                HUDManager.instance.Dead();

            }
            else if (LiveCheck_Enemy(Enemy.Cast<Character>().ToList()) == false)
            {
                Debug.Log("승리");
                EndBattle(12);
                GameManager.instance.GameScoreData.win_Boss += 1;
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
    }

    // 적 정보 설정
    public override void SetEnemy()
    {
        //BtLvl = GameManager.instance.Battle_Lvl;
        Enemy.Add(new Enemy());
        Enemy.Add(new Enemy());
        Enemy.Add(new Enemy());

        if (true)
        {
            // 1열 침묵 크리스탈
            Enemy[0] = new Cristal_Melle();
            Enemy[0].PoolSave = GetComponent<DamagePool>();
            Enemy[0].spwLoc = EnemyField[0].GetComponent<RectTransform>().anchoredPosition + new Vector2(50, 300);

            EnemyImage[0].sprite = EnemySprite[0];


            // 2열 흑마법사
            Enemy[1] = new Witch();
            Enemy[1].PoolSave = GetComponent<DamagePool>();
            Enemy[1].spwLoc = EnemyField[1].GetComponent<RectTransform>().anchoredPosition + new Vector2(50, 300);

            EnemyImage[1].sprite = EnemySprite[1];

            // 3열 공격 크리스탈
            Enemy[2] = new Cristal_Range();
            Enemy[2].PoolSave = GetComponent<DamagePool>();
            Enemy[2].spwLoc = EnemyField[2].GetComponent<RectTransform>().anchoredPosition + new Vector2(50, 300);
            EnemyImage[2].sprite = EnemySprite[0];
            EnemyImage[2].color = Color.red;
        }


    }

    //                   //
    // START SETTING END //
    //                   //

    public override void SetHP_MP()
    {
        base.SetHP_MP();

        if (Enemy[0].Role == e_Class.Barlog)
        {
            BressText.text = "" + ((Barlog)Enemy[0]).BressHp;

            float bressScale = (float)((Barlog)Enemy[0]).BressHp / 5;
            if (bressScale < 0) bressScale = 1;
            BressImage.GetComponent<RectTransform>().localScale = new Vector3(bressScale, bressScale, bressScale);
        }

    }

    void ChangePase()
    {
        Func<double> Hp = () => { if (Enemy[1].Hp < 0) return 0; return Enemy[1].Hp; };
        double BarlogHp = Hp() + Enemy[1].Mana * 3 + 300;

        Enemy Barlog = new Barlog();

        Barlog.status.MaxHp = BarlogHp;
        Barlog.Hp = BarlogHp;
        ((Barlog)Barlog).BressObj = BressObj;

        Enemy.RemoveAll(x => true);
        Enemy.Add(Barlog);

        HUDManager.instance.Enemys = Enemy.Cast<Enemy>().ToList();

        EnemyField[0].SetActive(false);
        EnemyField[1].SetActive(false);
        EnemyField[2].SetActive(false);
        
        EnemyField[0] = new GameObject();
        EnemyField[0] = BossObj;
        Enemy_HP[0] = BossHP;
        EnemyButton[0] = BossBtn;
        EnemyImage[0] = BossIMG;
        EnCondition_0 = new List<GameObject>();
        EnCondition_0 = BossCondition;

        foreach(var conditiong in EnCondition_0)
        {
            conditiong.SetActive(false);
        }

        EnemyField[0].SetActive(true);

        Enemy[0].PoolSave = GetComponent<DamagePool>();
        Enemy[0].spwLoc = EnemyField[0].GetComponent<RectTransform>().anchoredPosition + new Vector2(50, 300);
        // EnemyImage[0].sprite = EnemySprite[2];
        // EnemyImage[0].GetComponent<RectTransform>().sizeDelta = new Vector2(800, 800);

        // EnemyField[0].transform.GetChild(1).GetComponent<RectTransform>().localScale = new Vector3(4, 1, 1);
        // EnemyField[0].transform.GetChild(1).GetComponent<RectTransform>().anchoredPosition = new Vector3(0, -100, 0);
        // EnemyField[0].GetComponent<RectTransform>().anchoredPosition = new Vector2(550, 150);

        AttackEnemy[1].GetComponent<RectTransform>().sizeDelta = new Vector2(700, 700);
        AttackEnemy[1].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 250);
        // 550 150

        L_BattleSpeed.RemoveAll(x => true);
    }

    // Update is called once per frame
    void Update()
    {        
        SetHP_MP();
    }

   
}
