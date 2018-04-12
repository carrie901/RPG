using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Summer
{
    /// <summary>
    /// 技能流程线 触发
    /// </summary>
    public interface I_EntitySkill
    {

        //注册回调点
        bool RegisterHandler(E_EntityOutTrigger key, EventSet<E_EntityOutTrigger, EventEntityData>.EventHandler handler);

        //卸载回调点
        bool UnRegisterHandler(E_EntityOutTrigger key, EventSet<E_EntityOutTrigger, EventEntityData>.EventHandler handler);

        //触发回调点
        void RaiseEvent(E_EntityOutTrigger key, EventEntityData obj_info);
    }
}

