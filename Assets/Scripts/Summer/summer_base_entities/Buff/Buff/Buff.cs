using System.Collections.Generic;
using Summer;


/// <summary>
/// 1.Buff的逻辑和Buff数据要分离
/// 2.Buff数据(小部分逻辑和静态数据)和buff静态数据分离
/// 
/// TODO 10.20
/// 效果执行的触发点有一下几个点
/// 1.特定效果的触发，比如反弹，护盾,回血
/// 2.在添加Buff的时候立马执行，再失去Buff的时候消除
/// 3.在添加层的时候，刷新效果
/// 4.间隔时间触发效果 
/// 
/// TODO 10.26
/// 触发流程:
///     1.注册事件, 提示这个事件能触发什么效果
///     2.触发事件, 提示这个事件会执行什么效果
///     3.执行效果, 显示执行效果的前后数据
/// 移除流程:
/// 
/// TODO 11.30 触发效果的时候，如果因为效果引发连环效果，比如死亡，比如移除等会引发各种效果
///     触发效果
///     效果结束 这两个地方都会引发连环效果
/// </summary>
public class Buff
{
    #region prop

    public BuffInfo info;
    public BuffId _bid;
    public List<TriggerEffect> _effs = new List<TriggerEffect>();               //效果

    //TODO 相对于细节的多变性，抽象的东西要稳定的多，目前buff得到角色身上太多的内容(血量，无双值，攻击力等等相关内容），无法进行抽象化/
    public BaseEntity _caster;        //buff释放者
    public BaseEntity _target;        //buff释放目标 抽象成接口，依赖倒置

    #endregion

    #region virtual Buff -init

    public virtual void Init(BuffCnf buff_obj)
    {
        info = new BuffInfo(buff_obj);
    }

    #endregion

    #region virtual Buff 提供给BuffContainer控制

    //有些buff是过程无效果，上buff和下buff的时候带功能的。例如设置角色朝向/攻击力
    public virtual void OnAttach(BaseEntity target, BaseEntity caster)
    {
        /*_bid = new BuffId(target.entity_id.Eid, info.Id);
        _target = target;
        _caster = caster;
        // 1.默认为1层
        //vbo.OnDefaultLayer();
        // 2.初始化效果
        _init_effs();
        // 3.添加VFX/Sound
        _add_Vfx();
        _add_sound();
        // 4.触发OnAttach回调
        _internal_buff_trigger(E_AbilityTrigger.buff_on_attach);
        GameEventSystem.Instance.RaiseEvent(E_GLOBAL_EVT.buff_attach, this);*/
    }

    public virtual void OnDetach()
    {

        GameEventSystem.Instance.RaiseEvent(E_GLOBAL_EVT.buff_detach, this);
        // 1.移除vfx/sound
        _remove_Vfx();
        _remove_sound();
        // 2.触发OnDeatch回调,effect
        int length = _effs.Count;
        _internal_buff_trigger_on_detach();
        for (int i = 0; i < length; i++)
            _effs[i].OnDetach();
        _effs.Clear();
        _caster = null;
        _target = null;
        info = null;
        _bid = null;
    }
    public virtual void OnUpdate(float dt)
    {
        if (info == null) return;
        info.OnUpdate(dt);
        if (info.CanTick())
        {
            info.OnTick();
            OnTick();
        }
    }

    protected virtual void OnTick()
    {
        //_internal_buff_trigger(E_AbilityTrigger.buff_on_tick);
    }

    #endregion

    #region Add/Remove Layer 层级的处理外抛 因为层级的添加是由可能由两个Buff之间导致的

    public virtual bool AddLayer()
    {
        // 1.层数已到最高，return
        if (!info.CanAddLayer()) return false;
        // 2.层级+1
        bool result = info.AddLayer();
        //  3.触发到达最大层数
        return result;
    }

    public virtual bool RemoveLayer()
    {
        // 1.层数已经到0
        if (!info.CanRemoveLayer()) return false;
        // 2.层级-1
        bool result = info.RemoveLayer();
        //_internal_buff_trigger(E_AbilityTrigger.buff_remove_layer);
        return result;
    }

    #endregion

    #region 由外部触发加层和减层的

    public void TriggerAddLayer()
    {
        /*_internal_buff_trigger(E_AbilityTrigger.buff_add_layer);
        if (info != null && info.IsMaxLayer)
        {
            BuffLayerMaxEventBuff data = EventDataFactory.Pop<BuffLayerMaxEventBuff>();
            data.buff = this;
            _internal_buff_trigger(E_AbilityTrigger.buff_layer_max, data);
        }*/
    }

    public void TriggerRemoveLayer()
    {
        //_internal_buff_trigger(E_AbilityTrigger.buff_remove_layer);
    }

    #endregion

    #region public

    public void RemoveSelf()
    {
        //_target._buff_container.Remove(this);
    }

