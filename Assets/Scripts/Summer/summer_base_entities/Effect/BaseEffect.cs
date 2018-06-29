
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
        public BaseTrigger _base_trigger;                                           // 触发器
        public EffectValueData _param;                                              // 每一个效果的参数
        public Action<EventSetData> _action;                                        // 触发器执行的动作
        public EffectLogicInfo _info;

        public BaseEffect(I_Trigger entiry_trigger, EffectLogicInfo info)
        {
            _info = info;
            _action = ExcuteEffect;
            _base_trigger = new BaseTrigger();
            _base_trigger.Init(entiry_trigger, info.trigger_evt, _action);
        }

        public virtual void OnAttach() { }

        public virtual void OnDetach() { }

        public abstract void ExcuteEffect(EventSetData data);

        public abstract void ReverseEffect(EventSetData data);

        public abstract void OnUpdate(float dt);

    }
}