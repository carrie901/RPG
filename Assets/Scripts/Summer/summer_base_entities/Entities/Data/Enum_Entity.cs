using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Summer
{
    #region 人物相关事件
    // TODO 概念 由外部触发，然后内部执行一些行为，比如我收到攻击了，我应该怎么办 可以变相的让E_BuffTrigger事件消失或者说更加纯粹华
    /// <summary>
    /// 触发人物触发的相关事件
    /// </summary>
    public enum E_EntityOutTrigger
    {
        none,
        play_effect,                //播放特效
        play_sound,                 //播放声音
        play_animation,             //播放动作


        //play_camera_shake,          //镜头抖动
        //play_camera_effect,         //镜头特效
        //play_camera_offset,         //镜头偏移，提供机制回复到原始位置
        max,
    }

    // TODO 概念由Entity自身发出某一个事件，然后让entity自己做行为，目前无法确认是否有必要
    // 1. 比如播放动作 可以有外部触发播放一个动作，也有可能让内部触发播放一个动作
    // 2. 内部触发一个有生命周期的行为
    public enum E_EntityInTrigger
    {

    }

    /// <summary>
    /// 来一个EventData的缓存池
    /// </summary>
    public class EventEntityData
    {
        public virtual void Reset()
        {

        }
    }

    public class EventEntityDataFactory
    {
        public static T Push<T>() where T : EventEntityData, new()
        {
            T t = new T();
            return t;
        }

        public static void Pop<T>(T t) where T : EventEntityData
        {
            t.Reset();
        }
    }

    #endregion
}
