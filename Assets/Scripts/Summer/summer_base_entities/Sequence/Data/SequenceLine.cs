
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
    public class SequenceLine : I_Update
    {
        #region 属性

        public float _leftTime;                                             // 剩余时间--->流逝的时间
        public int _curFrame;                                               // 当前的帧数
        public E_Runing _runing = E_Runing.none;
        public bool _runingFlag;
        public int _maxFrame;

        public List<TrackLine> _tracks = new List<TrackLine>();             // 轨道通道
        public BaseEntity _owner;

        #endregion

        #region Override

        public void OnUpdate(float dt)
        {
            if (_runing == E_Runing.none || !_runingFlag) return;

            if (_runing == E_Runing.enter)
                OnEnter();

            if (_runing == E_Runing.update)
            {
                _leftTime += dt;
                OnUpdateLogic();
            }

            if (_runing == E_Runing.exit)
                OnExit();
        }

        public BlackBoard _blackboard = new BlackBoard();
        public virtual BlackBoard GetBlackBorad()
        {
            return _blackboard;
        }

        #endregion

        #region Public

        public void Start()
        {
            _runing = E_Runing.enter;
            _runingFlag = true;
            _lastFrame = TimeManager.FrameCount;
        }

        public void Pause(bool flag) { _runingFlag = flag; }

        public void Finish()
        {
            OnExit();
        }

        public void AddTrack(TrackLine track)
        {
            _tracks.Add(track);
            track.BindingContext(this);
        }

        public void CalEnd()
        {
            int length = _tracks.Count;
            for (int i = 0; i < length; i++)
            {
                TrackLine line = _tracks[i];
                int frameLength = line.FrameLength;
                if (frameLength > _maxFrame)
                    _maxFrame = frameLength;
            }
        }

        #endregion

        #region Sequence的三种 Enter/Update/Exit的状态

        public virtual void OnEnter()
        {
            _runing = E_Runing.update;
            _update_logic();
        }


        public int _lastFrame = 0;
        public virtual void OnUpdateLogic()
        {
            _curFrame++;
            _leftTime -= SequenceLineConst.TIME_INTERVAL;
            _update_logic();
            if (_curFrame >= _maxFrame)
            {
                _runing = E_Runing.exit;
                //break;
            }
            /*while (_left_time >= SequenceLineConst.TIME_INTERVAL)
            {
                _cur_frame++;
                _left_time -= SequenceLineConst.TIME_INTERVAL;
                _update_logic();
                if (_cur_frame >= _max_frame)
                {
                    _runing = E_Runing.exit;
                    break;
                }
            }*/
        }

        public virtual void OnExit()
        {
            _runing = E_Runing.none;
            _init_info();
        }

        #endregion

        #region Private Methods

        public void _update_logic()
        {
            BlackBoard bb = GetBlackBorad();
            int length = _tracks.Count;
            for (int i = 0; i < length; i++)
            {
                TrackLine trakcLine = _tracks[i];
                E_Runing runing = trakcLine.Check(_curFrame);

                if (trakcLine._is_lock(runing)) continue;
                if (runing == E_Runing.enter)
                    trakcLine.OnEnter(bb);

                if (runing == E_Runing.update)
                    trakcLine.OnUpdate(bb);

                if (runing == E_Runing.exit)
                    trakcLine.OnExit(bb);
            }
        }

        public void _init_info()
        {
            _curFrame = 0;
            _leftTime = 0;
            _runingFlag = false;
        }

        #endregion

    }
}