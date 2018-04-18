

namespace Summer
{
    public class SkillSet : I_Update
    {
        public SkillContainer _skill_container;                                 // 技能容器
        public BaseEntity entity;

        public bool _next_attack;                                               // 下一个普攻
        public bool _is_normal_attack;                                          // 处于普通状态
        public SkillSet(BaseEntity entity)
        {
            this.entity = entity;
            _skill_container = new SkillContainer(entity);
        }

        public void OnReset(int hero_id)
        {
            _skill_container.OnReset(hero_id);
        }

        public void OnUpdate(float dt)
        {
            if (_skill_container == null) return;

            if (_check_normal_attack())
            {
                _next_attack = false;
                _skill_container.CastAttack();
            }

            _skill_container.OnUpdate(dt);
        }

        #region public 

        public void CastAttack()
        {
            _next_attack = true;
            _is_normal_attack = true;
        }

        // 释放技能
        public void CastSkill(int skill_id)
        {
            bool result = _skill_container.CastSkill(skill_id);
            if (result)
            {
                _is_normal_attack = false;
                _next_attack = false;
            }
        }

        // 接受外部的游戏事件
        public void ReceiveTransitionEvent(E_SkillTransition transition_event)
        {
            if (_skill_container != null)
                _skill_container.ReceiveTransitionEvent(transition_event);
        }

        #endregion

        #region

        public bool _check_normal_attack()
        {
            // 处于普攻状态，并且有下一个普攻标志，并且普攻已经可以转换
            if (_is_normal_attack && _next_attack)
            {
                return true;
            }

            return false;
        }

        #endregion
    }
}
