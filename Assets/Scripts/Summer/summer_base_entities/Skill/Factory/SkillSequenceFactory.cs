
namespace Summer
{
    /// <summary>
    /// 1.通过配置文件来进行流程模板化
    /// 2.通过工厂的方式来进行组装模板化
    /// </summary>
    public interface I_SkillSequenceFactory
    {
        SkillSequence Create(SkillContainer container, SpellInfoCnf spell_info);
    }

    #region 一个普通的模板
    /// <summary>
    ///  一个普通的技能模板
    /// 1.播放技能特效，播放声音
    /// 2.击打目标
    /// 3.退出
    /// </summary>
    public class SkillSequenceNormal : I_SkillSequenceFactory
    {
        public SpellInfoCnf _spell_info;
        public SkillSequence Create(SkillContainer container, SpellInfoCnf spell_info)
        {
            _spell_info = spell_info;
            SkillSequence skill_sequence = new SkillSequence(container);

            {
                // 1.播放特效和动作，并且接受声音事件
                SkillNode anim_node = new SkillNode();
                skill_sequence.AddNode(anim_node);

                anim_node.AddAction(CreateAnimation());
                anim_node.AddAction(CreateEffect());
            }

            {
                // 3.查找目标，并且输出技能到目标身上，接受动画播放完
                SkillNode trigger_colllion = new SkillNode(E_SkillTransition.anim_hit);
                skill_sequence.AddNode(trigger_colllion);

                trigger_colllion.AddAction(CreateFindTarget());
                trigger_colllion.AddAction(CreateExportToTarget());
                trigger_colllion.AddAction(CreateWait());
            }

            {
                // 4.释放当前攻击
                SkillNode node = new SkillNode();
                skill_sequence.AddNode(node);

                node.AddAction(CreateReleaseAttack());
            }

            {
                // 5.释放当前技能结束
                SkillNode release_skill = new SkillNode(E_SkillTransition.anim_finish);
                skill_sequence.AddNode(release_skill);

                release_skill.AddAction(CreateSkillRelease());
            }


            //1.技能触发 接受技能触发事件SkillCast
            //2.朝向目标 播放之后自动转到下一个节点
            //  2.1 播放动作 动作名字
            //  2.2 播放特效 播放动作，指定绑定节点
            //3.播放声音 接受声音事件
            //4.攻击类型设置/超找目标
            //5.等待时间
            //
            return skill_sequence;
        }

        public SkillNodeAction CreateAnimation()
        {
            PlayAnimationAction pa = SkillNodeActionFactory.Create<PlayAnimationAction>();
            pa.animation_name = _spell_info.anim_name;
            return pa;
        }

        public SkillNodeAction CreateEffect()
        {
            PlayEffectAction pe = SkillNodeActionFactory.Create<PlayEffectAction>();
            pe.effect_name = "Prefab/Vfx/Skill/" + _spell_info.skill_effect[0];
            return pe;
        }

        public SkillNodeAction CreateFindTarget()
        {
            FindTargetAction find_target_action = SkillNodeActionFactory.Create<FindTargetAction>();
            find_target_action.radius = 5;
            find_target_action.degree = 60;
            return find_target_action;
        }

        public SkillNodeAction CreateExportToTarget()
        {
            ExportToTargetAction target_action = SkillNodeActionFactory.Create<ExportToTargetAction>();
            return target_action;
        }

        public SkillNodeAction CreateWait()
        {
            WaitTimeAction wait_action = SkillNodeActionFactory.Create<WaitTimeAction>();
            wait_action.duration = 0.2f;
            return wait_action;
        }

        public SkillNodeAction CreateReleaseAttack()
        {
            ReleaseAttackAction release_attack = SkillNodeActionFactory.Create<ReleaseAttackAction>();
            return release_attack;
        }

        public SkillNodeAction CreateSkillRelease()
        {
            SkillFinishAction finish_action = SkillNodeActionFactory.Create<SkillFinishAction>();
            return finish_action;
        }
    }

    #endregion

    public class SkillNodeActionFactory
    {
        public static T Create<T>() where T : SkillNodeAction, new()
        {
            T t = new T();
            return t;
        }
    }
}
