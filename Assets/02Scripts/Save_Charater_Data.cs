using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save_Charater_Data : MonoBehaviour
{
    public Save_Charater_Class.SD[] S_Character = new Save_Charater_Class.SD[3];
    
    public int c_Num;

    public void new_Charater_Worrier(int level)
    {        
        if(c_Num < 3)
        {
            S_Character[c_Num] = new Save_Charater_Class.SD(Save_Charater_Class.Worrier,e_Class.worrier);
            for(int i = 0; i < level; i++ ){
                S_Character[c_Num].level++;
                S_Character[c_Num].class_status.LevelUp();
            }    
            c_Num++;            
        }
    }
    public void new_Charater_Magicion(int level)
    {        
        if(c_Num < 3)
        {
            S_Character[c_Num] = new Save_Charater_Class.SD(Save_Charater_Class.Magicion,e_Class.magicion);
            for(int i = 0; i < level; i++ ){
                S_Character[c_Num].level++;
                S_Character[c_Num].class_status.LevelUp();
            }        
            c_Num++;        
        }
    }
    public void new_Charater_Supporter(int level)
    {        
        if(c_Num < 3)
        {
            S_Character[c_Num] = new Save_Charater_Class.SD(Save_Charater_Class.Supporter,e_Class.supporter);
            for(int i = 0; i < level; i++ ){
                S_Character[c_Num].level++;
                S_Character[c_Num].class_status.LevelUp();
            }   
            c_Num++;             
        }
    }
    public void new_Charater_Assassin(int level)
    {        
        if(c_Num < 3)
        {
            S_Character[c_Num] = new Save_Charater_Class.SD(Save_Charater_Class.Assassin,e_Class.assassin);
            for(int i = 0; i < level; i++ ){
                S_Character[c_Num].level++;
                S_Character[c_Num].class_status.LevelUp();
            }       
            c_Num++;         
        }
        
    }
    
    public void Delete_Character()
    {
        c_Num--;
        S_Character[c_Num] = new Save_Charater_Class.SD();
    }
}
