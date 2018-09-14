using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Summer;
public class TestChangeSprite : MonoBehaviour
{


    public SpriteBigAutoLoader loader;
    public bool flag = false;
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
            loader._isComplete = true;
            SpritePool.Instance.LoadSprite(loader);
        }
    }
}
