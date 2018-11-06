
namespace Summer
{
    /// <summary>
    /// 每一个叶子节点都是独立执行的,通过触发事件调用外部接口
    /// 
    /// TODO 节点和外部之间的联系如何维持
    /// 
    /// 
    /// 
    /// </summary>
    public abstract class SkillLeafNode
    {
        protected SkillNode _context;          //上下文（序列节点）
        protected I_EntityInTrigger _in_trigger;
        protected bool _is_complete;            //是否完成这个动作
        public void BindingContext(SkillNode context)
        {
            _context = context;

        }
        public bool IsFinish() { return _is_complete; }

        protected virtual void Finish() { _is_complete = true; }

        #region abstract OnEnter/OnExit/OnUpdate

        public abstract void OnEnter(EntityBlackBoard blackboard);

        public abstract void OnExit(EntityBlackBoard blackboard);

        public virtual void OnUpdate(float dt, EntityBlackBoard blackboard)
        {
            Finish();
        }

        public void LogExit()
        {
            SkillLog.Log("Time: {0}   Exit [{1}] Leaf Action", TimeModule.FrameCount, ToDes());
        }

        public void LogEnter()
        {
            SkillLog.Log("Time: {0} Enter [{1}] Leaf Action", TimeModule.FrameCount, ToDes());
        }

        #endregion

        #region virtual Reset/Destory

        /// <summary>
        /// 默认自动完成
        /// </summary>

        public virtual void Reset()
        {
            _is_complete = false;
        }

        public virtual void Destroy()
        {

        }

        #endregion

        #region Raise 触发事件

        public void RaiseEvent(E_EntityInTrigger key, EventSetData obj_info)
        {
            _context.RaiseEvent(key, obj_info);
        }

        #endregion


        public abstract string ToDes();

        //void OnFixedUpdate();
        //void OnLateUpdate();

    }

}

