
namespace Summer
{
    public abstract class ASkillAction
    {
        public string des = string.Empty;
        protected SkillState _context;          //上下文
        protected bool _is_complete;            //是否完成这个动作
        protected string _receive_event         //接受事件的名称
            = string.Empty;
        public void BindingContext(SkillState context)
        {
            _context = context;
        }
        public bool IsFinish()
        {
            return _is_complete;
        }

        protected void Finish()
        {
            _is_complete = true;
        }

        public void SetReceiveEvent(string name) { _receive_event = name; }
        public string GetReceiveEvent() { return _receive_event; }
        public bool HasEvent() { return _receive_event != string.Empty; }

        #region abstract OnEnter/OnExit/OnUpdate

        public abstract void OnEnter();
        public abstract void OnExit();
        public abstract void OnUpdate(float dt);

        #endregion

        #region virtual SendEvent/Reset/Destory

        public virtual void OnSendEvent(string event_name)
        {

        }
        /// <summary>
        /// 默认自动完成
        /// </summary>
        /// <param name="dt"></param>

        public virtual void Reset()
        {

        }

        public virtual void Destroy()
        {

        }

        #endregion

        #region Register/UnRegister/Raise

        public bool RegisterHandler(E_SkillTrigger key, EventSet<E_SkillTrigger, EventSkillSetData>.EventHandler handler)
        {
            return _context._link.RegisterHandler(key, handler);
        }

        public bool UnRegisterHandler(E_SkillTrigger key, EventSet<E_SkillTrigger, EventSkillSetData>.EventHandler handler)
        {
            return _context._link.UnRegisterHandler(key, handler);
        }

        public void RaiseEvent(E_SkillTrigger key, EventSkillSetData obj_info)
        {
            _context._link.RaiseEvent(key, obj_info);
        }

        #endregion

        public virtual string ToDes()
        {
            return des;
        }

        //void OnFixedUpdate();
        //void OnLateUpdate();

    }

}

