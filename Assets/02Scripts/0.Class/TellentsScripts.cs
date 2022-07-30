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
    public Func<List<Save.Character>, int, List<Save.St_Stat>, Save.St_Stat, List<Save.Character>, int,Save.St_Stat> TellentApply;
    
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
    Save.St_Stat BeforeTurn_0(List<Save.Character> Caster, int CasterIdx, List<Save.St_Stat> status, Save.St_Stat volastatus, List<Save.Character> Enemy,int EnemyIdx)
    {
        Debug.Log("Intelligence");
        Caster[CasterIdx].Mana += 2;
        
        return volastatus;
    }

    Save.St_Stat BeforeTurn_1(List<Save.Character> Caster, int CasterIdx, List<Save.St_Stat> status, Save.St_Stat volastatus, List<Save.Character> Enemy,int EnemyIdx)
    {
        Debug.Log("SlowStarter");
        Save.St_Stat st = status[CasterIdx];        
        st.Damage += Caster[CasterIdx].turn; 
        status[CasterIdx] = st;
        volastatus.Damage += st.Damage;
        
        return volastatus; 
    }

    Save.St_Stat BeforeTurn_2(List<Save.Character> Caster, int CasterIdx, List<Save.St_Stat> status, Save.St_Stat volastatus, List<Save.Character> Enemy,int EnemyIdx)
    {
        Debug.Log("LuckyStrike");
        if(Caster[CasterIdx].turn == 3)
            volastatus.Damage *= 3;

        return volastatus;
    }
    Save.St_Stat BeforeTurn_3(List<Save.Character> Caster, int CasterIdx, List<Save.St_Stat> status, Save.St_Stat volastatus, List<Save.Character> Enemy,int EnemyIdx)
    {
        Debug.Log("Ready");
        if(Caster[CasterIdx].turn == 1)
        {
            Debug.Log("10000");
            volastatus.Critical += 10000;
           
            Debug.Log(volastatus.Critical);
        }
        return volastatus;    
    }


    // 특성 획득
    Save.St_Stat GetAfter_0(List<Save.Character> Caster, int CasterIdx, List<Save.St_Stat> status, Save.St_Stat volastatus, List<Save.Character> Enemy,int EnemyIdx)
    {
        Debug.Log("Giant");
        foreach (var cha in Caster)
        {
            cha.LevelUp();
        }        
        
        return volastatus;
    }

    Save.St_Stat GetAfter_1(List<Save.Character> Caster, int CasterIdx, List<Save.St_Stat> status, Save.St_Stat volastatus, List<Save.Character> Enemy,int EnemyIdx)
    {
        Debug.Log("predation");
        foreach (var cha in Caster)
        {
            cha.status.MaxHp +=5;
            cha.Hp +=20;            
        }
        
        return volastatus; 
    }

    Save.St_Stat GetAfter_2(List<Save.Character> Caster, int CasterIdx, List<Save.St_Stat> status, Save.St_Stat volastatus, List<Save.Character> Enemy,int EnemyIdx)
    {
        Debug.Log("Exercise");
        Caster[CasterIdx].status.MaxHp += 20;
        Caster[CasterIdx].Hp +=20;

        return volastatus;
    }
    Save.St_Stat GetAfter_3(List<Save.Character> Caster, int CasterIdx, List<Save.St_Stat> status, Save.St_Stat volastatus, List<Save.Character> Enemy,int EnemyIdx)
    {
        Debug.Log("Fortune");
        
        return volastatus;    
    }

    // 전투 이후
    Save.St_Stat AfterBattle_0(List<Save.Character> Caster, int CasterIdx, List<Save.St_Stat> status, Save.St_Stat volastatus, List<Save.Character> Enemy,int EnemyIdx)
    {
        Debug.Log("Robbery");
        GameManager.instance.ResultData.Gold = (int)(GameManager.instance.ResultData.Gold * 1.3f) ;
        
        return volastatus;
    }

    Save.St_Stat AfterBattle_1(List<Save.Character> Caster, int CasterIdx, List<Save.St_Stat> status, Save.St_Stat volastatus, List<Save.Character> Enemy,int EnemyIdx)
    {
        Debug.Log("Trasure Hunter");
        if(GameManager.instance.ResultData.ResultMode != ResultEnum.NomalBattle)
        {
            GameManager.instance.ResultData.Gold += 100;
        }
        
        return volastatus; 
    }

    Save.St_Stat AfterBattle_2
    (List<Save.Character> Caster, int CasterIdx, List<Save.St_Stat> status, Save.St_Stat volastatus, List<Save.Character> Enemy,int EnemyIdx)
    {
        Debug.Log("ReGroup");
        foreach(var cha in Caster)
        {
            cha.Mana +=2;
        }
        return volastatus; 
    }
    Save.St_Stat AfterBattle_3(List<Save.Character> Caster, int CasterIdx, List<Save.St_Stat> status, Save.St_Stat volastatus, List<Save.Character> Enemy,int EnemyIdx)
    {
        Debug.Log("Alchemy");
        GameManager.instance.ResultData.ItemRate = 100;
        return volastatus;    
    }

    // 전투 시작시
    Save.St_Stat BeforeBattle_0(List<Save.Character> Caster, int CasterIdx, List<Save.St_Stat> status, Save.St_Stat volastatus, List<Save.Character> Enemy,int EnemyIdx)
    {
        Debug.Log("Reinforce");
        List<Save.St_Stat> newstatus = new List<Save.St_Stat>();
        foreach (var st in status)
        {
           Save.St_Stat _st = st;
           _st.Damage *= 1.3f;
           newstatus.Add(_st);
        }
        status = newstatus;
        return volastatus;
    }

    Save.St_Stat BeforeBattle_1(List<Save.Character> Caster, int CasterIdx, List<Save.St_Stat> status, Save.St_Stat volastatus, List<Save.Character> Enemy,int EnemyIdx)
    {
        Debug.Log("SoloPlayer");
        if(Caster.Count == 1)
        {
            Save.St_Stat st = status[0];
            st.Armor = st.Armor * 1.3f;
            st.Critical = (int)(st.Critical * 1.3f);
            st.Damage = (st.Damage * 1.3f);
            st.Dodge = (int)(st.Dodge * 1.3f);
            st.Speed = (int)(st.Speed * 1.3f);

            status.RemoveAt(0);
            status.Add(st);
        }
        return volastatus; 
    }

    Save.St_Stat BeforeBattle_2(List<Save.Character> Caster, int CasterIdx, List<Save.St_Stat> status, Save.St_Stat volastatus, List<Save.Character> Enemy,int EnemyIdx)
    {
        Debug.Log("ReaderShip");
        List<Save.St_Stat> newstatus = new List<Save.St_Stat>();
        foreach (var _st in status)
        {
            Save.St_Stat st = _st;

            st.Armor = st.Armor * 1.1f;
            st.Critical = (int)(st.Critical * 1.1f);
            st.Damage = (st.Damage * 1.1f);
            st.Dodge = (int)(st.Dodge * 1.1f);
            st.Speed = (int)(st.Speed * 1.1f);
            newstatus.Add(st);
            
        }
        status.Clear();
        for(int i = 0; i < newstatus.Count; i++)
        {
            status.Add(newstatus[i]);
        }
        
        return volastatus;
    }
    Save.St_Stat BeforeBattle_3(List<Save.Character> Caster, int CasterIdx, List<Save.St_Stat> status, Save.St_Stat volastatus, List<Save.Character> Enemy,int EnemyIdx)
    {
        Debug.Log("Foresight");
        List<Save.St_Stat> newstatus = new List<Save.St_Stat>();
        foreach (var _st in status)
        {
            Save.St_Stat st = _st;

            st.Speed = (int)(st.Speed +2);
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
