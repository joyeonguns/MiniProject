using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.Pool;


public class D_fontScripts : MonoBehaviour
{
    public IObjectPool<GameObject> Pool;
    public GameObject DamageObj;
    // Start is called before the first frame update
    public AnimationCurve x_Anim;
    public AnimationCurve y_Anim;
    public float Duration = 2.0f;
    public float curTime = 0;
    float x_val;


    TextMeshProUGUI tmp;
    public string Damage;
    public bool bTrigger = false;
    public float WaitTime = 2.0f;

    RectTransform rect;
    TextMeshProUGUI tmpro;

    void Start()
    {
        tmp = GetComponent<TextMeshProUGUI>();
        tmp.text = "";        
        rect = GetComponent<RectTransform>();

        //StartEffect();
    }

    public void StartEffect()
    {
        tmp = GetComponent<TextMeshProUGUI>();
        tmp.text = "";        
        rect = GetComponent<RectTransform>();
        

        Invoke("SetActive_true", WaitTime);
        

        rect.localScale = new Vector3(1,1,1);
        tmp.color = new Color(tmp.color.r,tmp.color.g,tmp.color.b,1);
        rect.anchoredPosition = new Vector2(0,0);
        
        curTime = 0;
        

        //tmpro.DOFade(0,2f).SetDelay(3.0f);
        rect.DOScale(Vector3.zero, 1.5f).SetDelay(3.0f);

        x_val = Random.Range(-1.5f, 1.5f);

        Invoke("Release", 5.0f);
    }

    void SetActive_true()
    {
        Debug.Log("SetActive_true!!!");
        tmp.text = ""+Damage;
        bTrigger = true;
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {      
        if (bTrigger == true)
        {
            if(curTime > 0.5f)
            {
                tmp.color = tmp.color - new Color32(0, 0, 0, 15);
            }
            
            curTime += Time.deltaTime;

            var x = x_Anim.Evaluate(curTime) * x_val * 10;
            var y = y_Anim.Evaluate(curTime) * 20;

            GetComponent<RectTransform>().anchoredPosition = new Vector3(x, y, 0);
        }
            
        if (tmp.color.a <= 0)
        {
            // tmp.color = new Color32((byte)tmp.color.r, (byte)tmp.color.g, (byte)tmp.color.b, 255); ;
            bTrigger = false;
            //Release();
        }
    }

    public void SetPoolManaged(IObjectPool<GameObject> _pool)
    {
        Pool = _pool;
    }

    public void Release()
    {
        bTrigger = false;
        tmp.color = new Color32(0,0,0,0);

        if(DamageObj.activeInHierarchy == true)
            Pool.Release(DamageObj);
    }
}
