using UnityEngine;

namespace Summer
{
    /// <summary>
    /// 原则上是让数据和动作文件进行分离
    /// </summary>
    [RequireComponent(typeof(Animator), typeof(BaseEntityController))]
    public class EntityAnimationGroup : MonoBehaviour
    {
        [HideInInspector]
        public BaseEntityController entity_controller;
        [HideInInspector]
        public Animator animator;
        [HideInInspector]
        public string curr_anim_name;

        public AnimationClip[] clips;

        public enum ClipType
        {
            take_001 = 0,
            attack_01,
            attack_02,
            attack_03,
            attack_04,
            attack_05,
            attack_06,
            run,
            die,
            hit,
            skill_01,
            skill_02,
            skill_03,
            skill_04,
            skill_05,
            skill_06,
            walk,

        };



        private void Awake()
        {
            _init();
        }

        #region public

        public string GetClipName(ClipType clip_type)
        {
            string anim_name = string.Empty;
            int index = (int)clip_type;
            if (index < clips.Length && index >= 0 && clips[index] != null)
                anim_name = clips[index].name;
            return anim_name;
        }

        public void PlayAnim(string anim_name)
        {
            curr_anim_name = anim_name;
            if (animator == null) return;
            animator.Play(anim_name);
        }

        public void PlayAnimation(string anim_name)
        {
            curr_anim_name = anim_name;
            if (animator == null) return;
            animator.CrossFade(anim_name, 0.2f);
        }

        public void StopAnim(string anim_name)
        {
            if (animator == null) return;
            animator.StopPlayback();
        }

        public AnimatorStateInfo GetAnimatorStateInfo()
        {
            AnimatorStateInfo state_info = animator.GetCurrentAnimatorStateInfo(0);
            return state_info;
        }

        public bool IsAttackNormal()
        {
            AnimatorStateInfo anim_info = GetAnimatorStateInfo();
            return false;
        }

        #region ReceiveEvent

        public void SkillHit()
        {
            if (entity_controller == null) return;
            entity_controller.SkillEvent(E_SkillTransition.anim_hit);
        }

        public void SkillFinish()
        {
            entity_controller.SkillEvent(E_SkillTransition.anim_finish);
        }

        public void SkillRelease()
        {
            entity_controller.SkillEvent(E_SkillTransition.anim_release);
        }

        #endregion

        #endregion

        #region private 

        public void _init()
        {
            entity_controller = gameObject.GetComponent<BaseEntityController>();
            animator = gameObject.GetComponent<Animator>();

            AnimatorOverrideController override_control = new AnimatorOverrideController();
            override_control.runtimeAnimatorController = animator.runtimeAnimatorController;
            int length = clips.Length;
            for (int i = 0; i < length; i++)
            {
                if (clips[i] != null)
                {
                    override_control[clips[i].name] = null;
                    override_control[clips[i].name] = clips[i];
                }
                else
                {

                }
            }
            animator.runtimeAnimatorController = override_control;
        }

        #endregion
    }

    public class AnimationNameConst
    {
        public const string IDLE = "";
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

        public static int attack_01 = Animator.StringToHash(ATTACK_01);
        public static int attack_02 = Animator.StringToHash(ATTACK_02);
        public static int attack_03 = Animator.StringToHash(ATTACK_03);
        public static int attack_04 = Animator.StringToHash(ATTACK_04);
        public static int attack_05 = Animator.StringToHash(ATTACK_05);
        public static int attack_06 = Animator.StringToHash(ATTACK_06);
    }

    public class SkillNormalInfo
    {
        public EntityAnimationGroup.ClipType clip_type;
        public float mormalized_time;
    }
}
