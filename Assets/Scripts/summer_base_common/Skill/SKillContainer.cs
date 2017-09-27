using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Summer
{
    public class SkillCont
    {
        public Dictionary<long, Timer> _buff_expire_timer; //用caster_iid + buff_id做key
        public Dictionary<int, SkillState> _skill_map;//SkillState cur_state;


        #region Update

        public void Update()
        {

        }

        public void FixedUpdate()
        {

        }

        public void LateUpdate()
        {

        }

        #endregion

        public void Add(int id)
        {
            
        }

        public void Remove(int id) { }

    }

}

