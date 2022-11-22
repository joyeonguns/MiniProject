using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameEnd : MonoBehaviour
{
    int resultScore;
    // Start is called before the first frame update
    void Start()
    {
        SetGameScoreData();
        SetResultScore();
    }

    public int Sumlevel(List<Player> players)
    {
        int sum = 0;
        foreach(var player in players)
        {
            sum += player.Level;
        }

        return sum;
    } 

    void SetGameScoreData()
    {
        GameManager.instance.GameScoreData.floor = GameManager.instance._CurMap.floor;
        GameManager.instance.GameScoreData.C_Rank = GameManager.instance.Tellents[0].Count;
        GameManager.instance.GameScoreData.B_Rank = GameManager.instance.Tellents[1].Count;
        GameManager.instance.GameScoreData.A_Rank = GameManager.instance.Tellents[2].Count;
        GameManager.instance.GameScoreData.S_Rank = GameManager.instance.Tellents[3].Count;
        GameManager.instance.GameScoreData.Gold = GameManager.instance.curGold;
        GameManager.instance.GameScoreData.All_Lvl = Sumlevel(GameManager.instance.MyParty);        

        Func<int,int,int> memberCount = (x,y) => {if(x == y) return 1; else return 0;};
        GameManager.instance.GameScoreData.Member_1 = memberCount(GameManager.instance.MyParty.Count, 1);
        GameManager.instance.GameScoreData.Member_3 = memberCount(GameManager.instance.MyParty.Count, 3);
    }

    void SetResultScore()
    {
        ScoreData SD = GameManager.instance.GameScoreData;

        resultScore = SD.floor * 1000 + SD.win_Battle * 3000 + SD.win_Boss * 150000 
            + SD.C_Rank * 1500 + SD.B_Rank * 5000 + SD.A_Rank * 20000 + SD.S_Rank * 50000
            + SD.Attack_Count * 500 + SD.Skill_Count * 100 + SD.Ulti_Count * 1000
            + SD.All_Lvl * 1000 + SD.Used_Itme * 1000 + SD.Member_1 * 300000 + SD.Member_3 * 100000
            + SD.Critical_Count * 300 + SD.Dodge_Count * 500;
    }


    
}
