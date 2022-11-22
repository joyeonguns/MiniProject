using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class SaveData
{
    public List<MapClass> mapsJson;
    public MapClass nowMap; //current 
    public List<Player> character;
    public int[] itemCodes;
    public List<TellentsScripts> tellent_C;
    public List<TellentsScripts> tellent_B;
    public List<TellentsScripts> tellent_A;
    public List<TellentsScripts> tellent_S;

    

    public int Gold;

    public SaveData()
    {        
                
    }

    public void Saves()
    {
        for (int i = 0; i < 12; i++)
        {
            mapsJson.Add(GameManager.instance.maps[i][0]);
            mapsJson.Add(GameManager.instance.maps[i][1]);
            mapsJson.Add(GameManager.instance.maps[i][2]);
            mapsJson.Add(GameManager.instance.maps[i][3]);
        }

        nowMap = GameManager.instance._CurMap;

        character = GameManager.instance.MyParty;

        tellent_C = GameManager.instance.Tellents[0];
        tellent_B = GameManager.instance.Tellents[1];
        tellent_A = GameManager.instance.Tellents[2];
        tellent_S = GameManager.instance.Tellents[3];
        
        itemCodes = GameManager.instance.ItemList_num;
    
        Gold = GameManager.instance.curGold;
    }    
}

[System.Serializable]
public class SavePlayerSetting
{
    public float BGMSize;
    public float EffectSize;

    public int resolution;

    public void Save()
    {
        float BGvalue;
        bool BGresult = SoundManager.instance.audioMixer.GetFloat("BGSound", out BGvalue);
        if(BGresult)
        {
            BGMSize = Mathf.Pow(2,BGvalue);
        }

        float Effectvalue;
        bool Effectresult = SoundManager.instance.audioMixer.GetFloat("SFXSound", out Effectvalue);
        if(Effectresult)
        {
            EffectSize =  Mathf.Pow(2,Effectvalue);
        }

    }
    
}

