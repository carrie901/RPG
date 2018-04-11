using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CheckSprite : MonoBehaviour
{

    public bool flag = false;
    public Image img;
    //public Sprite sprite;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (flag)
        {
            flag = false;
            if (img.sprite.name == "02")
                img.sprite.name = "03";
            else
                img.sprite.name = "02";
            /*if (sprite.name == "02")
                sprite.name = "02";
            else
                sprite.name = "01";*/
            img.SetNativeSize();
        }
    }
}
