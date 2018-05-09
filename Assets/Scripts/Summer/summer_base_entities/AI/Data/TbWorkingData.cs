using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Summer.AI
{
    public class TbWorkingData : TbAny
    {
        public Dictionary<int, TbActionContext> _context = new Dictionary<int, TbActionContext>();
        public Dictionary<int, TbActionContext> Context
        {
            get
            {
                return _context;
            }
        }
        //------------------------------------------------------
        public TbWorkingData()
        {

        }
    }
}
