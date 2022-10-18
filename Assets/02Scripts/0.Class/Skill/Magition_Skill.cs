using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magition_Skill : BaseSkill
{   
    public Magition_Skill(int num)
    {
        SKill_Data = SOManager.GetSkill().MagitionSKillDatas[num];
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
                Debug.LogError("Magition_Skill error");
                break;
        }
    } 

    void skill_0(List<Save.Character> Caster, int CasterIdx, List<Save.Character> Enemy, int EnemyIdx)
    {
        Debug.Log(Caster[CasterIdx].name + " : " + "FireBall");
        // 마나 소모
        Caster[CasterIdx].Mana -= SKill_Data.Cost;

        // 스텟 가져옴
        // 스킬 특성
        Caster[CasterIdx].Battlestatus.Damage *= 1.5f;
        Enemy[EnemyIdx].TakeDamage(Caster[CasterIdx]);
        
        Enemy[EnemyIdx].burnCount += 3;

    }
    void skill_1(List<Save.Character> Caster, int CasterIdx, List<Save.Character> Enemy, int EnemyIdx)
    {
        Debug.Log(Caster[CasterIdx].name + " : " + "Meditation");
        // 마나 소모
        Caster[CasterIdx].Mana += 4;
    }
    void skill_2(List<Save.Character> Caster, int CasterIdx, List<Save.Character> Enemy, int EnemyIdx)
    {
        Debug.Log(Caster[CasterIdx].name + " : " + "Acid Rain");
        // 마나 소모
        Caster[CasterIdx].Mana -= SKill_Data.Cost;

        // 스킬 특성
        Caster[CasterIdx].Battlestatus.Damage *= 0.7f;
        foreach(var enemy in Enemy)
        {
            enemy.TakeDamage(Caster[CasterIdx]);
            
            if(enemy.corrotionCount == 0)
            {
                enemy.Battlestatus.Armor *=  0.5f;
            }
            else
            {
                Debug.Log("corrotionCount : " + enemy.corrotionCount);
            }            
            enemy.corrotionCount += 3;
        }            
    }
    void skill_3(List<Save.Character> Caster, int CasterIdx, List<Save.Character> Enemy, int EnemyIdx)
    {
        Debug.Log(Caster[CasterIdx].name + " : " + "Tetkai");
        // 마나 소모
        Caster[CasterIdx].Mana -= SKill_Data.Cost;

        Caster[CasterIdx].Battlestatus.Damage *= 1f;
        // 스킬 특성        
        foreach(var enemy in Enemy)
        {
            enemy.TakeDamage(Caster[CasterIdx]);
            
            if(enemy.frostCount == 0)
            {
                enemy.Battlestatus.Speed -= 2;
            }
            else
            {
                Debug.Log("frostCount : " + enemy.frostCount);
            }
            enemy.frostCount += 3;
        }            

    }
    
    void Ulti(List<Save.Character> Caster, int CasterIdx, List<Save.Character> Enemy, int EnemyIdx)
    {
        Debug.Log("Super Nova");
        Caster[CasterIdx].Mana -= SKill_Data.Cost;

        Caster[CasterIdx].Battlestatus.Damage *= 2; 
        foreach(var enemy in Enemy)
        {
            enemy.TakeDamage(Caster[CasterIdx]);
            enemy.stunCount += 1;
        }            
    }
}
