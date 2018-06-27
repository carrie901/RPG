
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

using Summer;

public abstract class BaseTrigger
{

    #region 属性

    protected static int iindex = 0;

    public int id_unique;                               // uid 数据，唯一表示一个触发器。有系统自动生成，游戏中，所有触发器的 
    public float _life_time;                            // 触发器的生命周期
    public E_TRIGGER_EVT _trigger_evt;
    public I_ActionCondition _action;
    public I_TriggerCondition _condition;
    public EventSet<E_TRIGGER_EVT, BlackBorad> _event_map = new EventSet<E_TRIGGER_EVT, BlackBorad>(TriggerEqualityComparer.Instance, 1);
    public I_EntityTrigger _entiry_trigger;

    #endregion

    #region 构造

    /// <summary>
    /// 
    /// </summary>
    /// <param name="entiry_trigger">触发的结构体</param>
    /// <param name="trigger_evt">触发事件</param>
    /// <param name="action">触发之后的动作</param>
    /// <param name="condition">触发条件(非必须)</param>
    public BaseTrigger(I_EntityTrigger entiry_trigger, E_TRIGGER_EVT trigger_evt, I_ActionCondition action, I_TriggerCondition condition = null)
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
        _event_map.Clear();
        _event_map = null;
    }

    #region 触发响应

    public void OnTrigger(BlackBorad blackboard)
    {
        if (_check_conition(blackboard))
        {
            _onexcute(blackboard);
        }
    }

    #endregion


    #endregion

    #region Private Methods

    public bool _check_conition(BlackBorad blackboard)
    {
        if (_condition == null || _condition.IsTrue(blackboard))
        {
            return true;
        }

        return false;
    }

    public void _onexcute(BlackBorad blackboard)
    {
        if (_action == null) return;
        _action.Excute(blackboard);
    }

    public void _init_id()
    {
        iindex++;
        id_unique = iindex;

        _entiry_trigger.RegisterHandler(_trigger_evt, OnTrigger);
    }

    #endregion
}










