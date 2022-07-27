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
    public Func<GameInformation, List<Save_Charater_Class.SD>, int, List<Save_Charater_Class.Class_Status>, Save_Charater_Class.Class_Status, List<Save_Charater_Class.SD>, int,Save_Charater_Class.Class_Status> TellentApply;
    
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
    Save_Charater_Class.Class_Status BeforeTurn_0(GameInformation GI, List<Save_Charater_Class.SD> Caster, int CasterIdx, List<Save_Charater_Class.Class_Status> status, Save_Charater_Class.Class_Status volastatus, List<Save_Charater_Class.SD> Enemy,int EnemyIdx)
    {
        Debug.Log("Intelligence");
        Caster[CasterIdx].Mana += 2;
        
        return volastatus;
    }

    Save_Charater_Class.Class_Status BeforeTurn_1(GameInformation GI, List<Save_Charater_Class.SD> Caster, int CasterIdx, List<Save_Charater_Class.Class_Status> status, Save_Charater_Class.Class_Status volastatus, List<Save_Charater_Class.SD> Enemy,int EnemyIdx)
    {
        Debug.Log("SlowStarter");
        Save_Charater_Class.Class_Status st = status[CasterIdx];        
        st.s_Damage += GI.TurnCounts; 
        status[CasterIdx] = st;
        volastatus.s_Damage += GI.TurnCounts;
        
        return volastatus; 
    }

    Save_Charater_Class.Class_Status BeforeTurn_2(GameInformation GI, List<Save_Charater_Class.SD> Caster, int CasterIdx, List<Save_Charater_Class.Class_Status> status, Save_Charater_Class.Class_Status volastatus, List<Save_Charater_Class.SD> Enemy,int EnemyIdx)
    {
        Debug.Log("LuckyStrike");
        if(GI.TurnCounts == 3)
            volastatus.s_Damage += GI.TurnCounts;

        return volastatus;
    }
    Save_Charater_Class.Class_Status BeforeTurn_3(GameInformation GI, List<Save_Charater_Class.SD> Caster, int CasterIdx, List<Save_Charater_Class.Class_Status> status, Save_Charater_Class.Class_Status volastatus, List<Save_Charater_Class.SD> Enemy,int EnemyIdx)
    {
        Debug.Log("Ready");
        if(GI.TurnCounts == 1)
        {
            Debug.Log("10000");
            volastatus.s_Critical += 10000;
           
            Debug.Log(volastatus.s_Critical);
        }
        return volastatus;    
    }


    // 특성 획득
    Save_Charater_Class.Class_Status GetAfter_0(GameInformation GI, List<Save_Charater_Class.SD> Caster, int CasterIdx, List<Save_Charater_Class.Class_Status> status, Save_Charater_Class.Class_Status volastatus, List<Save_Charater_Class.SD> Enemy,int EnemyIdx)
    {
        Debug.Log("Giant");
        foreach (var cha in Caster)
        {
            cha.LevelUp();
        }        
        
        return volastatus;
    }

    Save_Charater_Class.Class_Status GetAfter_1(GameInformation GI, List<Save_Charater_Class.SD> Caster, int CasterIdx, List<Save_Charater_Class.Class_Status> status, Save_Charater_Class.Class_Status volastatus, List<Save_Charater_Class.SD> Enemy,int EnemyIdx)
    {
        Debug.Log("predation");
        foreach (var cha in Caster)
        {
            cha.status.s_MaxHp +=5;
            cha.Hp +=20;            
        }
        
        return volastatus; 
    }

    Save_Charater_Class.Class_Status GetAfter_2(GameInformation GI, List<Save_Charater_Class.SD> Caster, int CasterIdx, List<Save_Charater_Class.Class_Status> status, Save_Charater_Class.Class_Status volastatus, List<Save_Charater_Class.SD> Enemy,int EnemyIdx)
    {
        Debug.Log("Exercise");
        Caster[CasterIdx].status.s_MaxHp += 20;
        Caster[CasterIdx].Hp +=20;

        return volastatus;
    }
    Save_Charater_Class.Class_Status GetAfter_3(GameInformation GI, List<Save_Charater_Class.SD> Caster, int CasterIdx, List<Save_Charater_Class.Class_Status> status, Save_Charater_Class.Class_Status volastatus, List<Save_Charater_Class.SD> Enemy,int EnemyIdx)
    {
        Debug.Log("Fortune");
        
        return volastatus;    
    }

    // 전투 이후
    Save_Charater_Class.Class_Status AfterBattle_0(GameInformation GI, List<Save_Charater_Class.SD> Caster, int CasterIdx, List<Save_Charater_Class.Class_Status> status, Save_Charater_Class.Class_Status volastatus, List<Save_Charater_Class.SD> Enemy,int EnemyIdx)
    {
        Debug.Log("Robbery");
        GI.Golds = (int)(GI.Golds*1.3f);
        
        return volastatus;
    }

    Save_Charater_Class.Class_Status AfterBattle_1(GameInformation GI, List<Save_Charater_Class.SD> Caster, int CasterIdx, List<Save_Charater_Class.Class_Status> status, Save_Charater_Class.Class_Status volastatus, List<Save_Charater_Class.SD> Enemy,int EnemyIdx)
    {
        Debug.Log("Trasure Hunter");
        if(GI.Battletype != "Nomal")
        {
            GI.Golds += 100;
        }
        
        return volastatus; 
    }

    Save_Charater_Class.Class_Status AfterBattle_2
    (GameInformation GI, List<Save_Charater_Class.SD> Caster, int CasterIdx, List<Save_Charater_Class.Class_Status> status, Save_Charater_Class.Class_Status volastatus, List<Save_Charater_Class.SD> Enemy,int EnemyIdx)
    {
        Debug.Log("ReGroup");
        foreach(var cha in Caster)
        {
            cha.Mana +=2;
        }
        return volastatus; 
    }
    Save_Charater_Class.Class_Status AfterBattle_3(GameInformation GI, List<Save_Charater_Class.SD> Caster, int CasterIdx, List<Save_Charater_Class.Class_Status> status, Save_Charater_Class.Class_Status volastatus, List<Save_Charater_Class.SD> Enemy,int EnemyIdx)
    {
        Debug.Log("Alchemy");
        GI.ItemRate = 100;
        return volastatus;    
    }

    // 전투 시작시
    Save_Charater_Class.Class_Status BeforeBattle_0(GameInformation GI, List<Save_Charater_Class.SD> Caster, int CasterIdx, List<Save_Charater_Class.Class_Status> status, Save_Charater_Class.Class_Status volastatus, List<Save_Charater_Class.SD> Enemy,int EnemyIdx)
    {
        Debug.Log("Reinforce");
        List<Save_Charater_Class.Class_Status> newstatus = new List<Save_Charater_Class.Class_Status>();
        foreach (var st in status)
        {
           Save_Charater_Class.Class_Status _st = st;
           _st.s_Damage *= 1.3f;
           newstatus.Add(_st);
        }
        status = newstatus;
        return volastatus;
    }

    Save_Charater_Class.Class_Status BeforeBattle_1(GameInformation GI, List<Save_Charater_Class.SD> Caster, int CasterIdx, List<Save_Charater_Class.Class_Status> status, Save_Charater_Class.Class_Status volastatus, List<Save_Charater_Class.SD> Enemy,int EnemyIdx)
    {
        Debug.Log("SoloPlayer");
        if(Caster.Count == 1)
        {
            Save_Charater_Class.Class_Status st = status[0];
            st.s_Armor = st.s_Armor * 1.3f;
            st.s_Critical = (int)(st.s_Critical * 1.3f);
            st.s_Damage = (st.s_Damage * 1.3f);
            st.s_Dodge = (int)(st.s_Dodge * 1.3f);
            st.s_Speed = (int)(st.s_Speed * 1.3f);

            status.RemoveAt(0);
            status.Add(st);
        }
        return volastatus; 
    }

    Save_Charater_Class.Class_Status BeforeBattle_2(GameInformation GI, List<Save_Charater_Class.SD> Caster, int CasterIdx, List<Save_Charater_Class.Class_Status> status, Save_Charater_Class.Class_Status volastatus, List<Save_Charater_Class.SD> Enemy,int EnemyIdx)
    {
        Debug.Log("ReaderShip");
        List<Save_Charater_Class.Class_Status> newstatus = new List<Save_Charater_Class.Class_Status>();
        foreach (var _st in status)
        {
            Save_Charater_Class.Class_Status st = _st;

            st.s_Armor = st.s_Armor * 1.1f;
            st.s_Critical = (int)(st.s_Critical * 1.1f);
            st.s_Damage = (st.s_Damage * 1.1f);
            st.s_Dodge = (int)(st.s_Dodge * 1.1f);
            st.s_Speed = (int)(st.s_Speed * 1.1f);
            newstatus.Add(st);
            
        }
        status.Clear();
        for(int i = 0; i < newstatus.Count; i++)
        {
            status.Add(newstatus[i]);
        }
        
        return volastatus;
    }
    Save_Charater_Class.Class_Status BeforeBattle_3(GameInformation GI, List<Save_Charater_Class.SD> Caster, int CasterIdx, List<Save_Charater_Class.Class_Status> status, Save_Charater_Class.Class_Status volastatus, List<Save_Charater_Class.SD> Enemy,int EnemyIdx)
    {
        Debug.Log("Foresight");
        List<Save_Charater_Class.Class_Status> newstatus = new List<Save_Charater_Class.Class_Status>();
        foreach (var _st in status)
        {
            Save_Charater_Class.Class_Status st = _st;

            st.s_Speed = (int)(st.s_Speed +2);
            newstatus.Add(st);
        }
        
        status.Clear();
        for(int i = 0; i < newstatus.Count; i++)
        {
            status.Add(newstatus[i]);
        }

        return volastatus;    
    }
    
}
