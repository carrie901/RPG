using System.Collections.Generic;
using UnityEngine;

namespace Summer
{
    [System.Serializable]
    public class PipelineFollow : I_CameraPipeline
    {
        #region 属性

        //根据timer来blend
        public List<CameraSourceTimer> _list_camera_source_timer
            = new List<CameraSourceTimer>();                                 // 下一个镜头源包装列表

        public CameraSourceTimer next_camera_source_timer_timer;             // 下一个镜头源的数据

        //public CameraData _default_camera_data;                                     // 默认的镜头数据

        public CameraSourceData _form_source;                                       // 当前的镜头数据
        public CameraSourceData _to_source;                                         // 下一个镜头数据

        public CameraSource _default_source;
        public CameraSourceSpeed _default_speed;

        public BaseEntity _player;
        public I_Transform _target;


        public float _offset_speed = 5;                                                 // 便宜速度

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


        public CameraSourceData _dest_source_data;
        public Vector3 _move_speed_offset = Vector3.zero;
        public void Process(CameraPipelineData data, float dt)
        {
            // 如果默认的source没有，或者 _player 没有就直接返回
            if (_player == null)
            {
                return;
            }

            // 1.当前的镜头数据
            //CameraSourceData cur_source_data = Convert2CameraSource(data._now_data_witout_shake, _player.WroldPosition);

            // 
            CameraData cur_source_data = data._now_data_witout_shake;

            // 混合 source源，根据时间 递归的时候会对_dest_data数据进行赋值
            _blend_source(dt);

            /*if (_dest_source_data._offset.z >= -0.001f)
                _dest_source_data._offset.z = -0.001f;*/

            // 做一个递归的操作
            Vector3 dest_offset = _dest_source_data._offset;
            Vector3 tmp = dest_offset;
            Vector3 dir = (dest_offset - cur_source_data._pos);
            /*if (dir.sqrMagnitude > _offset_speed * _offset_speed) //太远用lerp
            {
                dest_offset = Vector3.Lerp(cur_source_data._pos, dest_offset, /*dt * _offset_speed#1#_default_speed._offset_speed);
            }
            else
            {
                dest_offset = Vector3.SmoothDamp(cur_source_data._pos, dest_offset, ref _move_speed_offset, _default_speed._offset_speed/*, #1#);
            }*/
            dest_offset = Vector3.SmoothDamp(cur_source_data._pos, dest_offset, ref _move_speed_offset, _default_speed._offset_speed/*, */);


            // 数据复制
            data._dest_data_without_shake._rot = Quaternion.Euler(_dest_source_data._rotaion);
            data._dest_data_without_shake._pos = dest_offset;
            data._dest_data = data._dest_data_without_shake;
        }

        #endregion

        #region public

        // 默认源之间的切换Bug
        public void SetDefaultSource(CameraSource default_source)
        {
            _default_source = default_source;
        }

        public void SetDefaultSourceSpeed(CameraSourceSpeed default_speed)
        {
            _default_speed = default_speed;

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

        // TODO bug不想管了，这里有一个Bug 就是拉回到原始的时候如果有新的节点会有bug
        public void _on_add_source(System.Object obj)
        {
            CameraSource source = obj as CameraSource;
            if (source == null) return;

            CameraSourceTimer timer = CameraSourceWrapperFactory.Create(source);

            _list_camera_source_timer.Add(timer);
            if (next_camera_source_timer_timer == null)
                _init_next_camera_source(timer);

        }

        #endregion

        #endregion

        #region private

        public void _blend_source(float dt)
        {
            // 更新下一个镜头源的数据
            if (next_camera_source_timer_timer != null)
                next_camera_source_timer_timer.OnUpdate(dt);

            _dest_source_data = _blend_next(dt);

            if (next_camera_source_timer_timer != null)
            {
                bool reulst = next_camera_source_timer_timer.IsEnd();
                if (!reulst) return;

                _list_camera_source_timer.Remove(next_camera_source_timer_timer);

                int last_index = _list_camera_source_timer.Count - 1;
                if (last_index >= 0)
                {
                    _init_next_camera_source(_list_camera_source_timer[last_index]);
                }
                else
                {
                    next_camera_source_timer_timer = null;
                }
            }
        }

        // 混合form和to之间权重
        public CameraSourceData _blend_next(float dt)
        {
            if (next_camera_source_timer_timer != null)
            {
                CameraSourceData data = next_camera_source_timer_timer.GetData(_form_source, dt);
                return data;
            }
            else
            {
                CameraSourceData data = CameraData2Player(_default_source, _player.WroldPosition);
                return data;
            }

        }

        // 初始化回归镜头源
        public void _init_back_camera_source()
        {
            //next_camera_source_timer_timer.OnReset(_default_source);
            //_init_camera_source_data();
        }

        /// <summary>
        /// 初始化下一个镜头源
        /// </summary>
        public void _init_next_camera_source(CameraSourceTimer timer)
        {
            next_camera_source_timer_timer = timer;
            _init_camera_source_data();
        }

        // 初始化
        public void _init_camera_source_data()
        {
            _form_source = _to_source;
            _to_source = next_camera_source_timer_timer._target._data;
            next_camera_source_timer_timer.OnReset();
        }

        /// <summary>
        /// 通过当前镜头的数据，剔除掉人物距离，得到实际的数据
        /// </summary>
        public CameraSourceData Convert2CameraSource(CameraData data, Vector3 target_pos)
        {
            CameraSourceData ret;
            ret._offset = data._pos - target_pos;
            ret._rotaion = data._rot.eulerAngles;

            return ret;
        }

        public CameraSourceData Source2Data(CameraData data)
        {
            CameraSourceData ret;
            ret._offset = data._pos;
            ret._rotaion = data._rot.eulerAngles;

            return ret;
        }

        public CameraSourceData CameraData2Player(CameraSource data, Vector3 target_pos)
        {
            CameraSourceData ret;
            ret._offset = data._data._offset + target_pos;
            ret._rotaion = data._data._rotaion;

            return ret;
        }

        #endregion
    }
}

