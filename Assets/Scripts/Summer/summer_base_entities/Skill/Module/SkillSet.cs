namespace Summer
{
    /// <summary>
    /// 技能的外壳
    /// 1.自身条件
    ///     血量低于xx之类的，比如
    /// 2.目标条件  
    ///     目标中了某种buff之类
    ///     目标距离：当目标和自身的距离在一定范围内，可以施放
    /// 3.技能使用前提条件
    /// 
    /// 攻击范围
    /// 
    /// TODO 2018.6.14 这块容器的结构很怪，另外就是虽然整体层次是按照行为树的结构层次来，但实际代码层次方面完全没有关系
    /// </summary>
    public class SkillSet : I_Update, I_RegisterHandler
    {
        public SkillContainer _skill_container;                                 // 技能容器

        public BaseEntity _base_entity;
        public bool _next_attack;                                               // 下一个普攻
        public bool _is_normal_attack;                                          // 处于普通状态
        public SkillSet(BaseEntity entity)
        {
            _base_entity = entity;
            _skill_container = new SkillContainer(entity);
        }

        #region

        public void OnRegisterHandler()
        {
            _base_entity.RegisterHandler(E_EntityInTrigger.skill_release, ReleaseSkill);
            _base_entity.RegisterHandler(E_EntityInTrigger.skill_finish, FinishSkill);

            _base_entity.RegisterHandler(E_EntityEvent.ANIMATION_EVENT, ReceiveAnimationEvent);
        }

        public void UnRegisterHandler()
        {

        }

        public void ReleaseSkill(EventSetData param)
        {
            _skill_container.ReleaseSkill();
        }

        // 技能结束
        public void FinishSkill(EventSetData param)
        {
            // 技能结束
            _skill_container.FinishSkill();
        }

        public void ReceiveAnimationEvent(EventSetData param)
        {
            AnimationEventData data = param as AnimationEventData;
            if (data == null) return;
            ReceiveTransitionEvent(data._eventData);
        }

        #endregion


        public void OnReset(int hero_id)
        {
            _skill_container.OnReset(hero_id);
        }

        public void OnUpdate(float dt)
        {
            if (_skill_container == null) return;

            if (_check_normal_attack())
            {

                bool result = _skill_container.CastAttack();
                if (result)
                {
                    _next_attack = false;
                }
            }

            _skill_container.OnUpdate(dt);
        }

        #region public 

        public void OnCastAttack()
        {
            _next_attack = true;
            _is_normal_attack = true;
        }

        public int _skill_id = 0;//10007;//10013 // 10008
        public void CastSkill()
        {
            if (_skill_id == 0)
                OnCastAttack();
            else
                OnCastSkill(_skill_id);
        }

        // 释放技能
        public void OnCastSkill(int skill_id)
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
