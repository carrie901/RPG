using System.Collections.Generic;
using Summer;
using UnityEngine;


/// BuffContainer 是iCharacterBaseController身上管理attach，detach，on timer，等等的功能
/// 因为buff是强逻辑模块，有很多上层的逻辑，会访问到iCharacterBaseController的各个member
/// 所以暂时预估相当长一段时间内，buff是要完整的own整个iCharacterBaseController，很难提炼一个interface注入 
/// 目前自己也没有太好的方法去处理这块关于own的,
/// 
/// 8.23 mashaomin
/// what:由于Buff是强逻辑模块,必定会own整个iCharacterBaseController，目前是把整体的逻辑迁移到iCharacterBaseControll中去
/// 1.角色本身需要对外提供这样的方法来改变自己的属性和状态 输入唯一
/// 2.完全剥离buff和iCharacterBaseController
/// how:通过Buff的回调机制,内部用的是EventSet来通知来传给给角色自己想做什么事
/// why:通过回调机制部分解耦buff和iCharacterBaseController之间的关系 以方面后期把iCharacterBaseController变成一个interface注入到其中
public class BuffContainer : MonoBehaviour
{
    public float last_level_cost;

    public void Awake()
    {
        //gameObject.AddComponent<TestBuff>();
    }


    private void Update()
    {
        float dt = LogManager.level_time() - last_level_cost;
        last_level_cost = LogManager.level_time();
        OnUpdate(dt);

    }

    public iCharacterBaseController _owner;
    List<Buff> _buff_list;
    public Dictionary<long, Timer> _buff_expire_timer; //用caster_iid + buff_id做key

    List<Buff> _tmp_to_del;
    public void Init(iCharacterBaseController c)
    {
        _owner = c;
        _buff_list = new List<Buff>();
        _tmp_to_del = new List<Buff>(8);
        _buff_expire_timer = new Dictionary<long, Timer>();
    }

    public void Clear()
    {
        int length = _buff_list.Count;
        for (int i = 0; i < length; i++)
        {
            _buff_list[i].OnDetach();
        }


        foreach (var timer in _buff_expire_timer)
        {
            timer.Value.Cancel();
        }
        _buff_expire_timer.Clear();

        _buff_list.Clear();
        _update_attr(0);
    }

    public void Destroy()
    {
        Clear();
        _owner = null;
        _buff_list = null;
        _buff_expire_timer = null;
    }

    public void OnUpdate(float dt)
    {
        //detach掉失效的buff
        _force_expire(dt);
        _expire_attachment_destroy(dt);

        //让在update中发挥作用的buff马上生效
        _update_attr(dt);
    }

    public void Remove(Buff buff)
    {
        if (!_buff_list.Contains(buff))
            return;
        DetachBuff(_owner, buff._bid);
    }

    public void RemoveAll()
    {
        int length = _buff_list.Count;
        for (int i = length - 1; i >= 0; i--)
        {
            Remove(_buff_list[i]);
        }
    }

    public List<Buff> GetBuffList()
    {
        return _buff_list;
    }

    public bool HasBuff(int buff_id)
    {
        int length = _buff_list.Count;
        for (int i = 0; i < length; i++)
        {
            if (_buff_list[i]._id == buff_id)
            {
                return true;
            }
        }
        return false;
    }

    public bool IsMultiLayer()
    {
        return false;
    }

    #region Attach & Detach 

    /// <summary>
    /// 提供给外部 添加Buff 处理buff之间的相互关系  重叠/替换/抵消
    /// </summary>
    /// <param name="caster">释放buff者</param>
    /// <param name="buff_id">Buff的Id</param>
    public void AttachBuff(iCharacterBaseController caster, int buff_id)
    {
        // 1.查找指定的buff
        Buff exist_buff = _get_exist_buff(buff_id);

        // 2.不存在buff，则Attach
        if (exist_buff == null)
        {
            Buff new_buff = _attach_buff(caster, buff_id);
            new_buff.AddLayer();
        }
        else
        {
            //3.buff已经存在，buff允许多层状态
            if (exist_buff.IsMultiLayer())
            {
                //存在buff，可叠加buff，加一层layer，并判断是否需要refresh_timer
                //刷新数据
                exist_buff.AddLayer();
                if (true)//强制刷新
                //if (exist_buff._conf.refresh_on_attach)
                {
                    //refresh timer
                    _remove_expire_timer(exist_buff._bid);
                    _add_expire_timer(exist_buff);
                }
            }
            else
            {
                //不可叠加的buff，refresh一下
                //refresh timer
                _remove_expire_timer(exist_buff._bid);
                _add_expire_timer(exist_buff);
            }
        }

        _update_attr(0);//让在update中发挥作用的buff马上生效
    }

