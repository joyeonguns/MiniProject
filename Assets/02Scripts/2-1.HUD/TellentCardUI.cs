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

        SpawnTellentCard(GameManager.instance.Tellents[0][0]);
        SpawnTellentCard(GameManager.instance.Tellents[0][1]);
        SpawnTellentCard(GameManager.instance.Tellents[1][0]);
        SpawnTellentCard(GameManager.instance.Tellents[2][0]);
    }

    public void SpawnTellentCard(TellentsScripts Tellent)
    {
        Debug.Log("Rand : " + Tellent.Rank);
        CardNum++;
        var spwCard = Instantiate(CardPreFabs);
        spwCard.transform.SetParent(SpwLocation.transform);
        spwCard.GetComponent<Image>().sprite = CardSprite[(int)(Tellent.Rank)];
        spwCard.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = Tellent.name;
        spwCard.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "[ " + Tellent.name +" ]";
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
