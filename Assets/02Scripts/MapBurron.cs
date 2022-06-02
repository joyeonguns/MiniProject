using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MapBurron : MonoBehaviour
{
    public TextMeshProUGUI name;
    GameManager GM;

    public int x = 1,y = 2;

    void Awake()
    {
       GameObject GO = GameObject.Find("GameManager");
       GM = GO.GetComponent<GameManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
      SetText(x,y);
      this.GetComponent<Button>().interactable = false;        
    }

    // Update is called once per frame
    void Update()
    {      
      // 버튼 활성화
      if(GM.cur_Map.floor == x-1)
      {
         // 왼쪽
         if(GM.cur_Map.roots[0] == y || GM.cur_Map.roots[1] == y || GM.cur_Map.roots[2] == y)
         {
            this.GetComponent<Button>().interactable = true;
         }   
         else if(x == 1)
         {
            this.GetComponent<Button>().interactable = true;
         }
         else
         {
            this.GetComponent<Button>().interactable = false;
         } 
      }
      else
      {
         this.GetComponent<Button>().interactable = false;
      } 
    }

    public void SetText(int a, int b)
    {
       x = a;
       y = b;
       name.text = "" + x + " , " + y;
    }

    public void ClickBTN()
    {
       Debug.Log("set : " + x + " " +y);
       GM.SetCurMap(x-1,y);       
       
    }
}
