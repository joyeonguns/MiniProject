using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using TMPro;
using UnityEngine.Pool;

[System.Serializable]
public struct Status
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

    public Status(int _Damage, float _Armor, int _Speed, int _Dodge, int _Critical, double _MaxHp, int _Resist)
    {
        Damage = _Damage;
        Armor = _Armor;
        Speed = _Speed;
        Dodge = _Dodge;
        Critical = _Critical;
        MaxHp = _MaxHp;
        Resist = _Resist;
    }
    public Status(CharacterDatas charData)
    {
        Damage = charData.Damage;
        Armor = charData.Armor;
        Speed = charData.Speed;
        Dodge = charData.Dodge;
        Critical = charData.Critical;
        MaxHp = charData.MaxHp;
        Resist = charData.Resist;
    }

    public void AllStatBuff(float buff)
    {
        float f = 1 + buff;
        Damage *= f;
        Armor *= (1 + buff / 2);
        Speed = (int)(Speed * f);
        Dodge = (int)(Dodge * f);
        Critical = (int)(Critical * f);
    }
}


[System.Serializable]
public class Character
{
    // SO 정보
    // public CharacterDatas 
    // 캐릭터 정보
    public string name;
    public e_Class Role;
    public Status Battlestatus;
    public Status status;
    public double curHp;
    public int mana;
    public int MaxMana;
    public bool Alive;
    public int turn = 0;
    public bool Main;
    public bool pierce;


    // 레벨
    public int exp;
    public int Level;

    // 데미지 폰트
    public DamagePool PoolSave;
    //public D_fontScripts font;
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
    public BaseSkill[] MySkill = new BaseSkill[4];

    public double Hp
    {
        get { return curHp; }
        set
        {
            curHp = value;
            if (curHp <= 0)
            {
                curHp = 0;
                CharDead();
            }
            else if (curHp > status.MaxHp)
            {
                curHp = status.MaxHp;
            }
        }
    }
    public int Mana
    {
        get { return mana; }
        set
        {
            mana = value;
            if (mana < 0)
                mana = 0;
            else if (mana > MaxMana)
                mana = MaxMana;
        }
    }

    public Character() { }
    public Character(Status stat, e_Class _role)
    {
        name = Enum.GetName(typeof(e_Class), _role) + "_0";
        status = stat;
        Battlestatus = stat;
        Role = _role;
        curHp = status.MaxHp;
        Alive = true;
        Level = 0;
        MaxMana = 10;
        Mana = 3;
        Main = false;
        pierce = false;
    }


    // 스킬
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

    // 데미지
    public virtual void TakeDamage(Character Other)
    {
        if (Alive == false)
        {
            return;
        }

        double Damage = RealDamage(Other);

        // 체력 감소
        Hp -= Damage;
        Mana++;
    }

    public double RealDamage(Character Other)
    {
        //크리 계산
        int CriRate = Other.Battlestatus.Critical - Battlestatus.Dodge;
        int rnd = UnityEngine.Random.Range(1, 101);

        double Damage;
        // 크리
        if (CriRate > 0 && CriRate > rnd)
        {
            Damage = Other.Battlestatus.Damage * 2 * (1 - Battlestatus.Armor);
            Printing_Damage(Color.red, "" + (int)Damage, 2.1f);

            // 크리티컬 스코어 업데이트
            if ((int)Other.Role <= 10)
            {
                GameManager.instance.GameScoreData.Critical_Count++;
            }
        }
        // 회피 
        else if (CriRate < 0 && (CriRate * -1) > rnd)
        {
            Damage = (int)(Other.Battlestatus.Damage / 2 * (1 - Battlestatus.Armor));
            Printing_Damage(Color.gray, "" + (int)Damage, 2.1f);

            // 회피 스코어 업데이트
            if ((int)this.Role <= 10)
            {
                GameManager.instance.GameScoreData.Dodge_Count++;
            }
        }
        // 기본
        else
        {
            Damage = (int)(Other.Battlestatus.Damage * (1 - Battlestatus.Armor));
            Printing_Damage(Color.black, "" + (int)Damage, 2.1f);
        }
        return Damage;
    }

    public virtual void TakeDamage_Item(int Damage, string type)
    {
        Hp -= Damage;
        Printing_Damage(Color.red, type + "\n" + Damage, 0.1f);
    }
    public virtual void TakeDamage_Bleed(int Damage, string type, float f)
    {
        Hp -= Damage;
        Printing_Damage(Color.red, type + "\n" + Damage, f);
    }
    public virtual void TakeDamage_Burn(int Damage, string type, float f, Vector2 Loc)
    {
        Hp -= Damage;
        Printing_Damage(Color.red, type + "\n" + Damage, f, Loc);
    }
    public virtual void TakeStun(float f)
    {
        Printing_Damage(Color.yellow, "Stun!!", f);
    }

