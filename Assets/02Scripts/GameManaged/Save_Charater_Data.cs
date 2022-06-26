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
                S_Character[c_Num].level++;
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
                S_Character[c_Num].level++;
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
                S_Character[c_Num].level++;
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
                S_Character[c_Num].level++;
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

    public void Ex_sk(int n)
    {
        Debug.Log(n+ " 번째 스킬 사용");
    }
    public void Ex_sk2(Save_Charater_Class.SD _this, int n)
    {
        Debug.Log(n+ " 번째 스킬 사용");
        
        Debug.Log(" 체력 : "+_this.Hp);
    }

    public void Attack(int n)
    {
        S_Character[1].nomalAttack(S_Character[n]);
    }

    public void Use_Skill(int n)
    {
        //S_Character[2].MySkill[3].UseSkill(S_Character,2,S_Character,1);
    }
    public void SetSkill(int k)
    {

    }

    //public Button btn;

    private void Start()
    {
        //btn.onClick.AddListener(() => {Debug.Log("버튼 클릭");});
    }
}
