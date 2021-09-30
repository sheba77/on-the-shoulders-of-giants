using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Menue : MonoBehaviour
{
    private string name;
    public void Awake()
    {
        name = "";
    }

    public void playGame()
    {
        name =  EventSystem.current.currentSelectedGameObject.name;
        Debug.Log("print name: " + name);
        if (name == "stat1")
        {
            LevelDic.LVL = 1;
            SceneManager.LoadScene("Main");
        }
    }
}
