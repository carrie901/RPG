using System;
using System.Collections.Generic;
using Summer.AI;

namespace Summer
{

    public class NodeContext
    {

    }
    public class WorkingData : BtAny
    {
        public Dictionary<int, NodeContext> _context = new Dictionary<int, NodeContext>();
        public Dictionary<int, NodeContext> Context
        {
            get
            {
                return _context;
            }
        }

        //------------------------------------------------------
        public WorkingData() { }

        public T GetContext<T>(int tmp_unique_key) where T : NodeContext
        {
            if (_context.ContainsKey(tmp_unique_key))
            {
                T t = _context[tmp_unique_key] as T;
                return t;
            }
            return null;
        }

        public void AddContext(int key, NodeContext context)
        {
            if (_context.ContainsKey(key))
            {
                LogManager.Error("Key:[{0}] Error,Type:[{1}]", key, context.GetType());
            }
            else
            {
                _context.Add(key, context);
            }
        }

    }
}
