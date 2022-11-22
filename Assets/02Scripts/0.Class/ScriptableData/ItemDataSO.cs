using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemData
{
    public int ItemCode;
    public string ItemName;
    public string ItemComments;
    public Sprite ItemImage;
}

[CreateAssetMenu (menuName = "Scriptable Object/Item Datas", order = int.MaxValue)]
public class ItemDataSO : ScriptableObject
{
    public ItemData[] itemDatas;
}


