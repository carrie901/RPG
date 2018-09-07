﻿using System.Collections.Generic;
using Summer.AI;
using UnityEngine;

namespace Summer
{
    /// <summary>
    /// 先把BaseEntities当做BaseCharacter用
    /// TODO 4.12
    /// QAQ:
    ///     根据高内聚原则,entity相关的方法最好内聚在本类，如果需求是多样化的。那么提供get方法让外部进行操作，但最总基础的方法还是在本类
    /// QAQ:
    ///     关于涉及到生命周期的一些东西，比如技能特效，声音，目前不太好处理，目前把这些东西放到SkillModule中
    /// 
    /// 
    /// 目前没有明确性质的方案来判断到底原子操作的具体逻辑是放在原子操作中来完成好，还是通过由原子操作来进行注册触发好
    /// </summary>
    public class BaseEntity : /*I_CharacterProperty,*/ I_Update, I_EntityInTrigger, I_Entity, I_EntityLife/*, I_SkillBlackBorad*//*, I_RegisterHandler*/
    {
        #region 属性

        public int Template { get; private set; }
        public BaseEntityController EntityController { get; private set; }                              // GameObject控制器
        public Vector3 WroldPosition { get { return EntityController._trans.position; } }                // 世界坐标
        public Vector3 Direction { get { return EntityController._trans.forward; } }                     // 当前方向
        public EntityAttributeProperty AttributeProp { get { return _attrProp; } }
        public bool CanMovement { get; set; }
        public EntityId _entityId;                                                                      // Entity的唯一表示
        public HeroInfoCnf _cnf;

        // TODO 是否通过黑箱进行操作 来规避掉内部的响应
        //public List<BaseEntity> _targets = new List<BaseEntity>();                                    // 目标 是否通过黑箱进行操作
        public EventSet<E_Entity_Event, EventSetData> _outEventSet                                      // 角色的外部事件
           = new EventSet<E_Entity_Event, EventSetData>(EntityEvtComparer.Instance);
        public EventSet<E_EntityInTrigger, EventSetData> _inEventSet                                    // 角色的内部事件
       = new EventSet<E_EntityInTrigger, EventSetData>();

        public EntityBlackBoard _entityBlackboard = new EntityBlackBoard();


        public List<I_Update> _updateList = new List<I_Update>();                                       // 对应需要注册到容器中的组件 进行Update
        public readonly List<I_RegisterHandler> _registerList = new List<I_RegisterHandler>();                   // 注册项

        #region 附带属性 AI/SkillSet/BuffSet/Attribute/Fsm

        public BtEntityAi _entityAi;                                                                    // 相关AI组件
        public SkillSet_1 _skillSet;                                                                    // 相关技能组件
        public BuffSet _buffSet;                                                                        // 相关Buff组件
        public EntityAttributeProperty _attrProp;                                                       // 相关人物属性组件
        public FsmSystem _fsmSystem;                                                                    // 相关状态机组件

        #endregion

        #region 缓存池相关

        public bool IsUse { get; set; }                                                                 // true=使用中
        public string ObjectName { get { return "BaseEntity"; } }

        #endregion

        #region Mono的附带属性

        public EntityAnimationGroup _animGroup;                                                         // 动画组件 播放动画
        public EntityMovement _movement;                                                                // 移动组件 人物移动
        public BaseEntityController _entityController;                                                  // 角色控制器 

        #endregion

        #endregion

        #region I_Update/I_EntityInTrigger/I_Entity/I_EntityLife缓存池/黑箱

        #region OnUpdate

        public void OnUpdate(float dt)
        {
            EntityController.OnUpdate(dt);
            _movement.OnUpdate(dt);
            int length = _updateList.Count;
            for (int i = 0; i < length; i++)
            {
                _updateList[i].OnUpdate(dt);
            }
        }

        #endregion

        #region I_EntityInTrigger 注册监听人物的外部事件

        public bool RegisterHandler(E_Entity_Event key, EventSet<E_Entity_Event, EventSetData>.EventHandler handler)
        {
            return _outEventSet.RegisterHandler(key, handler);
        }

        public bool UnRegisterHandler(E_Entity_Event key, EventSet<E_Entity_Event, EventSetData>.EventHandler handler)
        {
            return _outEventSet.UnRegisterHandler(key, handler);
        }

        public void RaiseEvent(E_Entity_Event key, EventSetData objInfo)
        {
            if (_outEventSet == null) return;
            _outEventSet.RaiseEvent(key, objInfo);
        }

        #endregion

        #region I_Entity 注册监听人物的内部事件 基本都是一些原子节点

        public bool RegisterHandler(E_EntityInTrigger key, EventSet<E_EntityInTrigger, EventSetData>.EventHandler handler)
        {
            return _inEventSet.RegisterHandler(key, handler);
        }

