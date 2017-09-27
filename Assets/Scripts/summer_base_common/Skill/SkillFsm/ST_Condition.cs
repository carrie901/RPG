using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Summer
{
    public class ST_Condition
    {
        public static ST_Condition _full_pass = new ST_Condition(delegate { return true; });
        public static ST_Condition FullPass { get { return _full_pass; } }

        public Func<bool> _cond_func;
        public ST_Condition(Func<bool> cond_func)
        {
            LogManager.Assert(cond_func != null, "func=null");
            _cond_func = cond_func;
        }
        public bool Valid() { return _cond_func != null && _cond_func(); }
    }


}
