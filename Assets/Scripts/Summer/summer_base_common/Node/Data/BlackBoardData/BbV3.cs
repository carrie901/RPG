
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

namespace Summer
{
    public class BbV3
    {
        public Vector3 _value;
        public Vector3 Value { get { return _value; } }

        public void SetValue(Vector3 value)
        {
            _value = value;
        }
    }

    public class BbInt
    {
        public int _value;
        public int Value { get { return _value; } }

        public void SetValue(int value)
        {
            _value = value;
        }
    }

    public class BbString
    {
        public string _value;
        public string Value { get { return _value; } }

        public void SetValue(string value)
        {
            _value = value;
        }
    }

    public class BbFloat
    {
        public float _value;
        public float Value { get { return _value; } }

        public void SetValue(float value)
        {
            _value = value;
        }
    }
}