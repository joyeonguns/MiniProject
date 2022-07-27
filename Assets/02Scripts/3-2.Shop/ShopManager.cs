using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ShopManager : MonoBehaviour
{
    public GameObject TellentLoc;
    public GameObject ItemLoc;
    public GameObject SaleLoc;
    public GameObject SpwObj;

    // Start is called before the first frame update
    void Start()
    {
        SpawnObject();
    }

    void SpawnObject()
    {
        // Tellent Spw
        for(int i = 0; i < 6; i++)
        {
            int Price = 0;
            int num = 0;
            string Rank = "C";

            // C : 100 ~ 50, B : 50 ~ 20, A : 20 ~ 5, S : 5
            int rnd = Random.Range(1,101);
            if(rnd > 50)
            {   // C                
                Price = 100; num = Random.Range(0,10); Rank = "C";
            }
            else if(rnd > 20)
            {   // B                
                Price = 150; num = Random.Range(0,10); Rank = "B";
            }
            else if(rnd > 5)
            {   // A                
                Price = 220; num = Random.Range(0,10); Rank = "A";
            }
            else if(rnd > 0)
            {   // S                
                Price = 300; num = Random.Range(0,10); Rank = "S";
            }

            // Spw
            var spw_Tel = Instantiate(SpwObj);
            spw_Tel.transform.SetParent(TellentLoc.transform);
            spw_Tel.GetComponent<RectTransform>().anchoredPosition = new Vector2(250 * i, 0);

            spw_Tel.GetComponent<SellPrefabScripts>().bTellent = true;
            spw_Tel.GetComponent<SellPrefabScripts>().Price = Price;
            spw_Tel.GetComponent<SellPrefabScripts>().Code = num;
            spw_Tel.GetComponent<SellPrefabScripts>().TellentRank = Rank;

            if(i == 5)
            {
                spw_Tel.transform.SetParent(SaleLoc.transform);
                spw_Tel.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);

                spw_Tel.GetComponent<SellPrefabScripts>().bSale = true;                
            }
        }

        // Item Spw
        for(int i = 0; i < 5; i++)
        {
            int Price = 0;
            int num = 0;
            string Rank = "I";

            Price = Random.Range(50,100);            

            // Spw
            var spw_Tel = Instantiate(SpwObj);
            spw_Tel.transform.SetParent(ItemLoc.transform);
            spw_Tel.GetComponent<RectTransform>().anchoredPosition = new Vector2(250 * i, 0);

            spw_Tel.GetComponent<SellPrefabScripts>().bItem = true;
            spw_Tel.GetComponent<SellPrefabScripts>().Price = Price;
            spw_Tel.GetComponent<SellPrefabScripts>().Code = num;
            spw_Tel.GetComponent<SellPrefabScripts>().TellentRank = Rank;

            if(i == 4)
            {
                spw_Tel.transform.SetParent(SaleLoc.transform);
                spw_Tel.GetComponent<RectTransform>().anchoredPosition = new Vector2(350, 0);

                spw_Tel.GetComponent<SellPrefabScripts>().bSale = true;                
            }
        }
    }
    
    public void ExitBtn()
    {
        SceneManager.LoadScene("1-2.MapScene");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
