using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeLevelData : ScriptableObject
{
    public List<Attribute> list = new List<Attribute>();

    [System.Serializable]
    public class Attribute
    {
        public int level, maxHP,	attack,	defence,	gainExp,	moveSpeed,	turnSpeed,	attackRange,	gainGold;
	

    }
}
