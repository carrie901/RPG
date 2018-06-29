
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
        MyScriptableObject ass = ScriptableObject.CreateInstance<MyScriptableObject>();
        ass.s = "121";
        ass.playSound = false;
        ass.configeration = MyScriptableObject.Configeration.HIGH;
        ass.tri = E_AbilityTrigger.buff_add_layer;

        AssetDatabase.CreateAsset(ass, "Assets/Resources/a.asset");
        EditorUtility.SetDirty(ass);

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
        UnityEngine.Profiling.Profiler.BeginSample("03");
        text += "1";
        UnityEngine.Profiling.Profiler.EndSample();
    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(100, 100, 100, 100), ""))
        {
            _excute_string_gc();
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

    #endregion

    [Flags]
    enum Days
    {
        Sunday = 0x01,
        Monday = 0x02,
        Tuesday = 0x04,
        Wednesday = 0x08,
        Thursday = 0x10,
        Friday = 0x20,
        Saturday = 0x40
    }
}


[System.Serializable]
public class A
{
    public string name;
}

[System.Serializable]
public class B : A
{
    public int index;
}
