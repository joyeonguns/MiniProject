using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using TMPro;

public enum BuffEnum {Burn, Bleeding, Stun, Corrotion, EnHanceCount, Frost, Rapid, Regen}

public class Save : MonoBehaviour
{
    [System.Serializable]
    public struct St_Stat
    {
        public float Damage;
        // 방어력
        public float Armor;
        // 속도
        public int Speed;
        // 회피율
        public int Dodge;
        // 치명타
        public int Critical;
        // 최대 체력
        public double MaxHp;
        // 저항력
        public int Resist;

        public St_Stat(int _Damage, float _Armor, int _Speed, int _Dodge, int _Critical, double _MaxHp, int _Resist)
        {            
            Damage = _Damage;
            Armor = _Armor;
            Speed = _Speed;
            Dodge = _Dodge;
            Critical = _Critical;
            MaxHp = _MaxHp;  
            Resist = _Resist;
        }
        public void AllStatBuff(float buff)
        {
            float f = 1+buff;
            Damage *= f;
            Armor *= (1+buff/2);
            Speed = (int)(Speed * f);
            Dodge = (int)(Dodge * f);
            Critical = (int)(Critical * f);
        }
    }  

    [System.Serializable]

    public class Character
    {
        // 캐릭터 정보
        public string name;
        public e_Class Role;
        public St_Stat Battlestatus;
        public St_Stat status;
        public double curHp;
        public int mana;
        public int MaxMana;
        public bool bAlive;
        public int turn = 0;
        public bool Main;
        public bool pierce;


        // 레벨
        public int exp;
        public int Level;

        // 데미지 폰트
        public TextMeshProUGUI font;
        public Vector2 spwLoc;

        // 상태이상
        public int burnCount = 0;
        public int stunCount = 0;
        public int bleedCount = 0;
        public int corrotionCount = 0;
        public int enHanceCount = 0;
        public int frostCount = 0;
        public int rapidCount = 0;
        public int regenCount = 0;

        // 스킬
        public int[] SkillNum = new int[4];
        public Skills[] MySkill = new Skills[4];

        public double Hp
        {
            get {return curHp;}
            set 
            {
                curHp = value;
                if(curHp < 0)
                {
                    curHp = 0;
                    Dead();
                }
                    
            }
        }
        public int Mana
        {
            get {return mana;}
            set 
            {
                mana = value;
                if(mana < 0)
                    mana = 0;
            }
        }

        public Character() {}
        public Character(St_Stat stat,  e_Class _role)
        {
            name = Enum.GetName(typeof(e_Class), _role)+"_0";
            status = stat;
            Battlestatus = stat;
            Role = _role;
            curHp = status.MaxHp;
            bAlive = true;
            Level = 0; 
            MaxMana = 10;
            Mana = 3;
            Main = false;
            pierce = false;
        }

        public virtual void SetSkill()
        {
            SkillNum[0] = 0;
            SkillNum[1] = 0;
            SkillNum[2] = 0;
            SkillNum[3] = 5;
        }

        public virtual void SetSkillClass()
        {
            
        }

        public virtual void TakeDamage(Character Other)
        {
            if(bAlive == false)
                return;

            double Damage = RealDamage(Other);

            // 체력 감소
            Hp -= Damage;
            Mana++;             
        }
        public double RealDamage(Character Other)
        {
            //크리 계산
            int CriRate = Other.Battlestatus.Critical - Battlestatus.Dodge;
            int rnd = UnityEngine.Random.Range(1,101);

            double Damage;
            // 크리
            if(CriRate > 0 && CriRate > rnd)
            {
                Damage = Other.Battlestatus.Damage * 2 * (1-Battlestatus.Armor);
                Printing_Damage(Color.red, ""+(int)Damage, 2.1f);
            }
            // 회피 
            else if(CriRate < 0 && (CriRate * -1) > rnd)
            {
                Damage = (int)(Other.Battlestatus.Damage / 2 * (1-Battlestatus.Armor));
                Printing_Damage(Color.gray, ""+(int)Damage, 2.1f);
            }
            // 기본
            else
            {
                Damage = (int)(Other.Battlestatus.Damage * (1-Battlestatus.Armor));
                Printing_Damage(Color.black, ""+(int)Damage, 2.1f);
            }
            return Damage;
        }

