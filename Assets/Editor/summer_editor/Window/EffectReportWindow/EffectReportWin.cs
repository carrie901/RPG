
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
using UnityEditor;
using UnityEngine;

namespace SummerEditor
{
    public class EffectReportWin : EditorWindow
    {

        #region 静态属性

        protected static EffectReportWin _window;
        protected static float _tWidth = 1600;
        protected static float _tHeight = 800;
        [MenuItem("策划/特效")]
        static void Init()
        {
            _window = GetWindow<EffectReportWin>();
            _window.titleContent = new GUIContent("特效报告");
            _window.Show();
            _window.GetAssets();
        }

        #endregion

        #region 属性

        public EComponent _container;                                           // 容器


        #endregion

        #region MONO Override

        public void GetAssets()
        {
            _container = new EComponent(_tWidth, _tHeight);
            _container.SetBg(false);
            _container.ResetPosition(_tWidth / 2, _tHeight / 2);
            _container.SetBg(0, 0, 0);

            EffectReportPanel texWin = new EffectReportPanel(this, _tWidth, _tHeight);
            _container.AddComponent(texWin, 5, 10);
        }

        #endregion


        #region Private Methods

        void OnGUI()
        {
            if (_container != null)
            {
                _container.OnDraw(5, 5);
            }
        }
        #endregion
    }
}