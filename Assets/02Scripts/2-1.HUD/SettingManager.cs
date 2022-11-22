using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class SettingManager : MonoBehaviour
{
    public GameObject Setting;
    public AudioMixer mixer;

    public Slider BGM;
    public Slider Effect;

    public int resolution;

    float BGvalue;
    float Effectvalue;

    public TMP_Dropdown ResolutionTap;

    private void Awake() {
        //ResolutionTap.onValueChanged.AddListener+=
    }
    // Start is called before the first frame update
    void Start()
    {
        OpenSetting();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    public void OpenSetting()
    {
        ResolutionTap.SetValueWithoutNotify(SaveDataManager.instance.playerSetting.resolution);
        print(SaveDataManager.instance.playerSetting.resolution);

        bool BGresult = mixer.GetFloat("BGSound", out BGvalue);
        if(BGresult)
        {
            Debug.Log("BGvalue  :  " + (float)Mathf.Pow(10,BGvalue / 20) );
            BGM.value = (float)Mathf.Pow(10,BGvalue  / 20 );
        }
        else
        {
            Debug.Log("not Exist BGSound");
            BGM.value = 0;
        }

        
        bool Effectresult = mixer.GetFloat("SFXSound", out Effectvalue);
        if(Effectresult)
        {
            Debug.Log("Effect  :  " + Effectvalue );
            Effect.value =  (float)Mathf.Pow(10,Effectvalue / 20) ;
        }
        else
        {
            Debug.Log("not Exist Effect");
            Effect.value = 0;
        }
        
    }

    public void BGMSoundVolume()
    {
        mixer.SetFloat("BGSound",Mathf.Log(BGM.value, 10F) * 20);
    }

    public void EffectSoundVolume()
    {
        mixer.SetFloat("SFXSound",Mathf.Log(Effect.value, 10f) * 20);
    }

    public void OnClickMain()
    {        
        SettingSave();
        StartCoroutine(ToMain());        
    }

    IEnumerator ToMain()
    {
        SaveDataManager.instance.SaveData();
        yield return new WaitForSeconds(1.0f);
        Destroy(this.gameObject);
        Destroy(GameManager.instance.gameObject);
        SceneManager.LoadScene(0);
    }

    public void OnClickGame()
    {
        SettingSave();
        Setting.SetActive(false);
    }


    public void OnChangeDropDownMenu()
    {

    }
    public void SettingSave()
    {
        SaveDataManager.instance.playerSetting.resolution = ResolutionTap.value;
        SaveDataManager.instance.playerSetting.BGMSize = BGM.value;
        SaveDataManager.instance.playerSetting.EffectSize = Effect.value;
        
        ConstData constData = new ConstData();
        Screen.SetResolution(constData.Resolution[ResolutionTap.value].Item1, constData.Resolution[ResolutionTap.value].Item2,false);
        //print($"w : {}")

        SaveDataManager.instance.SaveSetting();        
    }


    public void ChangeResolution(int idx)
    {
        ResolutionTap.SetValueWithoutNotify(idx);
    }

    public void OnClickOpen()
    {
        Setting.SetActive(true);
        OpenSetting();
    }
}
