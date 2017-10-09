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
public class Buff
{

    public BuffVbo vbo;

    public List<Effect> _effects;
    //TODO msm去修改 相对于细节的多变性，相对而已抽象的东西要稳定的多，目前buff得到角色身上太多的内容(血量，无双值，攻击力等等相关内容），无法进行抽象化/
    //TODO 通过目前的回调机制。把buff相关逻辑转移到了角色身上(本身角色就要提供这样的接口让别人调用，输入的接口唯一化）
    public BaseEntities _target;        //buff释放目标 抽象成接口，依赖倒置
    public BaseEntities _caster;        //buff释放者
    public BuffId _bid;                 //Buff的Id属性 包括自身buff所属Id
    public int _id;                     //Buff Id

    private BuffContainer _container;
    public I_BuffParam _param;

    #region virtual Buff -init/add/remove

    public virtual void Init(BuffCnf buff_obj)
    {
        vbo = new BuffVbo(buff_obj);
    }

    public virtual bool AddLayer()
    {
        // 层数已到最高，return
        if (!vbo.CanAddLayer())
            return false;
        vbo.AddLayer();
        // 1.effect
        _add_effect();
        // 2.sound 处理
        _playsound_add();
        //GameEventSystem.Instance.RaiseEvent(E_GLOBAL_EVT.buff_attach, this);
        return true;
    }

    public virtual void RemoveLayer()
    {
        //层数已经到0
        if (!vbo.CanRemoveLayer())
            return;
        vbo.RemoveLayer();
        //1.effect
        _remove_effect();
        //2.sound
        //GameEventSystem.Instance.RaiseEvent(E_GLOBAL_EVT.buff_detach, this);
    }

    #endregion

    //过期
    public void OnExpire(Timer timer)
    {
        if (_target == null || _target._buff_container == null)
            return;

        _target._buff_container.Remove(this);
    }

    //多层 区分叠加和重叠
    public bool IsMultiLayer() { return vbo.Multilayer; }

    #region virtual Buff 提供给BuffSst控制

    //有些buff是过程无效果，上buff和下buff的时候带功能的。例如设置角色朝向/攻击力
    public virtual void OnAttach(BaseEntities target, BaseEntities caster)
    {
        _container = target.GetBuffContainer();
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
        vbo = null;
        _bid = null;
    }
    public virtual void OnUpdate(float dt)
    {
        if (vbo != null && vbo.OnUpdate(dt))
            Use();
    }

    public float _use_time;

    //TODO 
    //有些buff只能被使用固定的次数，在使用的时候调用use 预留
    public virtual void Use()
    {
        vbo.RefreshPreUseTime();
    }

    public bool IsActive() { return _target != null; }

    #endregion

    #region Effect

    public void _add_effect()
    {
        /*BaseEntities caster = null;
        //iCharacterBaseController caster = ActorFinder.Instance.FindChar(_bid._char_id.iid);//_bid._caster_id.iid
        if (caster == null)
        {
            LogManager.Warning("buff _add_effect, caster[{0}] is null", _bid._char_id.iid);
            return;
        }*/

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
        if (LogManager.open_debug) return vbo.info.name;
        return string.Format(
           "name[{1}], owner[{0}]",
           (_target == null ? "" : _target.ToString()), vbo.info.name);
    }
}
