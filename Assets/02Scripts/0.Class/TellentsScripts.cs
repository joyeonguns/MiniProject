using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum Etel_type {Get,Battle,End};
public enum Etel_Rank {C,B,A,S};
public class TellentsScripts
{
    public int Code;
    public string name;
    public string Comments;
    public Etel_type type;
    public Etel_Rank Rank;

    // Action 적용 함수
    public Action<List<Save.Character>, int, List<Save.Character>, int> TellentApply;
    public Tuple<string, Action<List<Save.Character>, int, List<Save.Character>, int>, Etel_type>[] Rank_C = new Tuple<string,Action<List<Save.Character>, int, List<Save.Character>, int>, Etel_type>[]
    {
        new Tuple<string, Action<List<Save.Character>, int, List<Save.Character>, int>, Etel_type>("Strength1",Strength1, Etel_type.Get),
        new Tuple<string, Action<List<Save.Character>, int, List<Save.Character>, int>, Etel_type>("Strength2",Strength2, Etel_type.Get),
        new Tuple<string, Action<List<Save.Character>, int, List<Save.Character>, int>, Etel_type>("Handy1",Handy1, Etel_type.Get),
        new Tuple<string, Action<List<Save.Character>, int, List<Save.Character>, int>, Etel_type>("Handy2",Handy2, Etel_type.Get),
        new Tuple<string, Action<List<Save.Character>, int, List<Save.Character>, int>, Etel_type>("Nimble1",Nimble1, Etel_type.Get),
        new Tuple<string, Action<List<Save.Character>, int, List<Save.Character>, int>, Etel_type>("Nimble2",Nimble2, Etel_type.Get),
        new Tuple<string, Action<List<Save.Character>, int, List<Save.Character>, int>, Etel_type>("HardSkin1",HardSkin1, Etel_type.Get),
        new Tuple<string, Action<List<Save.Character>, int, List<Save.Character>, int>, Etel_type>("HardSkin2",HardSkin2, Etel_type.Get),
        new Tuple<string, Action<List<Save.Character>, int, List<Save.Character>, int>, Etel_type>("Squat1",Squat1, Etel_type.Get),
        new Tuple<string, Action<List<Save.Character>, int, List<Save.Character>, int>, Etel_type>("Squat2",Squat2, Etel_type.Get),
        new Tuple<string, Action<List<Save.Character>, int, List<Save.Character>, int>, Etel_type>("Ungijosic",Ungi, Etel_type.Battle)
    };

