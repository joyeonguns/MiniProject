using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using TMPro;
using UnityEngine.Pool;


[System.Serializable]
public class Player : Character
{
    public Player() { }
    public Player(Status stat, e_Class _role) : base(stat, _role)
    {

        SetSkill();
        SetSkillClass();
    }

    public override void SetSkill()
    {

        base.SetSkill();

        int[] arr = new int[4] { 1, 2, 3, 4 };
        System.Random rnd = new System.Random();
        arr = arr.OrderBy(x => rnd.Next()).ToArray();

        SkillNum[1] = arr[1];
        SkillNum[2] = arr[2];
    }
    public override void SetSkillClass()
    {

        switch ((int)Role)
        {
            case 0:
                for (int i = 0; i < 4; i++)
                {
                    MySkill[i] = new Warrior_Skill(SkillNum[i]);
                }
                break;

            case 1:
                for (int i = 0; i < 4; i++)
                {
                    MySkill[i] = new Warrior_Skill(SkillNum[i]);
                }
                break;

            case 2:
                for (int i = 0; i < 4; i++)
                {
                    MySkill[i] = new Magition_Skill(SkillNum[i]);
                }
                break;

            case 3:
                for (int i = 0; i < 4; i++)
                {
                    MySkill[i] = new Healer_Skill(SkillNum[i]);
                }
                break;

            case 4:
                for (int i = 0; i < 4; i++)
                {
                    MySkill[i] = new Assassin_Skill(SkillNum[i]);
                }
                break;

            default:
                Debug.LogError("SetSkillClass error");
                break;
        }
    }
    public void ApplyGetTellent(List<Character> MyGrup, int idx, List<Character> Enemy, int EnemyIdx)
    {
        var Applytell = from tellArray in GameManager.instance.Tellents
                        from tel in tellArray
                        where tel.telData.Type == Etel_type.Get
                        select tel;
        foreach (var tel in Applytell)
        {
            tel.TellentApply(MyGrup.Cast<Character>().ToList(), idx, Enemy.Cast<Character>().ToList(), 0);
        }
    }


    public override void StartTurn(List<Character> MyGrup, int idx, List<Character> Enemy, int EnemyIdx)
    {
        base.StartTurn(MyGrup, idx, Enemy, EnemyIdx);

        // 텔런트 적용
        var Applytell = from tellArray in GameManager.instance.Tellents
                        from tel in tellArray
                        where tel.telData.Type == Etel_type.Battle
                        select tel;
        foreach (var tel in Applytell)
        {
            tel.TellentApply(MyGrup.Cast<Character>().ToList(), idx, Enemy.Cast<Character>().ToList(), 0);
        }

    }
}
