using System.Collections.Generic;

namespace Summer
{
    /// BuffSet 是BaseEntities身上管理attach，detach，on timer，等等的功能
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
    /// BuffSet: 人物身上Buff的管理器，管理Buff的添加移除，处理Buff的重叠覆盖还有抵消等Buff之间的关系
    /// 
    /// TODO 10.20  问题
    /// 在层级处理方面还是有问题 以及如果Buff之间有多个特效的播放
    ///  问题1：叠加层级时候特效的播放机制，数据更新
    /// 间隔时间的处理
    public class BuffSet : I_Update, I_RegisterHandler
    {
        #region 属性

        protected BaseEntity _owner;
        protected List<BaseBuff> _buffList = new List<BaseBuff>();

        public BuffSet(BaseEntity baseEntity)
        {
            _owner = baseEntity;
        }

        #endregion

        #region public

        public void RemoveByDead()
        {
            int length = _buffList.Count;
            for (int i = length - 1; i >= 0; i--)
            {
                if (!_buffList[i].Info.DeathDelete) continue;
                DetachBuff(_buffList[i]._bid);
            }
        }

        #endregion

        #region Attach & Detach 

        // 提供给外部 caster(释放buff者)给自身owner添加Buff 处理buff之间的相互关系  重叠/替换/抵消
        public void AttachBuff(BaseEntity caster, int buffId)
        {
            BuffLog.Assert(!_owner.IsDead(), "目标[{0}]已经死亡,无法添加Buff:[{1}]", _owner.ToDes(), buffId);
            if (_owner.IsDead()) return;

            // 1.根据Id查找对应的ID
            BuffCnf newCnf = BuffHelper.FindBuffById(buffId);
            BuffLog.Log("Attach Buff [{0}]", newCnf.desc);
            // 2.检测buff之间的 关系
            int count = 0;
            int length = _buffList.Count;
            for (int i = length - 1; i >= 0; i--)
            {
                BaseBuff oldBuff = _buffList[i];
                if (!oldBuff.Info.CheckGroupById(newCnf.groupID)) continue;
                count++;
                // TODO BUG逻辑有一定的问题,如果注释掉下面一行代码
                //if (count != 1) continue;

                int newLv = newCnf.level;
                // 1/0/-1 1=本buff等级更高，0=等级相等，-1=本buff等级会第一点
                BuffInfo oldInfo = oldBuff.Info;
                int lvInfo = oldInfo.CheckLevel(newLv);

                if (lvInfo == BuffInfo.LESS) // 新buff等级低
                {

                    BuffLog.Log("新buff等级低 不处理,新Buff:[{0}],levell:[{1}],老Buff[{2}],level:[{3}]",
                        newCnf.desc, newCnf.level, oldInfo.ToDes(), oldInfo.Level);
                    continue;
                }
                if (lvInfo == BuffInfo.EQUAL && oldInfo.Multilayer) // 等级相等,可层级叠加
                {
                    BuffLog.Log("Buff:[{0}],等级相等,可层级叠加", oldInfo.ToDes());
                    _internal_buff_overlap(oldBuff, newCnf);
                }
                else if (lvInfo == BuffInfo.EQUAL && !oldInfo.Multilayer) // 等级相等，不可叠加
                {
                    BuffLog.Log("Buff:[{0}],等级相等，不可叠加", oldInfo.ToDes());
                    _internal_buff_refresh_time(oldBuff);
                }
                else // 新buff等级高
                {
                    BuffLog.Log("Buff:[{0}],新Buff等级高,覆盖", oldInfo.ToDes());
                    _internal_buff_overlay(caster, _buffList[i], newCnf);
                }

                _update_attr(0); //让在update中发挥作用的buff马上生效
            }
            if (count == 0)
            {
                _internal_add_new_buff(caster, newCnf);
            }
            else if (count > 1)
                LogManager.Error("Buff Group Id 超过1个数量");
        }

        //提供给外部 移除Buff
        // TODO 移除的这块逻辑，目前不是特别明确
        public void DetachBuff(BuffId buffId)
        {
            // 1.不存在buff，return
            BaseBuff existBuff = _get_exist_buff(buffId._buffId);
            if (existBuff == null) return;

            // 2.存在就移除当前buff
            _detach_buff(existBuff._bid);
        }

