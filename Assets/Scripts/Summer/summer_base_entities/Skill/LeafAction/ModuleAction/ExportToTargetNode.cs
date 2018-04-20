using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Summer
{
    public class ExportToTargetNode : SkillNodeAction
    {
        public const string DES = "==输出技能到目标身上==";
        public override void OnEnter()
        {
            LogEnter();
            EntityExportToTargetData data = EventDataFactory.Pop<EntityExportToTargetData>();
            RaiseEvent(E_EntityInTrigger.export_to_target, data);
            Finish();
        }

        public override void OnExit()
        {
            LogExit();
        }

        public override void OnUpdate(float dt)
        {

        }

        public override string ToDes()
        {
            return DES;
        }
    }
}

