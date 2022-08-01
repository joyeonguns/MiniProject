using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum Etel_type {getAfter,beforBattle,beforeTurn,AfterBattle};
public enum Etel_Rank {A,B,C};
public class TellentsScripts
{
    public int tel_num;
    public string Tel_Name;
    public string Comments;
    public Etel_type type;
    Etel_Rank rank;

    // Func 특성 적용 함수
    public Action<List<Save.Character>, int, List<Save.Character>, int> TellentApply;
    
    public List<Tuple<string,string>> BBComents = new List<Tuple<string,string>>
    {
        
    }; 
    
    public TellentsScripts()
    {

    }
    public TellentsScripts(Etel_type _type, int tel_num)
    {
        Tel_Name = "특성";
        type = _type;
        switch(type)
        {
            case Etel_type.beforBattle :
                SetBeforeBattle(tel_num);
                break;
            case Etel_type.AfterBattle :
                SetAfterBattle(tel_num);
                break;
            case Etel_type.beforeTurn :
                SetBeforeTurn(tel_num);
                break;
            case Etel_type.getAfter :
                SetGetAfter(tel_num);
                break;
        }
        
    }

    public void SetBeforeTurn(int _tel_num)
    {    
        tel_num = _tel_num;
        switch(tel_num)
        {
            case 0 :
                TellentApply = BeforeTurn_0;
                //_telApp = BeforeTurn_0;
                break;
            case 1 :
                TellentApply = BeforeTurn_1;
                break;
            case 2 :
                TellentApply = BeforeTurn_2;
                break;
            case 3 :
                TellentApply = BeforeTurn_3;
                break;
        }
    }

    public void SetGetAfter(int _tel_num)
    {
        tel_num = _tel_num;
        switch(tel_num)
        {
            case 0 :
                TellentApply = GetAfter_0;
                Tel_Name = "0";
                Comments = "급속 성장 \n 모두 레벨업";
                
                break;
            case 1 :
                TellentApply = GetAfter_1;
                Tel_Name = "1";
                Comments = "포 식 \n 모두 체력 20회복 \n 최대체력 +5";
                break;
            case 2 :
                TellentApply = GetAfter_2;
                Tel_Name = "2";
                Comments = "육체 수양 \n 최대체력 +20";
                break;
            case 3 :
                TellentApply = GetAfter_3;
                Tel_Name = "3";
                Comments = "행 운 \n 행운 + 3";
                break;
        }
    }

    public void SetAfterBattle(int _tel_num)
    {
        tel_num = _tel_num;
        switch(tel_num)
        {
            case 0 :
                TellentApply = AfterBattle_0;
                //_telApp = BeforeTurn_0;
                break;
            case 1 :
                TellentApply = AfterBattle_1;
                break;
            case 2 :
                TellentApply = AfterBattle_2;
                break;
            case 3 :
                TellentApply = AfterBattle_3;
                break;
        }
    }

    public void SetBeforeBattle(int _tel_num)
    {
        tel_num = _tel_num;
        switch(tel_num)
        {
            case 0 :
                TellentApply = BeforeBattle_0;
                //_telApp = BeforeTurn_0;
                break;
            case 1 :
                TellentApply = BeforeBattle_1;
                break;
            case 2 :
                TellentApply = BeforeBattle_2;
                break;
            case 3 :
                TellentApply = BeforeBattle_3;
                break;
        }
    }

    // 턴 직전
    void BeforeTurn_0(List<Save.Character> Caster, int CasterIdx, List<Save.Character> Enemy, int EnemyIdx)
    {
        Debug.Log("Intelligence");
        Caster[CasterIdx].Mana += 2;
        
        
    }

    void BeforeTurn_1(List<Save.Character> Caster, int CasterIdx, List<Save.Character> Enemy, int EnemyIdx)
    {
        Debug.Log("SlowStarter");
                
        
        
    }

    void BeforeTurn_2(List<Save.Character> Caster, int CasterIdx, List<Save.Character> Enemy, int EnemyIdx)
    {
        Debug.Log("LuckyStrike");

        
    }
    void BeforeTurn_3(List<Save.Character> Caster, int CasterIdx, List<Save.Character> Enemy, int EnemyIdx)
    {
        Debug.Log("Ready");
    }


    // 특성 획득
    void GetAfter_0(List<Save.Character> Caster, int CasterIdx, List<Save.Character> Enemy, int EnemyIdx)
    {
        Debug.Log("Giant");
        foreach (var cha in Caster)
        {
            cha.LevelUp();
        }         
    }

    void GetAfter_1(List<Save.Character> Caster, int CasterIdx, List<Save.Character> Enemy, int EnemyIdx)
    {
        Debug.Log("predation");
        
    }

    void GetAfter_2(List<Save.Character> Caster, int CasterIdx, List<Save.Character> Enemy, int EnemyIdx)
    {
        Debug.Log("Exercise");

        
    }
    void GetAfter_3(List<Save.Character> Caster, int CasterIdx, List<Save.Character> Enemy, int EnemyIdx)
    {
        Debug.Log("Fortune");
        
           
    }

    // 전투 이후
    void AfterBattle_0(List<Save.Character> Caster, int CasterIdx, List<Save.Character> Enemy, int EnemyIdx)
    {
        Debug.Log("Robbery");
        GameManager.instance.ResultData.Gold = (int)(GameManager.instance.ResultData.Gold * 1.3f) ;
        
        
    }

    void AfterBattle_1(List<Save.Character> Caster, int CasterIdx, List<Save.Character> Enemy, int EnemyIdx)
    {
        Debug.Log("Trasure Hunter");
        if(GameManager.instance.ResultData.ResultMode != ResultEnum.NomalBattle)
        {
            GameManager.instance.ResultData.Gold += 100;
        }
        
        
    }

    void AfterBattle_2
    (List<Save.Character> Caster, int CasterIdx, List<Save.Character> Enemy, int EnemyIdx)
    {
        Debug.Log("ReGroup");
        foreach(var cha in Caster)
        {
            cha.Mana +=2;
        }
        
    }
    void AfterBattle_3(List<Save.Character> Caster, int CasterIdx, List<Save.Character> Enemy, int EnemyIdx)
    {
        Debug.Log("Alchemy");
        GameManager.instance.ResultData.ItemRate = 100;
           
    }

    // 전투 시작시
    void BeforeBattle_0(List<Save.Character> Caster, int CasterIdx, List<Save.Character> Enemy, int EnemyIdx)
    {
        Debug.Log("Reinforce");
        
    }

    void BeforeBattle_1(List<Save.Character> Caster, int CasterIdx, List<Save.Character> Enemy, int EnemyIdx)
    {
        Debug.Log("SoloPlayer");
        
        
    }

    void BeforeBattle_2(List<Save.Character> Caster, int CasterIdx, List<Save.Character> Enemy, int EnemyIdx)
    {
        Debug.Log("ReaderShip");
        
    }
    void BeforeBattle_3(List<Save.Character> Caster, int CasterIdx, List<Save.Character> Enemy, int EnemyIdx)
    {
        Debug.Log("Foresight");

           
    }
    
}
