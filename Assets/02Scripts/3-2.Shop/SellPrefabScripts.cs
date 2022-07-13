using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SellPrefabScripts : MonoBehaviour
{
    public int Code;
    public string TellentRank;
    public int Price;
    public bool bTellent;
    public bool bItem;
    Button MyBtn;
    // Start is called before the first frame update
    void Start()
    {
        MyBtn = GetComponent<Button>();
        MyBtn.onClick.AddListener(OnClickBtn);

        if(Price > GameManager.instance.curGold)
        {
            MyBtn.interactable = false;
        }
    }

    void OnClickBtn()
    {    
        if(bTellent == true)
        {
            Debug.Log("Get Tellent");
            Debug.Log("code : "+ Code);
            Debug.Log("Rank : "+ TellentRank);
        }

        if(bItem == true)
        {
            Debug.Log("Get Item");
            Debug.Log("code : "+ Code);
        }

        GameManager.instance.curGold -= Price;
        HUDManager.instance.SetGold();
        
        Destroy(gameObject);
    }
}
