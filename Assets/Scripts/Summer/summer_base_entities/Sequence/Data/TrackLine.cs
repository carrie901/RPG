
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
        public int _sFrame;                                        // 开始的帧数 以0作为起点
        public int _eFrame;                                        // 
        public int _frameLength;
        public int FrameLength { get { return _sFrame + _frameLength; } }
        public List<SequenceLeafNode> _leafs = new List<SequenceLeafNode>();

        public SequenceLine _context;

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

        public E_Runing Check(int frame)
        {
            if (frame < _sFrame) return E_Runing.none;
            if (frame == _sFrame) return E_Runing.enter;
            if (frame > _sFrame && frame < _eFrame) return E_Runing.update;
            if (frame == _eFrame) return E_Runing.exit;
            if (frame > _eFrame) return E_Runing.complete;

            return E_Runing.none;
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

        public bool _is_lock(E_Runing runing)
        {
            if (runing == E_Runing.none || runing == E_Runing.complete) return true;
            return false;
        }

        #endregion
    }
}