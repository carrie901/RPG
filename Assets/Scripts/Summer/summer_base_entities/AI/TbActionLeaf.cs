using UnityEngine;
namespace Summer.AI
{

    public class TbActionLeafContext : TbActionContext
    {
        #region 属性

        internal int status;                                    // 当前叶子节点状态
        internal bool need_exit;                                // 需要退出
        protected object user_data;                             // 节点数据
        public T GetUserData<T>() where T : class, new()
        {
            if (user_data == null)
                user_data = new T();
            return user_data as T;
        }

        #endregion

        #region 构造

        public TbActionLeafContext()
        {
            status = TbActionLeaf.ACTION_READY;
            need_exit = false;

            user_data = null;
        }

        #endregion

        #region public 

        public void ResetData()
        {
            status = TbActionLeaf.ACTION_READY;
            need_exit = false;
        }

        #region OnEnter OnFinish OnExit

        public void OnEnter()
        {
            status = TbActionLeaf.ACTION_RUNNING;
            need_exit = true;
        }

        public void OnFinish()
        {
            status = TbActionLeaf.ACTION_FINISHED;
        }

        public void OnExit()
        {
            status = TbActionLeaf.ACTION_READY;
            need_exit = false;
        }
        #endregion

        #endregion
    }


    /// <summary>
    /// 叶子节点
    /// </summary>
    public class TbActionLeaf : TbAction
    {
        #region 状态

        public const int ACTION_READY = 0;                          // 准备
        public const int ACTION_RUNNING = 1;                        // 运行
        public const int ACTION_FINISHED = 2;                       // 结束

        #endregion

        #region 构造

        public TbActionLeaf() : base(0) { }

        #endregion

        #region public

        public void Reset()
        {

        }

        #endregion

        #region  protected override 

        protected override int OnUpdate(TbWorkingData work_data)
        {
            int running_state = TbRunningStatus.FINISHED;
            TbActionLeafContext leaf_context = GetContext<TbActionLeafContext>(work_data);
            int curr_status = leaf_context.status;
            if (curr_status == ACTION_READY)
            {
                OnEnter(work_data);
                leaf_context.OnEnter();
            }

            if (curr_status == ACTION_RUNNING)
            {
                running_state = OnExecute(work_data);
                if (TbRunningStatus.IsFinished(running_state))
                {
                    leaf_context.OnFinish();
                }
            }

            if (curr_status == ACTION_FINISHED)
            {
                if (leaf_context.need_exit)
                {
                    OnExit(work_data, running_state);
                }
                leaf_context.OnExit();
            }

            return running_state;
        }

        protected override void OnTransition(TbWorkingData work_data)
        {
            TbActionLeafContext leaf_context = GetContext<TbActionLeafContext>(work_data);
            if (leaf_context.need_exit)
            {
                OnExit(work_data, TbRunningStatus.TRANSITION);
            }

            leaf_context.ResetData();
        }

        protected T GetUserContextData<T>(TbWorkingData work_data) where T : class, new()
        {
            return GetContext<TbActionLeafContext>(work_data).GetUserData<T>();
        }

        #endregion

        #region virtual 叶子节点的生命过度 OnEnter.OnExecute.OnExit

        protected virtual void OnEnter(TbWorkingData work_data)
        {
        }
        protected virtual int OnExecute(TbWorkingData work_data)
        {
            return TbRunningStatus.FINISHED;
        }
        protected virtual void OnExit(TbWorkingData work_data, int running_status)
        {

        }

        #endregion
    }

}
