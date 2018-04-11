using System;
using UnityEngine;
using System.Collections.Generic;

namespace Summer
{
    /// <summary>
    /// 为了让SoundManager类更加纯粹化剥离这一部分数据独立成为一个类
    /// </summary>
    public class ButtonSoundManager
    {
        public static ButtonSoundManager Instance = new ButtonSoundManager();
        // TODO 这一部分的数据不应该放在这里，让这个类的相关数据不在纯粹
        private Dictionary<E_ViewId, Dictionary<string, int>> _ui_sound;                // UI 声音的数据

        #region public 
        public void Init()
        {
            _init_ui_sound();
        }

        public int FindSoundIdByKey(E_ViewId id, string key)
        {
            if (!_ui_sound.ContainsKey(id))
            {
                return -1;
            }
            if (!_ui_sound[id].ContainsKey(key))
            {
                return -1;
            }
            return _ui_sound[id][key];
        }

        #endregion

        #region private

        // 初始化界面UI的数据，对界面UI的声音进行转换
        public void _init_ui_sound()
        {
            // TODO QAQ 4.11 缺少数据StaticData类中的UiSoundObj类
            /*_ui_sound = new Dictionary<E_ViewId, Dictionary<string, int>>();
            Dictionary<int, UiSoundObj> ui_sound_map = StaticData.GetDic<UiSoundObj>();

            Type v = typeof(E_ViewId);
            foreach (var map_sound in ui_sound_map)
            {
                UiSoundObj obj = map_sound.Value;
                E_ViewId view_id = (E_ViewId)Enum.Parse(v, obj.view_id);
                if (!_ui_sound.ContainsKey(view_id))
                    _ui_sound[view_id] = new Dictionary<string, int>();

                if (_ui_sound[view_id].ContainsKey(obj.view_key))
                    LogManager.Error("UiSound配置文件出错UI:[{0}] Key:[{1}]", view_id, obj.view_key);
                else
                    _ui_sound[view_id].Add(obj.view_key, obj.sound_id);
            }*/
        }

        #endregion
    }
}

