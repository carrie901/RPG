
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
using UnityEngine.UI;

namespace Summer
{
    /// <summary>
    /// 倒计时的格式 还有待补充
    /// </summary>
    public class TextLeftTime : MonoBehaviour
    {

        public Text _text;
        public int _lastLeftTime;
        public float _leftTime;
        // Update is called once per frame
        void Update()
        {
            if (_text == null || _lastLeftTime <= 0) return;
            ResetText();
        }

        public void LeftTime(float leftTime)
        {
            _leftTime = leftTime;
            _lastLeftTime = Mathf.CeilToInt(_leftTime) + 1;
            if (leftTime <= -1)
            {
                if (_lastLeftTime <= 0)
                    _text.text = "";
            }
            else
            {
                ResetText();
            }
        }

        private void ResetText()
        {
            /*float tmp = _left_time - LogManager.level_time();
            int curr = Mathf.CeilToInt(tmp);
            _last_left_time = curr;
            _text.text = _last_left_time.ToString();
            if (tmp <= 0.1f)
                _text.text = "";*/
        }
    }
}