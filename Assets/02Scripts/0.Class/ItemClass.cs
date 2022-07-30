using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;


public class ItemClass
{
    public int ItemCode;
    public string ItemName;
    public string ItemComments;
    public Action<List<Save.Player>, int, Save.St_Stat , List<Save.Player>, int> UseItem;
    
    // 저장 리스트
    Action<List<Save.Player>, int, Save.St_Stat , List<Save.Player>, int>[] ItemFunction = 
     new Action<List<Save.Player>, int, Save.St_Stat , List<Save.Player>, int>[]
      {SmokePotion, ExplotionPotion, ManaPotion, HelthPotion, RecoverPotion, HitPotion, EnhanthPotion};

    string[] ItemNames = {"Smoke", "Explotion", "Mana", "Helth", "Recover", "Hit", "Enhanth"};

    public ItemClass(int code)
    {
        ItemCode = code-1;
        UseItem = ItemFunction[ItemCode];
        ItemName = ItemNames[ItemCode];
        ItemComments = "NULL";

        Debug.Log(ItemName);
    }

    static void SmokePotion(List<Save.Player> Caster, int CasterIdx,Save.St_Stat status, List<Save.Player> Enemy,int EnemyIdx)
    {
        // ResultManager 초기화
        // 씬 이동
    }
    static void ExplotionPotion(List<Save.Player> Caster, int CasterIdx,Save.St_Stat status, List<Save.Player> Enemy,int EnemyIdx)
    {

    }
    static void ManaPotion(List<Save.Player> Caster, int CasterIdx,Save.St_Stat status, List<Save.Player> Enemy,int EnemyIdx)
    {

    }
    static void HelthPotion(List<Save.Player> Caster, int CasterIdx,Save.St_Stat status, List<Save.Player> Enemy,int EnemyIdx)
    {

    }
    static void RecoverPotion(List<Save.Player> Caster, int CasterIdx,Save.St_Stat status, List<Save.Player> Enemy,int EnemyIdx)
    {
  
    }
    static void HitPotion(List<Save.Player> Caster, int CasterIdx,Save.St_Stat status, List<Save.Player> Enemy,int EnemyIdx)
    {

    }
    static void EnhanthPotion(List<Save.Player> Caster, int CasterIdx,Save.St_Stat status, List<Save.Player> Enemy,int EnemyIdx)
    {

    }
}
