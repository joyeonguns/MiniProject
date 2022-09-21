using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class DamagePool : MonoBehaviour
{
    ObjectPool<GameObject> Pool;
    public GameObject poolSave;
    public GameObject fontPrefab;



    // Start is called before the first frame update
    void Awake()
    {
        Pool = new ObjectPool<GameObject>(
            CreateFont, OnGetFonte, OnReleaseFont, OnDestroyFont, maxSize:10);
    }

    public GameObject GetPoolObject()
    {
        var font = Pool.Get();
        return font;
    }
    GameObject CreateFont()
    {
        GameObject font = Instantiate(fontPrefab);
        font.transform.SetParent(poolSave.transform);
        font.transform.GetChild(0).GetComponent<D_fontScripts>().SetPoolManaged(Pool);
        return  font;
    }

    void OnGetFonte(GameObject font)
    {
        font.gameObject.SetActive(true);
    }

    void OnReleaseFont(GameObject font)
    {
        font.gameObject.SetActive(false);
    }

    void OnDestroyFont(GameObject font)
    {
        Destroy(font.gameObject); 
    }
}
