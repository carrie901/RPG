
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

using System.Collections.Generic;

namespace Summer.Sequence
{
    public class TrackLine
    {
        #region 属性
        public int EndFrame { get { return _eFrame; } }
        private int _sFrame;                                        // 开始的帧数 以0作为起点
        private int _eFrame;                                        // 
        private int _frameLength;
        private List<SequenceLeafNode> _leafs = new List<SequenceLeafNode>(8);
        private SequenceLine _context;

        #endregion

        #region Override


        #endregion

        #region Public

        public TrackLine(int startFrame, int frameLength)
        {
            SkillLog.Assert(startFrame >= 0 && frameLength >= 0, "序列中的通道帧数据不合格,起始帧数:[{0}],长度:[{1}]", startFrame, frameLength);
            _sFrame = startFrame;
            _frameLength = frameLength;
            _eFrame = _sFrame + _frameLength;
        }

        public void BindingContext(SequenceLine context)
        {
            _context = context;
        }

        public void AddNode(SequenceLeafNode node)
        {
            _leafs.Add(node);
            node.BindingContext(_context, _frameLength);
        }

        public void Update(int currFrame, BlackBoard bb)
        {
            if (currFrame < _sFrame) return;
            if (currFrame > _eFrame) return;

            if (currFrame == _sFrame)
                OnEnter(bb);

            if (currFrame >= _sFrame)
                OnUpdate(bb);

            if (currFrame == _eFrame)
                OnExit(bb);
        }

        public void Reset()
        {
            int length = _leafs.Count;
            for (int i = 0; i < length; i++)
            {
                _leafs[i].Reset();
            }
        }

        #endregion

        #region  virtual

        public virtual void OnEnter(BlackBoard bb)
        {
            int length = _leafs.Count;
            for (int i = 0; i < length; i++)
            {
                SequenceLeafNode node = _leafs[i];
                if (node.IsFinish()) continue;
                node.OnEnter(bb);
            }
        }

        public virtual void OnExit(BlackBoard bb)
        {
            int length = _leafs.Count;
            for (int i = 0; i < length; i++)
            {
                SequenceLeafNode node = _leafs[i];
                if (node.IsFinish()) continue;
                node.OnExit(bb);
            }
        }

        public virtual void OnUpdate(BlackBoard bb)
        {
            int length = _leafs.Count;
            for (int i = 0; i < length; i++)
            {
                SequenceLeafNode node = _leafs[i];
                if (node.IsFinish()) continue;
                node.OnUpdate(SequenceLineConst.TIME_INTERVAL, bb);
            }
        }

        #endregion

        #region Private Methods
        #endregion
    }
}