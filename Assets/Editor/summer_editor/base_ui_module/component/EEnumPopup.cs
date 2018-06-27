using System;
using UnityEngine;
using System.Collections;

namespace SummerEditor
{

    public class EEnumPopup : ERectItem
    {
        public System.Enum _data;
        public Action<System.Enum> change_action;
        public EEnumPopup(float width, float height=DEFAULT_HEIGHT) : base(width, height)
        {
        }

        public override void Draw()
        {
            if (_data != null)
            {
                Enum tmp_data = _data;
                _data = EView.EnumPopup(_world_pos, _data);

                if (_data.CompareTo(tmp_data) != 0 && change_action != null)
                {
                    change_action(_data);
                }
            }

        }

        public void SetData(System.Enum data)
        {
            _data = data;
        }

        public string GetData()
        {
            if (_data == null) return string.Empty;
            return _data.ToString();
        }
    }
}
