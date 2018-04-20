
using System.Collections.Generic;
using UnityEngine;

namespace Summer
{
    #region 播放声音

    /// <summary>
    /// 播放声音
    /// </summary>
    public class PlaySoundEventData : EventSetData
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

    #region 动作停顿几帧
    public class AnimationSpeedEventData : EventSetData
    {
        public float animation_speed;
    }
    #endregion

    #region 播放特效

    /// <summary>
    /// 播放特效的参数
    /// </summary>
    public class PlayEffectEventSkill : EventSetData
    {
        public string effect_name;
        public GameObject bing_obj;

        public override void Reset()
        {
            bing_obj = null;
        }
    }


    #endregion

    #region 查找目标

    /// <summary>
    /// 查找目标
    /// </summary>
    public class EntityFindTargetData : EventSetData
    {
        public float radius;        //距离
        public float degree;        //角度
        //public List<BaseEntity> _targets = new List<BaseEntity>(16);
        public override void Reset()
        {
            //_targets.Clear();
        }
    }

    #endregion

    #region 输出伤害

    public class EntityExportToTargetData : EventSetData
    {
        public float damage = 100;
    }


    #endregion

    #region 外部触发的动画事件

    public class AnimationEventData : EventSetData
    {
        public E_SkillTransition event_data;
    }

    #endregion

    #region 改变状态

    public class ChangeEntityStateEventData : EventSetData
    {
        public E_StateId state_id;
    }

    #endregion

    #region 



    #endregion

    #region 



    #endregion

    #region 



    #endregion

    #region 



    #endregion

    #region 



    #endregion

    #region 



    #endregion

    #region 



    #endregion

    #region 



    #endregion


}
