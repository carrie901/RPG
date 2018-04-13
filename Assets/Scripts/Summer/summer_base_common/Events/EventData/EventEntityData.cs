
using System.Collections.Generic;
using UnityEngine;

namespace Summer
{
    #region 播放声音

    /// <summary>
    /// 播放声音
    /// </summary>
    public class PlaySoundEventSkill : EventSetData
    {
        public string sound_name;
        public Vector3 position;
    }

    #endregion

    #region 播放动作

    /// <summary>
    /// 播放动作
    /// </summary>
    public class PlayAnimationEventData : EventSetData
    {
        public string animation_name;
    }

    #endregion

    #region 查找目标

    /// <summary>
    /// 查找目标
    /// </summary>
    public class EntityFindTargetData : EventSetData
    {
        public List<BaseEntityController> _targets = new List<BaseEntityController>(16);
        public override void Reset()
        {
            _targets.Clear();
        }
    }

    #endregion

    #region 输出伤害

    public class EntityExportToTargetData : EventSetData
    {
        public float damage = 100;
    }


    #endregion

    #region 



    #endregion

    #region 



    #endregion
}
