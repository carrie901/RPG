
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

    public abstract class SkillFactory
    {
        public abstract SkillSequence Create(SkillContainer container, SpellInfoCnf spell_info);

        public SkillNode AddSkillNode(SkillSequence skill_sequence)
        {
            SkillNode anim_node = new SkillNode();
            skill_sequence.AddNode(anim_node);
            return anim_node;
        }

        public SkillNode AddSkillNode(SkillSequence skill_sequence, E_SkillTransition transition)
        {
            SkillNode anim_node = new SkillNode(transition);
            skill_sequence.AddNode(anim_node);
            return anim_node;
        }

        public SkillLeafNode CreateAnimation(SpellInfoCnf cnf)
        {
            PlayAnimationLeafNode pa = SkillNodeActionFactory.Create<PlayAnimationLeafNode>();
            pa.animation_name = cnf.anim_name;
            return pa;
        }

        public SkillLeafNode CreateChangeAnimationSpeed(float speed)
        {
            ChangeAnimationSpeedLeafNode leaf_node = SkillNodeActionFactory.Create<ChangeAnimationSpeedLeafNode>();
            leaf_node.speed = speed;
            return leaf_node;
        }

        public SkillLeafNode CreateEffect(SpellInfoCnf cnf)
        {
            PlayEffectLeafNode pe = SkillNodeActionFactory.Create<PlayEffectLeafNode>();
            pe.effect_name = "Prefab/Vfx/Skill/" + cnf.skill_effect[0];
            return pe;
        }

        public SkillLeafNode CreateFindTarget(SpellInfoCnf cnf)
        {
            FindTargetLeafNode find_target_leaf_node = SkillNodeActionFactory.Create<FindTargetLeafNode>();
            find_target_leaf_node.radius = 5;
            find_target_leaf_node.degree = 120;
            return find_target_leaf_node;
        }

        public SkillLeafNode CreateMoveToTargetLeafNode(SpellInfoCnf cnf)
        {
            MoveToTargetLeafNode node = SkillNodeActionFactory.Create<MoveToTargetLeafNode>();
            node.speed = 1;
            node.distance = 3;
            return node;
        }

        public SkillLeafNode CreateExportToTarget(SpellInfoCnf cnf)
        {
            ExportToTargetLeafNode target_leaf_node = SkillNodeActionFactory.Create<ExportToTargetLeafNode>();
            return target_leaf_node;
        }

        public SkillLeafNode CreateWait(SpellInfoCnf cnf)
        {
            WaitTimeLeafNodeNode wait_leaf_node_node = SkillNodeActionFactory.Create<WaitTimeLeafNodeNode>();
            wait_leaf_node_node.duration = 0.2f;
            return wait_leaf_node_node;
        }

        public SkillLeafNode CreateReleaseSkill(SpellInfoCnf cnf)
        {
            ReleaseSkillLeafNode release_skill_leaf = SkillNodeActionFactory.Create<ReleaseSkillLeafNode>();
            return release_skill_leaf;
        }

        public SkillLeafNode CreateSkillFinish(SpellInfoCnf cnf)
        {
            SkillFinishLeafNode finish_leaf_node = SkillNodeActionFactory.Create<SkillFinishLeafNode>();
            return finish_leaf_node;
        }
    }


    #region 一个普通的模板
    /// <summary>
    ///  一个普通的技能模板
    /// 1.播放技能特效，播放声音
    /// 2.击打目标
    /// 3.退出
    /// </summary>
    public class SkillSequenceNormal : SkillFactory
    {
        public override SkillSequence Create(SkillContainer container, SpellInfoCnf spell_info)
        {
            SkillSequence skill_sequence = new SkillSequence(container);

            {
                // 1.播放特效和动作，并且接受声音事件
                SkillNode anim_node = AddSkillNode(skill_sequence);

                anim_node.AddAction(CreateAnimation(spell_info));
                anim_node.AddAction(CreateEffect(spell_info));
            }

            {
                // 3.查找目标，并且输出技能到目标身上，接受动画播放完
                SkillNode trigger_colllion = AddSkillNode(skill_sequence, E_SkillTransition.anim_hit);


                trigger_colllion.AddAction(CreateFindTarget(spell_info));
                trigger_colllion.AddAction(CreateMoveToTargetLeafNode(spell_info));
                trigger_colllion.AddAction(CreateExportToTarget(spell_info));
                trigger_colllion.AddAction(CreateWait(spell_info));
            }

            {
                // 4.释放当前攻击
                SkillNode node = AddSkillNode(skill_sequence);

                node.AddAction(CreateReleaseSkill(spell_info));
            }

            {
                // 5.释放当前技能结束
                SkillNode release_skill = AddSkillNode(skill_sequence, E_SkillTransition.anim_finish);

                release_skill.AddAction(CreateSkillFinish(spell_info));
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

    }

    #endregion

    #region 赵云出生

    public class SkillZhaoYunBorn : SkillFactory
    {
        public override SkillSequence Create(SkillContainer container, SpellInfoCnf spell_info)
        {
            SkillSequence skill_sequence = new SkillSequence(container);

            {
                SkillNode anim_node = AddSkillNode(skill_sequence, E_SkillTransition.start);
                anim_node.AddAction(CreateAnimation(spell_info));
            }

            // 改变速度
            {
                SkillNode node = AddSkillNode(skill_sequence, E_SkillTransition.anim_event01);
                node.AddAction(CreateChangeAnimationSpeed(0.3f));
            }

            // 改变速度
            {
                SkillNode node = AddSkillNode(skill_sequence, E_SkillTransition.anim_event02);
                node.AddAction(CreateChangeAnimationSpeed(0.5f));
            }

            // 释放控制
            {
                SkillNode node = AddSkillNode(skill_sequence, E_SkillTransition.anim_release);
                node.AddAction(CreateReleaseSkill(spell_info));
            }
            {
                SkillNode node = AddSkillNode(skill_sequence, E_SkillTransition.anim_finish);
                node.AddAction(CreateSkillFinish(spell_info));
            }
            return skill_sequence;

        }
    }


    #endregion

    #region 赵云前冲

    public class SkillZhaoYunQianChong : SkillFactory
    {
        public override SkillSequence Create(SkillContainer container, SpellInfoCnf spell_info)
        {
            SkillSequence skill_sequence = new SkillSequence(container);

            {
                // 1.播放特效和动作，并且接受声音事件
                SkillNode anim_node = AddSkillNode(skill_sequence);

                anim_node.AddAction(CreateAnimation(spell_info));
                anim_node.AddAction(CreateEffect(spell_info));
            }

            // 释放控制
            {
                SkillNode node = AddSkillNode(skill_sequence, E_SkillTransition.anim_release);
                node.AddAction(CreateReleaseSkill(spell_info));

            }
            {
                SkillNode node = AddSkillNode(skill_sequence, E_SkillTransition.anim_finish);
                node.AddAction(CreateSkillFinish(spell_info));
            }


            return skill_sequence;
        }
    }

    #endregion

    public class SkillNodeActionFactory
    {
        public static T Create<T>() where T : SkillLeafNode, new()
        {
            T t = new T();
            return t;
        }
    }
}
