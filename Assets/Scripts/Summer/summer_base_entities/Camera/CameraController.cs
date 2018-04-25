using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Summer
{
    [ExecuteInEditMode]
    public class CameraController : MonoBehaviour
    {
        #region 属性

        protected Transform _target;
        protected Camera _camera;
        protected List<I_CameraPipeline> _pipe_line_list = new List<I_CameraPipeline>();
        protected CameraPipelineData _pipe_line_data = new CameraPipelineData();

        public CameraSource _default_source;
        public E_CameraSourceType _last_default_type;
        public CameraSourceLerp _default_source_lerp;

        public bool _show_safe_zone = false;

        protected PipelineFollow _pipe_line_follow;
        protected Transform trans;
        #endregion

        void Awake()
        {
            trans = transform;
            _pipe_line_follow = new PipelineFollow();
            _pipe_line_follow.SetDefaultSource(_default_source);
            _pipe_line_follow.SetDefaultSourceLerp(_default_source_lerp);


            //这个地方的顺序代表着优先级，越靠前优先级越低
            _pipe_line_list.Add(_pipe_line_follow);
            //_pipe_line_list.Add(new PipelineQTE());
            //_pipe_line_list.Add(new PipelineOffset());
            //_pipe_line_list.Add(new PipelineArea());
            //shake是在现有基础上叠加的数值，所以优先级放到最后
            _pipe_line_list.Add(new PipelineShake());


            _camera = GetComponent<Camera>();


            _pipe_line_data._now_data._fov = _camera.fieldOfView;
            _pipe_line_data._now_data._pos = trans.position;
            _pipe_line_data._now_data._rot = trans.rotation;

            _pipe_line_data._now_data_witout_shake = _pipe_line_data._now_data;
            _pipe_line_data._dest_data = _pipe_line_data._now_data;

            ReigsterEvents();
        }

        void OnEnable()
        {
            ReigsterEvents();
            foreach (var a in _pipe_line_list)
            {
                a.OnEnable();
            }
        }

        void OnDisable()
        {
            UnReigsterEvents();
            foreach (var a in _pipe_line_list)
            {
                a.OnDisable();
            }

            _pipe_line_list.Clear();
        }

        //void Update()
        void LateUpdate()
        {
            if (_last_default_type != _default_source._type)
            {
                _pipe_line_follow.SetDefaultSource(_default_source);
                _last_default_type = _default_source._type;
            }

            //if (!Application.isPlaying) return;
            //float dt = Time.fixedDeltaTime;
            float dt = Time.deltaTime;
            foreach (var a in _pipe_line_list)
            {
                a.Process(_pipe_line_data, dt);
            }

            //如果计算出的数据出错，那么就不要赋值了
            if (float.IsNaN(_pipe_line_data._dest_data._rot.eulerAngles.y))
            {
                LogManager.Error(string.Format("The final roatation euler is {0}", _pipe_line_data._dest_data._rot.eulerAngles));
                return;
            }
            trans.rotation = _pipe_line_data._dest_data._rot;
            trans.position = _pipe_line_data._dest_data._pos;
            //_camera.fieldOfView = _pipe_line_data._dest_data._fov;

            _pipe_line_data.OnUpdate();


        }

        #region 注册

        private bool _register_flag = false;
        private void ReigsterEvents()
        {
            if (_register_flag) return;
            _register_flag = true;

            GameEventSystem ges = GameEventSystem.Instance;

        }

        private void UnReigsterEvents()
        {
            if (!_register_flag) return;
            _register_flag = false;
            GameEventSystem ges = GameEventSystem.Instance;

        }

        #endregion

    }
}
