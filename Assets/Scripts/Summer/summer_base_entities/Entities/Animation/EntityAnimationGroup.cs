using UnityEngine;
using System.Collections.Generic;
namespace Summer
{

    /// <summary>
    /// 原则上是让数据和动作文件进行分离
    /// </summary>
    //[RequireComponent(typeof(Animator), typeof(BaseEntityController))]
    public class EntityAnimationGroup : MonoBehaviour, I_EntityAnimationGroup
    {
        #region 属性

        public I_EntityInTrigger _baseEntity;
        [HideInInspector]
        public Animator _animator;
        //[HideInInspector]
        public string _currAnimName;

        public List<AnimationClip> _animClips;

        public enum ClipType
        {
            TAKE_001 = 0,
            ATTACK_01,
            ATTACK_02,
            ATTACK_03,
            ATTACK_04,
            ATTACK_05,
            ATTACK_06,
            RUN,
            DIE,
            HIT,
            SKILL_01,
            SKILL_02,
            SKILL_03,
            SKILL_04,
            SKILL_05,
            SKILL_06,
            WALK,
        };

        #endregion

        void Awake()
        {
            _init();
        }

        public void OnInit(I_EntityInTrigger baseEntity)
        {
            _baseEntity = baseEntity;
        }

        #region 注册

        public void OnRegisterHandler()
        {
            _baseEntity.RegisterHandler(E_EntityInTrigger.play_animation, OnPlayAnimation);
            _baseEntity.RegisterHandler(E_EntityInTrigger.change_animation_speed, OnChangeAnimationSpeed);
        }

        public void UnRegisterHandler()
        {
            _baseEntity.UnRegisterHandler(E_EntityInTrigger.play_animation, OnPlayAnimation);
            _baseEntity.UnRegisterHandler(E_EntityInTrigger.change_animation_speed, OnChangeAnimationSpeed);
        }

        #endregion

        #region 响应

        // Entity播放动画
        public void OnPlayAnimation(EventSetData param)
        {
            PlayAnimationEventData data = param as PlayAnimationEventData;
            if (data == null) return;
            PlayAnimation(data.animation_name);
        }

        // 改变动画的速率
        public void OnChangeAnimationSpeed(EventSetData param)
        {
            AnimationSpeedEventData data = param as AnimationSpeedEventData;
            if (data == null) return;
            ChangeAnimationSpeed(data.animation_speed);
        }


        #endregion

        #region ReceiveEvent

        public void SkillStart()
        {
            //AnimatorStateInfo anim_info = animator.GetCurrentAnimatorStateInfo(0);
            SkillLog.Log("=================Animation触发事件:[{0}]", E_SkillTransition.ANIM_START);
            SkillEvent(E_SkillTransition.START);
        }

        public void SkillEvent01()
        {
            SkillLog.Log("=================Animation触发事件:[{0}]", E_SkillTransition.ANIM_EVENT01);
            SkillEvent(E_SkillTransition.ANIM_EVENT01);
        }

        public void SkillEvent02()
        {
            SkillLog.Log("=================Animation触发事件:[{0}]", E_SkillTransition.ANIM_EVENT02);
            SkillEvent(E_SkillTransition.ANIM_EVENT02);
        }

        public void SkillHit()
        {
            SkillLog.Log("=================Animation触发事件:[{0}]", E_SkillTransition.ANIM_HIT);
            SkillEvent(E_SkillTransition.ANIM_HIT);
        }

        public void SkillFinish()
        {
            SkillLog.Log("=================Animation触发事件:[{0}]", E_SkillTransition.ANIM_FINISH);
            SkillEvent(E_SkillTransition.ANIM_FINISH);
        }

        public void SkillRelease()
        {
            SkillEvent(E_SkillTransition.ANIM_RELEASE);
        }

        #endregion

        #region public

        public void SkillEvent(E_SkillTransition skillEvent)
        {
            Debug.LogError("暂停这一块");
            /*AnimationEventData param = EventDataFactory.Pop<AnimationEventData>();
            param._eventData = skillEvent;
            _baseEntity.RaiseEvent(E_EntityEvent.ANIMATION_EVENT, param);*/
        }

        public void PlayAnimation(string animName)
        {
            AnimationLog.Log("*****************播放动画:" + animName);
            _currAnimName = animName;
            if (_animator == null) return;
            //animator.CrossFade(anim_name, 0.2f);
            _animator.Play(animName);
            //animator.Play(anim_name, 0, 0);
        }

        public void ChangeAnimationSpeed(float speed)
        {
            _animator.speed = speed;
            //AnimatorTransitionInfo info = animator.GetAnimatorTransitionInfo(0);
        }

        public void StopAnim(string animName)
        {
            if (_animator == null) return;
            _animator.StopPlayback();
        }

        public AnimatorStateInfo GetAnimatorStateInfo()
        {
            AnimatorStateInfo stateInfo = _animator.GetCurrentAnimatorStateInfo(0);
            return stateInfo;
        }

        public bool IsAttackNormal()
        {
            //AnimatorStateInfo anim_info = GetAnimatorStateInfo();
            return false;
        }

        #endregion

        #region Editor Public 

        public void Clear()
        {
            _animClips.Clear();
        }

        public void AddAnims(string animName, AnimationClip animClip)
        {
            /*AnimationNameInfo info = new AnimationNameInfo
            {
                anim_name = anim_name,
                anim_clip = anim_clip
            };
            anims.Add(info);*/
            _animClips.Add(animClip);
        }

        #endregion

        #region private 

        private void _init()
        {
            _animator = gameObject.GetComponent<Animator>();

            AnimatorOverrideController overrideControl = new AnimatorOverrideController();
            overrideControl.runtimeAnimatorController = _animator.runtimeAnimatorController;
            int length = _animClips.Count;
            for (int i = 0; i < length; i++)
            {
                if (_animClips[i] != null)
                {
                    overrideControl[_animClips[i].name] = null;
                    overrideControl[_animClips[i].name] = _animClips[i];
                }
                else
                {

                }
            }
            _animator.runtimeAnimatorController = overrideControl;
        }

        #endregion
    }

    public class AnimationNameConst
    {
        public const string IDLE = "idle";
        public const string ATTACK_01 = "attack_01";
        public const string ATTACK_02 = "attack_02";
        public const string ATTACK_03 = "attack_03";
        public const string ATTACK_04 = "attack_04";
        public const string ATTACK_05 = "attack_05";
        public const string ATTACK_06 = "attack_06";
        public const string RUN = "run";
        public const string DIE = "die";
        public const string HIT = "hit";
        public const string WALK = "walk";
        public const string SKILL_01 = "skill_01";
        public const string SKILL_02 = "skill_02";
        public const string SKILL_03 = "skill_03";
        public const string SKILL_04 = "skill_04";
        public const string SKILL_05 = "skill_05";
        public const string ACTIONCMD = "ActionCMD";

        /*public static int idle = Animator.StringToHash(IDLE);
        public static int attack_01 = Animator.StringToHash(ATTACK_01);
        public static int attack_02 = Animator.StringToHash(ATTACK_02);
        public static int attack_03 = Animator.StringToHash(ATTACK_03);
        public static int attack_04 = Animator.StringToHash(ATTACK_04);
        public static int attack_05 = Animator.StringToHash(ATTACK_05);
        public static int attack_06 = Animator.StringToHash(ATTACK_06);*/
    }

    /* public class SkillNormalInfo
     {
         public EntityAnimationGroup.ClipType clip_type;
         public float mormalized_time;
     }*/
}
