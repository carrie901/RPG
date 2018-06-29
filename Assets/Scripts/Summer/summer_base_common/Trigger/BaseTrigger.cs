
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
    public class BaseTrigger
    {

        #region 属性

        protected static int iindex = 0;

        public int id_unique;                               // uid 数据，唯一表示一个触发器。有系统自动生成，游戏中，所有触发器的 
        public float _life_time;                            // 触发器的生命周期
        public string _trigger_evt;
        public Action<EventSetData> _action;
        public I_TriggerCondition _condition;
        //public EventSet<string, EventSetData> _event_map = new EventSet<string, EventSetData>(/*TriggerEqualityComparer.Instance,*/ 1);
        public I_Trigger _entiry_trigger;

        #endregion

        #region 构造

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entiry_trigger">触发的结构体</param>
        /// <param name="trigger_evt">触发事件</param>
        /// <param name="action">触发之后的动作</param>
        /// <param name="condition">触发条件(非必须)</param>
        public void Init(I_Trigger entiry_trigger, string trigger_evt, Action<EventSetData> action,
            I_TriggerCondition condition = null)
        {
            _entiry_trigger = entiry_trigger;
            _trigger_evt = trigger_evt;
            _action = action;
            _condition = condition;
            _init_id();
        }

        #endregion

        #region Public

        public void Dispose()
        {
            _action = null;
        }

        #region 触发响应

        public void OnTrigger(EventSetData data)
        {
            if (_check_conition(data))
            {
                _onexcute(data);
            }
        }

        #endregion


        #endregion

        #region Private Methods

        public bool _check_conition(EventSetData data)
        {
            if (_condition == null || _condition.IsTrue(data))
            {
                return true;
            }

            return false;
        }

        public void _onexcute(EventSetData data)
        {
            if (_action == null) return;
            _action(data);
        }

        public void _init_id()
        {
            iindex++;
            id_unique = iindex;

            _entiry_trigger.RegisterHandler(_trigger_evt, OnTrigger);
        }

        #endregion
    }
}









