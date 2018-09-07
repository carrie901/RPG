using System.Collections.Generic;
namespace Summer.AI
{
    /// <summary>
    /// 行为树的数据结构，内部包含了很多个ActionContext
    /// 这个个结构体被继承
    /// 
    /// 全局域（G）：此数据可以给其他行为树访问
    /// 行为树域（T）：此数据可以给行为树内的任意节点访问
    /// 指定节点域（N）：此数据可以给指定的行为树内的某节点（可以是多个）访问
    /// </summary>
    public class BtWorkingData : BtAny
    {
        public Dictionary<int, BtActionContext> _context = new Dictionary<int, BtActionContext>();
        public Dictionary<int, BtActionContext> Context
        {
            get
            {
                return _context;
            }
        }
        //------------------------------------------------------
        public BtWorkingData()
        {

        }

        public T GetContext<T>(int tmpUniqueKey) where T : BtActionContext
        {
            if (_context.ContainsKey(tmpUniqueKey))
            {
                T t = _context[tmpUniqueKey] as T;
                return t;
            }
            return null;
        }

        public void AddContext(int key, BtActionContext context)
        {
            if (_context.ContainsKey(key))
            {
                LogManager.Error("key:[{0}] Error,Type:[{1}]", key, context.GetType());
            }
            else
            {
                _context.Add(key, context);
            }
        }

    }

}
