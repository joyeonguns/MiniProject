using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    // 데미지
    public int i_bDamage;
    // 방어력
    public int i_bArmor;
    // 속도
    public int i_bSpeed;
    // 회피율
    public int i_bDodge;
    // 치명타
    public int i_bCritical;
    // 최대 체력
    public int i_MaxHp;
    // 현재 체력
    public int i_CurHp;
    // 생존
    public bool bAlive;


    
    // 기본 공격
    public void NomalAttack()
    {
        
    }
    // 첫번째 스킬
    public void FirstSkill()
    {
        
    }
    // 두번째 스킬
    public void SecondSkill()
    {
        
    }
    // 궁극기
    public void Ultimate()
    {
        
    }

    // 데미지 받기
    public void TakeDamage(int _Damage, GameObject other)
    {

    }

    public void Dead()
    {

    }
    public void BattlePage()
    {
        
    }
}
