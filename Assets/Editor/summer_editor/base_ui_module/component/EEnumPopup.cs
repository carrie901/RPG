using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace SummerEditor
{

    public class EEnumPopup : ERectItem
    {
        public System.Enum _data;
        public Action<System.Enum> change_action;
        public EEnumPopup(float width, float height = DEFAULT_HEIGHT) : base(width, height)
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

    public class EIntPopup : ERectItem
    {
        public EIntPopup(float width, float height) : base(width, height)
        {
        }

        public override void Draw()
        {

        }
    }

    public class EStringPopup : ERectItem
    {
        public List<StringPopupInfo> _data;
        public int select_index;
        public string[] _des;
        public string _lab;
        public EStringPopup(string lab, float width, float height = DEFAULT_HEIGHT) : base(width, height)
        {
            _lab = lab;
        }

        public void SetData(List<StringPopupInfo> data)
        {
            _data = data;
            _des = new string[data.Count];
            for (int i = 0; i < data.Count; i++)
            {
                _des[i] = data[i].des;
            }
        }

        public StringPopupInfo GetValue()
        {
            return _data[select_index];
        }

        public override void Draw()
        {
            if (_data != null)
            {
                select_index = EView.EnumPopup(_world_pos, _lab, select_index, _des);
            }
        }
    }

    public class StringPopupInfo
    {
        public string des;
    }

    public class StringStringPopupInfo : StringPopupInfo
    {
        public string value;
    }


}
