using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ItemClass
{
    public int ItemCode;
    public string ItemName;
    public string ItemComments;
    public Action<List<Save_Charater_Class.SD>, int, Save_Charater_Class.Class_Status , List<Save_Charater_Class.SD>, int> UseItem;

    public ItemClass(int code)
    {
        UseItem = SmokePotion;
    }

    void SmokePotion(List<Save_Charater_Class.SD> Caster, int CasterIdx,Save_Charater_Class.Class_Status status, List<Save_Charater_Class.SD> Enemy,int EnemyIdx)
    {

    }
    void ExplotionPotion(List<Save_Charater_Class.SD> Caster, int CasterIdx,Save_Charater_Class.Class_Status status, List<Save_Charater_Class.SD> Enemy,int EnemyIdx)
    {

    }
    void ManaPotion(List<Save_Charater_Class.SD> Caster, int CasterIdx,Save_Charater_Class.Class_Status status, List<Save_Charater_Class.SD> Enemy,int EnemyIdx)
    {

    }
    void HelthPotion(List<Save_Charater_Class.SD> Caster, int CasterIdx,Save_Charater_Class.Class_Status status, List<Save_Charater_Class.SD> Enemy,int EnemyIdx)
    {

    }
    void RecoverPotion(List<Save_Charater_Class.SD> Caster, int CasterIdx,Save_Charater_Class.Class_Status status, List<Save_Charater_Class.SD> Enemy,int EnemyIdx)
    {
  
    }
    void HitPotion(List<Save_Charater_Class.SD> Caster, int CasterIdx,Save_Charater_Class.Class_Status status, List<Save_Charater_Class.SD> Enemy,int EnemyIdx)
    {

    }
    void EnhanthPotion(List<Save_Charater_Class.SD> Caster, int CasterIdx,Save_Charater_Class.Class_Status status, List<Save_Charater_Class.SD> Enemy,int EnemyIdx)
    {

    }
}
