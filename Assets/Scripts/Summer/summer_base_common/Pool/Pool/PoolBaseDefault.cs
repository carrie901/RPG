using UnityEngine;
using System.Collections;

namespace Summer
{
    /// <summary>
    /// 默认池子
    /// </summary>
    public class PoolBaseDefault : PoolBase
    {

        public PoolBaseDefault(I_ObjectFactory factory, int max_count = 0) : base(factory, max_count)
        {
        }
    }
}
