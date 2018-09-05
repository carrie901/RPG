
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
using UnityEngine;

public class TestUpdate : MonoBehaviour
{

    #region 属性

    public int _last_frame;
    public float _last_time;
    public float _last_real_time;

    public float _all_time;

    public bool flag = true;

    #endregion

    #region MONO Override

    void Awake()
    {
        //Application.targetFrameRate = 60;
        Time.captureFramerate = 30;
    }

    // Use this for initialization
    void Start()
    {
        _last_real_time = Time.realtimeSinceStartup;
    }


    // Update is called once per frame
    void Update()
    {
        _all_time += Time.deltaTime;
        Debug.Log(_all_time + "__frame:" + Time.frameCount + "__" + (Time.realtimeSinceStartup - _last_real_time) + "__" + Time.deltaTime);

        /*if (flag)
        {
            _last_frame = Time.frameCount;
            _last_time = 0;
            _last_real_time = Time.realtimeSinceStartup;

            flag = false;
            return;
        }
        else
        {
            float real_time = Time.realtimeSinceStartup - _last_real_time;
            _last_real_time = Time.realtimeSinceStartup;

            _last_time += Time.deltaTime;

            _all_time += real_time;
            Debug.Log("Frame:" + (Time.frameCount - _last_frame));
            Debug.Log("Time:" + (_last_time) + "     deltaTime:" + Time.deltaTime + "   real_time:" + real_time + "   _all_time:" + _all_time);

        }*/

    }

    #endregion

    #region Public



    #endregion

    #region Private Methods



    #endregion
}
