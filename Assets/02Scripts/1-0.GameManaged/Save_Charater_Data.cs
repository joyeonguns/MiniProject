using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Save_Charater_Data : MonoBehaviour
{
    public List<Save_Charater_Class.SD>  S_Character = new List<Save_Charater_Class.SD>();
    
    public int c_Num;


    // 캐릭터 생성
    public void new_Charater_Worrier(int level)
    {        
        if(c_Num < 3)
        {
            S_Character.Add(new Save_Charater_Class.SD());
            S_Character[c_Num] = new Save_Charater_Class.SD(Save_Charater_Class.Worrier,e_Class.worrier);
            for(int i = 0; i < level; i++ ){
                S_Character[c_Num].LevelUp();
            }    
            c_Num++;            
        }
    }
    public void new_Charater_Magicion(int level)
    {        
        if(c_Num < 3)
        {
            S_Character.Add(new Save_Charater_Class.SD());
            S_Character[c_Num] = new Save_Charater_Class.SD(Save_Charater_Class.Magicion,e_Class.magicion);
            for(int i = 0; i < level; i++ ){
                S_Character[c_Num].LevelUp();
            }        
            c_Num++;        
        }
    }
    public void new_Charater_Supporter(int level)
    {        
        if(c_Num < 3)
        {
            S_Character.Add(new Save_Charater_Class.SD());
            S_Character[c_Num] = new Save_Charater_Class.SD(Save_Charater_Class.Supporter,e_Class.supporter);
            for(int i = 0; i < level; i++ ){
                S_Character[c_Num].LevelUp();
            }   
            c_Num++;             
        }
    }
    public void new_Charater_Assassin(int level)
    {        
        if(c_Num < 3)
        {
            S_Character.Add(new Save_Charater_Class.SD());
            S_Character[c_Num] = new Save_Charater_Class.SD(Save_Charater_Class.Assassin,e_Class.assassin);
            for(int i = 0; i < level; i++ ){
                S_Character[c_Num].LevelUp();
            }       
            c_Num++;         
        }
        
    }
    
    // 캐릭터 제거
    public void Delete_Character()
    {
        if (S_Character.Count > 0)
        {

            c_Num--;
            S_Character.RemoveAt(c_Num);
        }
    }

    // 캐릭터 경험치 획득
    public void ExpUp(int num)
    {
        if(c_Num > num)
            S_Character[num].SetEXp(500);        
    }

}
