using UnityEngine;

namespace Summer
{
    public class EventEntityOpertionData : EventSetData
    {
        public int value;                                   //数字 -=伤害 +=治疗
        public Vector3 _position_target;                    //目标位置
        public bool is_crit;                                //true=暴击
        public BaseEntity _caster;            //攻击者
        public BaseEntity _target;            //目标
        public int attack_type;                             //攻击手段


        //是否为伤害/治疗
        public bool is_damage()
        {
            return value > 0;
        }

        //抵消伤害
        public void reset_damge(int damge)
        {
            value = damge;
            if (value > 0)
                value = 0;
        }
    }
}
