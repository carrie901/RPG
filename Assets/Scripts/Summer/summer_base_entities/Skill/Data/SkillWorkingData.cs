using System.Collections.Generic;
using Summer.AI;

namespace Summer
{
    public class SkillNodeContext
    {

    }

    /// <summary>
    /// 技能树的黑箱
    /// </summary>
    public class SkillWorkingData : BtAny
    {
        public Dictionary<int, SkillNodeContext> _context = new Dictionary<int, SkillNodeContext>();
        public Dictionary<int, SkillNodeContext> Context
        {
            get
            {
                return _context;
            }
        }
        //------------------------------------------------------
        public SkillWorkingData()
        {

        }

        public T GetContext<T>(int tmp_unique_key) where T : SkillNodeContext
        {
            if (_context.ContainsKey(tmp_unique_key))
            {
                T t = _context[tmp_unique_key] as T;
                return t;
            }
            return null;
        }

        public void AddContext(int key, SkillNodeContext context)
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