    //提供给外部 移除Buff
    public void DetachBuff(iCharacterBaseController caster, BuffId buff_id)
    {
        Buff exist_buff = _get_exist_buff(buff_id._buff_id);
        if (exist_buff == null) return;//不存在buff，return
        _detach_buff(buff_id);
        exist_buff.RemoveLayer();
        _update_attr(0);//让在update中发挥作用的buff马上生效
    }

    #endregion

    #region internal (Attach & Detach)

    public Buff _attach_buff(iCharacterBaseController caster, int buff_id)
    {
        //1.创建一个Buff
        Buff buff = BuffFactoryMethod.Create(buff_id);

        //2.Buff添加到目标身上
        buff.OnAttach(_owner, caster);


        //3.add to list, by priority, 目前没有priority的设计
        _buff_list.Add(buff);

        //4.set new timer
        _add_expire_timer(buff);
        LogManager.Log("_attach_buff [{0}]", buff.ToString());
        return buff;
    }

    //detach所有同id的buff
    public void _detach_buff(BuffId buff_id)
    {
        _remove_expire_timer(buff_id);

        _tmp_to_del.Clear();
        //get buff with id
        int length = _buff_list.Count;
        for (int i = 0; i < length; i++)
        {
            if (_buff_list[i]._bid.BuffEqual(buff_id._buff_id))
            {
                _tmp_to_del.Add(_buff_list[i]);
            }

        }

        length = _tmp_to_del.Count;
        for (int i = 0; i < length; i++)
        {
            _buff_list.Remove(_tmp_to_del[i]);
            LogManager.Log("_detach_buff [{0}]", _tmp_to_del[i].ToString());
            _tmp_to_del[i].OnDetach();

        }
    }

    #endregion

    #region Internal

    //添加超时机制
    public void _add_expire_timer(Buff buff)
    {
        //set new timer
        Timer new_timer = Timer.AddTimer(buff._expire_duration, buff.OnExpire);
        _buff_expire_timer.Add(buff._bid._iid, new_timer);
        buff._timeout = LogManager.level_time() + buff._expire_duration;
    }

    //移除超时
    public void _remove_expire_timer(BuffId bid)
    {
        //remove old timer
        Timer old_timer;
        _buff_expire_timer.TryGetValue(bid._iid, out old_timer);
        if (old_timer != null)
        {
            old_timer.Cancel();
            _buff_expire_timer.Remove(bid._iid);
        }
    }

    //是否存在buff
    public Buff _get_exist_buff(int buff_id)
    {
        foreach (var v in _buff_list)
        {
            if (v._bid.BuffEqual(buff_id))
            {
                return v;
            }
        }
        return null;
    }

    //删除过期
    public void _force_expire(float dt)
    {
        _tmp_to_del.Clear();
        int length = _buff_list.Count;
        for (int i = 0; i < length; i++)
        {
            Buff buff = _buff_list[i];
            if (buff._force_expire)
            {
                _tmp_to_del.Add(buff);
            }
        }
        length = _tmp_to_del.Count;
        for (int i = 0; i < length; i++)
        {
            _tmp_to_del[i].OnExpire(null);
        }
    }


    //依附的主体被摧毁
    public void _expire_attachment_destroy(float dt)
    {
        _tmp_to_del.Clear();
        int length = _buff_list.Count;
        for (int i = 0; i < length; i++)
        {
            Buff buff = _buff_list[i];
            if (!buff.IsActive())
            {
                _tmp_to_del.Add(buff);
            }
        }
        length = _tmp_to_del.Count;
        for (int i = 0; i < length; i++)
        {
            _tmp_to_del[i].OnExpire(null);
        }
    }

    public void _update_attr(float dt)
    {
        int length = _buff_list.Count;
        for (int i = 0; i < length; i++)
        {
            _buff_list[i].OnUpdate(dt);
        }
    }

    #endregion
}

