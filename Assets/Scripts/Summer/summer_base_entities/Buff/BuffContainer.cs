using System.Collections.Generic;
using Summer;
using UnityEngine;

namespace Summer
{
    /// BuffContainer 是BaseEntities身上管理attach，detach，on timer，等等的功能
    /// 因为buff是强逻辑模块，有很多上层的逻辑，会访问到BaseEntities的各个member
    /// 所以暂时预估相当长一段时间内，buff是要完整的own整个BaseEntities，很难提炼一个interface注入 
    /// 目前自己也没有太好的方法去处理这块关于own的,
    /// 
    /// TODO 8.23 mashaomin
    /// what:由于Buff是强逻辑模块,必定会own整个BaseEntities，目前是把整体的逻辑迁移到iCharacterBaseControll中去
    /// 1.角色本身需要对外提供这样的方法来改变自己的属性和状态 输入唯一
    /// 2.完全剥离buff和BaseEntities
    /// how:通过Buff的回调机制,内部用的是EventSet来通知来传给给角色自己想做什么事
    /// why:通过回调机制部分解耦buff和BaseEntities之间的关系 以方面后期把BaseEntities变成一个interface注入到其中
    /// 回答9.27 
    /// buff针对三个方面做出了修改，可以从这方面进行提炼接口
    /// 1.属性值 体力/攻击力/防御力/速度等相关属性 
    /// 2.数值 血量/魔法/能量/无双值等等
    /// 3.状态 眩晕/禁魔/等状态
    /// 
    /// 
    /// TODO 9.27 删除可能会有问题  整体删除的逻辑不明确
    /// 问题:
    /// 根据Id删除，根据buff类型删除，由于角色身上同一个Buff的Id可以挂在好几个/Buff的层级
    /// 
    /// 移除目前有2种移除
    /// 1.被动自身删除，buff自身已经过期/或者buff已经失效(护盾)
    /// 2.主动删除，被驱散
    /// 目前删除的接口比较混乱/整体删除逻辑不明确
    /// 
    /// 
    /// TODO 10.20 对整个Buff做了调整
    /// 以结构BuffContainer--->Buff--->Effect
    /// Effect: 效果，指具体的动作，包括修改指定属性，修改指定数据，修改状态等等，可以被Skill/实体/BuffCnf/AOEObj等等应用
    /// Buff:包含了多个效果，Buff是一个控制器 ，控制各个效果如何出现，Buff触发点
    /// BuffContainer: 人物身上Buff的管理器，管理Buff的添加移除，处理Buff的重叠覆盖还有抵消等Buff之间的关系
    /// 
    /// TODO 10.20  问题
    /// 在层级处理方面还是有问题 以及如果Buff之间有多个特效的播放
    ///  问题1：叠加层级时候特效的播放机制，数据更新
    /// 间隔时间的处理
    public class BuffContainer : I_Update
    {
        private float _last_time;
        protected BaseEntity _owner;
        protected List<Buff> _buff_list = new List<Buff>();
        protected Dictionary<long, Timer> _buff_expire_timer
            = new Dictionary<long, Timer>();                        //用caster_iid + buff_id做key
        protected List<Buff> _tmp_to_del = new List<Buff>(8);

        #region OnUpdate

        public void OnUpdate(float dt)
        {
            //detach掉失效的buff
            _force_expire(dt);
            _expire_attachment_destroy(dt);

            //让在update中发挥作用的buff马上生效
            _update_attr(dt);
        }

        #endregion

        #region public

        public void OnReset(BaseEntity c)
        {
            _owner = c;
            Buff.Asset(_tmp_to_del.Count == 0, "_tmp_to_del: 不为0");
            Buff.Asset(_buff_expire_timer.Count == 0, "_buff_expire_timer: 不为0");
            Buff.Asset(_buff_list.Count == 0, "_buff_list: 不为0");
        }



        public void Remove(int buff_id)
        {
            int length = _buff_list.Count;
            for (int i = length - 1; i >= 0; i--)
            {
                if (_buff_list[i].info.CheckBuffId(buff_id))
                {
                    DetachBuff(_buff_list[i]._bid);
                }
            }
        }

