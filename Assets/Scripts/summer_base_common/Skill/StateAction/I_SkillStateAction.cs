
namespace Summer
{
    /// <summary>
    /// 每一个叶子节点都是独立执行的,通过触发事件调用外部接口
    /// </summary>
    public abstract class AskillActionLeaf
    {
        protected SkillState _context;          //上下文（序列节点）
        protected bool _is_complete;            //是否完成这个动作
        public void BindingContext(SkillState context)
        {
            _context = context;
        }
        public bool IsFinish() { return _is_complete; }

        protected void Finish() { _is_complete = true; }

        #region abstract OnEnter/OnExit/OnUpdate

        public abstract void OnEnter();

        public void LogEnter()
        {
            LogManager.Log("进入:[{0}]", ToDes());
        }

        public abstract void OnExit();

        public void LogExit()
        {
            LogManager.Log("退出:[{0}]", ToDes());
        }

        public virtual void OnUpdate(float dt)
        {
            Finish();
        }

        #endregion

        #region virtual SendEvent/Reset/Destory

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

        #region Register/UnRegister/Raise

        /*public bool RegisterHandler(E_SkillTriggerEvent key, EventSet<E_SkillTriggerEvent, EventSkillSetData>.EventHandler handler)
        {
            return _context._parent_node.RegisterHandler(key, handler);
        }

        public bool UnRegisterHandler(E_SkillTriggerEvent key, EventSet<E_SkillTriggerEvent, EventSkillSetData>.EventHandler handler)
        {
            return _context._parent_node.UnRegisterHandler(key, handler);
        }*/

        public void RaiseEvent(E_SkillTriggerEvent key, EventSkillSetData obj_info)
        {
            _context._parent_node.RaiseEvent(key, obj_info);
        }

        #endregion

        public virtual string ToDes()
        {
            return string.Empty;
        }

        //void OnFixedUpdate();
        //void OnLateUpdate();

    }

}

