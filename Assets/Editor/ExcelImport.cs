using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;

public class ExcelImport : AssetPostprocessor {

    static readonly string filePath = "Assets/Editor/Data/RPGData.xlsx";
    static readonly string playerExportPath = "Assets/Resources/Data/PlayerLevelData.asset";
    static readonly string slimeExportPath = "Assets/Resources/Data/SlimeLevelData.asset";
     
    
    // static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    // {
    //     Debug.Log("!!");
    //     foreach(string s in importedAssets)
    //     {
    //         if(s == filePath)
    //         {
    //             Debug.Log("Excel data covert start.");
    //         }

    //         MakePlayerData();
    //         MakeSlimeData();

    //         Debug.Log("Excel data covert complete.");
    //     }
    // }
    

    [MenuItem("Excel/Update RPGData")]
    static public void MakeData()
    {
        Debug.Log("Excel data covert start.");

        MakePlayerData();
        MakeSlimeData();

        Debug.Log("Excel data covert complete.");
        
    }

    static void MakePlayerData()
    {
        PlayerLevelData data = ScriptableObject.CreateInstance<PlayerLevelData>();
        AssetDatabase.CreateAsset((ScriptableObject)data, playerExportPath);

        data.hideFlags = HideFlags.NotEditable;

        data.list.Clear();

        using (FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
        {
            IWorkbook book = new XSSFWorkbook(stream);
            ISheet sheet = book.GetSheetAt(0);

            for(int i=2; i<= sheet.LastRowNum; i++)
            {
                IRow row = sheet.GetRow(i);

                    PlayerLevelData.Attribute a = new PlayerLevelData.Attribute()
                    {
                        level = (int)row.GetCell(0).NumericCellValue,
                        maxHP = (int)row.GetCell(1).NumericCellValue,
                        baseAttack = (int)row.GetCell(2).NumericCellValue,
                        requireNextLevelExp = (int)row.GetCell(3).NumericCellValue,
                        moveSpeed = (int)row.GetCell(4).NumericCellValue,
                        turnSpeed = (int)row.GetCell(5).NumericCellValue,
                        attackRange = (float)row.GetCell(6).NumericCellValue
                    };

                data.list.Add(a);
            }

            stream.Close();
        }

        ScriptableObject obj = AssetDatabase.LoadAssetAtPath(playerExportPath, typeof(ScriptableObject)) as ScriptableObject;
        EditorUtility.SetDirty(obj);
    }

    static void MakeSlimeData()
    {
        SlimeLevelData data = ScriptableObject.CreateInstance<SlimeLevelData>();
        AssetDatabase.CreateAsset((ScriptableObject)data, slimeExportPath);

        data.hideFlags = HideFlags.NotEditable;
        data.list.Clear();

        using (FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
        {
            IWorkbook book = new XSSFWorkbook(stream);
            ISheet sheet = book.GetSheetAt(1);
            
            for(int i=2; i<= sheet.LastRowNum; i++)
            {
                IRow row = sheet.GetRow(i);
                SlimeLevelData.Attribute a = new SlimeLevelData.Attribute();

                a.level = (int)row.GetCell(0).NumericCellValue;
                a.maxHP = (int)row.GetCell(1).NumericCellValue;
                a.attack = (int)row.GetCell(2).NumericCellValue;
                a.defence = (int)row.GetCell(3).NumericCellValue;
                a.gainExp = (int)row.GetCell(4).NumericCellValue;
                a.moveSpeed = (int)row.GetCell(5).NumericCellValue;
                a.turnSpeed = (int)row.GetCell(6).NumericCellValue;
                a.attackRange = (int)row.GetCell(7).NumericCellValue;
                a.gainGold = (int)row.GetCell(8).NumericCellValue;

                data.list.Add(a);
            }

            stream.Close();
        }

        ScriptableObject obj = AssetDatabase.LoadAssetAtPath(slimeExportPath, typeof(ScriptableObject)) as ScriptableObject;
        EditorUtility.SetDirty(obj);
    }
}
