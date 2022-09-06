using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterDatas 
{
    //데미지
    public float Damage;
    // 방어력
    public float Armor;
    // 속도
    public int Speed;
    // 회피율
    public int Dodge;
    // 치명타
    public int Critical;
    // 최대 체력
    public double MaxHp;
    // 저항력
    public int Resist;
}

[CreateAssetMenu (menuName = "Scriptable Object/Character Datas", order = int.MaxValue)]
public class CharacterSO : ScriptableObject
{
    public CharacterDatas[] CharDatas;  
}

