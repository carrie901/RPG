
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
/*    public abstract class BaseTrigger
    {
        #region 属性

        public Action<EventSetData> _action;
        public I_Condition _condition;
        public I_Trigger _entiry_trigger;

        #endregion

        #region 构造

        public void Init(Action<EventSetData> action,
            I_Condition condition = null)
        {
            _action = action;
            _condition = condition;
            OnInit();
            _init_id();
        }

        public abstract void OnInit();

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
            /*iindex++;
            id_unique = iindex;#1#

            //_entiry_trigger.RegisterHandler(evt, OnTrigger);
        }

        #endregion
    }*/
}









