using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Summer
{
    /// <summary>
    /// 处理镜头的一些特效
    /// </summary>
    public class CameraEffectManager
    {
        public static CameraEffectManager instance = new CameraEffectManager();

        public CameraEffectManager()
        {

        }

        public void RegisterHandler()
        {
            GameEventSystem.Instance.RegisterHandler(E_GLOBAL_EVT.camera_effect_motion_blur, _play_motion_blur);
            GameEventSystem.Instance.RegisterHandler(E_GLOBAL_EVT.camera_effect_radial_blur, _play_radial_blur);
            //TODO 镜头的抖动应该不在这个范围之内，后期剥离
            GameEventSystem.Instance.RegisterHandler(E_GLOBAL_EVT.camera_shake, _play_shake);
        }

        public void UnRegisterHandler()
        {
            GameEventSystem.Instance.UnRegisterHandler(E_GLOBAL_EVT.camera_effect_motion_blur, _play_motion_blur);
            GameEventSystem.Instance.UnRegisterHandler(E_GLOBAL_EVT.camera_effect_radial_blur, _play_radial_blur);
            GameEventSystem.Instance.UnRegisterHandler(E_GLOBAL_EVT.camera_shake, _play_shake);
        }

        public void _play_motion_blur(System.Object obj)
        {
            PlayCameraMotionBlurEffectEventSkill motion = obj as PlayCameraMotionBlurEffectEventSkill;
            if (motion == null) return;

            LogManager.Log("播放镜头运动模糊");
        }

        public void _play_radial_blur(System.Object obj)
        {
            PlayCameraRadialBlurEffectEventSkill radial = obj as PlayCameraRadialBlurEffectEventSkill;
            if (radial == null) return;
            LogManager.Log("播放镜头径向模糊");
        }

        public void _play_shake(System.Object obj)
        {
            PlayCameraShakeEventSkill shake = obj as PlayCameraShakeEventSkill;
            if (shake == null) return;
            LogManager.Log("播放镜头抖动");
        }
    }
}

