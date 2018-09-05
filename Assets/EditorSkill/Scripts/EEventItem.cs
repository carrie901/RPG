
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
using UnityEngine.UI;

namespace SummerEditorSkill
{
    public class EEventItem : MonoBehaviour
    {

        #region 属性

        public Text _track_name;
        public RectTransform _track_img;

        public float _size;
        public float _max_frame;
        public int _max_length;
        public TrackInfo _info;
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

        #endregion

        #region Public

        public void SetInfo(TrackInfo info)
        {
            _info = info;
            _init_info();
        }

        public void SetMaxLength(float max_frame, int max_length)
        {
            _max_frame = max_frame;
            _max_length = max_length;
        }

        #endregion

        #region Private Methods

        public void _init_info()
        {
            _track_name.text = _info.track_name;

            float length = ((_info.FrameCount * 1.0f) / (_max_frame * 1.0f)) * _max_length;
            _track_img.sizeDelta = new Vector2(length, _track_img.sizeDelta.y);

            //length = ((_info.FrameCenterIndex * 1.0f) / (_max_frame * 1.0f)) * _max_frame;

        }

        #endregion
    }
}