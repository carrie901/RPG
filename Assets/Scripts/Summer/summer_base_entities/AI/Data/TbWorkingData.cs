using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Summer;
using UnityEditor;
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
