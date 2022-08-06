using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class ItemClass
{
    public int ItemCode;
    public string ItemName;
    public string ItemComments;
    public Action<List<Save.Player>, int, List<Save.Enemy>, int> UseItem;
    
    // 저장 리스트
    Action<List<Save.Player>, int, List<Save.Enemy>, int>[] ItemFunction = 
     new Action<List<Save.Player>, int, List<Save.Enemy>, int>[]
      {Nothing, SmokePotion, ExplotionPotion, ManaPotion, HelthPotion, RecoverPotion, CorrotionPotion, EnhanthPotion, IcePotion};

    string[] ItemNames = {"No","Smoke", "Explotion", "Mana", "Helth", "Recover", "Corrotion", "Enhanth", "Ice"};

    public ItemClass(int code)
    {
        ItemCode = code;
        UseItem = ItemFunction[ItemCode];
        ItemName = ItemNames[ItemCode];
        ItemComments = "NULL";

        Debug.Log("Use Item : " + ItemName);
    }
    static void Nothing(List<Save.Player> Caster, int CasterIdx, List<Save.Enemy> Enemy, int EnemyIdx)
    {

    }

    static void SmokePotion(List<Save.Player> Caster, int CasterIdx, List<Save.Enemy> Enemy, int EnemyIdx)
    {
        // ResultManager 초기화
        GameManager.instance.ResultData.ResultMode = ResultEnum.Run;        
        // 씬 이동
        SceneManager.LoadScene("2-4.GiftScene");
    }
    static void ExplotionPotion(List<Save.Player> Caster, int CasterIdx, List<Save.Enemy> Enemy, int EnemyIdx)
    {
        // 전체 20데미지
        foreach (var enemy in Enemy)
        {
             Debug.Log("Use Item : Boom");
            enemy.TakeDamage(20,"Boom");   
        }
    }
    static void ManaPotion(List<Save.Player> Caster, int CasterIdx, List<Save.Enemy> Enemy, int EnemyIdx)
    {
        // 전체 마나 5회복        
        foreach (var target in Caster)
        {
            target.TakeMana(5);   
        }
    }
    static void HelthPotion(List<Save.Player> Caster, int CasterIdx, List<Save.Enemy> Enemy, int EnemyIdx)
    {
        // 전체 체력 15회복        
        foreach (var target in Caster)
        {
            target.TakeHeal(15);   
        }
    }
    static void RecoverPotion(List<Save.Player> Caster, int CasterIdx, List<Save.Enemy> Enemy, int EnemyIdx)
    {
        // 전체 턴당 체력 5회복        
        foreach (var target in Caster)
        {
            target.regenCount += 3;   
        }
    }
    static void CorrotionPotion(List<Save.Player> Caster, int CasterIdx, List<Save.Enemy> Enemy, int EnemyIdx)
    {
        // 3턴 방깍        
        foreach (var target in Enemy)
        {
            if(target.corrotionCount == 0)
            {
                target.Battlestatus.Armor /= 2;
            }
            target.corrotionCount += 3;   
        }
    }
    static void EnhanthPotion(List<Save.Player> Caster, int CasterIdx, List<Save.Enemy> Enemy, int EnemyIdx)
    {
        // 3턴 공증        
        foreach (var target in Caster)
        {
            if(target.enHanceCount == 0)
            {
                target.Battlestatus.Damage *= 1.3f;
            }
            target.enHanceCount += 3;   
        }
    }
    static void IcePotion(List<Save.Player> Caster, int CasterIdx, List<Save.Enemy> Enemy, int EnemyIdx)
    {
        // 3턴 공증        
        foreach (var target in Enemy)
        {
            if(target.frostCount == 0)
            {
                target.Battlestatus.Speed -= 2;
            }
            target.frostCount += 3;   
        }
    }
}
