using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magition_Skill : Skills
{
    Dictionary<int,string> DSkillName = new Dictionary<int, string>() {{0,"휘두르기"},{1,"파이어볼"},{2,"명상"},{3,"산성비"},{4,"눈사태"},{5,"Ultimate"}};
    Dictionary<int,int> DSkillCost = new Dictionary<int, int>() {{0,0},{1,2},{2,0},{3,5},{4,5},{5,10}};
    Dictionary<int,string> DSkillComent = new Dictionary<int, string>() {{0,"a"},{1,"b"},{2,"c"},{3,"d"},{4,"e"},{5,"u"}};      
    public Magition_Skill(int num)
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
                bmultiTarget = false;
                break;
            case 2:
                UseSkill = skill_1;
                bBuff = true;
                break;
            case 3:
                UseSkill = skill_2;
                bmultiTarget = true;
                break;
            case 4:
                UseSkill = skill_3;
                bmultiTarget = true;
                break;
            case 5:
                UseSkill = Ulti;
                bmultiTarget = true;
                break;
        }
    } 

    void skill_0(List<Save.Character> Caster, int CasterIdx, List<Save.Character> Enemy, int EnemyIdx)
    {
        Debug.Log(Caster[CasterIdx].name + " : " + "FireBall");
        // 마나 소모
        Caster[CasterIdx].Mana -= 2;

        // 스텟 가져옴
        // 스킬 특성
        Caster[CasterIdx].Battlestatus.Damage *= 1.2f;
        Enemy[EnemyIdx].TakeDamage(Caster[CasterIdx]);
        Enemy[EnemyIdx].burnCount +=3;

    }
    void skill_1(List<Save.Character> Caster, int CasterIdx, List<Save.Character> Enemy, int EnemyIdx)
    {
        Debug.Log(Caster[CasterIdx].name + " : " + "Meditation");
        // 마나 소모
        Caster[CasterIdx].Mana += 3;
    }
    void skill_2(List<Save.Character> Caster, int CasterIdx, List<Save.Character> Enemy, int EnemyIdx)
    {
        Debug.Log(Caster[CasterIdx].name + " : " + "Acid Rain");
        // 마나 소모
        Caster[CasterIdx].Mana -= 5;

        // 스킬 특성
        Caster[CasterIdx].Battlestatus.Damage *= 0.7f;
        foreach(var enemy in Enemy)
        {
            enemy.TakeDamage(Caster[CasterIdx]);
            enemy.corrotionCount += 3;
        }            
    }
    void skill_3(List<Save.Character> Caster, int CasterIdx, List<Save.Character> Enemy, int EnemyIdx)
    {
        Debug.Log(Caster[CasterIdx].name + " : " + "Tetkai");
        // 마나 소모
        Caster[CasterIdx].Mana -= 5;

        Caster[CasterIdx].Battlestatus.Damage *= 1f;
        // 스킬 특성        
        foreach(var enemy in Enemy)
        {
            enemy.TakeDamage(Caster[CasterIdx]);
            enemy.frostCount += 3;
        }            

    }
    
    void Ulti(List<Save.Character> Caster, int CasterIdx, List<Save.Character> Enemy, int EnemyIdx)
    {
        Debug.Log("Super Nova");
        Caster[CasterIdx].Mana -= 10;

        Caster[CasterIdx].Battlestatus.Damage *= 2; 
        foreach(var enemy in Enemy)
        {
            enemy.TakeDamage(Caster[CasterIdx]);
            enemy.stunCount += 1;
        }            
    }
}
