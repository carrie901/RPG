
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
    public class AnimationPanel : EComponent
    {
        public ETextArea _des;
        public EButton _swparation_animation_btn;
        public EButton _seo_animation;
        public AnimationPanel(float width, float height) : base(width, height)
        {
            _init();
        }

        public void _init()
        {
            _des = new ETextArea(_size.x - 100, 100);
            _des.SetInfo(
                "1.分离动画\n"
                + "2.动画内存优化\n");

            _swparation_animation_btn = new EButton("分离动画(针对Fbx)", 120);
            _swparation_animation_btn.on_click += ClickSwparationAnimation;

            _seo_animation = new EButton("动画内存优化(针对AnimationClip)", 240);
            _seo_animation.on_click += CheckSeoAnimation;
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
            anchor = AddComponentDown(_swparation_animation_btn, anchor);
            anchor = AddComponentRight(_seo_animation, anchor);
        }

        private void ClickSwparationAnimation(EButton button)
        {
            try
            {
                AnimationSeparationE.AllSeparationAnimationByFbx();
                UnityEditor.EditorUtility.DisplayDialog("执行成功", "分离动画(针对Fbx)", "Ok");
            }
            catch (Exception e)
            {
                UnityEditor.EditorUtility.DisplayDialog("执行失败", e.Message, "Ok");
            }
        }

        public void CheckSeoAnimation(EButton button)
        {
            try
            {
                AnimationSeoE.AllSeoAnimation();
                UnityEditor.EditorUtility.DisplayDialog("执行成功", "动画内存优化(针对AnimationClip)", "Ok");
            }
            catch (Exception e)
            {
                UnityEditor.EditorUtility.DisplayDialog("执行失败", e.Message, "Ok");
            }
        }
    }
}