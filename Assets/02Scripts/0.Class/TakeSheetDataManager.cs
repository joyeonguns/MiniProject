using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking; // 네트워킹 연결에 필요
using System.Linq;
using System;

public class TakeSheetDataManager : MonoBehaviour
{
    // Start is called before the first frame update
    
    const string CharURL = "https://docs.google.com/spreadsheets/d/1rfcfKjTVHpXqtF_5ADRL1l5i1wVQiwepq4YmAknUmjo/export?format=tsv&gid=0&&range=B2:H6";
    //const string ItemURL = "https://docs.google.com/spreadsheets/d/1rfcfKjTVHpXqtF_5ADRL1l5i1wVQiwepq4YmAknUmjo/export?format=tsv&gid=1908062173&&range=B2:H6";
    const string TellURL = "https://docs.google.com/spreadsheets/d/1rfcfKjTVHpXqtF_5ADRL1l5i1wVQiwepq4YmAknUmjo/export?format=tsv&gid=374789293&&range=A2:F250";

    const string SkillURL = "https://docs.google.com/spreadsheets/d/1rfcfKjTVHpXqtF_5ADRL1l5i1wVQiwepq4YmAknUmjo/export?format=tsv&gid=6294707&&range=B2:G25";

    public CharacterSO CharSO;
    public ItemDataSO ItemSO;
    public TellentDataSO TellSO;
    public CharSKillDataSO SkillSO;

    public GameObject PatchLoading;


    public void OnClickPatch()
    {
        PatchLoading.SetActive(true);
        StartCoroutine(PatchData());
    }

    IEnumerator PatchData()
    {
        UnityWebRequest www = UnityWebRequest.Get(CharURL);
        yield return www.SendWebRequest();

        string data = www.downloadHandler.text;
        SaveCharSO(data);


        Data.RemoveAll(x => true);
        www = UnityWebRequest.Get(TellURL);
        yield return www.SendWebRequest();

        data = www.downloadHandler.text;
        SaveTellSO(data);


        Data.RemoveAll(x => true);
        www = UnityWebRequest.Get(SkillURL);
        yield return www.SendWebRequest();

        data = www.downloadHandler.text;
        SaveSKillSO(data);

        PatchLoading.SetActive(false);
        Debug.Log("Patch Complete");
        
    }


    List<List<string>> Data = new List<List<string>>();  // 저장할 데이터
    public void SaveCharSO(string data)
    {
        string[] splitRow = data.Split("\n");       // 엔터를 기준으로 스플릿

        foreach(string str in splitRow)
        {
            string[] split = str.Split("\t");       // 탭을 기준으로 스플릿
            Data.Add(split.ToList());               // 데이터 추가
            
        }   
        for(int i = 0; i < splitRow.Length; i++)
        {
            CharacterDatas targetCharData = CharSO.CharDatas[i];

            targetCharData.Damage = float.Parse(Data[i][0]);
            targetCharData.Armor = float.Parse(Data[i][1]);
            targetCharData.Speed = int.Parse(Data[i][2]);
            targetCharData.Critical = int.Parse(Data[i][3]);
            targetCharData.Dodge = int.Parse(Data[i][4]);
            targetCharData.MaxHp = double.Parse(Data[i][5]);
            targetCharData.Resist = int.Parse(Data[i][6]);
        }    
    }

