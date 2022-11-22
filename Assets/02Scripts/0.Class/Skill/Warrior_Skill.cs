using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Warrior_Skill : BaseSkill
{
    public Warrior_Skill(int num)
    {
        SKill_Data = SOManager.GetSkill().WarriorSKillDatas[num];
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
                break;
            case 4:
                UseSkill = skill_3;
                break;
            case 5:
                UseSkill = Ulti;
                break;
            default :
                Debug.LogError("Warrior skill error");
                break;
        }
    } 

    void skill_0(List<Character> Caster, int CasterIdx, List<Character> Enemy, int EnemyIdx)
    {
        Debug.Log(Caster[CasterIdx].name + " : " + "Smash");
        // 마나 소모
        Caster[CasterIdx].Mana -= SKill_Data.Cost;

        // 스텟 가져옴
        // 스킬 특성
        Caster[CasterIdx].Battlestatus.Damage *= 0.5f;
        
        int rnd = UnityEngine.Random.Range(0,3);
        if(rnd == 0)
        {
            Enemy[EnemyIdx].stunCount += 1;
        }        
        Enemy[EnemyIdx].TakeDamage(Caster[CasterIdx]);

    }
    void skill_1(List<Character> Caster, int CasterIdx, List<Character> Enemy, int EnemyIdx)
    {
        Debug.Log(Caster[CasterIdx].name + " : " + "pierce");
        // 마나 소모
        Caster[CasterIdx].Mana -= SKill_Data.Cost;

        // 스킬 특성
        Caster[CasterIdx].Battlestatus.Critical += 1000;
        Enemy[EnemyIdx].TakeDamage(Caster[CasterIdx]);
        Enemy[EnemyIdx].bleedCount += 3;
       
    }
    void skill_2(List<Character> Caster, int CasterIdx, List<Character> Enemy, int EnemyIdx)
    {
        Debug.Log(Caster[CasterIdx].name + " : " + "aurora force");
        // 마나 소모
        Caster[CasterIdx].Mana -= SKill_Data.Cost;

        // 스킬 특성
        Caster[CasterIdx].Battlestatus.Damage *= 1.5f;
        foreach(var enemy in Enemy)
        {
            enemy.TakeDamage(Caster[CasterIdx]);
        }            
    }
    void skill_3(List<Character> Caster, int CasterIdx, List<Character> Enemy, int EnemyIdx)
    {
        Debug.Log("Tetkai");
        // 마나 소모
        Caster[CasterIdx].Mana -= SKill_Data.Cost;

        // 스킬 특성        
        foreach(var character in Caster)
        {
            character.TakeHeal(Caster[CasterIdx].Battlestatus.Damage * 0.4f);
        }

    }
    
    void Ulti(List<Character> Caster, int CasterIdx, List<Character> Enemy, int EnemyIdx)
    {
        Debug.Log("Ulti");
        Caster[CasterIdx].Mana -= SKill_Data.Cost;

        Caster[CasterIdx].Battlestatus.Damage *= 2; 
        Caster[CasterIdx].Battlestatus.Critical *= 2;
        foreach(var enemy in Enemy)
        {
            enemy.TakeDamage(Caster[CasterIdx]);
        }
            
    }
}
