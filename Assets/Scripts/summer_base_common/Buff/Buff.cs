using System.Collections.Generic;
using Summer;


public class Effect
{
    public void Play()
    {

    }
}

/// <summary>
/// 需要把BuffCnf的数据做一层包装，BuffVbo
/// 以防止BuffCnf数据改变的时候，对外部的影响降到最低
/// </summary>
public class Buff : I_ProcessUpdater
{
    #region Param

    public float _expire_duration;                  //持续时间
    public float _timeout;                          //超时时间=当前时间+ duration
    public float _left_time;                        //流逝的时间
    public bool _force_expire;                      //过期
    public BuffCnf _conf;
    public bool _need_use;

    public List<Effect> _effects;
    //TODO msm去修改 相对于细节的多变性，相对而已抽象的东西要稳定的多，目前buff得到角色身上太多的内容(血量，无双值，攻击力等等相关内容），无法进行抽象化/
    //TODO 通过目前的回调机制。把buff相关逻辑转移到了角色身上(本身角色就要提供这样的接口让别人调用，输入的接口唯一化）
    public iCharacterBaseController _target;        //buff释放目标 抽象成接口，依赖倒置
    public iCharacterBaseController _caster;        //buff释放者
    public BuffId _bid;
    public int _id;

    #endregion

    #region virtual Buff -init/add/remove

    public virtual void Init(BuffCnf conf)
    {
        _conf = conf;
        _expire_duration = _conf.duration;
        _id = conf.id;
        _max_layer = _conf.over_lay;
        _left_time = 0;
        _use_time = 0;
        _need_use = !MathHelper.IsZero(_conf.interval_time);
    }

    //如果出现可以叠加,buff层级
    public int _cur_layer;
    public int _max_layer;
    public virtual bool AddLayer()
    {
        //层数已到最高，return
        if (_cur_layer >= _max_layer)
        {
            LogManager.Warning("buff[{0}] layer reach max[{1}]", _conf.id, _conf.over_lay);
            return false;
        }
        _cur_layer++;
        //1.effect
        _add_effect();
        //2.sound 处理
        _playsound_add();
        GameEventSystem.Instance.RaiseEvent(E_GLOBAL_EVT.buff_attach, this);
        return true;
    }

    public virtual void RemoveLayer()
    {
        //层数已经到0
        if (_cur_layer <= 0)
        {
            LogManager.Warning("buff[{0}] layer reach 0]", _id);
            return;
        }

        _cur_layer--;
        //1.effect
        _remove_effect();
        //2.sound
        GameEventSystem.Instance.RaiseEvent(E_GLOBAL_EVT.buff_attach, this);
    }

    #endregion

    public E_BUFF_TYPE Type()
    {
        LogManager.Assert(_conf != null, "Init Before GetType");
        if (_conf == null) return (E_BUFF_TYPE.none);
        return (E_BUFF_TYPE)_conf.sub_type;
    }

    //过期
    public void OnExpire(Timer timer)
    {
        if (_target == null || _target._buff_container == null)
            return;

        _target._buff_container.Remove(this);
    }

    //多层 区分叠加和重叠
    public bool IsMultiLayer() { return _conf.over_lay > 1; }

    #region virtual Buff 提供给BuffSst控制

    //有些buff是过程无效果，上buff和下buff的时候带功能的。例如设置角色朝向/攻击力
    public virtual void OnAttach(iCharacterBaseController target, iCharacterBaseController caster)
    {
        _bid = new BuffId(target.char_id, _id);
        _target = target;
        _caster = caster;
        _effects = new List<Effect>();
        GameEventSystem.Instance.RaiseEvent(E_GLOBAL_EVT.buff_attach, this);
    }
    public virtual void OnDetach()
    {
        GameEventSystem.Instance.RaiseEvent(E_GLOBAL_EVT.buff_detach, this);

        _remove_effect();
        _playsound_remove();
        _effects.Clear();
        _effects = null;
        _caster = null;
        _target = null;
        _conf = null;
        _bid = null;
    }
    public virtual void OnUpdate(float dt)
    {
        _left_time += dt;
        if (_need_use)
        {
            if (_left_time >= _use_time)
            {
                Use();
            }
        }
    }

    public float _use_time;

    //TODO 
    //有些buff只能被使用固定的次数，在使用的时候调用use 预留
    public virtual void Use()
    {
        _use_time += _conf.interval_time;
    }

    #endregion

    #region I_ProcessUpdater

    public bool IsActive() { return _target != null; }

    public bool GetProcess(out float elapse_time, out float duration)
    {
        if (!IsActive())
        {
            elapse_time = 0;
            duration = float.MaxValue;
            return false;
        }

        duration = _expire_duration;
        elapse_time = LogManager.level_time() - (_timeout - _expire_duration);
        return true;
    }
    public void Destroy() { }

    #endregion

    #region Effect

    public void _add_effect()
    {
        iCharacterBaseController caster = ActorFinder.Instance.FindChar(_bid._char_id.iid);//_bid._caster_id.iid
        if (caster == null)
        {
            LogManager.Warning("buff _add_effect, caster[{0}] is null", _bid._char_id.iid);
            return;
        }

        //TODO 
        //遍历配置buff的effect属性
        //调用目标身上的EffectSet添加buff，同时塞到buff的effect中更好的管理
    }

    public void _remove_effect()
    {
        /*if (_effects == null) return;
        foreach (var e in _effects)
        {
            _target._effect_set.Detach(e);
        }*/
    }

    #endregion

    #region sound

    public void _playsound_add()
    {

    }

    public void _playsound_remove()
    {

    }
    #endregion

    public override string ToString()
    {
        if (LogManager.open_debug) return _conf.name;
        return string.Format(
            "id[{0}],name[{4}], owner[{1}] duration[{2}] timeout[{3}]",
            _id, (_target == null ? "" : _target.ToString()), _expire_duration, _timeout, _conf.name);
    }
}