    public Tuple<string, Action<List<Save.Character>, int, List<Save.Character>, int>, Etel_type>[] Rank_B = new Tuple<string,Action<List<Save.Character>, int, List<Save.Character>, int>, Etel_type>[]
    {
        new Tuple<string, Action<List<Save.Character>, int, List<Save.Character>, int>, Etel_type>("Protein1",Protein1, Etel_type.Get),
        new Tuple<string, Action<List<Save.Character>, int, List<Save.Character>, int>, Etel_type>("Protein2",Protein2, Etel_type.Get),
        new Tuple<string, Action<List<Save.Character>, int, List<Save.Character>, int>, Etel_type>("Pinpoint1",Pinpoint1, Etel_type.Get),
        new Tuple<string, Action<List<Save.Character>, int, List<Save.Character>, int>, Etel_type>("Pinpoint2",Pinpoint2, Etel_type.Get),
        new Tuple<string, Action<List<Save.Character>, int, List<Save.Character>, int>, Etel_type>("Flexible1",Flexible1, Etel_type.Get),
        new Tuple<string, Action<List<Save.Character>, int, List<Save.Character>, int>, Etel_type>("Flexible2",Flexible2, Etel_type.Get),
        new Tuple<string, Action<List<Save.Character>, int, List<Save.Character>, int>, Etel_type>("LuckyBrooch",LuckyBrooch, Etel_type.Get),
        new Tuple<string, Action<List<Save.Character>, int, List<Save.Character>, int>, Etel_type>("Greasing1",Greasing1, Etel_type.Get),
        new Tuple<string, Action<List<Save.Character>, int, List<Save.Character>, int>, Etel_type>("Greasing2",Greasing2, Etel_type.Get),
        new Tuple<string, Action<List<Save.Character>, int, List<Save.Character>, int>, Etel_type>("StonSkin1",StonSkin1, Etel_type.Get),
        new Tuple<string, Action<List<Save.Character>, int, List<Save.Character>, int>, Etel_type>("StonSkin2",StonSkin2, Etel_type.Get),
        new Tuple<string, Action<List<Save.Character>, int, List<Save.Character>, int>, Etel_type>("Speedster1",Speedster1, Etel_type.Get),
        new Tuple<string, Action<List<Save.Character>, int, List<Save.Character>, int>, Etel_type>("Speedster2",Speedster2, Etel_type.Get),
        new Tuple<string, Action<List<Save.Character>, int, List<Save.Character>, int>, Etel_type>("OutSider",OutSider, Etel_type.Battle),
        new Tuple<string, Action<List<Save.Character>, int, List<Save.Character>, int>, Etel_type>("Insider",Insider, Etel_type.Battle),
        new Tuple<string, Action<List<Save.Character>, int, List<Save.Character>, int>, Etel_type>("Rob",Rob, Etel_type.End),
        new Tuple<string, Action<List<Save.Character>, int, List<Save.Character>, int>, Etel_type>("MeetParty",MeetParty, Etel_type.Get),
        new Tuple<string, Action<List<Save.Character>, int, List<Save.Character>, int>, Etel_type>("Rest",Rest, Etel_type.Get)
    };

    public Tuple<string, Action<List<Save.Character>, int, List<Save.Character>, int>, Etel_type>[] Rank_A = new Tuple<string,Action<List<Save.Character>, int, List<Save.Character>, int>, Etel_type>[]
    {
        new Tuple<string, Action<List<Save.Character>, int, List<Save.Character>, int>, Etel_type>("ReadedShot",ReadedShot, Etel_type.Battle),
        new Tuple<string, Action<List<Save.Character>, int, List<Save.Character>, int>, Etel_type>("Battery",Battery, Etel_type.Battle),
        new Tuple<string, Action<List<Save.Character>, int, List<Save.Character>, int>, Etel_type>("FullCondition",FullCondition, Etel_type.Battle),
        new Tuple<string, Action<List<Save.Character>, int, List<Save.Character>, int>, Etel_type>("ManaForce",ManaForce, Etel_type.Battle),
        new Tuple<string, Action<List<Save.Character>, int, List<Save.Character>, int>, Etel_type>("ThroughStrike",ThroughStrike, Etel_type.Get)
    };

    public Tuple<string, Action<List<Save.Character>, int, List<Save.Character>, int>, Etel_type>[] Rank_S = new Tuple<string,Action<List<Save.Character>, int, List<Save.Character>, int>, Etel_type>[]
    {
        new Tuple<string, Action<List<Save.Character>, int, List<Save.Character>, int>, Etel_type>("Berserker",Berserker, Etel_type.Battle),
        new Tuple<string, Action<List<Save.Character>, int, List<Save.Character>, int>, Etel_type>("AccelationGrowth",AccelationGrowth, Etel_type.Battle)
    };
    

    public TellentsScripts(){}
    public TellentsScripts(Etel_Rank rank, int Code)
    {
        switch((int)rank)
        {
            case 0:
            name = Rank_C[Code].Item1;
            TellentApply = Rank_C[Code].Item2;
            type = Rank_C[Code].Item3;
            Rank = rank;
            break;

            case 1:
            name = Rank_B[Code].Item1;
            TellentApply = Rank_B[Code].Item2;
            type = Rank_B[Code].Item3;
            Rank = rank;
            break;

            case 2:
            name = Rank_A[Code].Item1;
            TellentApply = Rank_A[Code].Item2;
            type = Rank_A[Code].Item3;
            Rank = rank;
            break;

            case 3:
            name = Rank_S[Code].Item1;
            TellentApply = Rank_S[Code].Item2;
            type = Rank_S[Code].Item3;
            Rank = rank;
            break;
        }
        
    }

    

