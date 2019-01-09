
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
using System.Collections.Generic;
using System.Linq;
using Summer;
using UnityEngine;
using Object = UnityEngine.Object;

public class TestPanel : MonoBehaviour
{

    #region 属性

    public Transform CanvasParent;


    #endregion

    #region MONO Override

    // Use this for initialization
    void Start()
    {
        GC.Collect();
        Resources.UnloadUnusedAssets();
    }

    // Update is called once per frame
    void Update()
    {
        Test1();
        Test2();
        Test3();
    }

    #endregion

    #region Public

    public bool flag1;
    private AssetBundle main_ab;
    private AssetBundle ab1;
    public void Test1()
    {
        if (!flag1) return;
        flag1 = false;
         main_ab = AssetBundle.LoadFromFile(AssetBundleConst.GetAssetBundleRootDirectory() + "res_bundle/prefab/ui/PanelEquip.ab");

        Object[] objs = main_ab.LoadAllAssets();
        GameObject go = Instantiate(objs[0]) as GameObject;
        go.transform.SetParent(CanvasParent);
        go.Normalize();
        
        ab1 = AssetBundle.LoadFromFile(AssetBundleConst.GetAssetBundleRootDirectory() + "uiresources/uitexture/equip.ab");
        Object[] assets = ab1.LoadAllAssets();
        //ab1.Unload(false);
    }

    public bool flag2;

    public void Test2()
    {
        if (!flag2) return;
        flag2 = false;
        /*ab1.Unload(true);
        ab1 = null;*/

        main_ab.Unload(true);
        main_ab = null;
    }
    public bool flag3;

    public void Test3()
    {
        if (!flag3) return;
        flag3 = false;
        /*ab1 = AssetBundle.LoadFromFile(AssetBundleConst.GetAssetBundleRootDirectory() + "uiresources/uitexture/equip.ab");
        ab1.LoadAllAssets();*/

        main_ab = AssetBundle.LoadFromFile(AssetBundleConst.GetAssetBundleRootDirectory() + "res_bundle/prefab/ui/PanelEquip.ab");

        Object[] objs = main_ab.LoadAllAssets();
        GameObject go = Instantiate(objs[0]) as GameObject;
        go.transform.SetParent(CanvasParent);
        go.Normalize();
    }

    #endregion

    #region Private Methods



    #endregion
}
