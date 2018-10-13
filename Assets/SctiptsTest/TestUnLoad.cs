
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
using Summer;
using UnityEngine;

public class TestUnLoad : MonoBehaviour
{

    #region 属性

    public bool flag1;
    public bool flag2;

    public Object obj1;
    public GameObject _uiParent;
    [SerializeField]
    private GameObject _insObj;
    #endregion

    #region MONO Override

    // Use this for initialization
    void Start()
    {
        Resources.UnloadUnusedAssets();
        Application.backgroundLoadingPriority = ThreadPriority.High;
    }

    // Update is called once per frame
    void Update()
    {
        Check1();
        Check2();
    }

    #endregion

    #region Public



    #endregion

    #region Private Methods

    public AssetBundleRequest re;
    public Object _obj1;
    public void Check1()
    {
        if (!flag1) return;
        flag1 = false;
        //Debug.Log("--------------");
        StartCoroutineManager.Start(LoadA());
    }

    public IEnumerator LoadA()
    {
        yield return null;
        string configPath = AssetBundleConst.GetAbResDirectory() + "npc_zhaoyun_001_01.ab";
        AssetBundle ab = AssetBundle.LoadFromFile(configPath);
        //ab.LoadAllAssets();
        re = ab.LoadAllAssetsAsync();
        Debug.Log("Start:" + Time.realtimeSinceStartup);
        while (!re.isDone)
        {
            Debug.Log("_" + Time.renderedFrameCount);
            yield return null;
        }
        Debug.Log("End:" + Time.realtimeSinceStartup);
    }

    public void Check2()
    {
        if (!flag2) return;
        flag2 = false;
        //obj1 = null;

        Resources.UnloadAsset(_insObj);
        Destroy(_insObj.gameObject);
        //Resources.UnloadAsset(obj1);
    }

    #endregion
}
