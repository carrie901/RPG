using UnityEngine;

namespace Summer
{
    /// <summary>
    /// 原则上是让数据和动作文件进行分离
    /// </summary>
    [RequireComponent(typeof(Animator))]
    public class EntityAnimationGroup : MonoBehaviour
    {
        public string curr_anim_name;
        public AnimationClip[] clips;
        public Animator animator;
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

        public void StopAnim(string anim_name)
        {
            if (animator == null) return;
            animator.StopPlayback();
        }
    }
}
