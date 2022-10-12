using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveDataScripts : MonoBehaviour
{
    
    public void SaveDataFuction()
    {      
        SaveDataManager.instance.SaveData();
    }
}
