using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class D_fontScripts : MonoBehaviour
{
    // Start is called before the first frame update
    TextMeshProUGUI tmp;
    public string Damage;
    bool bTrigger = false;
    public float WaitTime;
    void Start()
    {
       tmp  = GetComponent<TextMeshProUGUI>();
       tmp.text = "";
       Invoke("SetActive_true",WaitTime);
    }

    void SetActive_true()
    {
        tmp.text = ""+Damage;
        bTrigger = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(bTrigger == true)
            tmp.color = tmp.color - new Color32(0,0,0,2);
        if(tmp.color.a <= 0 )
        {
           tmp.color = new Color32((byte)tmp.color.r,(byte)tmp.color.g,(byte)tmp.color.b,255);;
           bTrigger = false;
           Destroy(gameObject);
        }
    }
}
