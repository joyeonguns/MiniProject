using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class D_fontScripts : MonoBehaviour
{
    // Start is called before the first frame update
    TextMeshProUGUI tmp;
    void Start()
    {
       tmp  = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        tmp.color = tmp.color - new Color32(0,0,0,2);
        if(tmp.color.a <= 0)
        {
           tmp.color = new Color32((byte)tmp.color.r,(byte)tmp.color.g,(byte)tmp.color.b,255);;
           Destroy(gameObject);
        }
    }
}