        public bool UnRegisterHandler(E_EntityInTrigger key, EventSet<E_EntityInTrigger, EventSetData>.EventHandler handler)
        {
            return _inEventSet.UnRegisterHandler(key, handler);
        }

        // 被内部调用，由内部触发
        public void RaiseEvent(E_EntityInTrigger key, EventSetData param)
        {
            _inEventSet.RaiseEvent(key, param);
        }

        public E_StateId GetState()
        {
            return _fsmSystem.GetState();
        }

        #endregion

        #region I_EntityLife 缓存池相关函数

        // 第一次被初始化出来
        public virtual void OnInit()
        {
            _skillSet = new SkillSet_1(this);
            _buffSet = new BuffSet(this);
            _fsmSystem = EntityFsmFactory.CreateFsmSystem(this);
            _entityAi = new BtEntityAi(this);
        }

        // 从缓存池中提取
        public virtual void OnPop(int heroId)
        {
            Template = heroId;             // 模板ID
            _init_data();
            _init_gameobject();

            _entityBlackboard = new EntityBlackBoard();
            _entityBlackboard.entity = this;
            // 更新通道
            _updateList.Add(_skillSet);
            _updateList.Add(_fsmSystem);
            _updateList.Add(_buffSet);

            // 注册
            _fsmSystem.OnRegisterHandler();
            _skillSet.OnRegisterHandler();
            _buffSet.OnRegisterHandler();
            _attrProp.OnRegisterHandler();

            OnRegisterHandler();

            _animGroup.PlayAnimation(AnimationNameConst.IDLE);
            _fsmSystem.Start();
        }

        // 放入缓存池
        public virtual void OnPush()
        {
            Clear();
            TransformPool.Instance.Push(EntityController);
            EntityController = null;
            _entityBlackboard.entity = null;
            _entityBlackboard.Clear();
        }


        #endregion

        #region I_SkillBlackBorad 黑箱数据

        public EntityBlackBoard GetBlackBorad()
        {
            return _entityBlackboard;
        }

        /*public T GetBlackBoradValue<T>(string Key, T default_value)
        {
            T t = _skill_blackboard.GetValue<T>(Key, default_value);
            return t;
        }*/

        #endregion

        #endregion

        #region public

        #region 监听的内部事件,通过一些原子节点触发事件，迫使Entity做出某些行为

        // 目标死亡
        public void EntityDie(EventSetData param)
        {
            EntityEventFactory.ChangeInEntityState(this, E_StateId.die);
        }

        #endregion

        #region 监听的外部事件，比如动作文件为源头，触发动作事件

        public void OnBeHurt(EventSetData param)
        {
            EntityEventFactory.ChangeInEntityState(this, E_StateId.hurt);
        }

        #endregion

        public void OnRegisterHandler()
        {
            RegisterHandler(E_EntityInTrigger.entity_die, EntityDie);
            RegisterHandler(E_Entity_Event.on_be_hurt, OnBeHurt);
        }

        public bool IsDead() { return false; }

        public void ReceiveCommandMove(Vector2 diretion)
        {
            if (!CanMovement) return;
            _movement.AddDirection(diretion);
        }

        public void InitPosRot()
        {
            EntityController._trans.position = new Vector3(Random.value * 40 - 20f, 0, Random.value * 40 - 20f);
        }

        public void Clear()
        {
            _outEventSet.Clear();
            _inEventSet.Clear();
            //_targets.Clear();
            _updateList.Clear();
            for (int i = 0; i < _registerList.Count; i++)
                _registerList[i].UnRegisterHandler();
            _registerList.Clear();
        }

        public string ToDes()
        {
#if UNITY_EDITOR
            return "";
#else
            return string.Empty;
#endif
        }

        #endregion

        #region private

        // 初始化GameObject的相关东西
        public void _init_gameobject()
        {
            // 加载模型
            BaseEntityController go = TransformPool.Instance.Pop<BaseEntityController>
                ("res_bundle/prefab/model/Character/" + "NPC_Zhaoyun_001_02"/*_cnf.prefab_name*/);
            EntityController = go;
            EntityController.InitOutTrigger(this, this,this);

            _animGroup = go.GetComponent<EntityAnimationGroup>();
            _animGroup.OnInit(this);
            _animGroup.OnRegisterHandler();

            _movement = go.GetComponent<EntityMovement>();
            _movement._base_entity = this;
        }

        public void _init_data()
        {
            CanMovement = true;
            _entityId = new EntityId();

            _cnf = StaticCnf.FindData<HeroInfoCnf>(Template);
            _skillSet.OnReset(Template);
            _attrProp = new EntityAttributeProperty(_entityId);
        }

        #endregion

        #region Test

        public void AddBuff()
        {
            _buffSet.AttachBuff(this, 1300011);
        }

        #endregion
    }
}