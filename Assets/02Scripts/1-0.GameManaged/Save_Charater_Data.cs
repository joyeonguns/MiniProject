using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Save_Charater_Data : MonoBehaviour
{

    public static Save_Charater_Data instance = null;
    public List<Save.Player> MyParty = new List<Save.Player>();
    
    public int c_Num;

    private void Awake() 
    {
        instance = this;
        //cur_Map = new MapClass();
        DontDestroyOnLoad(this.gameObject); 
    }

    // 캐릭터 생성
    public void new_Charater_Worrier(int level)
    {        
        if(c_Num < 3)
        {
            MyParty.Add(new Save.Player());
            MyParty[c_Num] = new Save.Player(Save.Worrier, e_Class.worrier);
            for(int i = 0; i < level; i++ ){
                MyParty[c_Num].LevelUp();
            }    
            c_Num++;            
        }
    }
    public void new_Charater_Magicion(int level)
    {        
        if(c_Num < 3)
        {
            MyParty.Add(new Save.Player());
            MyParty[c_Num] = new Save.Player(Save.Magicion,e_Class.magicion);
            for(int i = 0; i < level; i++ ){
                MyParty[c_Num].LevelUp();
            }        
            c_Num++;        
        }
    }
    public void new_Charater_Supporter(int level)
    {        
        if(c_Num < 3)
        {
            MyParty.Add(new Save.Player());
            MyParty[c_Num] = new Save.Player(Save.Supporter,e_Class.supporter);
            for(int i = 0; i < level; i++ ){
                MyParty[c_Num].LevelUp();
            }   
            c_Num++;             
        }
    }
    public void new_Charater_Assassin(int level)
    {        
        if(c_Num < 3)
        {
            MyParty.Add(new Save.Player());
            MyParty[c_Num] = new Save.Player(Save.Assassin,e_Class.assassin);
            for(int i = 0; i < level; i++ ){
                MyParty[c_Num].LevelUp();
            }       
            c_Num++;         
        }
        
    }
    
    // 캐릭터 제거
    public void Delete_Character()
    {
        if (MyParty.Count > 0)
        {

            c_Num--;
            MyParty.RemoveAt(c_Num);
        }
    }

    // 캐릭터 경험치 획득
    public void ExpUp(int num)
    {
        if(c_Num > num)
            MyParty[num].SetEXp(500);        
    }

}