        #endregion

        #region OnUpdate

        public void OnUpdate(float dt)
        {
            // 删除过期
            _force_expire(dt);
            // 依附的主体被摧毁
            //_expire_attachment_destroy(dt);

            //让在update中发挥作用的buff马上生效
            _update_attr(dt);
        }

        #endregion

        #region I_RegisterHandler

        public void OnRegisterHandler()
        {

        }

        public void UnRegisterHandler()
        {

        }

        #endregion

        #region private

        // 是否存在buff
        public BaseBuff _get_exist_buff(int buffId)
        {
            int length = _buffList.Count;
            for (int i = 0; i < length; i++)
            {
                if (_buffList[i]._bid.CheckById(buffId))
                    return _buffList[i];
            }
            return null;
        }

        // 删除过期
        public void _force_expire(float dt)
        {
            int length = _buffList.Count - 1;
            for (int i = length; i >= 0; i--)
            {
                BaseBuff buff = _buffList[i];
                if (buff.Info.ForceExpire)
                    DetachBuff(buff._bid);
                else if (!buff.IsActive())
                    DetachBuff(buff._bid);
            }
        }

        public void _update_attr(float dt)
        {
            int length = _buffList.Count;
            for (int i = 0; i < length; i++)
            {
                _buffList[i].OnUpdate(dt);
            }
        }

        #endregion

        #region internal (Attach & Detach)

        // caster（施法者) 释放 Buff Apply在owner（目标身）上
        public BaseBuff _attach_buff(BaseEntity caster, int buffId)
        {
            // caster==施法者
            // 1.创建一个Buff
            BaseBuff buff = BuffFactoryMethod.Create(buffId);
            //Buff.Log("Attach Buff-->[{0}] add [{1}] Buff", _owner.ToDes(), buff.info.ToDes());
            // 2.Buff添加到目标身上
            buff.OnAttach(_owner, caster);

            // 3.add to list, by priority, 目前没有优先的设计
            _buffList.Add(buff);

            // 4.set new timer
            return buff;
        }

        //detach所有同id的buff
        public void _detach_buff(BuffId bid)
        {
            // 1.移除定时器
            int length = _buffList.Count - 1;
            for (int i = length; i >= 0; i--)
            {
                if (!_buffList[i]._bid.CheckByIid(bid)) continue;
                BuffLog.Log("Detach Buff-->[{0}] detach [{1}] Buff", _owner.ToDes(), _buffList[i].Info.ToDes());
                _buffList[i].OnDetach();
            }
        }

        #endregion

        #region Buff和Buff之间的规则

        // buff 覆盖 高等级覆盖低等级
        public void _internal_buff_overlay(BaseEntity caster, BaseBuff oldBuff, BuffCnf buffCnf)
        {
            DetachBuff(oldBuff._bid);
            _internal_add_new_buff(caster, buffCnf);
        }

        // buff 叠加
        public void _internal_buff_overlap(BaseBuff oldBuff, BuffCnf buffCnf)
        {
            // 层级+1
            bool result = oldBuff.AddLayer();

            if (!result)
            {
                BuffLog.Log("Buff Add Layer 达到最大等级");
                _internal_buff_refresh_time(oldBuff);
                return;
            }


            if (oldBuff.Info.RefreshOnAttach)
            {
                _internal_buff_refresh_time(oldBuff);
            }
            // 触发回调
            // old_buff.RaiseEvent(E_Buff_Event.buff_add_layer);
        }

        // buff 时间刷新
        public void _internal_buff_refresh_time(BaseBuff buff)
        {
            buff.Info.ResetTimeOut();
        }

        // 添加新Buff
        public void _internal_add_new_buff(BaseEntity caster, BuffCnf buffCnf)
        {
            BuffLog.Log("Attach Buff--> [{0}] add buff [{1}] ", _owner.ToDes(), buffCnf.desc);

            BaseBuff newBuff = _attach_buff(caster, buffCnf.id);
            /*//新Buff 默认情况下在OnAttach下会增加一层 TODO 剔除*/
            bool result = newBuff.AddLayer();
            // 触发回调
            if (result)
                newBuff.RaiseEvent(E_Buff_Event.buff_add_layer);
        }

        #endregion
    }
}