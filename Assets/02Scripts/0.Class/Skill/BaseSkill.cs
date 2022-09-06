using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class BaseSkill
{
     // 스킬 정보
    public string skillName;   
    public string skillComent;   
    public int manaCost;
    public bool bmultiTarget = false;
    public bool bBuff = false;
    // 스킬 할당
    // 기본 공격
    public void nomalAttack(List<Save.Character> Caster, int CasterIdx, List<Save.Character> Enemy, int EnemyIdx)
    {
        //GameManager.instance.Hit(Caster,CasterIdx,Enemy,EnemyIdx);
        Debug.Log(Caster[CasterIdx].name + " : 기본 공격!");
        Enemy[EnemyIdx].TakeDamage(Caster[CasterIdx]);
        Enemy[EnemyIdx].bleedCount += 2;
        Enemy[EnemyIdx].burnCount += 2;
        Caster[CasterIdx].Mana += 1;
    }
    public Action<List<Save.Character>, int, List<Save.Character>, int> UseSkill;
}
