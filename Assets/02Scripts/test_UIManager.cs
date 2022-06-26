using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class test_UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    
    public void ExitBtn()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public int num = 5;
    [HideInInspector] public int num2 = 10;

    [System.Serializable]
    public class car
    {
        public int size;
        public int speed;
        public int wheelNum;
    }

    public car car1;
    // public car car2;
    // [SerializeField] car car3;
    // [HideInInspector] public car car4;

}
