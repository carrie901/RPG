using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Summer
{

    /// <summary>
    /// 查找目标 
    /// TODO 通过依赖连来查找目标  范围/敌友/等等其他条件 一次次把结果传递
    /// </summary>
    public class FindTargetEntites : ASkillAction
    {
        //TODO 希望能通过抽象来描述查找目标
        public float radius;        //距离
        public float degree;        //角度
        public override void OnEnter()
        {

        }

        public override void OnExit()
        {

        }

        public override void OnUpdate(float dt)
        {
            
        }
    }

}
