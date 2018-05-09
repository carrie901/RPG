
namespace Summer.AI
{
    public class TbActionContext
    {

    }

    public abstract class TbAction : TbTreeNode
    {
        #region 属性

        protected int _unique_key = 0;
        protected TbPrecondition _precondition;

        protected string _tb_name;
        public string TbName { get { return _tb_name; } set { _tb_name = value; } }

        #endregion

        #region 构造

        public TbAction(int max_child_count) : base(max_child_count)
        {
            _unique_key = TbAction.GenUniqueKey();
        }

        #endregion

        #region public 

        public bool Evaluate(TbWorkingData work_data)
        {
            return (_precondition == null || _precondition.IsTrue(work_data)) && OnEvaluate(work_data);
        }

        public int Update(TbWorkingData work_data)
        {
            return OnUpdate(work_data);
        }

        public void Transition(TbWorkingData work_data)
        {
            OnTransition(work_data);
        }

        public TbAction SetPrecondition(TbPrecondition precondition)
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

        protected T GetContext<T>(TbWorkingData work_data) where T : TbActionContext, new()
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

        protected virtual bool OnEvaluate(TbWorkingData work_data)
        {
            return true;
        }

        protected virtual int OnUpdate(TbWorkingData work_data)
        {
            return 0;
        }

        protected virtual void OnTransition(TbWorkingData work_data)
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

