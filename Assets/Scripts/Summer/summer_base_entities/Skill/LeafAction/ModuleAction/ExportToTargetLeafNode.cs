using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Summer
{
    /// <summary>
    /// 伤害输出
    /// </summary>
    public class ExportToTargetLeafNode : SkillLeafNode
    {
        public const string DES = "==输出技能到目标身上==";
        public override void OnEnter(EntityBlackBoard blackboard)
        {
            LogEnter();
            EntityExportToTargetData data = EventDataFactory.Pop<EntityExportToTargetData>();
            RaiseEvent(E_EntityInTrigger.export_to_target, data);
            Finish();
        }

        public override void OnExit(EntityBlackBoard blackboard)
        {
            LogExit();
        }

        public override void OnUpdate(float dt, EntityBlackBoard blackboard)
        {

        }

        public override string ToDes()
        {
            return DES;
        }
    }
}

