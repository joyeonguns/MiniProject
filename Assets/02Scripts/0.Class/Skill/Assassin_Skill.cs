using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Assassin_Skill : Skills
{
    Dictionary<int,string> DSkillName = new Dictionary<int, string>() {{0,"단검투척"},{1,"칼날비"},{2,"약점공략"},{3,"특제 폭탄"},{4,"신속"},{5,"암살"}};
    Dictionary<int,int> DSkillCost = new Dictionary<int, int>() {{0,0},{1,2},{2,3},{3,5},{4,2},{5,10}};
    Dictionary<int,string> DSkillComent = new Dictionary<int, string>() {{0,"a"},{1,"b"},{2,"c"},{3,"d"},{4,"e"},{5,"u"}};      
    public Assassin_Skill(int num)
    {
        skillName = DSkillName[num];
        skillComent = DSkillComent[num];
        manaCost = DSkillCost[num];

        switch (num)
        {
            case 0:
                UseSkill = nomalAttack;
                break;
            case 1:
                UseSkill = skill_0;
                bmultiTarget = true;
                break;
            case 2:
                UseSkill = skill_1;
                break;
            case 3:
                UseSkill = skill_2;
                bmultiTarget = true;
                break;
            case 4:
                UseSkill = skill_3;
                bBuff = true;
                break;
            case 5:
                UseSkill = Ulti;
                break;
        }
    } 

    void skill_0(List<Save.Character> Caster, int CasterIdx, List<Save.Character> Enemy, int EnemyIdx)
    {
        Debug.Log(Caster[CasterIdx].name + " : " + "Blade Rain");
        // 마나 소모
        Caster[CasterIdx].Mana -= 2;

        // 스텟 가져옴
        // 스킬 특성
        Caster[CasterIdx].Battlestatus.Damage *= 0.5f;
        foreach(var enemy in Enemy)
        {
            enemy.TakeDamage(Caster[CasterIdx]);
            enemy.bleedCount += 2;
        }

    }
    void skill_1(List<Save.Character> Caster, int CasterIdx, List<Save.Character> Enemy, int EnemyIdx)
    {
        Debug.Log(Caster[CasterIdx].name + " : " + "Week Search");
        // 마나 소모
        Caster[CasterIdx].Mana -= 3;
        Enemy[EnemyIdx].corrotionCount += 2;
        Enemy[EnemyIdx].frostCount += 2;
    }
    void skill_2(List<Save.Character> Caster, int CasterIdx, List<Save.Character> Enemy, int EnemyIdx)
    {
        Debug.Log(Caster[CasterIdx].name + " : " + "Special Boom");
        // 마나 소모
        Caster[CasterIdx].Mana -= 5;

        foreach(var enemy in Enemy)
        {
            enemy.corrotionCount += 2;
            enemy.burnCount += 2;
            enemy.bleedCount += 2;
        }            
    }
    void skill_3(List<Save.Character> Caster, int CasterIdx, List<Save.Character> Enemy, int EnemyIdx)
    {
        Debug.Log(Caster[CasterIdx].name + " : " + "Swift");
        // 마나 소모
        Caster[CasterIdx].Mana -= 2;

        // 스킬 특성              
        Caster[CasterIdx].rapidCount += 4;

    }
    
    void Ulti(List<Save.Character> Caster, int CasterIdx, List<Save.Character> Enemy, int EnemyIdx)
    {
        Debug.Log("Assassination");
        Caster[CasterIdx].Mana -= 10;

        Caster[CasterIdx].Battlestatus.Damage *= 3.5f;
        Caster[CasterIdx].Battlestatus.Critical *= 2;      
        Enemy[EnemyIdx].TakeDamage(Caster[CasterIdx]);      
        Enemy[EnemyIdx].bleedCount += 5;
    }
}
