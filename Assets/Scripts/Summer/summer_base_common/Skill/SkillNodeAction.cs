
namespace Summer
{
    /// <summary>
    /// 每一个叶子节点都是独立执行的,通过触发事件调用外部接口
    /// </summary>
    public abstract class SkillNodeAction
    {
        protected SkillNode _context;          //上下文（序列节点）
        protected bool _is_complete;            //是否完成这个动作
        public void BindingContext(SkillNode context)
        {
            _context = context;
        }
        public bool IsFinish() { return _is_complete; }

        protected void Finish() { _is_complete = true; }

        #region abstract OnEnter/OnExit/OnUpdate

        public abstract void OnEnter();

        public void LogEnter()
        {
            LogManager.Log("Time: {0}   Enter Leaf Action:{1}", LogTime(), ToDes());
        }

        public abstract void OnExit();

        public void LogExit()
        {
            LogManager.Log("Time: {0}   Exit Leaf Action:{1}", LogTime(), ToDes());
        }

        public string LogTime()
        {
            return TimeManager.FrameCount.ToString();
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

        }

        public virtual void Destroy()
        {

        }

        #endregion

        #region Raise 触发事件

        public void RaiseEvent(E_SkillTriggerEvent key, EventSkillSetData obj_info)
        {
            _context._parent_node.RaiseEvent(key, obj_info);
        }

        #endregion


        public abstract string ToDes();

        //void OnFixedUpdate();
        //void OnLateUpdate();

    }

}

