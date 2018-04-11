﻿using System.Collections.Generic;
using UnityEngine;

namespace Summer
{
    public class SkillContainer : MonoBehaviour
    {
        public Dictionary<int, SkillSequence> _skill_map
            = new Dictionary<int, SkillSequence>(8);                    // SkillNode cur_state;
        public SkillSequence _curr_sequen;                              // 当前序列

        public float _last_time;                                        // 
        public bool test;
        void Awake()
        {
            SkillContainerTest.container = this;
            _skill_map.Clear();
            _skill_map.Add(1, SkillContainerTest.Create());
        }

        #region Update

        void Update()
        {
            if (test)
            {
                SkillContainerTest._last_time = 0;
                CastSkill(1);
                test = false;
                return;
            }

            float dt = TimeManager.RealtimeSinceStartup - _last_time;
            _last_time = TimeManager.RealtimeSinceStartup;

            // 通过时间来触发事件，
            SkillContainerTest.OnUpdate(dt);

            if (_curr_sequen == null) return;

            _curr_sequen.OnUpdate(dt);
        }

        #endregion

        public void Add(int id)
        {

        }

        public void Remove(int id) { }

        public void CastSkill(int id)
        {
            _last_time = TimeManager.DeltaTime;
            _curr_sequen = _skill_map[id];
            _curr_sequen.OnStart();
        }

        // 接收到指定的事件
        public void ReceiveTransitionEvent(E_SkillTransition transition_event)
        {
            if (_curr_sequen != null)
                _curr_sequen.ReceiveWithOutEvent(transition_event);
        }
    }


    public class SkillContainerTest
    {
        public static SkillContainer container;
        public static float _last_time = -1;

        public static List<E_SkillTransition> events = new List<E_SkillTransition>();
        public static SkillSequence Create()
        {
            SkillSequence skill_sequence = new SkillSequence();

            // 1.播放特效和动作，并且接受声音事件
            SkillNode anim_node = new SkillNode("1.播放特效和动作");
            skill_sequence.AddNode(anim_node);
            //anim_node.AddTransitionEvent(E_SkillTransition.start);

            //events.Add(E_SkillTransition.start);


            // 2.播放声音，接受打击事件
            SkillNode sound_node = new SkillNode("2.播放声音");
            skill_sequence.AddNode(sound_node);
            sound_node.AddTransitionEvent(E_SkillTransition.sound);

            events.Add(E_SkillTransition.sound);

            // 3.查找目标，并且输出技能到目标身上，接受动画播放完
            SkillNode trigger_colllion = new SkillNode("查找目标，并且输出技能到目标身上");
            skill_sequence.AddNode(trigger_colllion);
            trigger_colllion.AddTransitionEvent(E_SkillTransition.anim_hit);

            events.Add(E_SkillTransition.anim_hit);

            // 4.释放当前技能结束
            SkillNode release_skill = new SkillNode("释放当前技能结束");
            skill_sequence.AddNode(release_skill);
            release_skill.AddTransitionEvent(E_SkillTransition.anim_finish);

            events.Add(E_SkillTransition.anim_finish);

            {
                PlayAnimationAction pa = new PlayAnimationAction();
                pa.animation_name = "==普攻==";
                anim_node.AddAction(pa);

                PlayEffectAction pe = new PlayEffectAction();
                pe.effect_name = "==炫光==";
                anim_node.AddAction(pe);
            }


            {
                PlaySoundAction ps = new PlaySoundAction();
                ps.sound_name = "==普攻的声音==";
                sound_node.AddAction(ps);
            }

            {
                FindTargetAction find_target_action = new FindTargetAction();
                trigger_colllion.AddAction(find_target_action);

                ExportToTargetAction target_action = new ExportToTargetAction();
                trigger_colllion.AddAction(target_action);
            }

            {
                SkillReleaseAction release_action = new SkillReleaseAction();
                release_skill.AddAction(release_action);
                //release_skill
            }

            //_last_time = 0; //TimeManager.RealtimeSinceStartup;



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

        public static void OnUpdate(float dt)
        {
            if (_last_time < 0) return;
            _last_time += dt;
            if (_last_time > 3)
            {
                _last_time = 0;
                if (events.Count > 0)
                {
                    E_SkillTransition name = events[0];
                    container.ReceiveTransitionEvent(name);
                    events.RemoveAt(0);
                }
                else
                {
                    _last_time = -1;
                }
            }
        }
    }

}

