
namespace Summer.AI
{
    public class TbActionContext
    {

    }

    public abstract class BtAction : BtTreeNode
    {
        #region 属性

        protected int _unique_key = 0;
        protected BtPreconditionNode _precondition;

        protected string _tb_name;
        public string TbName { get { return _tb_name; } set { _tb_name = value; } }

        #endregion

        #region 构造

        public BtAction(int max_child_count) : base(max_child_count)
        {
            _unique_key = BtAction.GenUniqueKey();
        }

        #endregion

        #region public 

        public bool Evaluate(BtWorkingData work_data)
        {
            return (_precondition == null || _precondition.IsTrue(work_data)) && OnEvaluate(work_data);
        }

        public int Update(BtWorkingData work_data)
        {
            return OnUpdate(work_data);
        }

        public void Transition(BtWorkingData work_data)
        {
            OnTransition(work_data);
        }

        public BtAction SetPrecondition(BtPreconditionNode precondition)
        {
            _precondition = precondition;
            return this;
        }

        public override int GetHashCode()
        {
            return _unique_key;
        }

        #endregion

        #region protected virtual --> OnEvaluate.OnUpdate.OnTransition

        protected T GetContext<T>(BtWorkingData work_data) where T : TbActionContext, new()
        {
            int tmp_unique_key = GetHashCode();
            T this_context;
            if (work_data.Context.ContainsKey(tmp_unique_key) == false)
            {
                this_context = new T();
                work_data.Context.Add(tmp_unique_key, this_context);
            }
            else
            {
                this_context = work_data.Context[tmp_unique_key] as T;
            }
            return this_context;
        }

        #region virtual

        protected virtual bool OnEvaluate(BtWorkingData work_data)
        {
            return true;
        }

        protected virtual int OnUpdate(BtWorkingData work_data)
        {
            return 0;
        }

        protected virtual void OnTransition(BtWorkingData work_data)
        {

        }

        #endregion

        #endregion

        #region 唯一标志
        protected static int s_unique_key = 0;
        protected static int GenUniqueKey()
        {
            s_unique_key = s_unique_key >= int.MaxValue ? 0 : (s_unique_key + 1);
            return s_unique_key;
        }

        #endregion
    }
}

