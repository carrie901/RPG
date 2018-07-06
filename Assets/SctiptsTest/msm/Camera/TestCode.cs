
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
using MsgPb;
using UnityEngine;
using Summer;
using UnityEditor;

public class TestCode : MonoBehaviour
{

    #region 属性

    public TimeInterval interval = new TimeInterval(2f);

    #endregion

    #region MONO Override
    // Use this for initialization
    void Start()
    {
        /*MyScriptableObject ass = ScriptableObject.CreateInstance<MyScriptableObject>();
        ass.names = new string[100];
        for (int i = 0; i < ass.names.Length; i++)
        {
            ass.names[i] = "AA";
        }
        /* ass.s = "121";
         ass.playSound = false;
         ass.configeration = MyScriptableObject.Configeration.HIGH;
         ass.tri = E_AbilityTrigger.buff_add_layer;#1#

        AssetDatabase.CreateAsset(ass, "Assets/Resources/a.asset");
        EditorUtility.SetDirty(ass);*/

        NetManager instance = NetManager.Instance;
    }


    public float speed;
    public Vector3 _move_speed_offset = Vector3.zero;
    public Vector3 result;
    public string text = "";
    public int num;
    // Update is called once per frame
    void Update()
    {
        /* if (interval.OnUpdate())
         {
             result = Vector3.SmoothDamp(Vector3.zero, Vector3.one, ref _move_speed_offset, speed);
             Debug.Log(result);
         }
 */
        /* UnityEngine.Profiling.Profiler.BeginSample("03");
         text += "1";
         UnityEngine.Profiling.Profiler.EndSample();*/
    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(100, 100, 100, 100), ""))
        {
            _excute_load();
            //_excute_string_gc();
        }
    }

    #endregion

    #region Public



    #endregion

    #region Private Methods

    public void _excute_string_gc()
    {
        UnityEngine.Profiling.Profiler.BeginSample("01");
        string[] tmp = new string[1000];
        UnityEngine.Profiling.Profiler.EndSample();

        UnityEngine.Profiling.Profiler.BeginSample("02");
        for (int i = 0; i < 1000; i++)
        {
            string a = "_excute_string_gc";
            tmp[i] = a;
        }
        UnityEngine.Profiling.Profiler.EndSample();
    }

    public void _excute_load()
    {
        /*MyScriptableObject s = Resources.Load("a") as MyScriptableObject;
        if (s.names.Length == 100)
        {

        }*/

        ReqVersion req_version_data = new ReqVersion();
        req_version_data.ver = 1;
        NetManager.Send<ReqVersion>(NetworkMessageCode.REQVERSION, req_version_data);
    }

    #endregion

}

