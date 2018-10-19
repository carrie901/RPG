
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

using System.Collections.Generic;
using System.Linq;
using Summer;
using UnityEditor;
using UnityEngine;

public class TestResLoad : MonoBehaviour
{
    #region MONO Override

    public GameObject _rootCanvas;
    // Use this for initialization
    void Start()
    {
        //_flag0 = true;
        Resources.UnloadUnusedAssets();
    }

    // Update is called once per frame
    void Update()
    {
        Check0();
        Check1();
        Check10();
    }

    #endregion

    #region Public



    #endregion

    #region Private Methods

    public UnityEngine.UI.Text f;
    public bool _flag0 = false;
    public AssetBundle _altas01;
    public Object[] _altasobj;
    public Dictionary<string, string> s = new Dictionary<string, string>();
    public string dab = "strign";
    public void Check0()
    {
        if (!_flag0) return;
        _flag0 = false;
        /* AssetBundle ab = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/rpg/res_bundle/prefab/ui/panellogin.ab");
         Object obj = ab.LoadAllAssets()[0];
         GameObject go = GameObject.Instantiate(obj as GameObject);
         GameObjectHelper.SetParent(go, _rootCanvas);

         _altas01 = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/rpg/uiresources/uitexture/common/ui_hero_info_06.ab");
         _altasobj = _altas01.LoadAllAssets();
         Debug.Log(_altasobj.Length);*/
        GameObject obj = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/res_bundle/prefab/ui/PanelLogin.prefab");
        go = GameObject.Instantiate(obj as GameObject);
        GameObjectHelper.SetParent(go, _rootCanvas);
    }

    private GameObject go;
    public bool _flag1 = false;
    public void Check1()
    {
        if (!_flag1) return;
        _flag1 = false;
        /*Sprite obj = _altasobj[1] as Sprite;
        //_altasobj[0] = null;
        Resources.UnloadAsset(obj.texture);
        Resources.UnloadUnusedAssets();*/
        go.transform.localScale = Vector3.one;
    }

    public bool _flag10 = false;
    public void Check10()
    {
        if (!_flag10) return;
        _flag10 = false;
        go.transform.localScale = Vector3.zero;
    }

    #endregion
}
