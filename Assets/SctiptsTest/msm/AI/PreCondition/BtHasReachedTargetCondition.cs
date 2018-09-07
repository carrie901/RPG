
using UnityEngine;
using Summer.AI;
using Summer.Test;

namespace Summer
{
    /// <summary>
    /// 到达指定目的地
    /// </summary>
    public class BtHasReachedTargetCondition : TbPreconditionLeaf
    {
        public override bool IsTrue(BtWorkingData workData)
        {
            AIEntityWorkingData this_data = workData.As<AIEntityWorkingData>();
            Vector3 target_pos = MathHelper.Vector3ZeroY(this_data.EntityAi.GetBbValue<Vector3>(BtEntityAi.BBKEY_NEXTMOVINGPOSITION, Vector3.zero));
            Vector3 current_pos = MathHelper.Vector3ZeroY(this_data.EntityAi.BaseEntity.WroldPosition);
            bool result = TMathUtils.GetDistance2D(target_pos, current_pos) < 1f;
            return result;
        }
    }

}
