
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

namespace Summer
{
    public abstract class BaseEffect
    {
        public E_EffectOverlayType _eff_overlay_type;                               // 叠加效果
        public TargetSelector _target_select;                                       // 目标过滤器
        public EffectTemplateInfo _info;                                            // 效果的配置信息
        public BuffId _bid;

        public virtual void OnInit(BuffId bid, EffectTemplateInfo info)
        {
            _info = info;
            _bid = bid;
        }

        public virtual void OnAttach() { }

        public virtual void OnDetach()
        {
            _info = null;
            _target_select = null;
        }

        public virtual void ExcuteEffect(EventSetData data)
        {

        }

        public virtual void ReverseEffect(EventSetData data)
        {

        }


    }
}