        public virtual void TakeDamage(int Damage, string type)
        {
            Hp -= Damage;
            Printing_Damage(Color.red, type + "\n" + Damage, 0.1f);
        }

        // 힐
        public void TakeHeal(double getHill)
        {
            if(bAlive == false)
                return;

            //크리 계산
            int CriRate = 15;
            int rnd = UnityEngine.Random.Range(1,101);

            int _Heal;
            // 크리
            if(CriRate > 0 && CriRate > rnd)
            {
                _Heal = (int)(getHill * 2);
                Printing_Damage(Color.green, ""+(int)_Heal, 2.1f);
            }
            // 기본
            else
            {
                _Heal = (int)(getHill);
                Printing_Damage(Color.yellow, ""+(int)_Heal, 2.1f);
            }


            // 체력 증가
            Hp += _Heal;
        }

        public void TakeMana(int getMana)
        {
            if(bAlive == false)
                return;

            int resultMana = Mana + getMana;
            if(resultMana > MaxMana)
                resultMana = MaxMana;
            else if(resultMana < 0)
                resultMana = 0;
            getMana =  resultMana - Mana;

            Printing_Damage(Color.blue,""+(int)getMana, 2.1f);
            Mana += getMana;
        }

        

        public virtual void Dead()
        {
            this.bAlive = false;
            Debug.Log(name + " : 사망 ");
        }

        // 데미지 적용
        void Printing_Damage(Color color, string _Damage, float _WaitTime)
        {
            var spwDamage = Instantiate(font);
            spwDamage.transform.SetParent(GameObject.Find( "_Damage").transform);
            spwDamage.rectTransform.anchoredPosition = spwLoc;
            spwDamage.color = color;
            spwDamage.GetComponent<D_fontScripts>().Damage =_Damage;
            spwDamage.GetComponent<D_fontScripts>().WaitTime =_WaitTime;
        }

        // 레벨업
        public void SetEXp(int num)
        {
            exp += num;
            int fullExp = 100 + Level*50;
            while(exp >= fullExp)
            {
                exp -= fullExp;
                LevelUp();
            }
        }
        public void LevelUp()
        {
            Level++;
            status.Armor += 0.05f;
            status.Critical += 20;
            status.Damage += 2;
            status.Dodge += 20;
            status.MaxHp += 8;
            status.Speed += 1;
            Hp += 8;
            if(status.Armor > 0.9f)
                status.Armor = 0.9f;
        }

        public virtual void StartTurn(List<Character> MyGrup, int idx, List<Character> Enemy, int EnemyIdx)
        {
            Battlestatus = status;
            if(stunCount == 0)
                turn++;
            if(burnCount > 0)
            {
                foreach (var Char in MyGrup)
                {
                    int Dmg = (int)(Char.curHp * 0.1f);
                    if(Dmg < 1) Dmg = 1;
                    Char.TakeDamage(Dmg, "화상");
                }
            }
            if(bleedCount > 0)
            {
                int Dmg = (int)(status.MaxHp * 0.1f);
                if(Dmg < 1) Dmg = 1;
                TakeDamage(Dmg, "출혈");
            }
            if(regenCount > 0)
            {                
                TakeHeal(5);
            }
            if(corrotionCount > 0)
            {
                Battlestatus.Armor /= 2;
            }
            if(frostCount > 0)
            {
                Battlestatus.Speed += 2;
            }
            if(rapidCount > 0)
            {
                Battlestatus.Speed -= 2;
            }
            if(enHanceCount > 0)
            {
                Battlestatus.Damage *= 1.3f;
            }

            // 특성 적용
            
        } 

        public virtual void EndTurn()
        {       
            if(bleedCount > 0)
            {
                bleedCount--;
            }
            if(burnCount > 0)
            {
                burnCount--;
            }     
            if(stunCount > 0)
            {
                stunCount--;
            }
            if(corrotionCount > 0)
            {
                corrotionCount--;
            }
            if(frostCount > 0)
            {
                frostCount--;
            }
            if(rapidCount > 0)
            {
                rapidCount--;
            }
            if(enHanceCount > 0)
            {
                enHanceCount--;
            }
            if(regenCount > 0)
            {                
                regenCount--;
            }
        } 
    }

