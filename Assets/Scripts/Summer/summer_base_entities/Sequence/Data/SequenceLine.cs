
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



        public float _left_time;                                            // 剩余时间--->流逝的时间
        public int _cur_frame;                                              // 当前的帧数
        public E_Runing _runing = E_Runing.none;
        public bool _runing_flag;
        public int _max_frame;

        public List<TrackLine> _tracks = new List<TrackLine>();             // 轨道通道
        public BaseEntity _owner;

        #endregion

        #region Override

        public void OnUpdate(float dt)
        {
            if (_runing == E_Runing.none || !_runing_flag) return;

            if (_runing == E_Runing.enter)
                OnEnter();

            if (_runing == E_Runing.update)
            {
                _left_time += dt;
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
            _runing_flag = true;
            _last_frame = TimeManager.FrameCount;
        }

        public void Pause(bool flag) { _runing_flag = flag; }

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
                int frame_length = line.FrameLength;
                if (frame_length > _max_frame)
                    _max_frame = frame_length;
            }
        }

        #endregion

        #region Sequence的三种 Enter/Update/Exit的状态

        public virtual void OnEnter()
        {
            _runing = E_Runing.update;
            _update_logic();
        }


        public int _last_frame = 0;
        public virtual void OnUpdateLogic()
        {
            _cur_frame++;
            _left_time -= SequenceLineConst.TIME_INTERVAL;
            _update_logic();
            if (_cur_frame >= _max_frame)
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
                TrackLine trakc_line = _tracks[i];
                E_Runing runing = trakc_line.Check(_cur_frame);

                if (trakc_line._is_lock(runing)) continue;
                if (runing == E_Runing.enter)
                    trakc_line.OnEnter(bb);

                if (runing == E_Runing.update)
                    trakc_line.OnUpdate(bb);

                if (runing == E_Runing.exit)
                    trakc_line.OnExit(bb);
            }
        }

        public void _init_info()
        {
            _cur_frame = 0;
            _left_time = 0;
            _runing_flag = false;
        }

        #endregion

    }
}