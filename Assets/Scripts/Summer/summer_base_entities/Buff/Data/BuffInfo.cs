using System.Collections.Generic;

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

        protected BuffTemplateInfo _info;
        protected float _duration;                              // buff间隔时间 单位毫秒
        protected float _leftTime;                              // 流逝的时间
        protected bool _forceExpire;                            // 过期
        protected bool _needTick;                               // true=需要tick
        protected float _nextTickTime;                          // 下一次执行的效果
        protected float _intervalTime;                          // 间隔时间
        protected int _curLayer;                                // 当前层


        #endregion

        public BuffInfo(BuffTemplateInfo obj)
        {
            _info = obj;
            _curLayer = 0;

            _needTick = !MathHelper.IsZero(_intervalTime);
            _duration = _info.duration / 1000f;
            _intervalTime = _info.interval_time / 1000f;
            ResetTimeOut();
        }

        #region logic Layer

        public bool CanAddLayer()
        {
            if (_curLayer >= _info.max_layer) return false;
            return true;
        }
        public bool AddLayer()
        {
            if (_curLayer >= _info.max_layer)
            {
                _curLayer = _info.max_layer;
                //BuffLog.Log("add layer. buff [{0}] layer reach max[{1}]", _info.id, _info.over_lay);
                return false;
            }
            _curLayer++;
            return true;
        }
        public bool CanRemoveLayer()
        {
            if (_curLayer <= 0) return false;
            return true;
        }
        public bool RemoveLayer()
        {
            _curLayer--;
            if (_curLayer < 0)
            {
                //BuffLog.Error("Remove Layer. Buff [{0}] Layer Reach 0", _info.id);
                _curLayer = 0;
                return false;
            }
            return true;
        }

        #endregion

        #region public

        public void OnUpdate(float dt)
        {
            // 1.流逝时间的增加
            _leftTime += dt;
        }

        public bool CanTick()
        {
            // 如果结束，不可tick
            if (!_needTick || _forceExpire) return false;

            return _leftTime >= _nextTickTime;
        }

        public void OnTick()
        {
            _nextTickTime += _intervalTime;
        }

        // 层刷新的时候
        public void OnRefresh() { }

        // 重置超时时间
        public void ResetTimeOut()
        {
            _leftTime = 0;
            _nextTickTime = _intervalTime;
            _forceExpire = false;
        }

        public bool CheckBuffId(int buffId) { return buffId == _info.id; }

        public bool CheckGroupById(string groupId) {/* return _info.groupID == group_id;*/ return false; }

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

        public List<EffectTemplateInfo> GetEffs()
        {
            return _info._effs;
        }

        #endregion

        #region 涉及buff逻辑的相关数据

        public int Id { get { return _info.id; } }
        public int Level { get { return /*_info.level;*/ 1; } }
        // 多层
        public bool Multilayer { get { return _info.max_layer >= 1; } }
        // 是否到达最大层
        public bool IsMaxLayer { get { return _curLayer == _info.max_layer; } }
        // 是否过期
        public bool ForceExpire { get { return _forceExpire; } }
        // true=刷新Buff时间
        public bool RefreshOnAttach { get { return true; } }
        // 持续时间
        public float ExpireDuration { get { return _duration; } }
        public bool NeedTick { get { return _needTick; } }
        public int CurrLayer { get { return _curLayer; } }
        public bool DeathDelete { get { /*return _info.death_delete;*/return true; } }

        #endregion


        public string ToDes()
        {
            return _info.desc;
        }
    }
}