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
    public Action<List<Save_Charater_Class.SD>, int, Save_Charater_Class.Class_Status , List<Save_Charater_Class.SD>, int> UseItem;
    
    // 저장 리스트
    Action<List<Save_Charater_Class.SD>, int, Save_Charater_Class.Class_Status , List<Save_Charater_Class.SD>, int>[] ItemFunction = 
     new Action<List<Save_Charater_Class.SD>, int, Save_Charater_Class.Class_Status , List<Save_Charater_Class.SD>, int>[]
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

    static void SmokePotion(List<Save_Charater_Class.SD> Caster, int CasterIdx,Save_Charater_Class.Class_Status status, List<Save_Charater_Class.SD> Enemy,int EnemyIdx)
    {
        // ResultManager 초기화
        // 씬 이동
    }
    static void ExplotionPotion(List<Save_Charater_Class.SD> Caster, int CasterIdx,Save_Charater_Class.Class_Status status, List<Save_Charater_Class.SD> Enemy,int EnemyIdx)
    {

    }
    static void ManaPotion(List<Save_Charater_Class.SD> Caster, int CasterIdx,Save_Charater_Class.Class_Status status, List<Save_Charater_Class.SD> Enemy,int EnemyIdx)
    {

    }
    static void HelthPotion(List<Save_Charater_Class.SD> Caster, int CasterIdx,Save_Charater_Class.Class_Status status, List<Save_Charater_Class.SD> Enemy,int EnemyIdx)
    {

    }
    static void RecoverPotion(List<Save_Charater_Class.SD> Caster, int CasterIdx,Save_Charater_Class.Class_Status status, List<Save_Charater_Class.SD> Enemy,int EnemyIdx)
    {
  
    }
    static void HitPotion(List<Save_Charater_Class.SD> Caster, int CasterIdx,Save_Charater_Class.Class_Status status, List<Save_Charater_Class.SD> Enemy,int EnemyIdx)
    {

    }
    static void EnhanthPotion(List<Save_Charater_Class.SD> Caster, int CasterIdx,Save_Charater_Class.Class_Status status, List<Save_Charater_Class.SD> Enemy,int EnemyIdx)
    {

    }
}
