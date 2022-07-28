using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using TMPro;

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
    }  

    [System.Serializable]

    public class Character
    {
        // 캐릭터 정보
        public string name;
        public e_Class Role;
        public St_Stat basestatus;
        public St_Stat status;
        public double curHp;
        public int mana;
        public int MaxMana;
        public bool bAlive;

        // 레벨
        public int exp;
        public int Level;

        // 데미지 폰트
        public TextMeshPro font;
        public Vector2 spwLoc;

        // 상태이상
        int burnCount = 0;
        int stunCount = 0;
        int bleedCount = 0;
        int corrotionCount = 0;
        int frostCount = 0;
        int rapidCount = 0;

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
            basestatus = stat;
            Role = _role;
            curHp = status.MaxHp;
            bAlive = true;
            Level = 0;            
            SetSkill();
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

        public virtual void TakeDamage(Character Other, St_Stat OtherStat, St_Stat curStat)
        {
            if(bAlive == false)
                return;

            St_Stat myStat = curStat;

            //크리 계산
            int CriRate = OtherStat.Critical - myStat.Dodge;
            int rnd = UnityEngine.Random.Range(1,101);

            int Damage;
            // 크리
            if(CriRate > 0 && CriRate > rnd)
            {
                Damage = (int)(OtherStat.Damage * 2 * (1-myStat.Armor));
                Printing_Damage(Color.red, ""+Damage, 2.1f);
            }
            // 회피 
            else if(CriRate < 0 && (CriRate * -1) > rnd)
            {
                Damage = (int)(OtherStat.Damage / 2 * (1-myStat.Armor));
                Printing_Damage(Color.gray, ""+Damage, 2.1f);
            }
            // 기본
            else
            {
                Damage = (int)(OtherStat.Damage * (1-myStat.Armor));
                Printing_Damage(Color.black, ""+Damage, 2.1f);
            }


            // 체력 감소
            Hp -= Damage;
            Mana++;             
        }
        public virtual void TakeDamage(int Damage, string type)
        {
            Hp -= Damage;
            Printing_Damage(Color.red, type + "\n" + Damage, 0.1f);
        }

        // 힐
        public void TakeHeal(Character Other, St_Stat OtherStat)
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
                _Heal = (int)(OtherStat.Damage * 2);
                Printing_Damage(Color.green, ""+_Heal, 2.1f);
            }
            // 기본
            else
            {
                _Heal = (int)(OtherStat.Damage);
                Printing_Damage(Color.yellow, ""+_Heal, 2.1f);
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

            Printing_Damage(Color.blue,""+getMana, 2.1f);
            Mana += getMana;
        }

        

        public void Dead()
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
        }

        public virtual void StartTurn(List<Character> MyGrup)
        {
            if(burnCount > 0)
            {
                foreach (var Char in MyGrup)
                {
                    int Dmg = (int)(Char.curHp * 0.1f);
                    if(Dmg < 1) Dmg = 1;
                    Char.TakeDamage(Dmg, "화상");
                }
                burnCount--;
            }
            if(bleedCount > 0)
            {
                int Dmg = (int)(status.MaxHp * 0.1f);
                if(Dmg < 1) Dmg = 1;
                TakeDamage(Dmg, "출혈");
                bleedCount--;
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
        } 

        public virtual void EndTurn()
        {            
            if(stunCount > 0)
            {
                stunCount--;
            }
        } 
    }

    [System.Serializable]
    public class Player : Character
    {
        Player() {}
        Player(St_Stat stat,  e_Class _role) : base(stat, _role)
        {
            SetSkill();
            SetSkillClass();
        }

        public override void SetSkill()
        {
            base.SetSkill();

            int[] arr = new int[4];
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

        public override void StartTurn(List<Character> MyGrup)
        {
            base.StartTurn(MyGrup);
            // 텔런트 적용
        }
    }

    public static St_Stat Adventure = new St_Stat(7, 0.2f, 3, 30, 50, 30, 0);
    public static St_Stat Worrier = new St_Stat(10,0.5f,5,40,70,40, 30);
    public static St_Stat Magicion = new St_Stat(14,0.1f,2,30,70,25, 0);    
    public static St_Stat Supporter = new St_Stat(5,0.3f,1,30,50,30, 30);
    public static St_Stat Assassin = new St_Stat(7,0.2f,7,70,100,27, 10); 
}


