using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TellentCardUI : MonoBehaviour
{
    public GameObject TellentCard;
    public GameObject CardPreFabs;
    public GameObject SpwLocation;
    public GameObject BigCard;

    public Sprite[] CardSprite;
    public Vector2 NextSpwLoc = new Vector2(0,0); 
    
    int CardNum = 0;
    


    void Start() 
    {
        BigCard.SetActive(false);

        foreach(var telList in GameManager.instance.Tellents)
        {
            foreach(var tel in telList)
            {
                SpawnTellentCard(tel);
            }
        }
    }

    public void SpawnTellentCard(TellentsScripts Tellent)
    {
        CardNum++;
        var spwCard = Instantiate(CardPreFabs);
        spwCard.transform.SetParent(SpwLocation.transform);
        spwCard.GetComponent<Image>().sprite = CardSprite[(int)(Tellent.Rank)];
        
        TellentDataSO TellSO = SOManager.GetTellent();

        List<TellentData> targetTell = new List<TellentData>();
        switch (Tellent.Rank)
        {
            case Etel_Rank.C :
            targetTell = TellSO.tellentData_C;
            break;

            case Etel_Rank.B :
            targetTell = TellSO.tellentData_B;
            break;

            case Etel_Rank.A :
            targetTell = TellSO.tellentData_A;
            break;

            case Etel_Rank.S:
            targetTell = TellSO.tellentData_S;
            break;

            default:
            Debug.LogError("Tellent Rank Over");
            break;
        }

        spwCard.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = targetTell[Tellent.Code].Name;
        spwCard.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = targetTell[Tellent.Code].Contents;
        spwCard.GetComponent<RectTransform>().anchoredPosition = NextSpwLoc;
        spwCard.GetComponent<Button>().onClick.AddListener(() => ClickCard(spwCard));
        if(CardNum % 5 == 0)
        {
            NextSpwLoc = new Vector2(0, NextSpwLoc.y - 400);
        }
        else{
            NextSpwLoc = new Vector2( NextSpwLoc.x + 300, NextSpwLoc.y);
        }
    }

    public void ClickCard(GameObject Card)
    {
        BigCard.GetComponent<Image>().sprite = Card.GetComponent<Image>().sprite;
        BigCard.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = Card.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text;
        BigCard.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = Card.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text;
        BigCard.SetActive(true);
    }

    public void ClickRelease()
    {
        BigCard.SetActive(false);
    }
}
