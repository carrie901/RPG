using UnityEngine;

namespace Summer
{
    // TODO BaseEntityController和 BaseEntities的关系
    // 目前感觉BaseEntities持有BaseEntityController会比较好一点
    // 这种情况下如果没有BaseEntityController 也可以运行游戏

    /// <summary>
    /// TODO 4.16 QAQ 
    ///     QAQ: 相对于细节的多变性，相对而已抽象的东西要稳定的多
    ///     1. 由于I_EntityInTrigger.GetEntity(); 在原子节点中又遇到了老的buff节点遇到一样的问题
    ///     节点如果承担实际的原子操作（有关于自身的，也有外部的比如震屏），那么节点必须要有相关的自身引用
    ///         1.这里的处理方式有2种 1.通过一个ContextModule作为中介来得到双方的引用，进行处理相关问题
    ///         2.直接通过I_EntityInTrigger Raise触发事件，然后在I_EntityInTrigger的子类中进行处理
    ///         在没有对BaseEntity进行很好的抽象之前,原子操作的部分同时和功能内聚是有一定的冲突的
    /// </summary>
    //[RequireComponent(typeof(Rigidbody))]
    //[RequireComponent(typeof(EntityAnimationGroup), typeof(EntityMovement))]
    public class BaseEntityController : PoolDefaultGameObject, I_Update
    {
        #region 属性

        public BaseEntity _baseEntity;
        protected I_Entity _entity;
        protected I_EntityInTrigger _inEntity;
        public Transform _trans;                                                                         // 缓存Transform
        public Rigidbody _rigidBody;
        public I_EntityAnimationGroup _group;

        public Vector3 Direction { get { return _trans.forward; } }                                     // 当前方向           
        public Vector3 WroldPosition { get { return _trans.position; } }                                // 世界坐标
        public bool CanMovement { get; set; }
        #endregion

        #region Mono Override

        void Awake()
        {
            _trans = transform;
            _rigidBody = GetComponent<Rigidbody>();
            if (_rigidBody != null)
                _rigidBody.useGravity = false;
        }

        void OnDestroy()
        {

        }

        #endregion

        #region I_Update Update

        public void OnUpdate(float dt)
        {

            /*if (entity == null) return;
            entity.OnUpdate(dt);*/

            /*RaycastHit hit;
            Vector3 p1 = trans.position;
            Vector3 p2 = p1 + Vector3.forward * 0.5f;
            if (Physics.CapsuleCast(p1, p2, 0.0f, transform.forward, out hit, 0.2f))
            {
            }*/
        }

        #endregion

        #region 缓存池

        public override void OnPush()
        {
            base.OnPush();
            _entity = null;
        }

        #endregion

        #region public 

        public void OnRegisterHandler()
        {
            _baseEntity.RegisterHandler(E_EntityInTrigger.PLAY_EFFECT, OnPlayEffect);
        }

        public void InitOutTrigger(I_Entity trigger, I_EntityInTrigger inTrigger, BaseEntity baseEntity)
        {
            _entity = trigger;
            _inEntity = inTrigger;
            _baseEntity = baseEntity;
        }

        public I_EntityAnimationGroup GetAnimationGroup()
        {
            if (_group == null)
            {
                _group = GetComponent<EntityAnimGroupTTT>();
            }
            return _group;
        }


        #endregion

        #region 注册响应


        // 播放特效
        public void OnPlayEffect(EventSetData param)
        {
            EntityActionFactory.OnAction<EntityPlayEffectAction>(_baseEntity, param);
        }


        #endregion
    }
}

