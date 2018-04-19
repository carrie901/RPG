
namespace Summer
{
    /// <summary>
    /// 每一个叶子节点都是独立执行的,通过触发事件调用外部接口
    /// 
    /// TODO 节点和外部之间的联系如何维持
    /// </summary>
    public abstract class SkillNodeAction
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

        public abstract void OnEnter();

        public abstract void OnExit();

        public void LogExit()
        {
            if (!LogManager.open_skill) return;
            //LogManager.Log("Time: {0}   Exit [{1}] Leaf Action", TimeManager.FrameCount, ToDes());
        }

        public void LogEnter()
        {
            if (!LogManager.open_skill) return;
            SkillLog.Log("Time: {0} Enter [{1}] Leaf Action", TimeManager.FrameCount, ToDes());
        }

        public virtual void OnUpdate(float dt)
        {
            Finish();
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
            if (_in_trigger == null)
                _in_trigger = _context.GetTrigger();

            //if (_in_trigger == null) return;
            // TODO 这样一层一层的递交上次，是否违反了重构原则
            _in_trigger.RaiseEvent(key, obj_info);
            EventDataFactory.Push(obj_info);
        }

        public I_EntityInTrigger GetTrigger()
        {
            if (_in_trigger == null)
                _in_trigger = _context.GetTrigger();
            return _in_trigger;
        }

        #endregion


        public abstract string ToDes();

        //void OnFixedUpdate();
        //void OnLateUpdate();

    }

}

