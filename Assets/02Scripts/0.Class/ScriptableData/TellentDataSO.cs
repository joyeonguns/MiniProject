using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TellentData
{
    public string Name;
    public Etel_type Type;
    public string Contents;
    public Etel_Rank Rank;
}

[CreateAssetMenu (menuName = "Scriptable Object/Tellents Datas", order = int.MaxValue)]
public class TellentDataSO : ScriptableObject
{
   public List<TellentData> tellentData_C = new List<TellentData>();
   public List<TellentData> tellentData_B = new List<TellentData>();
   public List<TellentData> tellentData_A = new List<TellentData>();
   public List<TellentData> tellentData_S = new List<TellentData>();
}
