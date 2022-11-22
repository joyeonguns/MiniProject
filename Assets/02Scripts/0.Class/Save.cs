using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Save
{          
    public static Status Bandit = new Status(10, 0.2f, 5, 40, 30, 30, 0);
    public static Status Knight = new Status(13, 0.5f, 5, 70, 80, 50, 0);
    public static Status Abomination = new Status(17, 0.1f, 5, 100, 70, 110, 0);
    public static Status Witch_Stat = new Status(0, 0.2f, 3, 30, 0, 180, 30);
    public static Status Crystal_Stat = new Status(10, 0, 0, 30, 50, 50, 100);
    public static Status Barlog_Stat = new Status(30, 0.4f, 10, 100, 450, 0, 40);


}


