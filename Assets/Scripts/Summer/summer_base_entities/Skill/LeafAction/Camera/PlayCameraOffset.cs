using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Summer
{
    public class PlayCameraOffset : SkillLeafNode
    {
        public const string DES = "摄像头偏移";

        public Vector3 _offset;
        public Vector3 _rotaion;
        public float time = 0.01f;
        public override void OnEnter()
        {
            LogEnter();
            CameraSource camera_source = new CameraSource();
            camera_source._data._offset = _offset;
            camera_source._data._rotaion = _rotaion;
            camera_source._timer = time;

            GameEventSystem.Instance.RaiseEvent(E_GLOBAL_EVT.camera_source_add, camera_source);
            Finish();
        }

        public override void OnExit()
        {
            LogExit();
        }

        public override void OnUpdate(float dt)
        {
            base.OnUpdate(dt);
        }

        public override string ToDes() { return DES; }
    }

}
