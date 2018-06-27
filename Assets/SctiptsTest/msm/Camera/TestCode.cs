
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
public class TestCode : MonoBehaviour
{

    #region 属性

    public TimeInterval interval = new TimeInterval(2f);

    #endregion

    #region MONO Override

    // Use this for initialization
    void Start()
    {
        Days meetingDay = Days.Monday | Days.Tuesday;
        if ((meetingDay & Days.Monday) == Days.Monday)
            Debug.Log("yes");
        else
            Debug.Log("none");
    }

    public float speed;
    public Vector3 _move_speed_offset = Vector3.zero;
    public Vector3 result;
    // Update is called once per frame
    void Update()
    {
        if (interval.OnUpdate())
        {
            result = Vector3.SmoothDamp(Vector3.zero, Vector3.one, ref _move_speed_offset, speed);
            Debug.Log(result);
        }
        
    }

    #endregion

    #region Public



    #endregion

    #region Private Methods



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
