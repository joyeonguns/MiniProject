using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testPlayer : ScriptableObject
{
    public List<Attribute> list = new List<Attribute>();

    [System.Serializable]
    public class Attribute
    {
        public int level, maxHP,	baseAttack,	requireNextLevelExp,	moveSpeed,	turnSpeed;	
        public float attackRange;

    }
}