    // 힐
    public void TakeHeal(double getHill)
    {
        if (Alive == false)
            return;

        //크리 계산
        int CriRate = 15;
        int rnd = UnityEngine.Random.Range(1, 101);

        int _Heal;
        // 크리
        if (CriRate > 0 && CriRate > rnd)
        {
            _Heal = (int)(getHill * 2);
            Printing_Damage(Color.green, "" + (int)_Heal, 2.1f);
        }
        // 기본
        else
        {
            _Heal = (int)(getHill);
            Printing_Damage(Color.yellow, "" + (int)_Heal, 2.1f);
        }


        // 체력 증가
        Hp += _Heal;
    }

    public void TakeMana(int getMana)
    {
        if (Alive == false)
            return;

        int resultMana = Mana + getMana;
        if (resultMana > MaxMana)
            resultMana = MaxMana;
        else if (resultMana < 0)
            resultMana = 0;
        getMana = resultMana - Mana;

        Printing_Damage(Color.blue, "" + (int)getMana, 2.1f);
        Mana += getMana;
    }



    public virtual void CharDead()
    {
        Alive = false;
        Debug.Log(name + " : 사망 ");
    }

    // 데미지 적용
    void Printing_Damage(Color color, string _Damage, float _WaitTime)
    {
        var spwDamage = PoolSave.GetPoolObject();
        spwDamage.GetComponent<RectTransform>().anchoredPosition = spwLoc;

        TextMeshProUGUI damageText = spwDamage.transform.GetChild(0).GetComponent<TextMeshProUGUI>();

        damageText.color = color;
        damageText.GetComponent<D_fontScripts>().Damage = _Damage;
        damageText.GetComponent<D_fontScripts>().WaitTime = _WaitTime;

        damageText.GetComponent<D_fontScripts>().StartEffect();
    }
    void Printing_Damage(Color color, string _Damage, float _WaitTime, Vector2 loc)
    {
        var spwDamage = PoolSave.GetPoolObject();
        spwDamage.GetComponent<RectTransform>().anchoredPosition = spwLoc;

        TextMeshProUGUI damageText = spwDamage.transform.GetChild(0).GetComponent<TextMeshProUGUI>();

        damageText.color = color;
        damageText.GetComponent<D_fontScripts>().Damage = _Damage;
        damageText.GetComponent<D_fontScripts>().WaitTime = _WaitTime;

        damageText.GetComponent<D_fontScripts>().StartEffect();
    }

    // 레벨업
    public void SetEXp(int num)
    {
        exp += num;
        int fullExp = 100 + Level * 50;
        while (exp >= fullExp)
        {
            exp -= fullExp;
            LevelUp();
            fullExp += 50;
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
        if (status.Armor > 0.9f)
            status.Armor = 0.9f;
    }

    public virtual void StartTurn(List<Character> MyGrup, int idx, List<Character> Enemy, int EnemyIdx)
    {
        Battlestatus = status;
        if (stunCount == 0)
        {
            turn++;
        }

        if (bleedCount > 0)
        {
            int Dmg = (int)(status.MaxHp * 0.1f) + bleedCount;
            if (Dmg < 1) Dmg = 1;
            TakeDamage_Bleed(Dmg, "출혈", 0.1f);
        }
        if (burnCount > 0)
        {
            foreach (var Char in MyGrup)
            {
                if (Char.Alive != true)
                    break;
                int Dmg = (int)(Char.curHp * 0.1f) + burnCount;
                if (Dmg < 1) Dmg = 1;
                Char.TakeDamage_Burn(Dmg, "화상", 0.4f, new Vector2(0, 100));
            }
        }
        if (regenCount > 0)
        {
            TakeHeal(2 + regenCount);
        }
        if (corrotionCount > 0)
        {
            Battlestatus.Armor /= 2;
        }
        if (frostCount > 0)
        {
            Battlestatus.Speed -= 2;
        }
        if (rapidCount > 0)
        {
            Battlestatus.Speed += 2;
        }
        if (enHanceCount > 0)
        {
            Battlestatus.Damage *= 1.3f;
            Battlestatus.Critical = (int)(Battlestatus.Critical * 1.3f);
        }
        if (stunCount > 0)
        {
            TakeStun(0.1f);
        }

        // 특성 적용

    }

    public virtual void EndTurn()
    {
        if (bleedCount > 0)
        {
            bleedCount--;
        }
        if (burnCount > 0)
        {
            burnCount--;
        }
        if (stunCount > 0)
        {
            stunCount--;
        }
        if (corrotionCount > 0)
        {
            corrotionCount--;
        }
        if (frostCount > 0)
        {
            frostCount--;
        }
        if (rapidCount > 0)
        {
            rapidCount--;
        }
        if (enHanceCount > 0)
        {
            enHanceCount--;
        }
        if (regenCount > 0)
        {
            regenCount--;
        }
    }
}