using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum e_Class {Adventurer = 0, Warrior = 1, Magicion = 2, Supporter = 3, Assassin = 4, Bandit = 100, Knight = 101, Abomination = 102, Crystal_Melle = 202, Crystal_Range = 203, Witch = 200, Barlog = 201};
public enum BuffEnum {Burn, Bleeding, Stun, Corrotion, EnHanceCount, Frost, Rapid, Regen}
public enum ResultEnum {NomalBattle, EliteBattle, BossBattle, Run}
public class ConstData
{
    public const int BoomDmg = 20; 
    public const int ItemHeal = 15; 
   
    
}

// 전투 보상
public class ResultClass
{
    // 현재 전투 난이도
    public int Battle_Lvl = 1;
    public ResultEnum ResultMode = ResultEnum.NomalBattle;
    public int Gold;
    public int Exp;
    public int ItemRate;
    public string TellentRank;
    
    
}
