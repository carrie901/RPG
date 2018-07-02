
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

        public Action<EventSetData> _action;                                        // 触发器执行的动作
        public EffectTemplateInfo _info;
        public BaseBuff _target_buff;

        public virtual void OnInit(BaseBuff target_buff, EffectTemplateInfo info)
        {
            _info = info;
            _target_buff = target_buff;
            E_Buff_Event evt = info.GetBuffEvt();
            _target_buff.RegisterHandler(evt, ExcuteEffect);
        }

        public virtual void OnAttach() { }

        public virtual void OnDetach()
        {
            _action = null;
            _info = null;
            _target_buff = null;
            _target_select = null;
        }

        public abstract void ExcuteEffect(EventSetData data);

        public abstract void ReverseEffect(EventSetData data);
    }
}