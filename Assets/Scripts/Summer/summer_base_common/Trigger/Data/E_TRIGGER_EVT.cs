using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Summer
{
    /// <summary>
    /// 触发器
    /// </summary>
    public enum E_TRIGGER_EVT
    {
        none = 0,
    }

    public class TriggerEqualityComparer : IEqualityComparer<E_TRIGGER_EVT>
    {
        public static TriggerEqualityComparer Instance = new TriggerEqualityComparer();

        private TriggerEqualityComparer()
        {

        }
        public bool Equals(E_TRIGGER_EVT x, E_TRIGGER_EVT y)
        {
            return x == y;
        }

        public int GetHashCode(E_TRIGGER_EVT obj)
        {
            return (int)obj;
        }
    }
}
