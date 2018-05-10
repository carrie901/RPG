using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Summer
{
    public class AnimationSet : MonoBehaviour
    {
       /* public AnimationGroup AnimGroup { get; private set; }

        public Animation m_anim = null;
        protected AnimationGroup.ClipType _last_clip;

        private string m_group_name;

        void Start()
        {
            StartCoroutine(LoadAnimation());

        }

        private IEnumerator LoadAnimation()
        {
            while (m_anim.GetClipCount() <= 0) yield return null;

            AnimGroup = transform.GetComponent<AnimationGroup>();

            if (AnimGroup == null)
            {
                yield break;
            }

            AnimationState anim = m_anim[AnimGroup.GetClipName(AnimationGroup.ClipType.clip_idle1)];
            if (anim)
            {
                anim.wrapMode = WrapMode.Loop; anim.layer = -1;
            }
            anim = m_anim[AnimGroup.GetClipName(AnimationGroup.ClipType.clip_death1)]; if (anim) { anim.wrapMode = WrapMode.ClampForever; anim.layer = 1; }
            anim = m_anim[AnimGroup.GetClipName(AnimationGroup.ClipType.clip_damage)]; if (anim) { anim.wrapMode = WrapMode.Default; anim.layer = -1; }
            anim = m_anim[AnimGroup.GetClipName(AnimationGroup.ClipType.clip_attack2)]; if (anim) { anim.wrapMode = WrapMode.Default; anim.layer = 1; }
            anim = m_anim[AnimGroup.GetClipName(AnimationGroup.ClipType.clip_attack1)]; if (anim) { anim.wrapMode = WrapMode.Default; anim.layer = 1; }
            anim = m_anim[AnimGroup.GetClipName(AnimationGroup.ClipType.clip_attack_summon)]; if (anim) { anim.wrapMode = WrapMode.Default; anim.layer = 1; }
            anim = m_anim[AnimGroup.GetClipName(AnimationGroup.ClipType.clip_dash1)]; if (anim) { anim.wrapMode = WrapMode.Loop; anim.layer = 1; }
            anim = m_anim[AnimGroup.GetClipName(AnimationGroup.ClipType.clip_move1)]; if (anim) { anim.wrapMode = WrapMode.Loop; anim.layer = -1; }

            m_anim.cullingType = AnimationCullingType.AlwaysAnimate;
            yield return null;
            Play(AnimationGroup.ClipType.clip_idle1);
        }

        public void Play(AnimationGroup.ClipType type)
        {
            if (AnimGroup != null && _last_clip != type)
            {
                m_anim.Play(AnimGroup.GetClipName(type));
                //m_anim.Play();
                _last_clip = type;
            }
        }

        public void CrossFade(AnimationGroup.ClipType type)
        {
            if (AnimGroup != null&& _last_clip!=type)
            {
                m_anim.CrossFade(AnimGroup.GetClipName(type));
                _last_clip = type;
            }
        }*/
    }
}

