namespace Summer
{
    public class EntityExportToTarget : I_EntityAction
    {
        public void OnAction(BaseEntity entity, EventSetData param)
        {
            EntityExportToTargetData data = param as EntityExportToTargetData;
            if (data == null) return;
            int length = entity._targets.Count;
            for (int i = 0; i < length; i++)
            {
                LogManager.Log("对目标:[{0}]造成[{1}]点伤害", entity._targets[i].ToDes(), data.damage);
            }
            entity._targets.Clear();
        }
    }
}
