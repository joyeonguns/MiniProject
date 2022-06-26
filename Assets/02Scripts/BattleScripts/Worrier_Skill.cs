using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Skills : MonoBehaviour
{
     // 스킬 정보
    public string skillName;   
    public string skillComent;   
    public int manaCost;
    // 스킬 할당
    // 기본 공격
    public void nomalAttack(Save_Charater_Class.SD[] Caster, int CasterIdx, Save_Charater_Class.Class_Status status, Save_Charater_Class.SD[] Enemy,int EnemyIdx)
    {
        //GameManager.instance.Hit(Caster,CasterIdx,Enemy,EnemyIdx);
        Debug.Log(Caster[CasterIdx].c_Name + " : 기본 공격!");
        Enemy[EnemyIdx].HitDamage(Caster[CasterIdx], Caster[CasterIdx].CurStatus());
        Caster[CasterIdx].Mana += 1;
    }
    public Action<Save_Charater_Class.SD[], int, Save_Charater_Class.Class_Status , Save_Charater_Class.SD[], int> UseSkill;
}

public class Worrier_Skill : Skills
{
    Dictionary<int,string> DSkillName = new Dictionary<int, string>() {{0,"Attack"},{1,"Smash"},{2,"Pierce"},{3,"Aurora Force"},{4,"Tetkai"},{5,"Ultimate"}};
    Dictionary<int,int> DSkillCost = new Dictionary<int, int>() {{0,0},{1,2},{2,2},{3,4},{4,1},{5,10}};
    Dictionary<int,string> DSkillComent = new Dictionary<int, string>() {{0,"a"},{1,"b"},{2,"c"},{3,"d"},{4,"e"},{5,"u"}};      
    public Worrier_Skill(int num)
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
                break;
            case 4:
                UseSkill = skill_3;
                break;
            case 5:
                UseSkill = Ulti;
                break;
        }
    } 

    void skill_0(Save_Charater_Class.SD[] Caster, int CasterIdx,Save_Charater_Class.Class_Status status, Save_Charater_Class.SD[] Enemy,int EnemyIdx)
    {
        Debug.Log(Caster[CasterIdx].c_Name + " : " + "Smash");
        // 마나 소모
        Caster[CasterIdx].Mana -= 2;

        // 스텟 가져옴
        Save_Charater_Class.Class_Status curStatus = status;
        // 스킬 특성
        curStatus.s_Damage *= 1.7f;
        Enemy[EnemyIdx].HitDamage(Caster[CasterIdx],curStatus);

    }
    void skill_1(Save_Charater_Class.SD[] Caster, int CasterIdx,Save_Charater_Class.Class_Status status, Save_Charater_Class.SD[] Enemy,int EnemyIdx)
    {
        Debug.Log(Caster[CasterIdx].c_Name + " : " + "pierce");
        // 마나 소모
        Caster[CasterIdx].Mana -= 2;

        // 스텟 가져옴
        Save_Charater_Class.Class_Status curStatus = status;
        // 스킬 특성
        curStatus.s_Critical += 1000;
        Enemy[EnemyIdx].HitDamage(Caster[CasterIdx],curStatus);
    }
    void skill_2(Save_Charater_Class.SD[] Caster, int CasterIdx, Save_Charater_Class.Class_Status status, Save_Charater_Class.SD[] Enemy,int EnemyIdx)
    {
        Debug.Log(Caster[CasterIdx].c_Name + " : " + "aurora force");
        // 마나 소모
        Caster[CasterIdx].Mana -= 4;

        // 스텟 가져옴
        Save_Charater_Class.Class_Status curStatus = status;
        // 스킬 특성
        curStatus.s_Damage *= 1.5f;
        foreach(var enemy in Enemy)
            enemy.HitDamage(Caster[CasterIdx],curStatus);
    }
    void skill_3(Save_Charater_Class.SD[] Caster, int CasterIdx, Save_Charater_Class.Class_Status status, Save_Charater_Class.SD[] Enemy,int EnemyIdx)
    {
        Debug.Log("Tetkai");
        // 마나 소모
        Caster[CasterIdx].Mana -= 1;

        // 스텟 가져옴
        Save_Charater_Class.Class_Status curStatus = status;

        // 스킬 특성        
        foreach(var character in Caster)
            character.TakeHeal(Caster[CasterIdx],curStatus);

    }
    
    void Ulti(Save_Charater_Class.SD[] Caster, int CasterIdx, Save_Charater_Class.Class_Status status, Save_Charater_Class.SD[] Enemy,int EnemyIdx)
    {
        Debug.Log("Ulti");
        Caster[CasterIdx].Mana -= 10;

        // 스텟 가져옴
        Save_Charater_Class.Class_Status curStatus = status;
        curStatus.s_Damage *= 2; 
        curStatus.s_Critical *= 2;
        foreach(var enemy in Enemy)
            enemy.HitDamage(Caster[CasterIdx],curStatus);
    }
}
