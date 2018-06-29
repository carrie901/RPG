
namespace Summer
{

    /// <summary>
    /// Buff的信息，以及一些相关的简单内部逻辑
    /// </summary>
    public class BuffInfo
    {
        #region 属性

        #region 等级状态

        public const int LESS = -1;
        public const int EQUAL = 0;
        public const int GREATER = 1;

        #endregion

        private BuffTemplateInfo _info;
        protected float _duration;                          // buff间隔时间 单位毫秒
        protected float _left_time;                         // 流逝的时间
        protected bool _force_expire;                       // 过期
        protected bool _need_tick;                          // true=需要tick
        protected float _next_tick_time;                    // 下一次执行的效果
        protected float _interval_time;                     // 间隔时间
        protected int _cur_layer;                           // 当前层


        #endregion

        public BuffInfo(BuffTemplateInfo obj)
        {
            _info = obj;
            _cur_layer = 0;

            _need_tick = !MathHelper.IsZero(_interval_time);
            _duration = _info.duration / 1000f;
            _interval_time = _info.interval_time / 1000f;
            ResetTimeOut();
        }

        #region logic Layer

        public bool CanAddLayer()
        {
            if (_cur_layer >= _info.max_layer) return false;
            return true;
        }
        public bool AddLayer()
        {
            if (_cur_layer >= _info.max_layer)
            {
                _cur_layer = _info.max_layer;
                //BuffLog.Log("add layer. buff [{0}] layer reach max[{1}]", _info.id, _info.over_lay);
                return false;
            }
            _cur_layer++;
            return true;
        }
        public bool CanRemoveLayer()
        {
            if (_cur_layer <= 0) return false;
            return true;
        }
        public bool RemoveLayer()
        {
            _cur_layer--;
            if (_cur_layer < 0)
            {
                //BuffLog.Error("Remove Layer. Buff [{0}] Layer Reach 0", _info.id);
                _cur_layer = 0;
                return false;
            }
            return true;
        }

        #endregion

        #region public

        public void OnUpdate(float dt)
        {
            // 1.流逝时间的增加
            _left_time += dt;
        }

        public bool CanTick()
        {
            // 如果结束，不可tick
            if (!_need_tick || _force_expire) return false;

            return _left_time >= _next_tick_time;
        }

        public void OnTick()
        {
            _next_tick_time += _interval_time;
        }

        // 层刷新的时候
        public void OnRefresh() { }

        // 重置超时时间
        public void ResetTimeOut()
        {
            _left_time = 0;
            _next_tick_time = _interval_time;
            _force_expire = false;
        }

        public bool CheckBuffId(int buff_id) { return buff_id == _info.id; }

        public bool CheckGroupById(string group_id) {/* return _info.groupID == group_id;*/ return false; }

        // 检测等级
        public int CheckLevel(int level)
        {
            /*if (_info.level == level)
                return EQUAL;
            else if (_info.level > level)
                return GREATER;
            return LESS;*/
            return EQUAL;
        }

        #endregion

        #region 涉及buff逻辑的相关数据

        public int Id { get { return _info.id; } }
        public int Level { get { return /*_info.level;*/ 1; } }
        // 多层
        public bool Multilayer { get { return _info.max_layer >= 1; } }
        // 是否到达最大层
        public bool IsMaxLayer { get { return _cur_layer == _info.max_layer; } }
        // 是否过期
        public bool ForceExpire { get { return _force_expire; } }
        // true=刷新Buff时间
        public bool RefreshOnAttach { get { return true; } }
        // 持续时间
        public float ExpireDuration { get { return _duration; } }
        public bool NeedTick { get { return _need_tick; } }
        public int CurrLayer { get { return _cur_layer; } }
        public bool DeathDelete { get { /*return _info.death_delete;*/return true; } }

        #endregion


        public string ToDes()
        {
            return _info.desc;
        }
    }
}