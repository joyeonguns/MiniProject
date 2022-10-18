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
        float BGvalue;
        bool BGresult = mixer.GetFloat("BGSound", out BGvalue);
        if(BGresult)
        {
            Debug.Log("BGvalue  :  " + Mathf.Pow(2,BGvalue) );
            BGM.value = Mathf.Pow(2,BGvalue);
        }
        else
        {
            Debug.Log("not Exist BGSound");
            BGM.value = 0;
        }

        float Effectvalue;
        bool Effectresult = mixer.GetFloat("SFXSound", out Effectvalue);
        if(Effectresult)
        {
            Effect.value =  Mathf.Pow(2,Effectvalue);
        }
        else
        {
            Effect.value = 0;
        }
        
    }

    public void BGMSoundVolume()
    {
        mixer.SetFloat("BGSound",Mathf.Log(BGM.value) * 20);
    }

    public void EffectSoundVolume()
    {
        mixer.SetFloat("SFXSound",Mathf.Log(Effect.value) * 20);
    }

    public void OnClickMain()
    {
        StartCoroutine(ToMain());        
    }

    IEnumerator ToMain()
    {
        SaveDataManager.instance.SaveData();
        yield return new WaitForSeconds(1.0f);
        Destroy(this.gameObject);
        SceneManager.LoadScene(0);
    }

    public void OnClickGame()
    {
        Setting.SetActive(false);
    }
}
