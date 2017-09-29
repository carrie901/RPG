using System.Collections.Generic;
using UnityEngine;

namespace Summer
{
    public class SkillContainer : MonoBehaviour
    {
        public Dictionary<long, Timer> _buff_expire_timer;              //用caster_iid + buff_id做key
        public Dictionary<int, SequenceState> _skill_map
            = new Dictionary<int, SequenceState>(8);              //SkillState cur_state;

        public SequenceState _curr_sequen;

        public float _last_time;
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
                test = false;
                CastSkill(1);
            }

            float dt = TimeManager.DeltaTime - _last_time;
            _last_time = TimeManager.DeltaTime;
            if (_curr_sequen == null) return;
            _curr_sequen.OnUpdate(dt);

            SkillContainerTest.OnUpdate(dt);
        }

        #endregion

        public void Add(int id)
        {

        }

        public void Remove(int id) { }

        public void CastSkill(int id)
        {
            _curr_sequen = _skill_map[id];
            _curr_sequen.OnStart();
            _last_time = TimeManager.DeltaTime;
        }

        public void ReceiveTransitionEvent(E_SkillTransitionEvent event_name)
        {
            LogManager.Log("接收到[{0}]", event_name);
            if (_curr_sequen != null)
                _curr_sequen.ReceiveTransitionEvent(event_name);
        }

    }


    public class SkillContainerTest
    {
        public static SkillContainer container;
        public static float _last_time = 0;
        public static List<E_SkillTransitionEvent> events = new List<E_SkillTransitionEvent>();
        public static SequenceState Create()
        {
            SequenceState sequence = new SequenceState();

            // 1.播放特效和动作，并且接受声音事件
            SkillState anim_effect = new SkillState("1.播放特效和动作，并且接受声音事件");
            sequence.AddState(anim_effect);
            anim_effect.AddTransitionEvent(E_SkillTransitionEvent.sound);
            events.Add(E_SkillTransitionEvent.sound);


            // 2.播放声音，接受打击事件
            SkillState sound = new SkillState("2.播放声音，接受打击事件");
            sequence.AddState(sound);
            sound.AddTransitionEvent(E_SkillTransitionEvent.anim_hit);
            events.Add(E_SkillTransitionEvent.anim_hit);

            // 3.查找目标，并且输出技能到目标身上，接受动画播放完
            SkillState trigger_colllion = new SkillState("查找目标，并且输出技能到目标身上，接受动画播放完");
            sequence.AddState(trigger_colllion);
            trigger_colllion.AddTransitionEvent(E_SkillTransitionEvent.anim_finish);
            events.Add(E_SkillTransitionEvent.anim_finish);

            // 4.释放当前技能结束
            SkillState release_skill = new SkillState("释放当前技能结束");
            sequence.AddState(release_skill);


            {
                PlayAnimationAction pa = new PlayAnimationAction();
                pa.animation_name = "普攻";
                anim_effect.AddAction(pa);

                PlayEffectAction pe = new PlayEffectAction();
                pe.effect_name = "炫光";
                anim_effect.AddAction(pe);
            }


            {
                PlaySoundAction ps = new PlaySoundAction();
                ps.sound_name = "普攻的声音";
                sound.AddAction(ps);
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

            _last_time = 0; //TimeManager.RealtimeSinceStartup;



            //1.技能触发 接受技能触发事件SkillCast
            //2.朝向目标 播放之后自动转到下一个节点
            //  2.1 播放动作 动作名字
            //  2.2 播放特效 播放动作，指定绑定节点
            //3.播放声音 接受声音事件
            //4.攻击类型设置/超找目标
            //5.等待时间
            //




            return sequence;
        }

        public static void OnUpdate(float dt)
        {
            _last_time += dt;
            if (_last_time > 3)
            {
                _last_time = 0;
                if (events.Count > 0)
                {
                    E_SkillTransitionEvent event_name = events[0];
                    container.ReceiveTransitionEvent(event_name);
                    events.RemoveAt(0);
                }
            }
        }
    }

}

