

namespace Summer
{
    public abstract class FsmState
    {
        #region 属性

        protected E_StateId _state_id;
        public E_StateId Id { get { return _state_id; } }
        public BaseEntity entity;
        public I_EntityCommnad _command;
        #endregion

        #region public

        #region 状态切换

        /// <summary>
        /// 在进入之前设置状态的条件
        /// 由FsmSystem自动条用
        /// </summary>
        public abstract void DoBeforeEntering();

        public abstract void DoBeforeLeaving();

        public virtual void OnUpdate(float dt)
        {
            if (_command != null)
                _command.OnUpdate(entity, dt);
        }

        public void OnEnter(I_EntityCommnad command)
        {
            _command = command;
        }

        #endregion

        #endregion
    }
}

