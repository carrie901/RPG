
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

using System;
using System.Collections;
using System.Collections.Generic;
using Summer;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;
public class TestResLoad1 : MonoBehaviour
{

    #region 属性

    public Image _img;

    #endregion

    #region MONO Override

    // Use this for initialization
    void Start()
    {
        Resources.UnloadUnusedAssets();
        //StartCoroutineManager.Start(testIe());
    }

    public IEnumerator testIe()
    {
        yield return null;
        Debug.Log("1");
    }

    // Update is called once per frame
    void Update()
    {
        Check1();
        Check2();
        Check3();
        Check4();
        Check10();
    }

    #endregion

    #region Public

    public bool flag1;

    public void Check1()
    {
        if (!flag1) return;
        flag1 = false;
        string path = "UIResources/UITexture/Common/UI_Hero_Info_071.png";
        Load<GameObject>("res_bundle/prefab/ui/PanelMain.prefab", CallbackGameObject);
        //Load<Sprite>("UIResources/UITexture/Common/UI_Hero_Info_07.png", CallBackSprite);
        //Load<Sprite>(path, CallBackSprite);
        
    }

    private GameObject _go;
    private void CallbackGameObject(GameObject go)
    {
        _go = go;
        GameObject insGo = GameObject.Instantiate(go);
        GameObjectHelper.SetParent(insGo, gameObject);
    }

    private void CallBackSprite(Sprite sprite)
    {
        _img.sprite = sprite;
        //LogManager.Log("加载完成" + sprite.name);
    }

    public bool flag2;
    /// <summary>
    /// 一次性加载10个相同名字的资源
    /// </summary>
    public void Check2()
    {
        if (!flag2) return;
        flag2 = false;
        Load<Sprite>("UIResources/UITexture/Other/activity_yeqian_1.png", CallBackSprite);
       
        //Unload();
    }

    public bool flag3;
    public void Check3()
    {
        if (!flag3) return;
        flag3 = false;
        ResLoader.Instance.UnLoadRes("UIResources/UITexture/Other/activity_yeqian_1.png");
        
    }

    public bool flag4;
    public void Check4()
    {
        if (!flag4) return;
        flag4 = false;
        Load<Sprite>("UIResources/UITexture/Other/activity_yeqian_1.png", null);
        //Destroy(_g1);
    }

    public bool _flag10 = false;
    public void Check10()
    {
        if (!_flag10) return;
        _flag10 = false;
        ResLoader.Instance.CheckInfo();
    }

    #endregion

    #region Private Methods

    // 一次性加载10个资源
    private void Load10()
    {
        for (int i = 0; i < 10; i++)
        {
            ResLoader.Instance.LoadAssetAsync<GameObject>("res_bundle/prefab/ui/PanelMain.prefab", CallbackGameObject);
        }
    }

    private void Load<T>(string resPath, Action<T> callBack) where T : Object
    {
        ResLoader.Instance.LoadAssetAsync<T>(resPath, callBack);
    }

    private void Unload()
    {
        ResLoader.Instance.UnLoadRes(_go);
    }


    private void CheckString()
    {

    }

    #endregion
}
