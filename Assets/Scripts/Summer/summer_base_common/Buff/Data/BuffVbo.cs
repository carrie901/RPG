
namespace Summer
{
    /// <summary>
    /// 影影约约的感觉要把BuffObj整体包裹到BuffVbo下
    /// 目前是把一些相关的Buff涉及的逻辑数据包裹下，其他一些描述性的数据不做处理
    /// </summary>
    public class BuffVbo
    {
        public BuffCnf info;

        public E_BUFF_TYPE _buff_type;
        public float _timeout;                           //超时时间=当前时间+ duration
        public float _left_time;                         //流逝的时间
        public bool _force_expire;                       //过期
        public bool _need_use;
        public int _cur_layer;                           //当前层
        public float _use_time;


        public BuffVbo(BuffCnf obj)
        {
            info = obj;
            _timeout = 0;
            _left_time = 0;
            _use_time = 0;
            _cur_layer = 0;
            _buff_type = (E_BUFF_TYPE)info.sub_type;
            _need_use = !MathHelper.IsZero(obj.interval_time);
        }
        public bool CanAddLayer()
        {
            if (_cur_layer >= info.over_lay)
                return false;
            return true;
        }
        public bool CanRemoveLayer()
        {
            if (_cur_layer <= 0)
                return false;
            return true;
        }
        public void AddLayer()
        {
            _cur_layer++;
            if (_cur_layer > info.over_lay)
                LogManager.Error("buff[{0}] layer reach max[{1}]", info.id, info.over_lay);
            _cur_layer = info.over_lay;
        }
        public void RemoveLayer()
        {
            _cur_layer--;
            if (_cur_layer < 0)
                LogManager.Warning("buff[{0}] layer reach 0]", info.id);
            _cur_layer = 0;
        }
        public bool OnUpdate(float dt)
        {
            _left_time += dt;
            if (_need_use)
            {
                if (_left_time >= _use_time)
                {
                    return true;
                }
            }
            return false;
        }
        public void RefreshPreUseTime()
        {
            _use_time += info.interval_time;
        }
        public bool Multilayer { get { return info.over_lay >= 1; } }
        public bool RefreshOnAttach { get { return true; } }
        // 设置超时时间
        public void SetTimeOut(float ftimeout)
        {
            _timeout = ftimeout + info.duration;
        }
        // 是否过期
        public bool IsForceExpire() { return _force_expire; }
        // 设置是否过期
        public void SetForceExpire(bool force_expire) { _force_expire = force_expire; }

        public bool CheckBuffId(int buff_id) { return buff_id == info.id; }

        public bool CheckBuffType(E_BUFF_TYPE type)
        {
            return type == _buff_type;
        }
        #region 涉及buff逻辑的相关数据

        public int Id { get { return info.id; } }
        // 持续时间
        public float ExpireDuration { get { return info.duration; } }
        // 死亡是否消失
        public bool DeathDelete { get { return info.death_delete; } }
        // Buff的类型
        public E_BUFF_TYPE BuffType { get { return _buff_type; } }

        #endregion
        public string ToDes()
        {
            return string.Empty;
        }
    }
}