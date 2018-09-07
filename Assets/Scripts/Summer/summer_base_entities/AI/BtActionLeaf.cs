
namespace Summer.AI
{

    public class BtActionLeafContext : BtActionContext
    {
        #region 属性

        internal int status;                                    // 当前叶子节点状态
        internal bool need_exit;                                // 需要退出
        protected object user_data;                             // 节点数据
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetUserData<T>() where T : class, new()
        {
            if (user_data == null)
                user_data = new T();
            return user_data as T;
        }

        #endregion

        #region 构造

        public BtActionLeafContext()
        {
            status = BtActionLeaf.ACTION_READY;
            need_exit = false;

            user_data = null;
        }

        #endregion

        #region public 

        public void ResetData()
        {
            status = BtActionLeaf.ACTION_READY;
            need_exit = false;
        }

        #region OnEnter OnFinish OnExit

        public void OnEnter()
        {
            status = BtActionLeaf.ACTION_RUNNING;
            need_exit = true;
        }

        public void OnFinish()
        {
            status = BtActionLeaf.ACTION_FINISHED;
        }

        public void OnExit()
        {
            ResetData();
        }
        #endregion

        #endregion
    }


    /// <summary>
    /// 叶子节点
    /// </summary>
    public class BtActionLeaf : BtAction
    {
        #region 状态

        public const int ACTION_READY = 0;                          // 准备
        public const int ACTION_RUNNING = 1;                        // 运行
        public const int ACTION_FINISHED = 2;                       // 结束

        #endregion

        #region 构造

        public BtActionLeaf() : base(0) { }

        #endregion

        #region public

        public void Reset()
        {

        }

        #endregion

        #region  protected override 

        protected override int OnUpdate(BtWorkingData workData)
        {
            int running_state = BtRunningStatus.FINISHED;
            BtActionLeafContext leaf_context = GetContext<BtActionLeafContext>(workData);
            int curr_status = leaf_context.status;
            if (curr_status == ACTION_READY)
            {
                OnEnter(workData);
                leaf_context.OnEnter();
            }

            if (curr_status == ACTION_RUNNING)
            {
                running_state = OnExecute(workData);
                if (BtRunningStatus.IsFinished(running_state))
                {
                    leaf_context.OnFinish();
                }
            }

            if (curr_status == ACTION_FINISHED)
            {
                if (leaf_context.need_exit)
                {
                    OnExit(workData, running_state);
                }
                else
                {
                    LogManager.Error("节点退出错误,[{0}]", TbName);
                }
                leaf_context.OnExit();
            }

            return running_state;
        }

        protected override void OnTransition(BtWorkingData workData)
        {
            BtActionLeafContext leaf_context = GetContext<BtActionLeafContext>(workData);
            if (leaf_context.need_exit)
            {
                OnExit(workData, BtRunningStatus.TRANSITION);
            }

            leaf_context.ResetData();
        }

        protected T GetUserContextData<T>(BtWorkingData work_data) where T : class, new()
        {
            BtActionLeafContext action_leaf_context = GetContext<BtActionLeafContext>(work_data);
            T t = action_leaf_context.GetUserData<T>();
            return t;
        }

        #endregion

        #region virtual 叶子节点的生命过度 OnEnter.OnExecute.OnExit

        protected virtual void OnEnter(BtWorkingData workData)
        {
        }
        protected virtual int OnExecute(BtWorkingData workData)
        {
            return BtRunningStatus.FINISHED;
        }
        protected virtual void OnExit(BtWorkingData work_data, int running_status)
        {

        }

        #endregion
    }

}
