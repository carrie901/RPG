
namespace Summer.AI
{
    public class BtActionContext
    {
        
    }
    public abstract class BtAction : BtTreeNode
    {
        #region 属性

        protected readonly int _uniqueKey = 0;
        protected BtPreconditionNode _precondition;

        protected string _tbName;
        public string TbName { get { return _tbName; } set { _tbName = value; } }

        #endregion

        #region 构造

        public BtAction(int maxChildCount) : base(maxChildCount)
        {
            _uniqueKey = BtAction.GenUniqueKey();
        }

        #endregion

        #region public 

        public bool Evaluate(BtWorkingData workData)
        {
            return (_precondition == null || _precondition.IsTrue(workData)) && OnEvaluate(workData);
        }

        public int Update(BtWorkingData workData)
        {
            return OnUpdate(workData);
        }

        public void Transition(BtWorkingData workData)
        {
            OnTransition(workData);
        }

        public BtAction SetPrecondition(BtPreconditionNode precondition)
        {
            _precondition = precondition;
            return this;
        }

        public override int GetHashCode()
        {
            return _uniqueKey;
        }

        #endregion

        #region protected virtual --> OnEvaluate.OnUpdate.OnTransition

        /// <summary>
        /// 根据节点的Hashcode值得到节点的ACtionContext
        /// </summary>
        protected T GetContext<T>(BtWorkingData workData) where T : BtActionContext, new()
        {
            int tmpUniqueKey = GetHashCode();
            T t = workData.GetContext<T>(tmpUniqueKey);
            if (t == null)
            {
                t = new T();
                workData.AddContext(tmpUniqueKey, t);
            }

            return t;
        }

        #region virtual

        protected virtual bool OnEvaluate(BtWorkingData workData)
        {
            return true;
        }

        protected virtual int OnUpdate(BtWorkingData workData)
        {
            return 0;
        }

        protected virtual void OnTransition(BtWorkingData workData)
        {

        }

        #endregion

        #endregion

        #region 唯一标志
        protected static int _sUniqueKey;
        protected static int GenUniqueKey()
        {
            _sUniqueKey = _sUniqueKey >= int.MaxValue ? 0 : (_sUniqueKey + 1);
            return _sUniqueKey;
        }

        #endregion
    }
}

