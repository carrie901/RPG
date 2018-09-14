using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestLog : MonoBehaviour
{

    public string a = "";
    public bool flag = false;
    public Text tex;
    // Use this for initialization
    void Start()
    {

        string path = Application.persistentDataPath + "/Screenshot.png";
        Debug.Log(path);
        tex.text = path;
        Application.CaptureScreenshot(path);

    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 100; i++)
        {
            Summer.LogManager.Log("Call:[i]", i);
        }
    }


}
