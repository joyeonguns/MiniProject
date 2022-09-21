using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healer_Skill : BaseSkill
{   
    public Healer_Skill(int num)
    {
        SKill_Data = SOManager.GetSkill().SupporterSKillDatas[num];
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
                Debug.LogError("Healer_Skill error");
                break;
        }
    } 

    void skill_0(List<Save.Character> Caster, int CasterIdx, List<Save.Character> Enemy, int EnemyIdx)
    {
        Debug.Log(Caster[CasterIdx].name + " : " + "heal");
        // 마나 소모
        Caster[CasterIdx].Mana -= SKill_Data.Cost;

        // 스텟 가져옴
        // 스킬 특성
        double heal = 2 + Enemy[EnemyIdx].status.MaxHp * ((15.0f + (float)Caster[CasterIdx].Level * 2) / 100);
        Enemy[EnemyIdx].TakeHeal(heal);
    }
    void skill_1(List<Save.Character> Caster, int CasterIdx, List<Save.Character> Enemy, int EnemyIdx)
    {
        Debug.Log(Caster[CasterIdx].name + " : " + "Emergency rescue");
        // 마나 소모
         Caster[CasterIdx].Mana -= SKill_Data.Cost;

        // 스킬 특성
        double heal = 3 + Enemy[EnemyIdx].status.MaxHp * 0.2f;
        Enemy[EnemyIdx].TakeHeal(heal);
        Enemy[EnemyIdx].corrotionCount = 0;
        Enemy[EnemyIdx].frostCount = 0;
        Enemy[EnemyIdx].burnCount = 0;
        Enemy[EnemyIdx].bleedCount = 0;
        Enemy[EnemyIdx].regenCount += 3;
    }
    void skill_2(List<Save.Character> Caster, int CasterIdx, List<Save.Character> Enemy, int EnemyIdx)
    {
        Debug.Log(Caster[CasterIdx].name + " : " + "Haste");
        // 마나 소모
        Caster[CasterIdx].Mana -= SKill_Data.Cost;

        foreach(var caster in Caster)
        {
            caster.rapidCount += 2;
        }            
    }
    void skill_3(List<Save.Character> Caster, int CasterIdx, List<Save.Character> Enemy, int EnemyIdx)
    {
        Debug.Log(Caster[CasterIdx].name + " : " + "Sun Light");
        // 마나 소모
        Caster[CasterIdx].Mana -= SKill_Data.Cost;

        // 스킬 특성              
        foreach(var enemy in Enemy)
        {
            int rnd = UnityEngine.Random.Range(1,11);
            if(rnd > 5)
            {
                enemy.stunCount += 1;
            }            
        }

    }
    
    void Ulti(List<Save.Character> Caster, int CasterIdx, List<Save.Character> Enemy, int EnemyIdx)
    {
        Debug.Log("Saint Grace");
        Caster[CasterIdx].Mana -= SKill_Data.Cost;
        
        int heal = 15 + (Caster[CasterIdx].Level * 5);
        foreach(var caster in Caster)
        {
            caster.rapidCount += 3;
            caster.regenCount += 3;
            caster.enHanceCount += 3;
            
            caster.bleedCount = 0;
            caster.burnCount = 0;

            // 디버프 걸렸을 경우
            if(caster.frostCount != 0)
            {
                caster.frostCount = 0;
                caster.Battlestatus.Speed += 2; 
            }
                       
            if(caster.corrotionCount != 0)
            {
                caster.corrotionCount = 0;
                caster.Battlestatus.Speed -= 2; 
            }
            
            caster.TakeHeal(heal);
        }
    }
}
