using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDic : MonoBehaviour
{
    public static int LVL;
     
    void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
