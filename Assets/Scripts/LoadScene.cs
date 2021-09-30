using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadScene : MonoBehaviour
{
    public GameObject pref1;
    public Mesh pref1Mesh;
    public GameObject pref2;
    public GameObject pref3;
    public GameObject ChosenStatue;

    public void load()
    {
        Debug.Log("load");
        switch (LevelDic.LVL)
        {
            case 1:
                ChosenStatue = Instantiate(pref1, new Vector3(-1.25f, -1.69f, 16.18f), Quaternion.Euler(0f, 180f, 0f));
                ChosenStatue.transform.localScale = new Vector3(30,30,30);
                var child = ChosenStatue.transform.GetChild(0).gameObject;
                child.AddComponent<MeshCollider>();
                child.GetComponent<MeshCollider>().sharedMesh = pref1Mesh;
                break;
            case 2:
                ChosenStatue = Instantiate(pref1, new Vector3(-1.25f, -1.69f, 16.18f), Quaternion.Euler(0f, 180f, 0f));
                ChosenStatue.transform.localScale = new Vector3(30,30,30);
                break;
            case 3:
                ChosenStatue = Instantiate(pref1, new Vector3(-1.25f, -1.69f, 16.18f), Quaternion.Euler(0f, 180f, 0f));
                ChosenStatue.transform.localScale = new Vector3(30,30,30);
                break;
        }
    } 
}
