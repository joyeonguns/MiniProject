using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveDataManager : MonoBehaviour
{
    public static SaveDataManager instance;


    public SavePlayerSetting playerSetting;
    public SaveData nowSave;
    public string path;
    public string playerSetting_path;
    private void Awake() 
    {
        #region 싱글톤
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            // Destroy(instance .gameObject);
        }

        // DontDestroyOnLoad(this.gameObject); 
        #endregion
    
        path = Application.persistentDataPath + "/Save";
        playerSetting_path = Application.persistentDataPath + "/plSetting";
    }

    void Start() 
    {
        playerSetting.BGMSize = 1.0f;
        playerSetting.EffectSize = 1.0f;
        playerSetting.resolution = 0;

        LoadSetting();
        GameManager.instance.GetComponent<SoundManager>().SetAudio(playerSetting.BGMSize, playerSetting.EffectSize);

        ConstData constData = new ConstData();
        Screen.SetResolution(constData.Resolution[playerSetting.resolution].Item1, constData.Resolution[playerSetting.resolution].Item2, false);     
    }

    public void SaveSetting()
    {
        //playerSetting = _playerSetting;
        string json =JsonUtility.ToJson(playerSetting);

        if (File.Exists(playerSetting_path))
        {
            System.IO.File.Delete(playerSetting_path);            
            StartCoroutine(WaitDelete_Setting(playerSetting_path));
        }
        else
        {
            saveSetting(playerSetting_path);
        }
    }

    public void SaveData()
    {
        //nowSave = new SaveData();
        nowSave.Saves();        
        string jsonData = JsonUtility.ToJson(nowSave);

        if (File.Exists(path))
        {
            System.IO.File.Delete(path);            
            StartCoroutine(WaitDelete(path));
        }
        else
        {
            savetest(path);
        }
        
        // Invoke("savetest", 1.5f);
        
    }

    IEnumerator WaitDelete(string _path)
    {     
        yield return null;
        if(File.Exists(_path))
        {
            StartCoroutine(WaitDelete(_path));
        }
        else
        {
            File.WriteAllText(_path, JsonUtility.ToJson(nowSave));
            Debug.Log("Save!!");
        }
    }

    IEnumerator WaitDelete_Setting(string _path)
    {     
        yield return null;
        if(File.Exists(_path))
        {
            StartCoroutine(WaitDelete_Setting(_path));
        }
        else
        {
            File.WriteAllText(_path, JsonUtility.ToJson(playerSetting));
            Debug.Log("Save!!");
        }
    }

    void savetest(string _path)
    {
        File.WriteAllText(_path, JsonUtility.ToJson(nowSave));
        Debug.Log("Save!!");
    }

    void saveSetting(string _path)
    {
        File.WriteAllText(_path, JsonUtility.ToJson(playerSetting));
        Debug.Log("Save!!");
    }

    public void LoadData()
    {
        if(File.Exists(path))
        {
            string jsonData = File.ReadAllText(path);

            nowSave = JsonUtility.FromJson<SaveData>(jsonData);
        }
    }
    public void LoadSetting()
    {
        if (File.Exists(playerSetting_path))
        {
            string jsonData = File.ReadAllText(playerSetting_path);

            playerSetting = JsonUtility.FromJson<SavePlayerSetting>(jsonData);
        }
    }

    public void newStart()
    {
        if (File.Exists(path))
        {
            
            LoadData();
        }
    }
    public void DeleteSave()
    {
        if (File.Exists(path))
        {
            System.IO.File.Delete(path);
        }
    }
}
