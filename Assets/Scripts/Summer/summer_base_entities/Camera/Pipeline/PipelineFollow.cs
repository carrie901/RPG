using System.Collections.Generic;
using UnityEngine;

namespace Summer
{
    [System.Serializable]
    public class PipelineFollow : I_CameraPipeline
    {

        //按照优先级来排序，如果第一个发生了变化，就在_list_camera_source_timer 插入一个timer
        //优先级从大到小排序
        public List<CameraSourceWrapper> _list_camera_source = new List<CameraSourceWrapper>();

        //根据timer来blend
        public List<CameraSourceTimer> _list_camera_source_timer = new List<CameraSourceTimer>();

        public CameraSourceWrapper _default_source;

        public CameraSourceLerp _default_lerp;

        public BaseEntity _player;
        public I_Transform _target;

        #region I_CameraPipeline

        public void OnEnable()
        {
            GameEventSystem ges = GameEventSystem.Instance;
            ges.RegisterHandler(E_GLOBAL_EVT.camera_set_player, _on_camera_set_player);
            ges.RegisterHandler(E_GLOBAL_EVT.camera_remove_player, _on_camera_remove_player);

            ges.RegisterHandler(E_GLOBAL_EVT.camera_source_set, _on_set_source);
            ges.RegisterHandler(E_GLOBAL_EVT.camera_source_add, _on_add_source);
            ges.RegisterHandler(E_GLOBAL_EVT.camera_source_rem, _on_rem_source);
        }

