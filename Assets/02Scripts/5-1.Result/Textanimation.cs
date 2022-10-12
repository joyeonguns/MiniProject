using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Textanimation : MonoBehaviour
{
    public TextMeshProUGUI Tmp;
    public string Score = "1446482";

    public char[] newScore;
    public float times;

    // Start is called before the first frame update
    void Start()
    {
        newScore = new char[] {'0','0','0','0','0','0'};

        StartCoroutine(SetText_1(0));
    }

    IEnumerator SetText_1(int i)
    {      
        if (times >= 1.5)
        {
            newScore[i] = Score[i];
            i++;
            times = 0;
        }
        else
        {
            int rnd = Random.Range(0,10);
            string s = ""+rnd;
            newScore[i] = s[0];
        }

        string str = new string(newScore);

        Tmp.text = str;

        yield return new WaitForSeconds(0.05f);

        times += 0.1f;

        if(i < 5)
        {
            StartCoroutine(SetText_1(i));
        }       
       
    }

}