    // C 랭크
    static void Strength1(List<Save.Character> Caster, int CasterIdx, List<Save.Character> Enemy, int EnemyIdx)
    {
        Debug.Log("Strength1");
        Caster[CasterIdx].status.Damage += 1;
    }
    static void Strength2(List<Save.Character> Caster, int CasterIdx, List<Save.Character> Enemy, int EnemyIdx)
    {        
        if(Caster[CasterIdx].Main == true)
        {
            Debug.Log("Strength2");
            Caster[CasterIdx].status.Damage += 2;
        }              
    }

    static void Handy1(List<Save.Character> Caster, int CasterIdx, List<Save.Character> Enemy, int EnemyIdx)
    {
        Debug.Log("Handy1");
        Caster[CasterIdx].status.Critical += 10;
    }
    static void Handy2(List<Save.Character> Caster, int CasterIdx, List<Save.Character> Enemy, int EnemyIdx)
    {
        Debug.Log("Handy2");
        if(Caster[CasterIdx].Main == true)
            Caster[CasterIdx].status.Critical += 15;  
    }

    static void Nimble1(List<Save.Character> Caster, int CasterIdx, List<Save.Character> Enemy, int EnemyIdx)
    {
        Debug.Log("Nimble1");
        Caster[CasterIdx].status.Dodge += 10;
    }
    static void Nimble2(List<Save.Character> Caster, int CasterIdx, List<Save.Character> Enemy, int EnemyIdx)
    {
        Debug.Log("Nimble2");
        if(Caster[CasterIdx].Main == true)
            Caster[CasterIdx].status.Dodge += 15;  
    }

    static void HardSkin1(List<Save.Character> Caster, int CasterIdx, List<Save.Character> Enemy, int EnemyIdx)
    {
        Debug.Log("HardSkin1");
        Caster[CasterIdx].status.Armor += 0.05f;
    }
    static void HardSkin2(List<Save.Character> Caster, int CasterIdx, List<Save.Character> Enemy, int EnemyIdx)
    {
        Debug.Log("HardSkin2");
        if(Caster[CasterIdx].Main == true)
            Caster[CasterIdx].status.Armor += 0.07f;  
    }

    static void Squat1(List<Save.Character> Caster, int CasterIdx, List<Save.Character> Enemy, int EnemyIdx)
    {
        Debug.Log("Squat1");
        Caster[CasterIdx].status.Speed += 1;
    }
    static void Squat2(List<Save.Character> Caster, int CasterIdx, List<Save.Character> Enemy, int EnemyIdx)
    {
        Debug.Log("Squat2");
        if(Caster[CasterIdx].Main == true)
            Caster[CasterIdx].status.Speed += 2;  
    }
    static void Ungi(List<Save.Character> Caster, int CasterIdx, List<Save.Character> Enemy, int EnemyIdx)
    {
        Debug.Log("Ungijosic");
        if(Caster[CasterIdx].turn == 1)
            Caster[CasterIdx].Mana += 2;
    }

    // B 랭크
    static void Protein1(List<Save.Character> Caster, int CasterIdx, List<Save.Character> Enemy, int EnemyIdx)
    {
        Debug.Log("Protein1");
        Caster[CasterIdx].status.Damage += 2;
    }
    static void Protein2(List<Save.Character> Caster, int CasterIdx, List<Save.Character> Enemy, int EnemyIdx)
    {
        Debug.Log("Protein2");
        if(Caster[CasterIdx].Main == true)
            Caster[CasterIdx].status.Damage += 4;  
    }

