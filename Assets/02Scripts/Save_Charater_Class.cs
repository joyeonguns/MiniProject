using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum e_Class {worrier, magicion, supporter, assassin};

public class Save_Charater_Class : MonoBehaviour
{
    [System.Serializable]
    public struct Class_Status
    {
        public float s_Damage;
        // 방어력
        public float s_Armor;
        // 속도
        public int s_Speed;
        // 회피율
        public int s_Dodge;
        // 치명타
        public int s_Critical;
        // 최대 체력
        public int s_MaxHp;

        public Class_Status(int _s_Damage,float _s_Armor,int _s_Speed,int _s_Dodge,int _s_Critical,int _s_MaxHp)
        {
            s_Damage = _s_Damage;
            s_Armor = _s_Armor;
            s_Speed = _s_Speed;
            s_Dodge = _s_Dodge;
            s_Critical = _s_Critical;
            s_MaxHp = _s_MaxHp;      
        }
        public void LevelUp()
        {
            s_Damage = (float)(s_Damage *1.2f);
            s_MaxHp = (int)(s_MaxHp * 1.2f);
            s_Dodge = (int)(s_Dodge * 1.2f);
            s_Critical = (int)(s_Critical * 1.2f);
        }
    }
    
    [System.Serializable]
    public class SD
    {
        // 이름
        public string c_Name;
        // 클래스
        public e_Class c_Class;
        // 클래스 스텟
        public Class_Status class_status;       
        // 현재 체력
        public int s_CurHp;
        // 생존
        public bool bAlive;
        // 레벨
        public int level;
        
        // 생성자 오버로딩
        delegate string _Name();
        public SD(){}
        public SD(Class_Status _class, e_Class ec)
        {
            _Name _name = () => 
            {   int rand = UnityEngine.Random.Range(0,10);
                return ""+ Enum.GetName(typeof(e_Class),ec)+"_" + rand;
            };
            
            c_Name = _name();
            c_Class = ec;
            class_status = _class;
            s_CurHp = class_status.s_MaxHp;
            bAlive = true;      
            level = 0; 
        }
    }

    public static Class_Status Worrier = new Class_Status(10,0.5f,5,40,70,40);
    public static Class_Status Magicion = new Class_Status(14,0.1f,2,30,70,25);    
    public static Class_Status Supporter = new Class_Status(5,0.3f,1,30,50,30);
    public static Class_Status Assassin = new Class_Status(7,0.2f,7,70,100,27);    

}
