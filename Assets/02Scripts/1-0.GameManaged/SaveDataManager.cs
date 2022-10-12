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
        //nowSave = new SaveData();
        nowSave.Saves();        
        string jsonData = JsonUtility.ToJson(nowSave);

        if (File.Exists(path))
        {
            System.IO.File.Delete(path);            
            // StartCoroutine(WaitDelete());
        }
        else
        {
            
        }
        
        Invoke("savetest", 1.5f);
        
    }

    IEnumerator WaitDelete()
    {     
        yield return null;
        if(File.Exists(path))
        {
            StartCoroutine(WaitDelete());
        }
        else
        {
            Debug.Log("Delete File");
            File.WriteAllText(path, JsonUtility.ToJson(nowSave));
            Debug.Log("Save!!");
        }
    }

    void savetest()
    {
        File.WriteAllText(path, JsonUtility.ToJson(nowSave));
        Debug.Log("Save!!");
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
