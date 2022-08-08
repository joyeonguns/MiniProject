using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hiller_Skill : Skills
{
    Dictionary<int,string> DSkillName = new Dictionary<int, string>() {{0,"돌팔매질"},{1,"힐"},{2,"응급구조"},{3,"헤이스트"},{4,"광명"},{5,"성스러운 축복"}};
    Dictionary<int,int> DSkillCost = new Dictionary<int, int>() {{0,0},{1,2},{2,5},{3,2},{4,3},{5,10}};
    Dictionary<int,string> DSkillComent = new Dictionary<int, string>() {{0,"a"},{1,"b"},{2,"c"},{3,"d"},{4,"e"},{5,"u"}};      
    public Hiller_Skill(int num)
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
                bBuff = true;
                break;
            case 2:
                UseSkill = skill_1;                
                bBuff = true;
                break;
            case 3:
                UseSkill = skill_2;
                bBuff = true;
                break;
            case 4:
                UseSkill = skill_3;
                bmultiTarget = true;
                break;
            case 5:
                UseSkill = Ulti;
                bmultiTarget = true;
                bBuff = true;
                break;
        }
    } 

    void skill_0(List<Save.Character> Caster, int CasterIdx, List<Save.Character> Enemy, int EnemyIdx)
    {
        Debug.Log(Caster[CasterIdx].name + " : " + "Hill");
        // 마나 소모
        Caster[CasterIdx].Mana -= 2;

        // 스텟 가져옴
        // 스킬 특성
        double hill = 2 + Enemy[EnemyIdx].status.MaxHp * ((15.0f + (float)Caster[CasterIdx].Level * 2) / 100);
        Enemy[EnemyIdx].TakeHeal(hill);
    }
    void skill_1(List<Save.Character> Caster, int CasterIdx, List<Save.Character> Enemy, int EnemyIdx)
    {
        Debug.Log(Caster[CasterIdx].name + " : " + "Emergency rescue");
        // 마나 소모
         Caster[CasterIdx].Mana -= 5;

        // 스킬 특성
        double hill = 3 + Enemy[EnemyIdx].status.MaxHp * 0.2f;
        Enemy[EnemyIdx].TakeHeal(hill);
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
        Caster[CasterIdx].Mana -= 2;

        foreach(var caster in Caster)
        {
            caster.rapidCount += 2;
        }            
    }
    void skill_3(List<Save.Character> Caster, int CasterIdx, List<Save.Character> Enemy, int EnemyIdx)
    {
        Debug.Log(Caster[CasterIdx].name + " : " + "Sun Light");
        // 마나 소모
        Caster[CasterIdx].Mana -= 3;

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
        Caster[CasterIdx].Mana -= 10;
        
        int heal = 15 + (Caster[CasterIdx].Level * 5);
        foreach(var caster in Caster)
        {
            caster.rapidCount += 3;
            caster.regenCount += 3;
            caster.enHanceCount += 3;
            
            caster.bleedCount = 0;
            caster.burnCount = 0;
            caster.frostCount = 0;
            caster.corrotionCount = 0;
            caster.TakeHeal(heal);
        }
    }
}
