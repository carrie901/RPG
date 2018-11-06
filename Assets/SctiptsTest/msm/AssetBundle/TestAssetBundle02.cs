
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
using Summer;
using UnityEngine;

public class TestAssetBundle02 : MonoBehaviour
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


    public AssetBundle main_ab;
    public GameObject pfb_go;
    private void OnGUI()
    {
        if (GUI.Button(new Rect(100, 100, 100, 100), ""))
        {
            TimeModule.BeginSampleTime();
            main_ab = AssetBundle.LoadFromFile(AssetBundleConst.GetAssetBundleRootDirectory() + "model_01.ab");
            TimeModule.InputSimpleTime();

           


        }

        if (GUI.Button(new Rect(200, 100, 100, 100), ""))
        {
            TimeModule.BeginSampleTime();
            pfb_go = main_ab.LoadAsset<GameObject>("CH_Barbarian01_E_Tall4");
            TimeModule.InputSimpleTime();
        }

        if (GUI.Button(new Rect(300, 100, 100, 100), ""))
        {
            TimeModule.BeginSampleTime();
            GameObject go = Instantiate(pfb_go) as GameObject;
            TimeModule.InputSimpleTime();
        }
    }


    public void BeginTime()
    {

    }

    #endregion

    #region Public



    #endregion

    #region Private Methods



    #endregion
}
