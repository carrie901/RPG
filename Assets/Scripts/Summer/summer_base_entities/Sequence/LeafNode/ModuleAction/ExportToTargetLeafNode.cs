
using System.Collections.Generic;


namespace Summer.Sequence
{
    /// <summary>
    /// 伤害输出
    /// </summary>
    public class ExportToTargetLeafNode : SequenceLeafNode
    {
        public const string DES = "==输出技能到目标身上==";
        public override void OnEnter(BlackBoard blackboard)
        {
            LogEnter();
            EntityExportToTargetData data = EventDataFactory.Pop<EntityExportToTargetData>();
            _export_to_target_damage(data, blackboard as EntityBlackBoard);
            EventDataFactory.Push(data);
            Finish();
        }

        public override void OnExit(BlackBoard blackboard)
        {
            LogExit();
        }
        public override void SetConfigInfo(EdNode cnf)
        {

        }
        public override string ToDes()
        {
            return DES;
        }

        #region private

        public List<BaseEntity> target_list;
        public void _export_to_target_damage(EntityExportToTargetData data, EntityBlackBoard blackboard)
        {
            if (data == null || blackboard == null) return;
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

