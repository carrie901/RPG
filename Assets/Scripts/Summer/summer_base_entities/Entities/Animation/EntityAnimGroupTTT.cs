
//
//                            _ooOoo_
//                           o8888888o
//                           88" . "88
//                           (| -_- |)
//                           O\  =  /O
//                        ____/`---'\____
//                      .'  \\|     |//  `.
//                     /  \\|||  :  |||//  \
//                    /  _||||| -:- |||||-  \
//                    |   | \\\  -  /// |   |
//                    | \_|  ''\---/''  |   |
//                    \  .-\__  `-`  ___/-. /
//                  ___`. .'  /--.--\  `. . __
//               ."" '<  `.___\_<|>_/___.'  >'"".
//              | | :  `- \`.;`\ _ /`;.`/ - ` : | |
//              \  \ `-.   \_ __\ /__ _/   .-` /  /
//         ======`-.____`-.___\_____/___.-`____.-'======
//                            `=---='
//        ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
//                 			 佛祖 保佑             

using System.Collections.Generic;
using UnityEngine;

namespace Summer
{
    [RequireComponent(typeof(Animation), typeof(BaseEntityController))]
    public class EntityAnimGroupTTT : MonoBehaviour, I_EntityAnimationGroup
    {

        #region 属性

        public I_EntityInTrigger _baseEntity;
        [HideInInspector]
        public Animation _anim;
        public string _currAnimName;
        public enum ClipType
        {
            NONE,
            MAX,
        };

        #endregion

        #region MONO Override

        void Awake()
        {
            _init();
        }

        public AnimationName.ClipType _lastClip;
        public AnimationName.ClipType _currentClip;
        // Update is called once per frame
        void Update()
        {
            if (_anim == null) return;

            if (_lastClip != _currentClip)
            {
                PlayAnimation(AnimNameFactory.GetAnimName(_currentClip));
                _lastClip = _currentClip;
            }

            if (!_anim.isPlaying)
            {
                PlayAnimation(AnimNameFactory.GetAnimName(AnimationName.ClipType.IDLE));
                _currentClip = AnimationName.ClipType.IDLE;
                _lastClip = _currentClip;
            }

        }

        #endregion

        #region Public



        #endregion

        #region Override I_EntityAnimationGroup

        public void OnInit(I_EntityInTrigger baseEntity)
        {
            _baseEntity = baseEntity;
        }

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

        public void SkillEvent(E_SkillTransition skillEvent)
        {
            Debug.LogError("暂停这一块");
        }

        public void PlayAnimation(string animName)
        {
            AnimationLog.Log("*****************播放动画:" + animName);
            _currAnimName = animName;
            if (_anim == null) return;
            //animator.CrossFade(anim_name, 0.2f);
            _anim.Play(animName);
            //animator.Play(anim_name, 0, 0);
        }

        public void StopAnim(string animName)
        {
            if (_anim == null) return;
            _anim.Stop();
        }

        public void ChangeAnimationSpeed(float speed)
        {
            AnimationLog.Log("*****************[{0}]更改动画速度:[{1}]", _currAnimName, speed);
        }

        public void Clear()
        {

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

        #region Private Methods

        private void _init()
        {
            _anim = GetComponent<Animation>();
            foreach (AnimationState state in _anim)
            {
                //state.speed = 0.5F;
            }
            _anim["idle"].wrapMode = WrapMode.Loop;
        }

        #endregion

    }

    public class AnimationName
    {
        public enum ClipType
        {
            NONE,
            TAKE_001,
            AMAZED,
            ATTACK,
            SATTACK,
            MIN_SKILL,
            AVGLANCH,
            BACKWARD,
            IDLE,
            MAX,
        };
        public Dictionary<ClipType, string> _clipName
            = new Dictionary<ClipType, string>(AnimClipNameComparer.Instance)
            {
                { ClipType.TAKE_001,"Take_001" },
                { ClipType.AMAZED,"amazed" },
                { ClipType.ATTACK,"attack" },
                { ClipType.AVGLANCH,"avglanch" },
                { ClipType.BACKWARD,"backward" },
                { ClipType.IDLE,"idle" },
                { ClipType.MIN_SKILL,"minskill" },
                { ClipType.SATTACK,"sattack" },
            };
    }

    public class AnimClipNameComparer : IEqualityComparer<AnimationName.ClipType>
    {
        public static AnimClipNameComparer Instance = new AnimClipNameComparer();
        private AnimClipNameComparer() { }
        public bool Equals(AnimationName.ClipType x, AnimationName.ClipType y)
        {
            return x == y;
        }

        public int GetHashCode(AnimationName.ClipType obj)
        {
            return (int)obj;
        }
    }

    public class AnimNameFactory
    {
        public static AnimationName _animName = new AnimationName();
        public static string GetAnimName(AnimationName.ClipType clipType)
        {
            return _animName._clipName[clipType];
        }
    }
}

