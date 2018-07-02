using System.Collections.Generic;

namespace Summer
{
    // TODO 概念 由外部触发，然后内部执行一些行为，比如我收到攻击了，我应该怎么办 可以变相的让E_BuffTrigger事件消失或者说更加纯粹华
    // 1. 比如播放动作 可以有外部触发播放一个动作，也有可能让内部触发播放一个动作
    // 2. 内部触发一个有生命周期的行为
    // 3. 这个和GameSystemEvent的差别在于 一个是全局性质的，一个是针对个人的
    public enum E_Entity_Event
    {
        none = 0,
        animation_event,                                    // 由动作事件作为源头，发出事件

        on_hit_damage = 1001,                               // 击中目标，造成伤害       

        on_be_hurt = 2001,                                  // 收到伤害之前  
        on_be_attack_damage = 2002,                         // 自身被攻击，收到了X伤害
        on_before_killed,                                   // 必死之前
        on_after_killed,                                    // 必死之后
        on_died = 3001,                                     // 自身死亡

    }

    public class EntityEvtComparer : TSingleton<EntityEvtComparer>, IEqualityComparer<E_Entity_Event>
    {
        public bool Equals(E_Entity_Event x, E_Entity_Event y)
        {
            return x == y;
        }

        public int GetHashCode(E_Entity_Event obj)
        {
            return (int)obj;
        }
    }
}