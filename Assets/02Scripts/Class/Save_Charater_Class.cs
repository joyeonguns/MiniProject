using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using TMPro;


public enum e_Class {worrier, magicion, supporter, assassin,bandit};

public class Save_Charater_Class : MonoBehaviour
{
    [System.Serializable]
    public struct Class_Status
    {
        public float s_Damage;
        // 방어력
        public float s_Armor;
        // 속도
        public int s_Speed;
        // 회피율
        public int s_Dodge;
        // 치명타
        public int s_Critical;
        // 최대 체력
        public int s_MaxHp;

        public Class_Status(int _s_Damage,float _s_Armor,int _s_Speed,int _s_Dodge,int _s_Critical,int _s_MaxHp)
        {
            s_Damage = _s_Damage;
            s_Armor = _s_Armor;
            s_Speed = _s_Speed;
            s_Dodge = _s_Dodge;
            s_Critical = _s_Critical;
            s_MaxHp = _s_MaxHp;      
        }
    }
    
    [System.Serializable]
    public class SD
    {
        // 이름
        public string c_Name;
        // 클래스
        public e_Class c_Class;
        // 클래스 스텟
        public Class_Status status;       
        // 현재 체력
        public int CurHp;
        // 마나
        int mana;
        // 생존
        public bool bAlive;
        // 레벨
        public int level;
        // 스킬 인덱스
        public int[] skill = new int[2];
        public Skills[] MySkill = new Skills[4];
        
        // 경험치
        public int exp;
        // 레벨
        public int Level;

        // 데미지스폰
        public float spwX, spwY;
        public TextMeshProUGUI font;

        
        // 생성자 오버로딩
        public SD(){}
        public SD(Class_Status _class, e_Class ec)
        {
            c_Name = Enum.GetName(typeof(e_Class),ec)+"_0";
            c_Class = ec;
            status = _class;
            CurHp = status.s_MaxHp;
            bAlive = true;      
            level = 0; 
            SetSkillnum();
        }

        // HP / Mana
        public int Mana 
        {
            get {return mana;} 
            set 
            {
                mana = value; 
                if(mana > 10)
                    mana = 10;
            }
        }
        public int Hp
        {
            get {return CurHp;}
            set
            {
                CurHp = value;
                if(CurHp > status.s_MaxHp)
                    CurHp = status.s_MaxHp;
                else if(CurHp < 0)
                    Dead();
            }
        }

        // 스킬 
        public void SetSkillnum()
        {
            int[] arr = new int[4] {1,2,3,4};
            System.Random random = new System.Random();
            arr = arr.OrderBy(x => random.Next()).ToArray();

            skill[0] = arr[0];
            skill[1] = arr[1];
        }

        public void SetSkillClass()
        {
            MySkill[0] = new Worrier_Skill(0);
            MySkill[1] = new Worrier_Skill(skill[0]);
            MySkill[2] = new Worrier_Skill(skill[1]);
            MySkill[3] = new Worrier_Skill(5);
        }

        // 전투
        // 버프
        public Class_Status ApplyBuff(Class_Status OtherStat)
        {
            return OtherStat;
        }
        // 데미지 받음
        public void TakeDamage(SD Other, Class_Status OtherStat)
        {
            Class_Status myStat = status;
            myStat = ApplyBuff(myStat);

            //크리 계산
            int CriRate = OtherStat.s_Critical - myStat.s_Dodge;
            int rnd = UnityEngine.Random.Range(1,101);

            int Damage;
            // 크리
            if(CriRate > 0 && CriRate > rnd)
            {
                Damage = (int)(OtherStat.s_Damage * 2 * (1-myStat.s_Armor));
                Printing_Damage(Color.red,Damage);
            }
            // 회피 
            else if(CriRate < 0 && (CriRate * -1) > rnd)
            {
                Damage = (int)(OtherStat.s_Damage / 2 * (1-myStat.s_Armor));
                Printing_Damage(Color.gray,Damage);
            }
            // 기본
            else
            {
                Damage = (int)(OtherStat.s_Damage * (1-myStat.s_Armor));
                Printing_Damage(Color.black,Damage);
            }


            // 체력 감소
            Hp -= Damage;
            Mana++;                        
        }
        // 힐
        public void TakeHeal(SD Other, Class_Status OtherStat)
        {

            //크리 계산
            int CriRate = OtherStat.s_Critical / 2;
            int rnd = UnityEngine.Random.Range(1,101);

            int _Heal;
            // 크리
            if(CriRate > 0 && CriRate > rnd)
            {
                _Heal = (int)(OtherStat.s_Damage * 2);
                Printing_Damage(Color.green,_Heal);
            }
            // 기본
            else
            {
                _Heal = (int)(OtherStat.s_Damage);
                Printing_Damage(Color.blue,_Heal);
            }


            // 체력 감소
            Hp += _Heal;
        }

        public void nomalAttack(SD Other)
        {
            Other.TakeDamage(this,this.status);
        }
        public void Dead()
        {
            this.bAlive = false;
            Debug.Log(c_Name + " : 사망 ");
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
            status.s_Armor += 0.05f;
            status.s_Critical += 20;
            status.s_Damage += 2;
            status.s_Dodge += 20;
            status.s_MaxHp += 8;
            status.s_Speed += 1;
        }

        // 데미지 적용
        void Printing_Damage(Color color, int _Damage)
        {
            var spwDamage = Instantiate(font);
            spwDamage.transform.SetParent(GameObject.Find( "_Damage").transform);
            spwDamage.rectTransform.anchoredPosition = new Vector2(spwX, spwY);
            spwDamage.color = color;
            spwDamage.text = ""+_Damage;
        }

    }

    public static Class_Status Worrier = new Class_Status(10,0.5f,5,40,70,40);
    public static Class_Status Magicion = new Class_Status(14,0.1f,2,30,70,25);    
    public static Class_Status Supporter = new Class_Status(5,0.3f,1,30,50,30);
    public static Class_Status Assassin = new Class_Status(7,0.2f,7,70,100,27);    


    public static Class_Status Bandit = new Class_Status(10,0.2f,5,40,30,30);
}
