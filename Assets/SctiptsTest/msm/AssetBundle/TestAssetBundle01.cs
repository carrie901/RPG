/*

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

using UnityEngine;
using Summer;
public class TestAssetBundle01 : MonoBehaviour
{

    #region 属性



    #endregion

    #region MONO Override

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(100, 100, 100, 100), "加载"))
        {
            _load();
        }

        if (GUI.Button(new Rect(200, 100, 100, 100), "卸载"))
        {
            _unload();
        }

        if (GUI.Button(new Rect(300, 100, 100, 100), "再次加载"))
        {
            _reload();
        }
        if (GUI.Button(new Rect(400, 100, 100, 100), "加载子依赖"))
        {
            _load_child();
        }

    }

    #endregion

    #region Public



    #endregion

    #region Private Methods

    public void _init()
    {

    }

    public AssetBundle main_ab;
    public AssetBundle dep_ab;
    //public AssetBundle dep_ab1;
    //public Object[] deps;
    public void _load()
    {


        //dep_ab1 = dep_ab;
        //deps = dep_ab.LoadAllAssets();
        main_ab = AssetBundle.LoadFromFile(AssetBundleConst.GetAssetBundleRootDirectory() + "res_bundle/prefab/ui/panellogin.ab");
        Object[] objs = main_ab.LoadAllAssets();
        GameObject go = objs[0] as GameObject;
        GameObject g = Instantiate(go) as GameObject;
    }

    public void _load_child()
    {
        dep_ab = AssetBundle.LoadFromFile(AssetBundleConst.GetAssetBundleRootDirectory() + "uiresources/uitexture/login.ab");
    }

    public void _unload()
    {
        //dep_ab.Unload(false);
        dep_ab.Unload(true);
        dep_ab = null;

        Resources.UnloadUnusedAssets();
    }

    public void _reload()
    {
        dep_ab = AssetBundle.LoadFromFile(AssetBundleConst.GetAssetBundleRootDirectory() + "uiresources/uitexture/login.ab");
        //dep_ab1 = dep_ab;
        //deps = dep_ab.LoadAllAssets();
    }

    #endregion
}
*/