    [System.Serializable]
    public class Player : Character
    {        
        public Player() {}
        public Player(St_Stat stat,  e_Class _role) : base(stat, _role)
        {
            
            SetSkill();
            SetSkillClass();
        }

        public override void SetSkill()
        {
            
            base.SetSkill();

            int[] arr = new int[4] {1,2,3,4};
            System.Random rnd = new System.Random();
            arr = arr.OrderBy(x => rnd.Next()).ToArray();

            SkillNum[1] = arr[1];
            SkillNum[2] = arr[2];
        }
        public override void SetSkillClass()
        {
            for(int i = 0; i < 4; i++)
            {
                MySkill[i] = new Worrier_Skill(SkillNum[i]);
            }
            switch ((int)Role)
            {
                case 0:
                break;

                case 1:
                break;

                case 2:
                break;

                case 3:
                break;

                case 4:
                break;
            }
        }
        public void ApplyGetTellent(List<Character> MyGrup, int idx, List<Character> Enemy, int EnemyIdx)
        {
            foreach (var tel in GameManager.instance.Tellents_C)
            {
                if (tel.type == Etel_type.Get)
                {
                    tel.TellentApply(MyGrup.Cast<Save.Character>().ToList(), idx, Enemy.Cast<Save.Character>().ToList(), 0);
                }
            }
            foreach (var tel in GameManager.instance.Tellents_B)
            {
                if (tel.type == Etel_type.Get)
                {
                    tel.TellentApply(MyGrup.Cast<Save.Character>().ToList(), idx, Enemy.Cast<Save.Character>().ToList(), 0);
                }
            }
            foreach (var tel in GameManager.instance.Tellents_A)
            {
                if (tel.type == Etel_type.Get)
                {
                    tel.TellentApply(MyGrup.Cast<Save.Character>().ToList(), idx, Enemy.Cast<Save.Character>().ToList(), 0);
                }
            }
            foreach (var tel in GameManager.instance.Tellents_S)
            {
                if (tel.type == Etel_type.Get)
                {
                    tel.TellentApply(MyGrup.Cast<Save.Character>().ToList(), idx, Enemy.Cast<Save.Character>().ToList(), 0);
                }
            }
        }

        public override void StartTurn(List<Character> MyGrup, int idx, List<Character> Enemy, int EnemyIdx)
        {
            base.StartTurn(MyGrup, idx, Enemy, EnemyIdx);
            // 텔런트 적용
            foreach (var tel in GameManager.instance.Tellents_C)
            {
                if (tel.type == Etel_type.Battle)
                {
                    tel.TellentApply(MyGrup.Cast<Save.Character>().ToList(), idx, Enemy.Cast<Save.Character>().ToList(), 0);
                }
            }
            foreach (var tel in GameManager.instance.Tellents_B)
            {
                if (tel.type == Etel_type.Battle)
                {
                    tel.TellentApply(MyGrup.Cast<Save.Character>().ToList(), idx, Enemy.Cast<Save.Character>().ToList(), 0);
                }
            }
            foreach (var tel in GameManager.instance.Tellents_A)
            {
                if (tel.type == Etel_type.Battle)
                {
                    tel.TellentApply(MyGrup.Cast<Save.Character>().ToList(), idx, Enemy.Cast<Save.Character>().ToList(), 0);
                }
            }
            foreach (var tel in GameManager.instance.Tellents_S)
            {
                if (tel.type == Etel_type.Battle)
                {
                    tel.TellentApply(MyGrup.Cast<Save.Character>().ToList(), idx, Enemy.Cast<Save.Character>().ToList(), 0);
                }
            }
        }
    }

    [System.Serializable]
    public class Enemy : Character
    {
       
        public Enemy() {}
        public Enemy(St_Stat stat,  e_Class _role) : base(stat, _role)
        {
            SetSkill();
            SetSkillClass();
        }

        public override void SetSkill()
        {
            SkillNum[0] = 0;
        }
        public override void SetSkillClass()
        {
            MySkill[0] = new BossSkillScripts(0);
        }

        public override void StartTurn(List<Character> MyGrup, int idx, List<Character> Enemy, int EnemyIdx)
        {
            base.StartTurn(MyGrup, idx, Enemy, EnemyIdx);
            // 텔런트 적용
        }

