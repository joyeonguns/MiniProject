using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;


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

       RectTransform rect = GetComponent<RectTransform>();
       TextMeshProUGUI tmpro = GetComponent<TextMeshProUGUI>();

        transform.DOMoveY(-1,2.0f).SetDelay(2.7f);
        tmpro.DOFade(0,2f).SetDelay(3.0f);
        rect.DOScale(Vector3.zero, 2).SetDelay(3.0f);
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
