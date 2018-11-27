
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
        public bool _isComplete;
        public int _frameLength;

        public int _seqId;
        private static int SeqId;
        public SequenceLeafNode()
        {
            SeqId++;
            _seqId = SeqId;
        }

        #endregion

        #region  Override

        public abstract void OnEnter(BlackBoard blackboard);

        public abstract void OnExit(BlackBoard blackboard);

        public virtual void OnUpdate(float dt, BlackBoard blackboard)
        {
            Finish();
        }

        public virtual void Reset()
        {
            _isComplete = false;
        }

        public abstract void SetConfigInfo(EdNode cnf);

        public virtual string ToDes()
        {
            return string.Empty;
        }

        #endregion

        #region Public

        public void BindingContext(SequenceLine context, int frameLength)
        {
            _frameLength = frameLength;
            _context = context;
        }

        public void Finish() { _isComplete = true; }

        public bool IsFinish() { return _isComplete; }

        public void RaiseEvent(E_EntityInTrigger key, EventSetData objInfo)
        {
            SkillLog.Assert(_context != null && _context._owner != null, "SequenceLeafNode RaiseEvent:[{0}] Fail", key);
            if (_context == null || _context._owner == null) return;
            _context._owner.RaiseEvent(key, objInfo);
            EventDataFactory.Push(objInfo);
        }

        #endregion

        #region

        public void LogEnter()
        {
            if (!LogManager._openSkill) return;
            LogManager.Log("Time: {1}   Enter [{0}] 节点", ToDes(), TimeModule.FrameCount);
        }

        public void LogExit()
        {
            if (!LogManager._openSkill) return;
            LogManager.Log("Time: {1}   Exit [{0}] 节点,开始跳转到下一个节点 ", ToDes(), TimeModule.FrameCount);
        }

        #endregion

        #region Private Methods

        #endregion
    }
}