
//
//                            _ooOoo_
//                           o8888888o
//                           88" . "88
//                           (| -_- |)
//                           O\  =  /O
//                        ____/`---'\____
//                      .'  \\|     |//  `.
//                     /  \\|||  :  |||//  \
//                    /  _||||| -:- |||||-  \
//                    |   | \\\  -  /// |   |
//                    | \_|  ''\---/''  |   |
//                    \  .-\__  `-`  ___/-. /
//                  ___`. .'  /--.--\  `. . __
//               ."" '<  `.___\_<|>_/___.'  >'"".
//              | | :  `- \`.;`\ _ /`;.`/ - ` : | |
//              \  \ `-.   \_ __\ /__ _/   .-` /  /
//         ======`-.____`-.___\_____/___.-`____.-'======
//                            `=---='
//        ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
//                 			 佛祖 保佑             

using System.Collections.Generic;

namespace Summer
{
    /// <summary>
    /// TODO 相对于细节的多变性，抽象的东西要稳定的多，目前buff得到角色身上太多的内容(血量，无双值，攻击力等等相关内容），无法进行抽象化/
    /// 
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
    /// 
    /// Buff 以某几种机制触发某几种效果 
    /// 
    /// </summary>
    public class BaseBuff : I_Buff
    {
        public BuffId _bid;
        public BuffInfo Info;
        public BaseEntity _caster;        //buff释放者
        public BaseEntity _target;        //buff释放目标 抽象成接口，依赖倒置
        public List<BaseEffect> _effects
            = new List<BaseEffect>();

        public BaseBuff(BuffTemplateInfo buffObj)
        {
            Info = new BuffInfo(buffObj);
        }

        #region public 

        //有些buff是过程无效果，上buff和下buff的时候带功能的。例如设置角色朝向/攻击力
        public void OnAttach(BaseEntity target, BaseEntity caster)
        {
            _bid = new BuffId(target._entityId.Eid, Info.Id);
            _target = target;
            _caster = caster;
            // 1.默认为1层
            //vbo.OnDefaultLayer();
            // 2.初始化效果
            _init_effs();
            // 3.添加VFX/Sound
            //_add_Vfx();
            //_add_sound();
            // 4.触发OnAttach回调
            RaiseEvent(E_Buff_Event.buff_on_attach);
            GameEventSystem.Instance.RaiseEvent(E_GLOBAL_EVT.buff_attach, this);
        }

        public void OnDetach()
        {
            GameEventSystem.Instance.RaiseEvent(E_GLOBAL_EVT.buff_detach, this);
            RaiseEvent(E_Buff_Event.buff_on_detach);

            // 1.移除vfx/sound
            //_remove_Vfx();
            //_remove_sound();
            // 2.触发OnDeatch回调,effect
            int length = _effects.Count;
            for (int i = 0; i < length; i++)
                _effects[i].OnDetach();
            _effects.Clear();
            _caster = null;
            _target = null;
            Info = null;
            _bid = null;
        }

        public void OnUpdate(float dt)
        {
            if (Info == null || Info.ForceExpire) return;
            Info.OnUpdate(dt);
            if (Info.CanTick())
            {
                Info.OnTick();
                OnTick();
            }
        }

        public bool IsActive() { return _target != null; }

        protected void OnTick()
        {
            RaiseEvent(E_Buff_Event.buff_on_tick);
        }


        #endregion

        #region Add/Remove Layer 层级的处理外抛 因为层级的添加是由可能由两个Buff之间导致的

        public bool AddLayer()
        {
            // 1.层数已到最高，return
            if (!Info.CanAddLayer()) return false;
            // 2.层级+1
            bool result = Info.AddLayer();
            //  3.触发到达最大层数
            return result;
        }

        public bool RemoveLayer()
        {
            // 1.层数已经到0
            if (!Info.CanRemoveLayer()) return false;
            // 2.层级-1
            bool result = Info.RemoveLayer();
            //_internal_buff_trigger(E_AbilityTrigger.buff_remove_layer);
            return result;
        }

        #endregion

        #region 触发器 RegisterHandler

        #region Buff 自身

        public EventConditionSet<E_Buff_Event> _buffEventSet =
           new EventConditionSet<E_Buff_Event>(BuffEvtComparer.Instance);


        public bool RegisterHandler(E_Buff_Event key, EventSet<E_Buff_Event, EventSetData>.EventHandler handler,I_Condition condition=null)
        {
            return _buffEventSet.RegisterHandler(key, handler, condition);
        }

        public bool UnRegisterHandler(E_Buff_Event key, EventSet<E_Buff_Event, EventSetData>.EventHandler handler, I_Condition condition = null)
        {
            return _buffEventSet.UnRegisterHandler(key, handler, condition);
        }

        public void RaiseEvent(E_Buff_Event key, EventSetData data)
        {
            _buffEventSet.RaiseEvent(key, data);
        }

        public void RaiseEvent(E_Buff_Event key)
        {
            EventSetEffectData data = EventDataFactory.Pop<EventSetEffectData>();
            data.entity = _target;
            _buffEventSet.RaiseEvent(key, data);
        }

        #endregion

        #region Entity 自身


        public Dictionary<E_EntityEvent, I_Condition> _conditionSet
            = new Dictionary<E_EntityEvent, I_Condition>(EntityEvtComparer.Instance);
        public bool RegisterHandler(E_EntityEvent key, EventSet<E_EntityEvent, EventSetData>.EventHandler handler, I_Condition condition = null)
        {
            if (condition != null)
                _conditionSet.Add(key, condition);

            return _target.RegisterHandler(key, handler);
        }

        public bool UnRegisterHandler(E_EntityEvent key, EventSet<E_EntityEvent, EventSetData>.EventHandler handler, I_Condition condition = null)
        {
            if (condition != null)
                _conditionSet.Remove(key);

            return _target.UnRegisterHandler(key, handler);
        }

        #endregion

        #endregion

        #region private 

        public void _init_effs()
        {
            List<EffectTemplateInfo> effs = Info.GetEffs();

            int length = effs.Count;
            for (int i = 0; i < length; i++)
            {
                BaseEffect baseEffect = EffectFactory.instance.Create(this, effs[i]);
                if (baseEffect == null) continue;

                _effects.Add(baseEffect);
                _init_single_effect(baseEffect);
            }
        }

        public void _init_single_effect(BaseEffect baseEffect)
        {
            baseEffect.OnAttach();
            E_Buff_Event buffEvent = baseEffect._info.GetBuffEvt();
            RegisterHandler(buffEvent, baseEffect.ExcuteEffect);
        }

        #endregion
    }
}