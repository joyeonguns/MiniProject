using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Warrior_Skill : BaseSkill
{
    Dictionary<int,string> DSkillName = new Dictionary<int, string>() {{0,"Attack"},{1,"Smash"},{2,"Pierce"},{3,"Aurora Force"},{4,"Tetkai"},{5,"Ultimate"}};
    Dictionary<int,int> DSkillCost = new Dictionary<int, int>() {{0,0},{1,2},{2,2},{3,4},{4,1},{5,10}};
    Dictionary<int,string> DSkillComent = new Dictionary<int, string>() {{0,"a"},{1,"b"},{2,"c"},{3,"d"},{4,"e"},{5,"u"}};      
    public Warrior_Skill(int num)
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
                bmultiTarget = true;
                break;
            default :
                Debug.LogError("Warrior skill error");
                break;
        }
    } 

    void skill_0(List<Save.Character> Caster, int CasterIdx, List<Save.Character> Enemy, int EnemyIdx)
    {
        Debug.Log(Caster[CasterIdx].name + " : " + "Smash");
        // 마나 소모
        Caster[CasterIdx].Mana -= DSkillCost[1];

        // 스텟 가져옴
        // 스킬 특성
        Caster[CasterIdx].Battlestatus.Damage *= 1.7f;
        Enemy[EnemyIdx].TakeDamage(Caster[CasterIdx]);

    }
    void skill_1(List<Save.Character> Caster, int CasterIdx, List<Save.Character> Enemy, int EnemyIdx)
    {
        Debug.Log(Caster[CasterIdx].name + " : " + "pierce");
        // 마나 소모
        Caster[CasterIdx].Mana -= DSkillCost[2];

        // 스킬 특성
        Caster[CasterIdx].Battlestatus.Critical += 1000;
        Enemy[EnemyIdx].TakeDamage(Caster[CasterIdx]);
        Enemy[EnemyIdx].stunCount = 3;
    }
    void skill_2(List<Save.Character> Caster, int CasterIdx, List<Save.Character> Enemy, int EnemyIdx)
    {
        Debug.Log(Caster[CasterIdx].name + " : " + "aurora force");
        // 마나 소모
        Caster[CasterIdx].Mana -= DSkillCost[3];

        // 스킬 특성
        Caster[CasterIdx].Battlestatus.Damage *= 1.5f;
        foreach(var enemy in Enemy)
        {
            enemy.TakeDamage(Caster[CasterIdx]);
        }            
    }
    void skill_3(List<Save.Character> Caster, int CasterIdx, List<Save.Character> Enemy, int EnemyIdx)
    {
        Debug.Log("Tetkai");
        // 마나 소모
        Caster[CasterIdx].Mana -= DSkillCost[4];

        // 스킬 특성        
        foreach(var character in Caster)
        {
            character.TakeHeal(Caster[CasterIdx].Battlestatus.Damage);
        }

    }
    
    void Ulti(List<Save.Character> Caster, int CasterIdx, List<Save.Character> Enemy, int EnemyIdx)
    {
        Debug.Log("Ulti");
        Caster[CasterIdx].Mana -= 10;

        Caster[CasterIdx].Battlestatus.Damage *= 2; 
        Caster[CasterIdx].Battlestatus.Critical *= 2;
        foreach(var enemy in Enemy)
        {
            enemy.TakeDamage(Caster[CasterIdx]);
        }
            
    }
}
