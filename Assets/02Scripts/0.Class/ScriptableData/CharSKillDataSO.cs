using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharSKillDatas
{
    public int Code;
    public string Name;
    public string Contents;
    public bool MultiTarget;
    public int Cost;
    public bool Buff;
    public AudioClip Sound;

}

[CreateAssetMenu (menuName = "Scriptable Object/CharSkill Datas", order = int.MaxValue)]
public class CharSKillDataSO : ScriptableObject
{
    public List<CharSKillDatas> WarriorSKillDatas = new List<CharSKillDatas>();
    public List<CharSKillDatas> MagitionSKillDatas = new List<CharSKillDatas>();
    public List<CharSKillDatas> SupporterSKillDatas = new List<CharSKillDatas>();
    public List<CharSKillDatas> AssassinSKillDatas = new List<CharSKillDatas>();
}