    //过期函数
    public void OnExpire(Timer timer)
    {
        /*if (_target == null || _target._buff_container == null)
            return;

        _target._buff_container.Remove(this);*/
    }

    public bool IsActive() { return _target != null; }

    #endregion

    #region  播放特效

    private PoolVfxObject effect_obj;
    public void _add_Vfx()
    {
        /*//TODO 
        //遍历配置buff的effect属性
        //调用目标身上的EffectSet添加buff，同时塞到buff的effect中更好的管理
        if (string.IsNullOrEmpty(info.info.effect) || string.IsNullOrEmpty(info.info.bind_pos))
            return;
        effect_obj = _target.AddVfx(info.info.effect, info.info.bind_pos);*/
    }

    public void _remove_Vfx()
    {
        /*if (effect_obj == null) return;
        bool result = _target.RemoveVfx(effect_obj);
        LogManager.Assert(result, "删除特效失败", info.info.effect);*/
    }

    #endregion

    #region 播放声音

    public void _add_sound()
    {
        /* if (info.info.sound == 0) return;
         SoundManager.instance.Play(info.info.sound);*/
    }

    public void _remove_sound() { }

    #endregion

    #region 初始化buff中的效果

    public void _init_effs()
    {
       /* _effs.Clear();
        //if (info.info.effect_type == 1)
        {
            E_AbilityTrigger trigger = info.NeedTick ? E_AbilityTrigger.buff_on_tick : E_AbilityTrigger.buff_add_layer;
            _init_eff(_effs, info.info.effect_type, info.info.effect_value, trigger);
        }
        //else
        //    UnityEngine.Debug.LogError("本类型的Buff没有做:" + info.info.effect_type);
        // _init_eff(_effs, info.info.effect_1, (E_AbilityTrigger)info.info.callback_1);
        //_init_eff(_effs, info.info.effect_2, (E_AbilityTrigger)info.info.callback_2);
        //_init_eff(_effs, info.info.effect_3, (E_AbilityTrigger)info.info.callback_3);*/
    }

    public void _init_weapon_effect()
    {

    }

    public void _init_model_effect()
    {

    }

    // TODO 如果可以发挥，最好吧所有的效果给归类话
    public void _init_eff(List<TriggerEffect> efs, int big_effect_type, string param, E_AbilityTrigger trigger)
    {
        if (big_effect_type == 0)
            return;
        string[] data = param.Split('|');
        // TODO 防御性检测不做了
        int sub_type = int.Parse(data[0]);

        string[] tmps = new string[data.Length - 1];

        for (int i = 0; i < tmps.Length; i++)
            tmps[i] = data[i + 1];

        TriggerEffect trig_eff = new TriggerEffect(this, _target);
        E_EffectType effect_type = (E_EffectType)big_effect_type;
        trig_eff.InitData(effect_type, sub_type, tmps, trigger);
        if (trig_eff._effect == null) return;

        trig_eff.OnAttach();
        efs.Add(trig_eff);
    }

    #endregion

    #region Buff 内部的触发 回调

    public void _internal_buff_trigger(E_AbilityTrigger trigger, EventSetData data = null)
    {
        //if (_effs == null) return;
        Log("Buff Self Raise: [{1}] event,[{0}] Left Time:[{2}] ", info.ToDes(), trigger, info.LifeTime());
        int length = _effs.Count;
        for (int i = 0; i < length; i++)
        {
            //if (_end||_effs[i] == null) continue;
            if (!_effs[i].IsTrigger(trigger)) continue;
            _effs[i].OnExcute(data);
        }
        EventDataFactory.Push(data);
    }

    public void _internal_buff_trigger_on_detach()
    {
        if (_effs == null) return;
        Log("Buff Self Raise: [{1}] event,[{0}] Left Time:[{2}] ", info.ToDes(), E_AbilityTrigger.buff_on_detach, info.LifeTime());
        int length = _effs.Count;
        for (int i = 0; i < length; i++)
        {
            //if (_end) continue;
            //if (_effs[i] == null)continue;
            _effs[i].OnReverse();
        }
    }

    #endregion

    #region Log日志

    public static void Log(string message)
    {
        if (!LogManager.open_debug_buff) return;
        LogManager.Log(message);
    }

    public static void Log(string message, params object[] args)
    {
        if (!LogManager.open_debug_buff) return;
        LogManager.Log(message, args);
    }

    public static void Asset(bool condition, string message)
    {
        if (!LogManager.open_debug_buff) return;
        LogManager.Assert(condition, message);
    }

    public static void Asset(bool condition, string message, params object[] args)
    {
        if (!LogManager.open_debug_buff) return;
        LogManager.Assert(condition, message, args);
    }

    public static void Error(string message)
    {
        if (!LogManager.open_debug_buff) return;
        LogManager.Error(message);
    }

    public static void Error(string message, params object[] args)
    {
        if (!LogManager.open_debug_buff) return;
        LogManager.Error(message, args);
    }

    #endregion
}
