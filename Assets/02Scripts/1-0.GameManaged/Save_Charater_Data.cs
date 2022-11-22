using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Save_Charater_Data : MonoBehaviour
{

    // public static Save_Charater_Data instance = null;   
    
    public int c_Num;

    public List<Player> My = new List<Player>();
    public CharacterSO CharSO;
    
    private void Awake() 
    {
        // instance = this;
        //cur_Map = new MapClass();
        // DontDestroyOnLoad(this.gameObject); 

    }

    private void Start()
    {
        My = GameManager.instance.MyParty;
        CharSO = SOManager.instance.CharSO;


    }

    // 캐릭터 생성
    public void new_Charater_Worrier(int level)
    {        
        if(c_Num < 3)
        {
            My.Add(new Player());
            Status worrier = new Status(CharSO.CharDatas[1]);
            My[c_Num] = new Player(worrier, e_Class.Warrior);
            for(int i = 0; i < level; i++ ){
                My[c_Num].LevelUp();
            }    
            c_Num++;            
        }
    }
    public void new_Charater_Magicion(int level)
    {        
        if(c_Num < 3)
        {
            My.Add(new Player());
            Status magicion = new Status(CharSO.CharDatas[2]);
            My[c_Num] = new Player(magicion,e_Class.Magicion);
            for(int i = 0; i < level; i++ ){
                My[c_Num].LevelUp();
            }        
            c_Num++;        
        }
    }
    public void new_Charater_Supporter(int level)
    {        
        if(c_Num < 3)
        {
            My.Add(new Player());
            Status supporter = new Status(CharSO.CharDatas[3]);
            My[c_Num] = new Player(supporter,e_Class.Supporter);

            // MyParty[c_Num] = new Player(Supporter,e_Class.Supporter);

            for(int i = 0; i < level; i++ ){
                My[c_Num].LevelUp();
            }   
            My[c_Num].Main = true;
            c_Num++;             
        }
    }
    public void new_Charater_Assassin(int level)
    {        
        if(c_Num < 3)
        {
            My.Add(new Player());
            Status assassin = new Status(CharSO.CharDatas[4]);
            My[c_Num] = new Player(assassin,e_Class.Assassin);
            for(int i = 0; i < level; i++ ){
                My[c_Num].LevelUp();
            }       
            c_Num++;         
        }
        
    }
    
    // 캐릭터 제거
    public void Delete_Character()
    {
        if (My.Count > 0)
        {
            c_Num--;
            My.RemoveAt(c_Num);
        }
    }

    // 캐릭터 경험치 획득
    public void ExpUp(int num)
    {
        if(c_Num > num)
            My[num].SetEXp(500);        
    }

}