    static void Pinpoint1(List<Save.Character> Caster, int CasterIdx, List<Save.Character> Enemy, int EnemyIdx)
    {
        Debug.Log("Pinpoint1");
        Caster[CasterIdx].status.Critical += 15;
        Caster[CasterIdx].status.Damage += 1;
    }
    static void Pinpoint2(List<Save.Character> Caster, int CasterIdx, List<Save.Character> Enemy, int EnemyIdx)
    {
        Debug.Log("Pinpoint2");
        if(Caster[CasterIdx].Main == true)
        {
            Caster[CasterIdx].status.Critical += 30;
            Caster[CasterIdx].status.Damage += 2;
        }  
    }

    static void Flexible1(List<Save.Character> Caster, int CasterIdx, List<Save.Character> Enemy, int EnemyIdx)
    {
        Debug.Log("Flexible1");
        Caster[CasterIdx].status.Dodge += 20;
    }
    static void Flexible2(List<Save.Character> Caster, int CasterIdx, List<Save.Character> Enemy, int EnemyIdx)
    {
        Debug.Log("Flexible2");
        if(Caster[CasterIdx].Main == true)
            Caster[CasterIdx].status.Dodge += 40;  
    }

    static void LuckyBrooch(List<Save.Character> Caster, int CasterIdx, List<Save.Character> Enemy, int EnemyIdx)
    {
        Debug.Log("LuckyBrooch");
    }
    static void Greasing1(List<Save.Character> Caster, int CasterIdx, List<Save.Character> Enemy, int EnemyIdx)
    {
        Debug.Log("Greasing1");
        Caster[CasterIdx].status.Dodge += 40;  
        Caster[CasterIdx].status.Critical -= 20;  
    }

    static void Greasing2(List<Save.Character> Caster, int CasterIdx, List<Save.Character> Enemy, int EnemyIdx)
    {
        Debug.Log("Greasing2");
        if(Caster[CasterIdx].Main == true)
        {
            Caster[CasterIdx].status.Dodge += 60;
            Caster[CasterIdx].status.Critical -= 20;
        }
             
    }
    static void StonSkin1(List<Save.Character> Caster, int CasterIdx, List<Save.Character> Enemy, int EnemyIdx)
    {
        Debug.Log("StonSkin1");
        Caster[CasterIdx].status.Armor += 0.08f;  
        Caster[CasterIdx].status.Speed -= 2;  
    }

    static void StonSkin2(List<Save.Character> Caster, int CasterIdx, List<Save.Character> Enemy, int EnemyIdx)
    {
        Debug.Log("StonSkin2");
        if(Caster[CasterIdx].Main == true)
        {
            Caster[CasterIdx].status.Armor += 0.12f;  
            Caster[CasterIdx].status.Speed -= 2;  
        }             
    }
    static void Speedster1(List<Save.Character> Caster, int CasterIdx, List<Save.Character> Enemy, int EnemyIdx)
    {
        Debug.Log("Speedster1");
        Caster[CasterIdx].status.Speed += 3;  
        Caster[CasterIdx].status.Armor -= 0.08f;  
    }

    static void Speedster2(List<Save.Character> Caster, int CasterIdx, List<Save.Character> Enemy, int EnemyIdx)
    {
        Debug.Log("Speedster2");
        if(Caster[CasterIdx].Main == true)
        {
            Caster[CasterIdx].status.Armor -= 0.08f;  
            Caster[CasterIdx].status.Speed += 6;  
        }             
    }
    static void OutSider(List<Save.Character> Caster, int CasterIdx, List<Save.Character> Enemy, int EnemyIdx)
    {
        Debug.Log("OutSider");
        if(Caster[CasterIdx].Main == true && Caster.Count != 3)
        {
            float buff = (3-Caster.Count)*0.1f;
            Caster[CasterIdx].Battlestatus.AllStatBuff(buff); 
        }   
    }

