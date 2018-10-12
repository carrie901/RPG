﻿
//
//                            _ooOoo_
//                           o8888888o
//                           88" . "88
//                           (| -_- |)
//                           O\  =  /O
//                        ____/`---'\____
//                      .'  \\|     |//  `.
//                     /  \\|||  :  |||//  \
//                    /  _||||| -:- |||||-  \
//                    |   | \\\  -  /// |   |
//                    | \_|  ''\---/''  |   |
//                    \  .-\__  `-`  ___/-. /
//                  ___`. .'  /--.--\  `. . __
//               ."" '<  `.___\_<|>_/___.'  >'"".
//              | | :  `- \`.;`\ _ /`;.`/ - ` : | |
//              \  \ `-.   \_ __\ /__ _/   .-` /  /
//         ======`-.____`-.___\_____/___.-`____.-'======
//                            `=---='
//        ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
//                 			 佛祖 保佑             
#if UNITY_EDITOR
using System.Collections.Generic;
using Summer;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
public class DemoTexturePacker : MonoBehaviour
{

    #region 属性

    public List<Image> _imgs;
    public TexturePacker _texPacker;
    public Texture2D MainTex;
    public bool flagDis = false;

    #endregion

    #region MONO Override

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        DisposeTex();
        Render2();
        Render1();
    }

    #endregion

    #region Public



    #endregion

    #region Private Methods

    private void DisposeTex()
    {
        if (flagDis)
        {
            flagDis = false;
            _texPacker.Dispose();
        }
    }

    public bool _flag2;
    public CameraRenderTexture _camRender;
    public void Render2()
    {
        if (!_flag2) return;
        _flag2 = false;
        _camRender.RenderCamera();
    }

    public bool _flag1;
    public void Render1()
    {
        if (!_flag1) return;
        _flag1 = false;
        _texPacker = new TexturePacker();
        List<Texture2D> tmp = new List<Texture2D>();
        for (int i = 1; i < 16; i++)
        {
            Texture2D tex = AssetDatabase.LoadAssetAtPath<Texture2D>(string.Format("Assets/res_bundle/icon/Buff/{0:00}.png", i));
            _texPacker.AddTexture2D(tex);
            tmp.Add(tex);
        }
        _texPacker.PackerTexture();
        for (int i = tmp.Count - 1; i >= 0; i--)
        {
            Texture2D tex = tmp[i];
            tmp.RemoveAt(i);
            Resources.UnloadAsset(tex);
        }
        for (int i = 0; i < _imgs.Count; i++)
        {
            _imgs[i].sprite = _texPacker.GetSprite(string.Format("{0:00}", (i + 1)));
        }

        MainTex = _texPacker._mainTex;
    }

    #endregion
}
#endif