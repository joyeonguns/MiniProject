using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Enemy : Character
{
    public bool melee = true;
    public Enemy() { }
    public Enemy(Status stat, e_Class _role) : base(stat, _role)
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
        if (rnd >= 5)
        {
            target = 0;
        }
        else if (rnd >= 3)
        {
            target = 1;
        }
        else
        {
            target = 2;
        }

        Debug.Log("target : " + target);
        // 공격
        if (target >= characters.Count || characters[target].Alive == false)
        {
            target = (target + 1) % 3;
            if (target >= characters.Count || characters[target].Alive == false)
            {
                target = (target + 1) % 3;
                if (target >= characters.Count || characters[target].Alive == false)
                {
                    Debug.LogError("enemy target error : " + target);
                }
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
        get { return bressHp; }
        set
        {
            bressHp = value;
            if (bressHp < 0)
            {
                bressHp = 0;
            }
        }
    }
    public Barlog() : base(Save.Barlog_Stat, e_Class.Barlog)
    {
        SetSkillClass();
    }

    public override void TakeDamage(Character Other)
    {
        if (Alive == false)
        {
            return;
        }

        double Damage = RealDamage(Other);

        // 체력 감소
        Hp -= Damage;
        Mana++;

        BressHp -= Damage;
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
        if (turn % 5 == 3)
        {
            BressHp = 40;
            BressObj.SetActive(true);
            skillNum = 2;
        }
        else if (turn % 5 == 0)
        {
            Debug.Log("turn % 5 : " + (turn % 5));
            BressObj.SetActive(false);
            skillNum = 3;
        }
        else
        {
            skillNum = UnityEngine.Random.Range(0, 2);
        }
        return skillNum;
    }

}

[System.Serializable]
public class Witch : Enemy
{
    public bool pase = false;

    public Witch() : base(Save.Witch_Stat, e_Class.Witch)
    {
        MaxMana = 150;
        pase = false;
        SetSkillClass();
    }

    public override void SetSkillClass()
    {
        MySkill[1] = new BossSkillScripts(6);
        MySkill[2] = new BossSkillScripts(7);
    }

    public override int SelectSkill()
    {
        int skillNum = 0;

        if (turn == 7)
        {
            skillNum = 2;
        }
        else
        {

            skillNum = 1;
        }
        return skillNum;
    }

    public override void CharDead()
    {
        base.CharDead();
        pase = true;
    }

}

[System.Serializable]
public class Cristal_Melle : Enemy
{
    public Cristal_Melle() : base(Save.Crystal_Stat, e_Class.Crystal_Melle)
    {
        MaxMana = 100;
        SetSkillClass();
    }

    public override void SetSkillClass()
    {
        MySkill[1] = new BossSkillScripts(4);
        MySkill[2] = new BossSkillScripts(5);
    }

    public override int SelectSkill()
    {
        int skillNum = 0;

        if (turn % 3 != 0)
        {
            skillNum = 1;
        }
        else
        {
            skillNum = 2;
        }
        return skillNum;
    }

    public override void CharDead()
    {
        base.CharDead();
    }
}

[System.Serializable]
public class Cristal_Range : Enemy
{
    public Cristal_Range() : base(Save.Crystal_Stat, e_Class.Crystal_Range)
    {
        MaxMana = 100;
        SetSkillClass();
    }

    public override void SetSkillClass()
    {
        MySkill[1] = new BossSkillScripts(1);
        MySkill[2] = new BossSkillScripts(2);
        MySkill[3] = new BossSkillScripts(3);
    }

    public override int SelectSkill()
    {
        int skillNum = 0;

        if (this.curHp < 20)
        {
            skillNum = 3;
        }
        else
        {
            skillNum = UnityEngine.Random.Range(1, 3);
        }
        return skillNum;
    }

    public override void CharDead()
    {
        base.CharDead();
    }

}

