
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

namespace SummerEditor
{
    /// <summary>
    /// 特效性能
    /// </summary>
    public class EffPerformancePanel : EComponent
    {
        public ETextArea _des;
        public EButton _check_tex_ref_btn;
        public EButton _check_dc_btn;
        public EffPerformancePanel(float width, float height) : base(width, height)
        {
            _init();
        }

        public void _init()
        {
            _des = new ETextArea(_size.x - 100, 100);
            _des.SetInfo(
                "1.特效纹理引用\n"
                + "2.特效Dc个数\n");

            _check_tex_ref_btn = new EButton("特效纹理引用", 120);
            _check_tex_ref_btn.OnClick += ClickCheckTexRef;

            _check_dc_btn = new EButton("特效Dc个数", 120);
            _check_dc_btn.OnClick += CheckCheckDcBtn;
            _init_position();
        }

        public void _init_position()
        {
            float tmp_x = 10;
            float tmp_y = 10;
            float tmp_height = 10;
            AddComponent(_des, tmp_x, tmp_y);
            tmp_y += _des.Size.y + tmp_height;

            ERect anchor = _des;
            anchor = AddComponentDown(_check_tex_ref_btn, anchor);
            anchor = AddComponentRight(_check_dc_btn, anchor);
        }

        private void ClickCheckTexRef(EButton button)
        {
            try
            {
                EditorEffectCheckHelper.CheckEffectTexture();
                UnityEditor.EditorUtility.DisplayDialog("执行成功", "特效纹理引用", "Ok");
            }
            catch (Exception e)
            {
                UnityEngine.Debug.Log(e);
                UnityEditor.EditorUtility.DisplayDialog("执行失败", e.Message, "Ok");
            }
        }

        public void CheckCheckDcBtn(EButton button)
        {
            try
            {
                EditorEffectCheckHelper.CheckEffectDrawCall();
                UnityEditor.EditorUtility.DisplayDialog("执行成功", "特效Dc个数", "Ok");
            }
            catch (Exception e)
            {
                UnityEditor.EditorUtility.DisplayDialog("执行失败", e.Message, "Ok");
            }
        }
    }
}