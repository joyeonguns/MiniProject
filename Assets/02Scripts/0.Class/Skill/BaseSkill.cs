using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;


public class BaseSkill
{
    // 스킬 정보
    public CharSKillDatas SKill_Data = new CharSKillDatas();
    // 스킬 할당
    // 기본 공격
    public void nomalAttack(List<Character> Caster, int CasterIdx, List<Character> Enemy, int EnemyIdx)
    {
        //GameManager.instance.Hit(Caster,CasterIdx,Enemy,EnemyIdx);
        Debug.Log(Caster[CasterIdx].name + " : 기본 공격!");
        Enemy[EnemyIdx].TakeDamage(Caster[CasterIdx]);
        // Enemy[EnemyIdx].bleedCount += 2;
        // Enemy[EnemyIdx].burnCount += 2;
        Caster[CasterIdx].Mana += 1;
    }
    public Action<List<Character>, int, List<Character>, int> UseSkill;
}