    public void SaveTellSO(string data)
    {
        string[] splitRow = data.Split("\n");       // 엔터를 기준으로 스플릿

        foreach(string str in splitRow)
        {
            string[] split = str.Split("\t");       // 탭을 기준으로 스플릿
            Data.Add(split.ToList());               // 데이터 추가
            
        }   
        // C Rank
        for(int i = 0; i < 11; i++)
        {
            TellentData targetData = TellSO.tellentData_C[i];

            targetData.Name = Data[i][3];
            targetData.Type = (Etel_type)Enum.Parse(typeof(Etel_type), Data[i][2]);
            targetData.Rank = (Etel_Rank)Enum.Parse(typeof(Etel_Rank), Data[i][1]);;
            targetData.Contents = Data[i][4];
        }    

        // B Rank
        for(int i = 99; i < 117; i++)
        {
            TellentData targetData = TellSO.tellentData_B[i-99];

            targetData.Name = Data[i][3];
            targetData.Type = (Etel_type)Enum.Parse(typeof(Etel_type), Data[i][2]);
            targetData.Rank = (Etel_Rank)Enum.Parse(typeof(Etel_Rank), Data[i][1]);;
            targetData.Contents = Data[i][4];
        }  

        // A Rank
        for(int i = 199; i < 204; i++)
        {
            TellentData targetData = TellSO.tellentData_A[i-199];

            targetData.Name = Data[i][3];
            targetData.Type = (Etel_type)Enum.Parse(typeof(Etel_type), Data[i][2]);
            targetData.Rank = (Etel_Rank)Enum.Parse(typeof(Etel_Rank), Data[i][1]);;
            targetData.Contents = Data[i][4];
        }  

        // S Rank
        for(int i = 228; i < 230; i++)
        {
            TellentData targetData = TellSO.tellentData_S[i-228];

            targetData.Name = Data[i][3];
            targetData.Type = (Etel_type)Enum.Parse(typeof(Etel_type), Data[i][2]);
            targetData.Rank = (Etel_Rank)Enum.Parse(typeof(Etel_Rank), Data[i][1]);;
            targetData.Contents = Data[i][4];
        }  
    }

    public void SaveSKillSO(string data)
    {
        string[] splitRow = data.Split("\n");       // 엔터를 기준으로 스플릿

        foreach (string str in splitRow)
        {
            string[] split = str.Split("\t");       // 탭을 기준으로 스플릿
            Data.Add(split.ToList());               // 데이터 추가

        }
        for (int i = 0; i < 6; i++)
        {
            CharSKillDatas targetData = SkillSO.WarriorSKillDatas[i];

            targetData.Code = int.Parse(Data[i][0]);
            targetData.Name = Data[i][1];
            targetData.Contents = Data[i][2];
            targetData.MultiTarget = bool.Parse(Data[i][3]);
            targetData.Cost = int.Parse(Data[i][4]);
            targetData.Buff = bool.Parse(Data[i][5]);
        }
        for (int i = 6; i < 12; i++)
        {
            CharSKillDatas targetData = SkillSO.MagitionSKillDatas[i - 6];

            targetData.Code = int.Parse(Data[i][0]);
            targetData.Name = Data[i][1];
            targetData.Contents = Data[i][2];
            targetData.MultiTarget = bool.Parse(Data[i][3]);
            targetData.Cost = int.Parse(Data[i][4]);
            targetData.Buff = bool.Parse(Data[i][5]);
        }
        for (int i = 12; i < 18; i++)
        {
            CharSKillDatas targetData = SkillSO.SupporterSKillDatas[i - 12];

            targetData.Code = int.Parse(Data[i][0]);
            targetData.Name = Data[i][1];
            targetData.Contents = Data[i][2];
            targetData.MultiTarget = bool.Parse(Data[i][3]);
            targetData.Cost = int.Parse(Data[i][4]);
            targetData.Buff = bool.Parse(Data[i][5]);
        }
        for (int i = 18; i < 24; i++)
        {
            CharSKillDatas targetData = SkillSO.AssassinSKillDatas[i - 18];

            targetData.Code = int.Parse(Data[i][0]);
            targetData.Name = Data[i][1];
            targetData.Contents = Data[i][2];
            targetData.MultiTarget = bool.Parse(Data[i][3]);
            targetData.Cost = int.Parse(Data[i][4]);
            targetData.Buff = bool.Parse(Data[i][5]);
        }


    }
}
