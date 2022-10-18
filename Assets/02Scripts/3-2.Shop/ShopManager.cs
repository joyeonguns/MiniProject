using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ShopManager : MonoBehaviour
{
    public GameObject Contents;
    
    public GameObject SpwTellent;
    public Sprite[] TellentSprite;

    public GameObject SpwItem;

    int tellentCount;
    int ItemCount;

    List<int> price_tel = new List<int>();
    List<int> price_item = new List<int>();

    // Start is called before the first frame update
    void Start()
    {
        tellentCount = Random.Range(0,3) + 4;
        ItemCount = Random.Range(0,3) + 4;

        SpawnObject();
    }

    void SpawnObject()
    {
        // Tellent Spw
        for(int i = 0; i < tellentCount; i++)
        {
            int Price = 0;
            int num = 0;
            Etel_Rank Rank = Etel_Rank.C;

            // C : 100 ~ 50, B : 50 ~ 10, A : 10 ~ 3, S : 3
            int rnd = Random.Range(1,101);
            if(rnd > 50)
            {   // C                
                Price = Random.Range(70,120); 
                num = Random.Range(0,11); Rank = Etel_Rank.C;
            }
            else if(rnd > 10)
            {   // B                
                Price = Random.Range(120,175);; 
                num = Random.Range(0,18); Rank = Etel_Rank.B;
            }
            else if(rnd > 3)
            {   // A                
                Price = Random.Range(180,270);; 
                num = Random.Range(0,5); Rank = Etel_Rank.A;
            }
            else if(rnd > 0)
            {   // S                
                Price = Random.Range(290,330);; 
                num = Random.Range(0,2); Rank = Etel_Rank.S;
            }


            // Spw
            TellentsScripts tellent = new TellentsScripts(Rank,num);
            var spw_Tel = Instantiate(SpwTellent);
            spw_Tel.transform.SetParent(Contents.transform);
            var tel_Image = spw_Tel.transform.GetChild(2).GetComponent<Image>();
            var tel_Name = spw_Tel.transform.GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>();
            var tel_Price = spw_Tel.transform.GetChild(4).GetChild(1).GetComponent<TextMeshProUGUI>();

            tel_Image.sprite = TellentSprite[(int)Rank];      
            tel_Name.text = tellent.telData.Name;

            // sale
            int SaleRnd = Random.Range(0,100); 
            bool isSale = (SaleRnd <= 15) ? true : false;

            if(isSale)
            {
                Price /= 2;
                price_tel.Add(Price);
                tel_Price.text = ""+Price;
                spw_Tel.transform.GetChild(5).gameObject.SetActive(true);
            }
            else
            {
                price_tel.Add(Price);
                tel_Price.text = ""+Price;
                spw_Tel.transform.GetChild(5).gameObject.SetActive(false);
            }   

            // 버튼 적용
            var btn = spw_Tel.transform.GetChild(4).GetChild(0).GetComponent<Button>();
            btn.onClick.AddListener(() => BuyTellent( Price,tellent, spw_Tel) );        

            Contents.GetComponent<RectTransform>().offsetMax += new Vector2(400,0);
        }

        // Item Spw
        for(int i = 0; i < ItemCount; i++)
        {
            int Price = 0;
            int num = 0;

            Price = Random.Range(50,100);            
            num = Random.Range(1,9);

            ItemClass item = new ItemClass(num);
            // Spw
            var spw_Item = Instantiate(SpwItem);
            spw_Item.transform.SetParent(Contents.transform);
            var item_Image = spw_Item.transform.GetChild(2).GetComponent<Image>();
            var item_Name = spw_Item.transform.GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>();
            var item_Price = spw_Item.transform.GetChild(4).GetChild(1).GetComponent<TextMeshProUGUI>();
            
            
            // item_Image.sprite = TellentSprite[(int)Rank];      
            item_Name.text = item.ItemName;

            // sale
            int SaleRnd = Random.Range(0,100); 
            bool isSale = (SaleRnd <= 15) ? true : false;

            if(isSale)
            {
                price_item.Add(Price / 2);
                item_Price.text = ""+Price/2;
                spw_Item.transform.GetChild(5).gameObject.SetActive(true);
            }
            else
            {
                price_item.Add(Price);
                item_Price.text = ""+Price;
                spw_Item.transform.GetChild(5).gameObject.SetActive(false);
            } 

            // 버튼 적용
            var btn = spw_Item.transform.GetChild(4).GetChild(0).GetComponent<Button>();
            btn.onClick.AddListener(() => BuyItem( Price, item, spw_Item) );  

            Contents.GetComponent<RectTransform>().offsetMax += new Vector2(400,0);
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

            Contents.GetComponent<RectTransform>().offsetMax -= new Vector2(400,0);
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

                    Contents.GetComponent<RectTransform>().offsetMax -= new Vector2(400,0);
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
