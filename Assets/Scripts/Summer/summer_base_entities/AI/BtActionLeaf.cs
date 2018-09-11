
namespace Summer.AI
{

    public class BtActionLeafContext : BtActionContext
    {
        #region 属性

        internal int _status;                                    // 当前叶子节点状态
        internal bool _needExit;                                // 需要退出
        protected object _userData;                             // 节点数据
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetUserData<T>() where T : class, new()
        {
            if (_userData == null)
                _userData = new T();
            return _userData as T;
        }

        #endregion

        #region 构造

        public BtActionLeafContext()
        {
            _status = BtActionLeaf.ACTION_READY;
            _needExit = false;

            _userData = null;
        }

        #endregion

        #region public 

        public void ResetData()
        {
            _status = BtActionLeaf.ACTION_READY;
            _needExit = false;
        }

        #region OnEnter OnFinish OnExit

        public void OnEnter()
        {
            _status = BtActionLeaf.ACTION_RUNNING;
            _needExit = true;
        }

        public void OnFinish()
        {
            _status = BtActionLeaf.ACTION_FINISHED;
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
            int runningState = BtRunningStatus.FINISHED;
            BtActionLeafContext leafContext = GetContext<BtActionLeafContext>(workData);
            int currStatus = leafContext._status;
            if (currStatus == ACTION_READY)
            {
                OnEnter(workData);
                leafContext.OnEnter();
            }

            if (currStatus == ACTION_RUNNING)
            {
                runningState = OnExecute(workData);
                if (BtRunningStatus.IsFinished(runningState))
                {
                    leafContext.OnFinish();
                }
            }

            if (currStatus == ACTION_FINISHED)
            {
                if (leafContext._needExit)
                {
                    OnExit(workData, runningState);
                }
                else
                {
                    LogManager.Error("节点退出错误,[{0}]", TbName);
                }
                leafContext.OnExit();
            }

            return runningState;
        }

        protected override void OnTransition(BtWorkingData workData)
        {
            BtActionLeafContext leafContext = GetContext<BtActionLeafContext>(workData);
            if (leafContext._needExit)
            {
                OnExit(workData, BtRunningStatus.TRANSITION);
            }

            leafContext.ResetData();
        }

        protected T GetUserContextData<T>(BtWorkingData workData) where T : class, new()
        {
            BtActionLeafContext actionLeafContext = GetContext<BtActionLeafContext>(workData);
            T t = actionLeafContext.GetUserData<T>();
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
        protected virtual void OnExit(BtWorkingData workData, int runningStatus)
        {

        }

        #endregion
    }

}
