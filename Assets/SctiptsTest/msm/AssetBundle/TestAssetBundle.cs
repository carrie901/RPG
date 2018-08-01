
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAssetBundle : MonoBehaviour
{

    /*#region 属性

    public AssetBundle _mian_ab;
    public GameObject _panel;
    #endregion

    #region MONO Override

    // Use this for initialization
    void Start()
    {
        _init();
    }


    public bool flag = false;
    public bool flag1 = false;
    // Update is called once per frame
    void Update()
    {
        if (flag)
        {
            flag = false;
            _excute_logic();
        }

        if (flag1)
        {
            flag1 = false;
            _excute_logic1();
        }
    }

    #endregion

    #region Public



    #endregion

    #region Private Methods

    public void _init()
    {
        AssetBundle.LoadFromFile(GetRoot() + "uiresources/uitexture/other/activity_yeqian_1.ab");


        _mian_ab = AssetBundle.LoadFromFile(GetRoot() + "res_bundle/testres/testpanel02.ab");
        GameObject template_go = _mian_ab.LoadAsset<GameObject>("testpanel02");
        _panel = Instantiate(template_go);
        _panel.transform.parent = transform;

        _excute_logic_check_time();
    }

    public void _excute_logic()
    {
        Destroy(_panel);
        _panel = null;
        Debug.Log("Start");
        StartCoroutine("UnLoad");

    }

    public void _excute_logic1()
    {
        GameObject template_go = _mian_ab.LoadAsset<GameObject>("testpanel02");

        GameObject template_go2 = _mian_ab.LoadAsset<GameObject>("testpanel02");
        GameObject template_go3 = _mian_ab.LoadAsset<GameObject>("testpanel02");
        _panel = Instantiate(template_go);
        _panel.transform.parent = transform;


    }

    public void _excute_logic_check_time()
    {
       /* AssetBundle ab_img = AssetBundle.LoadFromFile(GetRoot() + "uiresources/uitexture/other/activity_yeqian_2.ab");
        float start_time = Time.realtimeSinceStartup;
        Object o1 = ab_img.LoadAsset("activity_yeqian_2");
        Debug.Log(Time.realtimeSinceStartup - start_time);
        start_time = Time.realtimeSinceStartup;
        Object o2 = ab_img.LoadAsset("activity_yeqian_2");
        Debug.Log(Time.realtimeSinceStartup - start_time);
        start_time = Time.realtimeSinceStartup;
        Object o3 = ab_img.LoadAsset("activity_yeqian_2");
        Debug.Log(Time.realtimeSinceStartup - start_time);
        start_time = Time.realtimeSinceStartup;
        Object o4 = ab_img.LoadAsset("activity_yeqian_2");
        Debug.Log(Time.realtimeSinceStartup - start_time);
        start_time = Time.realtimeSinceStartup;#1#
    }

    public IEnumerator UnLoad()
    {
        yield return null;
        AsyncOperation ao = Resources.UnloadUnusedAssets();
        yield return ao;
        Debug.Log("--------------");


    }

    public string GetRoot()
    {
        return Application.streamingAssetsPath + "/rpg/";
    }

    #endregion*/
}
