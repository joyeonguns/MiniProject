using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveDataManager : MonoBehaviour
{
    public static SaveDataManager instance;

    public SaveData nowSave;
    public string path;
    private void Awake() 
    {
        #region 싱글톤
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance .gameObject);
        }

        DontDestroyOnLoad(this.gameObject); 
        #endregion
    
        path = Application.persistentDataPath + "/Save";
    }

    public void SaveData()
    {
        nowSave.Saves();
        
        string jsonData = JsonUtility.ToJson(nowSave);
        
        File.WriteAllText(path,jsonData);
    }

    public void LoadData()
    {
        string jsonData = File.ReadAllText(path);

        nowSave = JsonUtility.FromJson<SaveData>(jsonData);
    }

    public void newStart()
    {
        if (File.Exists(path))
        {
            
            LoadData();
        }
    }
    public void reStart()
    {

    }
}
