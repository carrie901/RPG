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
            _export_to_target_damage(data, blackboard);
            EventDataFactory.Push(data);
            Finish();
        }

        public override void OnExit(EntityBlackBoard blackboard)
        {
            LogExit();
        }

        public override string ToDes()
        {
            return DES;
        }

        #region private

        public List<BaseEntity> target_list;
        public void _export_to_target_damage(EntityExportToTargetData data, EntityBlackBoard blackboard)
        {
            if (data == null) return;
            BaseEntity entity = blackboard.entity;
            target_list = blackboard.GetValue<List<BaseEntity>>(EntityBlackBoardConst.TARGET_LIST);
            int length = target_list.Count;
            for (int i = 0; i < length; i++)
            {
                DamageHelper.ExportDamageToTarget(entity, target_list[i], 10);
                ActionLog.Log("对目标:[{0}]造成[{1}]点伤害", target_list[i].ToDes(), data.damage);
            }
            target_list.Clear();
        }

        #endregion
    }
}