        public void Remove(Buff buff)
        {
            DetachBuff(buff._bid);
        }

        public void RemoveByDead()
        {
            int length = _buff_list.Count;
            for (int i = length - 1; i >= 0; i--)
            {
                if (_buff_list[i].info.info.death_delete)
                    Remove(_buff_list[i]);
            }
        }

        public List<Buff> FindBuffList()
        {
            return _buff_list;
        }

        #endregion

        #region Attach & Detach 

        // 提供给外部 caster(释放buff者)给自身owner添加Buff 处理buff之间的相互关系  重叠/替换/抵消
        public void AttachBuff(BaseEntity caster, int buff_id)
        {
            LogManager.Assert(!_owner.IsDead(), "目标[{0}]已经死亡,无法添加Buff:[{1}]", _owner.ToDes(), buff_id);
            if (_owner.IsDead()) return;


            // 1.根据Id查找对应的ID
            BuffCnf new_cnf = BuffHelper.FindBuffById(buff_id);
            Buff.Log("Attach Buff [{0}]", new_cnf.desc);
            int count = 0;
            int length = _buff_list.Count;
            for (int i = length - 1; i >= 0; i--)
            {
                if (!_buff_list[i].info.CheckGroupById(new_cnf.groupID)) continue;
                count++;
                if (count != 1) continue;

                int new_buff_level = new_cnf.level;
                // 1/0/-1 1=本buff等级更高，0=等级相等，-1=本buff等级会第一点
                BuffInfo old_info = _buff_list[i].info;
                if (new_buff_level == old_info.info.level && old_info.Multilayer)// 等级相等,并且可以层级叠加
                {
                    _internal_buff_overlap(_buff_list[i], new_cnf);
                }
                else if (new_buff_level == old_info.info.level && !old_info.Multilayer)
                {
                    Buff.Log("Buff:[{0}],不可叠加", old_info.ToDes());
                    _internal_buff_refresh_time(_buff_list[i]);
                }
                else if (new_buff_level < _buff_list[i].info.info.level)// 新buff等级低
                {

                    Buff.Log("新Buff:[{0}],levell:[{1}],老Buff[{2}],level:[{3}]",
                        new_cnf.desc, new_cnf.level, old_info.info.desc, old_info.info.level);
                    continue;
                }
                else // 新buff等级高
                {
                    _internal_buff_overlay(caster, _buff_list[i], new_cnf);
                }

                _update_attr(0);//让在update中发挥作用的buff马上生效
            }
            if (count == 0)
            {
                _internal_add_new_buff(caster, new_cnf);
            }
            else if (count > 1)
                LogManager.Error("Buff Group Id 超过1个数量");
        }

        //提供给外部 移除Buff
        // TODO 移除的这块逻辑，目前不是特别明确
        public void DetachBuff(BuffId buff_id)
        {
            // 1.不存在buff，return
            Buff exist_buff = _get_exist_buff(buff_id._buff_id);
            if (exist_buff == null) return;

            // 2.存在就移除当前buff
            _detach_buff(exist_buff._bid);

            //exist_buff.RemoveLayer();
            //_update_attr(0);//让在update中发挥作用的buff马上生效
        }

        #endregion

        #region internal (Attach & Detach)

        // caster（施法者) 释放 Buff Apply在owner（目标身）上
        public Buff _attach_buff(BaseEntity caster, int buff_id)
        {
            // caster==施法者
            // 1.创建一个Buff
            Buff buff = BuffFactoryMethod.Create(buff_id);
            //Buff.Log("Attach Buff-->[{0}] add [{1}] Buff", _owner.ToDes(), buff.info.ToDes());
            // 2.Buff添加到目标身上
            buff.OnAttach(_owner, caster);

            // 3.add to list, by priority, 目前没有优先的设计
            _buff_list.Add(buff);

            // 4.set new timer
            _add_expire_timer(buff);
            return buff;
        }

