using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSkillScripts : Skills
{
    Dictionary<int,string> DSkillName = new Dictionary<int, string>() {
        {0,"Attack"},
        {1,"FireBall"},
        {2,"IceRain"},
        {3,"Heal"},
        {4,"ManaDrain"},        
        {5,"Injection"},
        {6,"ManaCharge"},
        {7,"SummonBarlog"},
        {8,"Earthquake"},
        {9,"Charge"},
        {10,"Bress"}
        };
    Dictionary<int,int> DSkillCost = new Dictionary<int, int>() {{0,0},{1,2},{2,2},{3,4},{4,1},{5,10}};
    Dictionary<int,string> DSkillComent = new Dictionary<int, string>() {{0,"a"},{1,"b"},{2,"c"},{3,"d"},{4,"e"},{5,"u"}};      
    public BossSkillScripts(int num)
    {
        //skillName = DSkillName[num];
        //skillComent = DSkillComent[num];
        //manaCost = DSkillCost[num];

        skillName = DSkillName[num];
        switch (num)
        {            
            case 0:
                UseSkill = Attack;
                break;
            case 1:
                UseSkill = FireBall;
                break;
            case 2:
                UseSkill = IceRain;
                bmultiTarget = true;
                break;
            case 3:
                UseSkill = Heal;
                bBuff = true;
                break;
            case 4:
                UseSkill = ManaDrain;
                bmultiTarget = true;
                break;
            case 5:
                UseSkill = Injection;
                bBuff = true;
                break;
            case 6:
                UseSkill = ManaCharge;
                bBuff = true;
                break;
            case 7:
                UseSkill = SummonBarlog;
                bBuff = true;
                break;
            case 8:
                UseSkill = Earthquake;
                bmultiTarget = true;
                break;
            case 9:
                UseSkill = Charge;
                bBuff = true;
                break;
            case 10:
                UseSkill = Bress;
                bmultiTarget = true;
                break;        
        }
    }
    void Attack(List<Save.Character> Caster, int CasterIdx, List<Save.Character> Enemy, int EnemyIdx)
    {
        Debug.Log(Caster[CasterIdx].name + " : " + DSkillName[0]);
        
        // 스킬 특성
        Caster[CasterIdx].Battlestatus.Damage *= 1f;
        Enemy[EnemyIdx].TakeDamage(Caster[CasterIdx]);
    }
    void FireBall(List<Save.Character> Caster, int CasterIdx, List<Save.Character> Enemy, int EnemyIdx)
    {
        Debug.Log(Caster[CasterIdx].name + " : " + DSkillName[1]);
        
        // 스킬 특성
        Caster[CasterIdx].Battlestatus.Damage *= 1.5f;
        Enemy[EnemyIdx].TakeDamage(Caster[CasterIdx]);
    }
    void IceRain(List<Save.Character> Caster, int CasterIdx, List<Save.Character> Enemy, int EnemyIdx)
    {
        Debug.Log(Caster[CasterIdx].name + " : " + DSkillName[2]);
        
        // 스킬 특성
        Caster[CasterIdx].Battlestatus.Damage *= 1f;

        foreach(var enemy in Enemy)
            enemy.TakeDamage(Caster[CasterIdx]);
    }
    void Heal(List<Save.Character> Caster, int CasterIdx, List<Save.Character> Enemy, int EnemyIdx)
    {
        Debug.Log(Caster[CasterIdx].name + " : " + DSkillName[3]);
        
        // 스킬 특성
        Caster[CasterIdx].Battlestatus.Damage *= 1.5f;
        foreach(var caster in Caster)
        {
            caster.TakeHeal(Caster[CasterIdx].Battlestatus.Damage);
        }
    }
    void ManaDrain(List<Save.Character> Caster, int CasterIdx, List<Save.Character> Enemy, int EnemyIdx)
    {
        Debug.Log(Caster[CasterIdx].name + " : " + DSkillName[4]);
        
        
        // 스킬 특성
        int getMana = 0;
        foreach (var target in Enemy)
        {
            getMana += target.Mana;
            target.Mana = 0;
        }
        Caster[CasterIdx].TakeMana(getMana);
    }
    void Injection(List<Save.Character> Caster, int CasterIdx, List<Save.Character> Enemy, int EnemyIdx)
    {
        Debug.Log(Caster[CasterIdx].name + " : " + DSkillName[5]);
        
        // 스킬 특성
        Caster[1].TakeMana(Caster[CasterIdx].Mana);
        Caster[CasterIdx].TakeMana((-1) * Caster[CasterIdx].Mana);
    }
    void ManaCharge(List<Save.Character> Caster, int CasterIdx, List<Save.Character> Enemy, int EnemyIdx)
    {
        Debug.Log(Caster[CasterIdx].name + " : " + DSkillName[6]);
        
        // 스킬 특성
        Caster[CasterIdx].TakeMana(15);
    }
    void SummonBarlog(List<Save.Character> Caster, int CasterIdx, List<Save.Character> Enemy, int EnemyIdx)
    {
        Debug.Log(Caster[CasterIdx].name + " : " + DSkillName[7]);
        
        ((Save.Witch)Caster[CasterIdx]).pase = true;
        // 스킬 특성
        foreach (var target in Enemy)
        {
            Caster[CasterIdx].Battlestatus.Damage = (int)(target.Hp * 0.1);
            target.TakeDamage(Caster[CasterIdx]);
        }
    }
    void Earthquake(List<Save.Character> Caster, int CasterIdx, List<Save.Character> Enemy, int EnemyIdx)
    {
        Debug.Log(Caster[CasterIdx].name + " : " + DSkillName[8]);

        // 스킬 특성
        Caster[CasterIdx].Battlestatus.Damage *= 0.5f;
        foreach (var target in Enemy)
        {
            target.TakeDamage(Caster[CasterIdx]);
        }
    }
    void Charge(List<Save.Character> Caster, int CasterIdx, List<Save.Character> Enemy, int EnemyIdx)
    {
        Debug.Log(Caster[CasterIdx].name + " : " + DSkillName[9]);
        ((Save.Barlog)Caster[CasterIdx]).BressHp = 40;
        
    }
    void Bress(List<Save.Character> Caster, int CasterIdx, List<Save.Character> Enemy, int EnemyIdx)
    {
        if( ((Save.Barlog)Caster[CasterIdx]).BressHp <= 0)
        {
            return;
        }
        Debug.Log( ((Save.Barlog)Caster[CasterIdx]).name + " : " + DSkillName[10]);
        
        
        // 스킬 특성
        Caster[CasterIdx].Battlestatus.Damage = 50;
        foreach (var target in Enemy)
        {
            target.TakeDamage(Caster[CasterIdx]);
        }
    }
}
