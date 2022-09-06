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

    public GameObject SpwTellent;
    public Sprite[] TellentSprite;

    public GameObject SpwItem;

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
            Etel_Rank Rank = Etel_Rank.C;

            // C : 100 ~ 50, B : 50 ~ 20, A : 20 ~ 5, S : 5
            int rnd = Random.Range(1,101);
            if(rnd > 50)
            {   // C                
                Price = 100; num = Random.Range(0,11); Rank = Etel_Rank.C;
            }
            else if(rnd > 20)
            {   // B                
                Price = 150; num = Random.Range(0,18); Rank = Etel_Rank.B;
            }
            else if(rnd > 5)
            {   // A                
                Price = 220; num = Random.Range(0,5); Rank = Etel_Rank.A;
            }
            else if(rnd > 0)
            {   // S                
                Price = 300; num = Random.Range(0,2); Rank = Etel_Rank.S;
            }

            // Spw
            TellentsScripts tellent = new TellentsScripts(Rank,num);
            var spw_Tel = Instantiate(SpwTellent);
            spw_Tel.transform.SetParent(TellentLoc.transform);
            spw_Tel.GetComponent<Image>().sprite = TellentSprite[(int)Rank];
            spw_Tel.GetComponent<RectTransform>().anchoredPosition = new Vector2(250 * i, 0);

            spw_Tel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = tellent.name;
            spw_Tel.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text ="[ " + tellent.name + " ]";
            spw_Tel.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = ""+Price;
            spw_Tel.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text ="";      

            // sale
            if(i == 5)
            {
                spw_Tel.transform.SetParent(SaleLoc.transform);
                spw_Tel.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);

                spw_Tel.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "<s>" + Price + "</s>";
                spw_Tel.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "<i><color=red>" + (Price/2) + "</color></i>";     
                spw_Tel.GetComponent<Button>().onClick.AddListener(() => BuyTellent(Price/2,tellent,spw_Tel));
            }
            else
            {
                spw_Tel.GetComponent<Button>().onClick.AddListener(() => BuyTellent(Price,tellent,spw_Tel));
            }
        }

        // Item Spw
        for(int i = 0; i < 5; i++)
        {
            int Price = 0;
            int num = 0;

            Price = Random.Range(50,100);            
            num = Random.Range(1,9);

            ItemClass item = new ItemClass(num);
            // Spw
            var spw_Item = Instantiate(SpwItem);
            spw_Item.transform.SetParent(ItemLoc.transform);
            spw_Item.GetComponent<RectTransform>().anchoredPosition = new Vector2(250 * i, 0);

            spw_Item.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "";//item.ItemName;
            spw_Item.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = ""+Price;
            spw_Item.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "";
            
            if(i == 4)
            {
                spw_Item.transform.SetParent(SaleLoc.transform);
                spw_Item.GetComponent<RectTransform>().anchoredPosition = new Vector2(350, 0);

                spw_Item.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "<s>"+Price + "</s>";
                spw_Item.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "<i><color=red>" + (Price/2) + "</color></i>";
                spw_Item.GetComponent<Button>().onClick.AddListener(() => BuyItem(Price/2, item, spw_Item));
            }
            else
            {
                spw_Item.GetComponent<Button>().onClick.AddListener(() => BuyItem(Price, item, spw_Item));
            }
        }
    }


    public void BuyTellent(int price, TellentsScripts tell, GameObject telObj)
    {
        if(GameManager.instance.curGold >= price)
        {
            telObj.SetActive(false);
            GameManager.instance.curGold -= price;
            GameManager.instance.Tellents[(int)tell.Rank].Add(tell);
            HUDManager.instance.SetAll();
            HUDManager.instance.GetComponent<TellentCardUI>().SpawnTellentCard(tell);
        }
    }

    public void BuyItem(int price, ItemClass item, GameObject telObj)
    {
        if(GameManager.instance.curGold >= price)
        {            
            for (int i = 0; i < 3; i++)
            {
                if(GameManager.instance.ItemList_num[i] == 0)
                {
                    GameManager.instance.ItemList_num[i] = item.ItemCode;
                    telObj.SetActive(false);
                    GameManager.instance.curGold -= price;
                    break;
                }
            }
            HUDManager.instance.SetAll();
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
