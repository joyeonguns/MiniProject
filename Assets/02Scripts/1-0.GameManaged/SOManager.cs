using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SOManager : MonoBehaviour
{
    
    public static SOManager instance = null;

    public CharacterSO CharSO;
    public ItemDataSO ItemSO;
    public TellentDataSO TellSO;
    public CharSKillDataSO SkillSO;

    private void Awake() {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject); 
            this.gameObject.SetActive(true);
        }
        else if(instance != this)
        {
            //instance = null;
            Destroy(this.gameObject);
        }  
    }

    public static CharacterSO GetChar()
    {
        return instance.CharSO;
    }
    public static TellentDataSO GetTellent()
    {
        return instance.TellSO;
    }
    public static CharSKillDataSO GetSkill()
    {
        return instance.SkillSO;
    }
}
