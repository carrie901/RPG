
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


namespace Summer.Sequence
{
    /// <summary>
    /// 控制她生命周期的有两个因数
    ///     1.帧数
    ///     2.完整状态
    /// </summary>
    public abstract class SequenceLeafNode
    {

        #region 属性

        public SequenceLine _context;
        public bool _is_complete;
        public int _frame_length;

        public int _seq_id;
        private static int seq_id;
        public SequenceLeafNode()
        {
            seq_id++;
            _seq_id = seq_id;
        }

        #endregion

        #region  Override

        public abstract void OnEnter(BlackBoard blackboard);

        public abstract void OnExit(BlackBoard blackboard);

        public virtual void OnUpdate(float dt, BlackBoard blackboard)
        {
            Finish();
        }

        public abstract void SetConfigInfo(EdNode cnf);

        public virtual string ToDes()
        {
            return string.Empty;
        }

        #endregion

        #region Public

        public void BindingContext(SequenceLine context, int frame_length)
        {
            _frame_length = frame_length;
            _context = context;
        }

        public void Finish() { _is_complete = true; }

        public bool IsFinish() { return _is_complete; }

        public void Reset() { _is_complete = false; }

        public void RaiseEvent(E_EntityInTrigger key, EventSetData obj_info)
        {
            if (_context == null || _context._owner == null) return;
            _context._owner.RaiseEvent(key, obj_info);
            EventDataFactory.Push(obj_info);
        }

        #endregion

        #region

        public void LogEnter()
        {
            if (!LogManager.open_skill) return;
            LogManager.Log("Time: {1}   Enter [{0}] 节点", ToDes(), TimeManager.FrameCount);
        }

        public void LogExit()
        {
            if (!LogManager.open_skill) return;
            LogManager.Log("Time: {1}   Exit [{0}] 节点,开始跳转到下一个节点 ", ToDes(), TimeManager.FrameCount);
        }

        #endregion

        #region Private Methods



        #endregion
    }
}