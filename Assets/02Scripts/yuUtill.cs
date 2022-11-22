using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace YU.Util
{
    [System.Serializable]
    public class UISize
    {
        public float width;
        public float height;
        public float ChangedHeight(float _width, float _height, float target)
        {
            float h = (width / height) * (_height / _width) * target;
            return h;
        }
        public float CameraDelta()
        {            
            return ((float)Screen.height / 1080);
        }
    }
}

public class yuUtill : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
