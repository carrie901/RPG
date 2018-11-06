using System.Collections.Generic;
using Summer.Sequence;

namespace Summer
{
    /// <summary>
    /// SkillContainer 容器的功能其实不太纯粹
    /// </summary>
    public class SkillContainer : I_Update
    {
        #region 属性

        public Dictionary<int, SequenceLine> _sequence_map
            = new Dictionary<int, SequenceLine>(8);                                         // Entity的技能容器
        public SequenceLine _curr_sequece;                                                  // 当前技能序列
        public BaseEntity _entity;                                                          // 内部触发器

        public bool _can_cast_skill;                                                        // 可以释放下一个技能了

        public float _last_time;

        public SkillNormalAttack normal_attack = new SkillNormalAttack();

        #endregion

        public SkillContainer(BaseEntity entity)
        {
            _entity = entity;
        }

        public void OnReset(int hero_id)
        {
            _sequence_map.Clear();

            // 初始化技能列表
            HeroInfoCnf hero_cnf = StaticCnf.FindData<HeroInfoCnf>(hero_id);
            int[] skill_list = hero_cnf.skillid_list;
            int length = skill_list.Length;
            int skill_type_normal_attack = (int)E_SkillType.normal;


            for (int i = 0; i < length; i++)
            {
                if (skill_list[i] == 0) continue;
                // 确定普通攻击
                SpellInfoCnf space_info = StaticCnf.FindData<SpellInfoCnf>(skill_list[i]);
                if (space_info.skill_types == skill_type_normal_attack)
                    normal_attack.AddSkill(skill_list[i]);
                /*SkillFactory skill = null;
                //
                if (space_info.process_template == "normal")
                {
                    skill = new SkillSequenceNormal();
                }
                else if (space_info.process_template == "born")
                {
                    skill = new SkillZhaoYunBorn();
                }
                else if (space_info.process_template == "qianchong")
                {
                    skill = new SkillZhaoYunQianChong();
                }
                else if (space_info.process_template == "tiaokong")
                {
                    skill = new SkillZhaoYunTiao();
                }
                if (skill == null) continue;
                _sequence_map.Add(skill_list[i], skill.Create(this, space_info));*/

                _sequence_map.Add(skill_list[i], SkillFactory.Create());

            }

            _can_cast_skill = true;
        }

        #region Update

        public void OnUpdate(float dt)
        {
            // 通过时间来触发事件，
            //SkillContainerTest.OnUpdate(dt);

            if (_curr_sequece == null) return;


            _curr_sequece.OnUpdate(dt);

            // TODO test code
            float curr_time = TimeModule.RealtimeSinceStartup;
            if (curr_time - _last_time > 15.0)
            {
                LogManager.Error("技能释放错误,超过时间，Skill:{0}", _curr_sequece);
                _last_time = TimeModule.RealtimeSinceStartup;
            }
        }


        #endregion

        #region public 

        public EntityBlackBoard GetBlackboard()
        {
            return _entity.GetBlackBorad();
        }


        public bool CastAttack()
        {
            if (!CanCastSkill()) return false;
            int skill_id = normal_attack.Cast();
            CastSkill(skill_id);
            return true;
        }

        public bool _test = false;

        // 释放技能普攻
        public void ReleaseSkill()
        {
            SkillLog.Log("======================释放技能控制======================");
            _test = false;
            _can_cast_skill = true;
        }

        public void FinishSkill()
        {
            normal_attack.ResetAttack();
            EntityEventFactory.ChangeInEntityState(_entity, E_StateId.idle);
        }

        public bool CastSkill(int id)
        {
            if (!CanCastSkill()) return false;
            EntityEventFactory.ChangeInEntityState(_entity, E_StateId.attack);
            SkillLog.Assert(!_test, "释放技能bug:[{0}]", id);
            SkillLog.Log("======================释放技能:[{0}]======================", id);
            _test = true;
            _can_cast_skill = false;
            // TODO QAQ:有bug的可能性很大，例如技能释放到一半的时候，释放了另外一个技能，这样就需要破坏掉原来的技能
            _curr_sequece = _sequence_map[id];
            //_curr_sequece.OnStart();
            _last_time = TimeModule.RealtimeSinceStartup;

            return true;
        }

        public bool CanCastSkill()
        {
            return _can_cast_skill;
        }

        // 接收到指定的事件
        public void ReceiveTransitionEvent(E_SkillTransition transition_event)
        {
            LogManager.Error("接收到指定的事件 还没开始做");
            /*if (_curr_sequece != null)
                _curr_sequece.ReceiveWithOutEvent(transition_event);*/
        }

        public void OnFinish()
        {
            _curr_sequece = null;
        }

        #endregion
    }


    /// <summary>
    /// 普攻
    /// </summary>
    public class SkillNormalAttack
    {
        public List<int> _skill_combo = new List<int>();
        public int _index;

        public SkillNormalAttack()
        {
            _index = -1;
        }
        public int Cast()
        {
            _index++;
            if (_index >= _skill_combo.Count)
                _index = 0;
            return _skill_combo[_index];
        }

        public void AddSkill(int skill_id)
        {
            _skill_combo.Add(skill_id);
        }

        public void ResetAttack()
        {
            _index = -1;
        }
    }
}

