
namespace Summer
{

    /// <summary>
    /// Buff的信息，以及一些相关的简单内部逻辑
    /// </summary>
    public class BuffInfo
    {
        #region Porp

        public BuffCnf info;
        public float duration;                          // buff间隔时间 单位毫秒
        public float _timeout;                          // 超时时间=当前时间+ duration
        public float _internal_time_out;                // 内部结束时间 = _left_time+duration( 主要是和_left_time比较)
        protected float _left_time;                         // 流逝的时间
        public bool _force_expire;                      // 过期
        public bool _need_tick;                         // true=需要tick
        public float _next_tick_time;                   // 下一次执行的效果
        public int _cur_layer;                          // 当前层
        public float _interval_time;                    // 间隔时间

        public float IntervalTime
        {
            get { return _interval_time; }
            set
            {
                _interval_time = value;
                _need_tick = !MathHelper.IsZero(_interval_time);
            }
        }

        #endregion

        public BuffInfo(BuffCnf obj)
        {
           /* info = obj;
            _timeout = 0;

            _cur_layer = 0;
            _need_tick = false;
            IntervalTime = 0;
            _need_tick = !MathHelper.IsZero(IntervalTime);
            _left_time = 0;
            duration = info.duration / 1000f;
            _internal_time_out = _left_time + duration;
            ResetTimeOut(LogManager.level_time());
            // 2=数值修改
            if (obj.effect_type == 2)
            {
                // TODO 头炸的策划数据呀。通过强制性修改，来配合原始框架
                string[] tmp_values = obj.effect_value.Split('|');
                int tmp = int.Parse(tmp_values[0]);
                if (tmp == 2)// 持续减血
                {
                    IntervalTime = int.Parse(tmp_values[1]) / 1000f;
                }
                /*else if (tmp == 1)
                {
                    IntervalTime = int.Parse(tmp_values[1]) / 1000f;
                }#1#
                else if (tmp == 3)
                {
                    IntervalTime = int.Parse(tmp_values[1]) / 1000f;
                }
            }*/
        }

        #region logic Layer

        public bool CanAddLayer()
        {
            if (_cur_layer >= info.over_lay)
                return false;
            return true;
        }
        public bool AddLayer()
        {
            _cur_layer++;
            if (_cur_layer > info.over_lay)
            {
                _cur_layer = info.over_lay;
                Buff.Log("add layer. buff [{0}] layer reach max[{1}]", info.id, info.over_lay);
                return false;
            }
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
                Buff.Error("Remove Layer. Buff [{0}] Layer Reach 0", info.id);
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
            if (!_need_tick) return false;

            if (_left_time >= _internal_time_out) return false;

            return _left_time >= _next_tick_time;
        }

        public void OnTick()
        {
            _next_tick_time += IntervalTime;
        }

        // 层刷新的时候
        public void OnRefresh()
        {

        }

        // 设置是否过期
        public void SetForceExpire(bool force_expire)
        {
            _force_expire = force_expire;
        }

        // 设置超时时间
        public void ResetTimeOut(float timeout)
        {
            _timeout = timeout + duration;
            _internal_time_out = _left_time + duration;
            _next_tick_time = _left_time;
        }

        // 剩余时间
        public float LifeTime()
        {
            return _timeout - TimerHelper.RealtimeSinceStartup();
        }

        public bool CheckBuffId(int buff_id) { return buff_id == info.id; }

        public bool CheckGroupById(string group_id)
        {
            return info.groupID == group_id;
        }

        #endregion

        #region 涉及buff逻辑的相关数据

        public int Id { get { return info.id; } }
        // 多层
        public bool Multilayer { get { return info.over_lay >= 1; } }
        // 是否到达最大层
        public bool IsMaxLayer { get { return _cur_layer >= info.over_lay; } }
        // 是否过期
        public bool IsForceExpire() { return _force_expire; }
        // true=刷新Buff时间
        public bool RefreshOnAttach { get { return true; } }
        // 持续时间
        public float ExpireDuration { get { return duration; } }

        // 死亡是否消失
        public bool DeathDelete { get { return info.death_delete; } }

        public bool NeedTick { get { return _need_tick; } }

        public int CurrLayer { get { return _cur_layer; } }

        #endregion

        public string ToDes()
        {
            return info.desc;
        }
    }
}