    static void Insider(List<Save.Character> Caster, int CasterIdx, List<Save.Character> Enemy, int EnemyIdx)
    {
        Debug.Log("Insider");
        if(Caster[CasterIdx].Main == true && Caster.Count == 3)
        {
            Caster[CasterIdx].Battlestatus.AllStatBuff(0.1f);   
        }             
    }
    static void Rob(List<Save.Character> Caster, int CasterIdx, List<Save.Character> Enemy, int EnemyIdx)
    {
        Debug.Log("Rob");
        GameManager.instance.ResultData.Gold += 50;
    }

    static void MeetParty(List<Save.Character> Caster, int CasterIdx, List<Save.Character> Enemy, int EnemyIdx)
    {
        Debug.Log("MeetParty");
        Caster[CasterIdx].status.MaxHp += 10;           
    }
    static void Rest(List<Save.Character> Caster, int CasterIdx, List<Save.Character> Enemy, int EnemyIdx)
    {
        Debug.Log("Rest");
        Caster[CasterIdx].Hp += (Caster[CasterIdx].status.MaxHp * 0.5f);   
    } 
    
    // Rank A
    static void ReadedShot(List<Save.Character> Caster, int CasterIdx, List<Save.Character> Enemy, int EnemyIdx)
    {
        Debug.Log("ReadedShot");
        if(Caster[CasterIdx].turn == 1)
        {
            Caster[CasterIdx].Battlestatus.Damage += 15;  
            Caster[CasterIdx].Battlestatus.Critical += 40;  
        }
                     
    }
    static void Battery(List<Save.Character> Caster, int CasterIdx, List<Save.Character> Enemy, int EnemyIdx)
    {
        Debug.Log("Battery");
        Caster[CasterIdx].Mana += 3;   
    } 
    static void FullCondition(List<Save.Character> Caster, int CasterIdx, List<Save.Character> Enemy, int EnemyIdx)
    {
        Debug.Log("FullCondition");
        if(Caster[CasterIdx].Hp == Caster[CasterIdx].status.MaxHp)
        {
            Caster[CasterIdx].Battlestatus.Damage *= 1.5f;
            Caster[CasterIdx].Battlestatus.Critical += (int)(Caster[CasterIdx].status.Critical * 1.5f);
        }
                   
    }
    static void ManaForce(List<Save.Character> Caster, int CasterIdx, List<Save.Character> Enemy, int EnemyIdx)
    {
        Debug.Log("ManaForce");
        Caster[CasterIdx].Battlestatus.Damage += Caster[CasterIdx].Mana;   
    } 
    static void ThroughStrike(List<Save.Character> Caster, int CasterIdx, List<Save.Character> Enemy, int EnemyIdx)
    {
        Debug.Log("ThroughStrike");
        Caster[CasterIdx].pierce = true;
    }

    // 랭크 S
    static void Berserker(List<Save.Character> Caster, int CasterIdx, List<Save.Character> Enemy, int EnemyIdx)
    {
        Debug.Log("Berserker");
        if(Caster[CasterIdx].Hp < (Caster[CasterIdx].status.MaxHp / 2))
        {
            Caster[CasterIdx].Battlestatus.Damage *= 2;
            Caster[CasterIdx].Battlestatus.Critical *= 2;
        }
    }
    static void AccelationGrowth(List<Save.Character> Caster, int CasterIdx, List<Save.Character> Enemy, int EnemyIdx)
    {
        Debug.Log("AccelationGrowth");
        if(Caster[CasterIdx].Main == true)
        {
            int n = 0;
            float dmg = 0;
            while(n < Caster[CasterIdx].turn)
            {                
                n++;
                dmg += n;
            }
            Caster[CasterIdx].Battlestatus.Damage += dmg;
            Caster[CasterIdx].Battlestatus.Critical += (int)(dmg*5);
        }
    }
}