        //detach所有同id的buff
        public void _detach_buff(BuffId bid)
        {

            // 1.移除定时器
            _remove_expire_timer(bid);

            int length = _buff_list.Count - 1;
            for (int i = length; i >= 0; i--)
            {
                if (_buff_list[i]._bid.CheckByIid(bid))
                {
                    Buff buff = _buff_list[i];
                    Buff.Log("Detach Buff-->[{0}] detach [{1}] Buff", _owner.ToDes(), buff.info.ToDes());

                    // 2.从Buff容器中移除
                    _buff_list.Remove(buff);
                    // 3.移除触碰器
                    //RemoveTriggerEvent(buff);
                    // 4.buff自身移除
                    buff.OnDetach();

                }
            }
        }

        #endregion

        #region private

        // 添加超时机制
        public void _add_expire_timer(Buff buff)
        {
            // 1.设置新的定时器
            Timer new_timer = Timer.AddTimer(buff.info.ExpireDuration, buff.OnExpire);
            _buff_expire_timer.Add(buff._bid._iid, new_timer);
            // 2.重新更新超时时间
            buff.info.ResetTimeOut(TimerHelper.RealtimeSinceStartup());
        }

        // 移除超时
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

        // 是否存在buff
        public Buff _get_exist_buff(int buff_id)
        {
            int length = _buff_list.Count;
            for (int i = 0; i < length; i++)
            {
                if (_buff_list[i]._bid.CheckById(buff_id))
                {
                    return _buff_list[i];
                }
            }
            return null;
        }


        #region Buff和Buff之间的规则

        // buff 覆盖 高等级覆盖低等级
        public void _internal_buff_overlay(BaseEntity caster, Buff old_buff, BuffCnf buff_cnf)
        {
            Buff.Log("high buff overlay low buff -->(覆盖)icharacter:[{0}]  Buff:[{1}] level:[{2}] overlay Buff:[{3}] level:[{4}] ",
                _owner.ToDes(), old_buff.info.info.desc, old_buff.info.info.level, buff_cnf.desc, buff_cnf.level);

            Remove(old_buff);
            _internal_add_new_buff(caster, buff_cnf);
        }

        // buff 叠加
        public void _internal_buff_overlap(Buff old_buff, BuffCnf buff_cnf)
        {
            Buff.Log("high buff overlap low buff -->(叠加)icharacter:[{0}]  Buff:[{1}] level:[{2}] overlay Buff:[{3}] level:[{4}] ",
              _owner.ToDes(), old_buff.info.info.desc, old_buff.info.info.level, buff_cnf.desc, buff_cnf.level);
            // 层级+1
            bool result = old_buff.AddLayer();

            if (!result)
            {
                Buff.Log("Buff Add Layer 达到最大等级");
                _internal_buff_refresh_time(old_buff);
                return;
            }


            if (old_buff.info.RefreshOnAttach)
            {
                _internal_buff_refresh_time(old_buff);
            }
            // 触发回调
            old_buff.TriggerAddLayer();
        }

        // buff 时间刷新
        public void _internal_buff_refresh_time(Buff buff)
        {
            Buff.Log("Refresh Buff Time--> icharacter [{0}] refresh time for buff  [{1}] ", _owner.ToDes(), buff.info.info.desc);

            _remove_expire_timer(buff._bid);
            _add_expire_timer(buff);
        }

        // 添加新Buff
        public void _internal_add_new_buff(BaseEntity caster, BuffCnf buff_cnf)
        {
            Buff.Log("Attach Buff--> [{0}] add buff [{1}] ", _owner.ToDes(), buff_cnf.desc);

            Buff new_buff = _attach_buff(caster, buff_cnf.id);
            /*//新Buff 默认情况下在OnAttach下会增加一层 TODO 剔除*/
            bool result = new_buff.AddLayer();
            // 触发回调
            if (result)
                new_buff.TriggerAddLayer();
        }

        #endregion

        // 删除过期
        public void _force_expire(float dt)
        {
            _tmp_to_del.Clear();
            // 遍历buff是否超时了
            int length = _buff_list.Count;
            for (int i = 0; i < length; i++)
            {
                Buff buff = _buff_list[i];
                if (buff.info.IsForceExpire())
                {
                    _tmp_to_del.Add(buff);
                }
            }
            // 移除这些buff
            length = _tmp_to_del.Count;
            for (int i = 0; i < length; i++)
            {
                _tmp_to_del[i].OnExpire(null);
            }
        }

        // 依附的主体被摧毁
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

}