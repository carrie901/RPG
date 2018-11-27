using System.Collections.Generic;

namespace Summer
{
    public class PipelineShake : I_CameraPipeline
    {
        public List<CameraShake> _list_shake = new List<CameraShake>();

        public void OnEnable()
        {
            GameEventSystem.Instance.RegisterHandler(E_GLOBAL_EVT.camera_shake, _on_camera_shake);
        }

        public void OnDisable()
        {
            GameEventSystem.Instance.UnRegisterHandler(E_GLOBAL_EVT.camera_shake, _on_camera_shake);
        }

        public void _on_camera_shake(System.Object obj)
        {
            /*CameraShake shake = obj as CameraShake;
            if (shake == null) return;

            _list_shake.Add(shake);*/
        }

        public void Process(CameraPipelineData data, float dt)
        {
            int length = _list_shake.Count;
            for (int i = 0; i < length; i++)
            {
                _list_shake[i].Process(data, dt);
            }

            for (int i = _list_shake.Count - 1; i >= 0; i--)
            {
                if (_list_shake[i].IsEnd())
                {
                    _list_shake.RemoveAt(i);
                }
            }
        }
    }
}