using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Summer.AI;
using Summer.Test;
public class CON_HasReachedTarget : TbPreconditionLeaf
{

    public override bool IsTrue(TbWorkingData work_data)
    {
        AIEntityWorkingData this_data = work_data.As<AIEntityWorkingData>();
        Vector3 target_pos = TMathUtils.Vector3ZeroY(this_data.Entity.GetBbValue<Vector3>(AIEntity.BBKEY_NEXTMOVINGPOSITION, Vector3.zero));
        Vector3 current_pos = TMathUtils.Vector3ZeroY(this_data.EntityTrans.position);
        return TMathUtils.GetDistance2D(target_pos, current_pos) < 1f;
    }
}
