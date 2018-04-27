using System.Collections.Generic;
using UnityEngine;

namespace Summer
{
    [System.Serializable]
    public class PipelineFollow : I_CameraPipeline
    {
        #region 属性

        //根据timer来blend
        public List<CameraSourceTimer> _list_camera_source_timer = new List<CameraSourceTimer>();

        public CameraSource _to_source;
        public CameraSource _form_source;
        public CameraSourceTimer _next_camera_source_timer;

        public CameraSourceWrapper _default_source;
        public CameraSourceLerp _default_lerp;

        public BaseEntity _player;
        public I_Transform _target;

        #endregion

        #region I_CameraPipeline

        public void OnEnable()
        {
            GameEventSystem ges = GameEventSystem.Instance;
            ges.RegisterHandler(E_GLOBAL_EVT.camera_set_player, _on_camera_set_player);
            ges.RegisterHandler(E_GLOBAL_EVT.camera_remove_player, _on_camera_remove_player);

            ges.RegisterHandler(E_GLOBAL_EVT.camera_source_add, _on_add_source);
        }

        public void OnDisable()
        {
            GameEventSystem ges = GameEventSystem.Instance;
            ges.UnRegisterHandler(E_GLOBAL_EVT.camera_set_player, _on_camera_set_player);
            ges.RegisterHandler(E_GLOBAL_EVT.camera_remove_player, _on_camera_remove_player);

            ges.UnRegisterHandler(E_GLOBAL_EVT.camera_source_add, _on_add_source);

        }


        public CameraSourceData _dest_data;
        public Vector3 _move_speed_offset = Vector3.zero;
        public float smoothing = 5;
        public void Process(CameraPipelineData data, float dt)
        {
            // 如果默认的source没有，或者 _player 没有就直接返回
            if (_default_source == null || _player == null || _default_lerp == null)
            {
                return;
            }

            // 1.转换成camerasourcedata 剔除掉_player.WroldPosition
            CameraSourceData cur_source_data = Convert2CameraSource(data._now_data_witout_shake, _player.WroldPosition);

            // 混合 source源，根据时间 递归的时候会对_dest_data数据进行赋值
            _blend_source(dt);

            if (_dest_data._offset.z >= -0.001f)
                _dest_data._offset.z = -0.001f;

            Vector3 dest_offset = _dest_data._offset;

            // dest_offset=
            /*Vector3 dir = dest_offset - cur_source_data._offset;
            float offset_speed = _default_lerp._offset_speed;
            if (dir.sqrMagnitude > offset_speed * offset_speed) //太远用lerp
            {
                dest_offset = Vector3.Lerp(cur_source_data._offset, dest_offset, dt * _default_lerp._offset_speed);
            }
            else
                dest_offset = Vector3.SmoothDamp(cur_source_data._offset, dest_offset, ref _move_speed_offset, dt, _default_lerp._offset_speed);*/


            // 数据复制
            data._dest_data_without_shake._rot = Quaternion.Euler(_dest_data._rotaion);
            data._dest_data_without_shake._pos = dest_offset + _player.WroldPosition;
            data._dest_data = data._dest_data_without_shake;

            /*Quaternion dest_rot = Quaternion.Euler(_dest_data._rotaion);
            data._dest_data_without_shake._rot = dest_rot;

            data._dest_data_without_shake._pos = dest_rot * dest_offset + _player.WroldPosition;
            data._dest_data = data._dest_data_without_shake;*/
        }

        #endregion

        #region public
        public void SetDefaultSourceLerp(CameraSourceLerp default_source_lerp)
        {
            _default_lerp = default_source_lerp;
        }

        // 默认源之间的切换Bug
        public void SetDefaultSource(CameraSource default_source)
        {
            _default_source = CameraSourceWrapperFactory.Create(default_source);
            _default_source.SetDefaultSourceLerp(_default_lerp);
        }


        #region 监听的消息的反馈

        private void _on_camera_set_player(System.Object obj)
        {
            _player = obj as BaseEntity;
        }

        private void _on_camera_remove_player(System.Object obj)
        {
            _player = null;
        }

        // 不想管了，这里有一个Bug 就是拉回到原始的时候如果有新的节点会有bug
        public void _on_add_source(System.Object obj)
        {

            CameraSource source = obj as CameraSource;
            if (source == null) return;

            CameraSourceWrapper wrapper = CameraSourceWrapperFactory.Create(source);
            wrapper.SetDefaultSourceLerp(_default_lerp);

            CameraSourceTimer timer = new CameraSourceTimer
            {
                _source = wrapper,
                _timer = 0
            };
            _list_camera_source_timer.Add(timer);
            Debug.Log("新增加" + _list_camera_source_timer.Count);
            if (_next_camera_source_timer == null)
                _init_next_camera_source(timer);

        }

        #endregion

        #endregion

        public void _blend_source(float dt)
        {
            if (_next_camera_source_timer != null)
                _next_camera_source_timer.OnUpdate(dt);

            _dest_data = _blend_next(dt);

            if (_next_camera_source_timer != null)
            {
                bool reulst = _next_camera_source_timer.IsEnd();
                if (!reulst) return;

                if (_list_camera_source_timer.Count >= 2)
                {
                    _list_camera_source_timer.RemoveAt(0);
                    _init_next_camera_source(_list_camera_source_timer[0]);
                }
                else if (_list_camera_source_timer.Count == 1)
                {
                    _list_camera_source_timer.RemoveAt(0);
                    _init_back_camera_source();
                }
                else
                {
                    _next_camera_source_timer = null;
                }
            }
        }

        // 混合form和to之间权重
        public CameraSourceData _blend_next(float dt)
        {
            if (_to_source == null || _form_source == null || _next_camera_source_timer == null)
            {
                return _default_source.GetData(dt, _player, _target);
            }
            else
            {
                return CameraSourceData.Lerp(_form_source._data, _to_source._data,
                    _next_camera_source_timer._blend_priority);
            }
        }

        // 初始化回归镜头源
        public void _init_back_camera_source()
        {
            float time = TimeManager.EndSimpleTime();
            Debug.Log("消耗了多少时间：" + time);
            _next_camera_source_timer.OnReset(_default_source);
            _init_camera_source_data();
        }

        // 初始化下一个镜头源
        public void _init_next_camera_source(CameraSourceTimer timer)
        {
            TimeManager.BeginSampleTime();
            _next_camera_source_timer = timer;
            _init_camera_source_data();
        }

        public void _init_camera_source_data()
        {
            if (_to_source != null)
            {
                _form_source = _to_source;
            }
            else
            {
                _form_source = _default_source._source;
                Debug.Log("老目标");
            }

            _to_source = _next_camera_source_timer._source._source;
            _next_camera_source_timer.OnReset();
        }

        // 通过当前镜头的数据，剔除掉人物距离，得到实际的数据
        public CameraSourceData Convert2CameraSource(CameraData data, Vector3 target_pos)
        {
            //return Convert2CameraSource(data._pos, data._rot, target_pos);
            CameraSourceData ret;

            Vector3 world_offset = data._pos - target_pos;
            /*Quaternion rot_orig = data._rot;
            Quaternion rot_orig_inv = Quaternion.Inverse(rot_orig);
            ret._offset = rot_orig_inv * world_offset;*/

            ret._offset = world_offset;

            ret._rotaion = data._rot.eulerAngles;

            return ret;
        }
    }
}

