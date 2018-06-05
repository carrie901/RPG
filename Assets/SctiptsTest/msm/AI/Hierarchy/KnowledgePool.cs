using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Summer.AI
{
    /// <summary>
    /// 知识池，世界信息的数据存储
    /// 其实就是一个大型的黑板数据盒子
    /// 也可以分成多个知识池来管理不同类型的数据
    /// 关键是要有清晰的世界信息获取方式
    /// </summary>
    public class KnowledgePool
    {
        public static Dictionary<string, Object> _know_map = new Dictionary<string, object>();
        public static Object Get(string key)
        {
            return null;
        }
    }
}
