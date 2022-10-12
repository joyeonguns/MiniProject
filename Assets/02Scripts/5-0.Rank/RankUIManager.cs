using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;
using YU.Util;

public class RankUIManager : MonoBehaviour
{
    // 랭크 오브젝트
    public float MaxHight;
    public float MinHight;

    public float MouseSpeed = 200;

    public GameObject RankingObj;
    public GameObject ScrollView;
    public List<GameObject> RankText;


    // 새로고침
    public GameObject RefreshButton;
    bool isRefresh = true;
    float coolTime = 5.0f;

    // 기록 등록
    public GameObject RegistPannel;
    public TMP_InputField inpuField;
    public int Score;
    public TextMeshProUGUI scoreText;



    // Start is called before the first frame update
    void Start()
    {
        OnClickRefresh();
        scoreText.text = Score.ToString("D8");

        UISize uiSize = new UISize(){width = 1920, height = 1080};
        float h = uiSize.ChangedHeight(Screen.width, Screen.height, 700);
        ScrollView.GetComponent<RectTransform>().sizeDelta = new Vector2(1200, h);

        Destroy(HUDManager.instance.gameObject);
        Destroy(GameManager.instance.gameObject);

    }

    // Update is called once per frame
    void Update()
    {
        RankScroll();
    }

    void RankScroll()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel") * MouseSpeed;
        RectTransform RankPos = RankingObj.GetComponent<RectTransform>();

        if(RankPos.anchoredPosition.y > MaxHight)
        {
            RankPos.anchoredPosition = new Vector2(RankPos.anchoredPosition.x , MaxHight);
        }
        else if(RankPos.anchoredPosition.y < MinHight)
        {
            RankPos.anchoredPosition = new Vector2(RankPos.anchoredPosition.x , MinHight);
        }
        else
        {
            RankPos.anchoredPosition -= new Vector2(0,scroll);
        }
    }

    public void OnClickRefresh()
    {
        // 새로고침
        GetComponent<Take_Rank>().GetData();
        // 쿨다운
        StartCoroutine(RefreshCoolDown());
    }

    IEnumerator RefreshCoolDown()
    {
        isRefresh = false;
        RefreshButton.GetComponent<Button>().interactable = false;

        yield return new WaitForSeconds(coolTime);
        isRefresh = true;
        RefreshButton.GetComponent<Button>().interactable = true;
    }

    public void OnClickRegist()
    {
        Regex regex = new Regex(@"^[a-zA-Z]{3}");
        if(!regex.IsMatch(inpuField.text))
        {
            Debug.Log("영어만 입력 가능");
        }
        else
        {
            string names = inpuField.text.ToUpper();
            GetComponent<Take_Rank>().Register(Score, names);
            RegistPannel.SetActive(false);
        }

        inpuField.text = "";        
    }

    public void OnClickExit()
    {
        SceneManager.LoadScene(0);
    }


}
