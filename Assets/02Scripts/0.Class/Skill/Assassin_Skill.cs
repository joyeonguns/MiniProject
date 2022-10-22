using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Assassin_Skill : BaseSkill
{
    public Assassin_Skill(int num)
    {
        SKill_Data = SOManager.GetSkill().AssassinSKillDatas[num];

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
                Debug.LogError("Assassin_Skill error");
                break;
        }
    } 

    void skill_0(List<Save.Character> Caster, int CasterIdx, List<Save.Character> Enemy, int EnemyIdx)
    {
        Debug.Log(Caster[CasterIdx].name + " : " + "Blade Rain");
        // 마나 소모
        Caster[CasterIdx].Mana -= SKill_Data.Cost;

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
        Caster[CasterIdx].Mana -= SKill_Data.Cost;
        Caster[CasterIdx].enHanceCount += 2;
        Enemy[EnemyIdx].corrotionCount += 2;
        Enemy[EnemyIdx].frostCount += 2;
    }
    void skill_2(List<Save.Character> Caster, int CasterIdx, List<Save.Character> Enemy, int EnemyIdx)
    {
        Debug.Log(Caster[CasterIdx].name + " : " + "Special Boom");
        // 마나 소모
        Caster[CasterIdx].Mana -= SKill_Data.Cost;

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
        Caster[CasterIdx].Mana -= SKill_Data.Cost;


        // 스킬 특성              
        Caster[CasterIdx].rapidCount += 4;

    }
    
    void Ulti(List<Save.Character> Caster, int CasterIdx, List<Save.Character> Enemy, int EnemyIdx)
    {
        Debug.Log("Assassination");
        Caster[CasterIdx].Mana -= SKill_Data.Cost;

        Caster[CasterIdx].Battlestatus.Damage *= 3.5f;
        Caster[CasterIdx].Battlestatus.Critical *= 2;      
        Enemy[EnemyIdx].TakeDamage(Caster[CasterIdx]);      
        Enemy[EnemyIdx].bleedCount += 5;
    }
}
