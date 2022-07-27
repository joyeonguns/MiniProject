using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldPreFabScripts : MonoBehaviour
{
    Button MyBtn;
    // Start is called before the first frame update
    void Start()
    {
        MyBtn = GetComponent<Button>();

        MyBtn.onClick.AddListener(OnClickBtn);
    }

    public void OnClickBtn()
    {
        RewardManager RM = GameObject.Find("RewardManager").GetComponent<RewardManager>();
        RM.SetGoldBtn();
        Destroy(gameObject);
    }
}