        public virtual int Enemy_SetTarget(List<Player> characters)
        {
            // 타겟 설정
            int target;
            int rnd = UnityEngine.Random.Range(1, 11);
            if (rnd >= 5) target = 0;
            else if (rnd >= 3) target = 1;
            else target = 2;

            Debug.Log("target : " + target);
            // 공격
            if (target >= characters.Count || characters[target].bAlive == false)
            {
                target = (target + 1) % 3;
                if (target >= characters.Count || characters[target].bAlive == false)
                {
                    target = (target + 1) % 3;
                }
            }
            return target;
        }
        
        public virtual int SelectSkill()
        {
            // MySkill[0].UseSkill(Casters, caster, Status, Targets, target);
            return 0;
        }

    }

    [System.Serializable]
    public class Barlog : Enemy
    {
        public GameObject BressObj;
        double bressHp;
        public double BressHp
        {
            get{return bressHp;}
            set{
                bressHp = value;
                if(bressHp < 0)
                    bressHp = 0;
            }
        }
        public Barlog() : base(Barlog_Stat, e_Class.barlog)
        {            
            SetSkillClass();
        }

        public override void TakeDamage(Character Other)
        {
            base.TakeDamage(Other);
            BressHp -= RealDamage(Other);
        }

        public override void SetSkillClass()
        {
            MySkill[0] = new BossSkillScripts(0);
            MySkill[1] = new BossSkillScripts(8);
            MySkill[2] = new BossSkillScripts(9);
            MySkill[3] = new BossSkillScripts(10);
        }
        
        public override int SelectSkill()
        {         
            int skillNum = 0;
            if(turn % 5 == 3)
            {
                BressHp = 40;
                BressObj.SetActive(true);
                skillNum = 2;
            }
            else if(turn % 5 == 0)
            {
                Debug.Log("turn % 5 : " +(turn % 5));
                BressObj.SetActive(false);
                skillNum = 3;
            }
            else
            {
                skillNum = UnityEngine.Random.Range(0,2);                
            }
            return skillNum;
        }

    }

    [System.Serializable]
    public class Witch : Enemy
    {
        public bool pase = false;

        public Witch() : base(Witch_Stat, e_Class.witch)
        {            
            MaxMana = 150;
            pase =false;
            SetSkillClass();
        }

        public override void SetSkillClass()
        {
           MySkill[1] = new BossSkillScripts(6);
           MySkill[2] = new BossSkillScripts(7);
        }
        
        public override int SelectSkill()
        {
            // if ((int)(Enemy[n].c_Class) != 8)
            // {
            //     // 발록이 아님
            //     switch (n)
            //     {   
            //         case 0:
            //             if (BattleInfo.TurnCounts != 3)
            //                 skillIdx = 1;
            //             else
            //                 skillIdx = 2;
            //             break;

            //         case 1:
            //             if (BattleInfo.TurnCounts != 7)
            //                 skillIdx = 1;
            //             else
            //                 skillIdx = 2;
            //             break;

            //         case 2:
            //             int rnd = UnityEngine.Random.Range(1, 4);
            //             skillIdx = rnd;
            //             break;
            //     }

            // }
            int skillNum = 0;
            if(turn == 7)
            {
                skillNum = 2;
            }
            else
            {
                skillNum = 1;                  
            }
            return skillNum;
        }

        public override void Dead()
        {
            base.Dead();
            pase = true;
        }

    }

    public static St_Stat Adventure = new St_Stat(7, 0.2f, 3, 30, 50, 30, 0);
    public static St_Stat Worrier = new St_Stat(10,0.5f,5,40,70,40, 30);
    public static St_Stat Magicion = new St_Stat(14,0.1f,2,30,70,25, 0);    
    public static St_Stat Supporter = new St_Stat(5,0.3f,1,30,50,30, 30);
    public static St_Stat Assassin = new St_Stat(7,0.2f,7,70,100,27, 10); 

    public static St_Stat Bandit = new St_Stat(10,0.2f,5,40,30,30,0);
    public static St_Stat Witch_Stat = new St_Stat(0,0.2f,3,30,0,100,30);
    public static St_Stat Crystal_Stat = new St_Stat(10,0,0,30,50,50,100);
    public static St_Stat Barlog_Stat = new St_Stat(30,0.4f,10,100,100,0,40);


    





}