        public void OnDisable()
        {
            GameEventSystem ges = GameEventSystem.Instance;
            ges.UnRegisterHandler(E_GLOBAL_EVT.camera_set_player, _on_camera_set_player);
            ges.RegisterHandler(E_GLOBAL_EVT.camera_remove_player, _on_camera_remove_player);

            ges.UnRegisterHandler(E_GLOBAL_EVT.camera_source_set, _on_set_source);
            ges.UnRegisterHandler(E_GLOBAL_EVT.camera_source_add, _on_add_source);
            ges.UnRegisterHandler(E_GLOBAL_EVT.camera_source_rem, _on_rem_source);
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

            // 转换成camerasourcedata 剔除掉_player.WroldPosition
            CameraSourceData cur_source_data = Convert2CameraSource(data._now_data_witout_shake, _player.WroldPosition);

            // 混合 source源，根据时间 递归的时候会对_dest_data数据进行赋值
            //_blend_source(dt, ref cur_source_data, data._now_data._fov);

            if (_dest_data._offset.z >= -0.001f)
                _dest_data._offset.z = -0.001f;

            _dest_data._rotaion = _default_source._source._data._rotaion;
            cur_source_data = _default_source._source._data;
            //根据source源，计算最终的offset
            Vector3 dest_offset = cur_source_data._offset;

            Vector3 dir = dest_offset - cur_source_data._offset;
            float offset_speed = _default_lerp._offset_speed;
            if (dir.sqrMagnitude > offset_speed * offset_speed) //太远用lerp
            {
                dest_offset = Vector3.Lerp(cur_source_data._offset, dest_offset, dt * _default_lerp._offset_speed);
            }
            else
                dest_offset = Vector3.SmoothDamp(cur_source_data._offset, dest_offset, ref _move_speed_offset, dt, _default_lerp._offset_speed);

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

        #region 监听的消息的反馈

        private void _on_camera_set_player(System.Object obj)
        {
            _player = obj as BaseEntity;
        }

        private void _on_camera_remove_player(System.Object obj)
        {
            _player = null;
        }

        public void _on_set_source(System.Object obj)
        {
            CameraSource cs = obj as CameraSource;
            if (cs == null) return;

            AddSource(cs, true);
        }

        public void _on_add_source(System.Object obj)
        {
            CameraSource cs = obj as CameraSource;
            if (cs == null) return;

            AddSource(cs);
        }

        public void _on_rem_source(System.Object obj)
        {
            CameraSource cs = obj as CameraSource;
            if (cs == null) return;

            RemoveSource(cs);
        }

        #endregion

        #region public

        public void SetDefaultSource(CameraSource default_source)
        {
            if (_default_source != null)
            {
                RemoveSource(_default_source._source);
                _default_source = null;
            }
            _default_source = AddSource(default_source);
        }

        public void SetDefaultSourceLerp(CameraSourceLerp default_source_lerp)
        {
            _default_lerp = default_source_lerp;
        }

        public CameraSourceWrapper AddSource(CameraSource source, bool once = false)
        {
            if (source == null) return null;

            CameraSourceWrapper wrapper = CameraSourceWrapperFactory.Create(source);
            wrapper.SetDefaultSourceLerp(_default_lerp);

            int index_to_insert = 0;
            for (int i = 0; i < _list_camera_source.Count; i++)
            {
                //如果新插入的优先级大于等于i的优先级，就插入当前
                if (_list_camera_source[i]._source._priority <= source._priority)
                {
                    index_to_insert = i;
                    break;
                }
            }

            if (!once)
                _list_camera_source.Insert(index_to_insert, wrapper);

            //如果头发生了变化，在timer list里面插入一个timer到头部
            if (index_to_insert == 0)
            {
                CameraSourceTimer timer = new CameraSourceTimer
                {
                    _source = wrapper,
                    _timer = 0
                };
                _list_camera_source_timer.Insert(0, timer);
            }

            return wrapper;
        }

        public void RemoveSource(CameraSource source)
        {
            if (source == null) return;

            int index_to_remove = -1;

            for (int i = 0; i < _list_camera_source.Count; i++)
            {
                if (_list_camera_source[i]._source == source)
                {
                    index_to_remove = i;
                    break;
                }
            }

            //没找到
            if (index_to_remove < 0)
            {
                return;
            }

            _list_camera_source.RemoveAt(index_to_remove);


            //如果是第一个，说明头部发生了变化，就把新的头重新插入到timer里面
            //所以timer list 里面有可能会包含两个相同的 source
            if (index_to_remove == 0 && _list_camera_source.Count > 0)
            {
                CameraSourceTimer timer = new CameraSourceTimer();
                timer._source = _list_camera_source[0];
                timer._timer = 0;
                _list_camera_source_timer.Insert(0, timer);
            }
        }

        #endregion

        public void _blend_source(float dt, ref CameraSourceData now_data, float fov)
        {
            //剩下的权重
            int index_to_remove = -1;
            for (int i = 0; i < _list_camera_source_timer.Count; i++)
            {
                CameraSourceTimer timer = _list_camera_source_timer[i];
                timer._timer += dt; //每一个timer update
                //计算当前的timer的权重
                float cur_blend_pri = 0;

                if (timer._source._source._timer < 0.0001f) //防止策划填的数值为0，然后dt时间为0，比如暂停，导致出问题
                {
                    cur_blend_pri = 1;
                }
                else if (timer._timer > timer._source._source._timer)
                {
                    cur_blend_pri = 1;
                }
                else
                {
                    cur_blend_pri = timer._timer / timer._source._source._timer;

                    if (cur_blend_pri >= 1)
                        cur_blend_pri = 1;
                    else if (cur_blend_pri < 0)
                        cur_blend_pri = 0;
                }

                timer._blend_priority = cur_blend_pri;
                //timer._blend_priority = blend_priority_shengxia*cur_blend_pri;
                // blend_priority_shengxia = blend_priority_shengxia * (1 - cur_blend_pri);

                if (cur_blend_pri >= 1)
                {
                    //timer._blend_priority = blend_priority_shengxia;
                    //blend_priority_shengxia = 0;
                    index_to_remove = i + 1;
                    break;
                }
            }

            //移除不用的timer
            if (index_to_remove >= 0)
            {
                int count_to_remove = _list_camera_source_timer.Count - index_to_remove;
                if (count_to_remove > 0)
                    _list_camera_source_timer.RemoveRange(index_to_remove, count_to_remove);
            }

            //递归混合
            _dest_data = _blend_next(dt, 0, ref now_data, fov);
        }

        public CameraSourceData _blend_next(float dt, int index, ref CameraSourceData now_data, float fov)
        {
            //到了末尾
            if (_list_camera_source_timer.Count <= index)
            {
                return _default_source.GetData(dt, _player, _target, ref now_data, fov);
            }
            else
            {

                CameraSourceData to = _list_camera_source_timer[index]._source.GetData(dt, _player, _target, ref now_data, fov);

                float blend_priority = _list_camera_source_timer[index]._blend_priority;
                if (blend_priority >= 1.0f)
                {
                    return to;
                }

                CameraSourceData from = _blend_next(dt, index + 1, ref now_data, fov);
                return CameraSourceData.Lerp(from, to, _list_camera_source_timer[index]._blend_priority);
            }
        }

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

