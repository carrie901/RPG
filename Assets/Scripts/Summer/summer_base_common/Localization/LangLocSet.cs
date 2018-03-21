using System;
using System.Collections.Generic;

namespace Summer
{
    public class LangLocSet 
    {
        public const string NULL_ = "null";
        public static LangLocSet Instance = new LangLocSet();

        protected Dictionary<string, Dictionary<string, string>> _loc_map
            = new Dictionary<string, Dictionary<string, string>>();


        private LangLocSet()
        {

        }

        public void Init()
        {
            _init_loc();
        }

        public string FindTextIdByKey(string pfb_name, string key)
        {
            if (!_loc_map.ContainsKey(pfb_name))
            {
                LogManager.Error("pfb_name  找不到对应的文本.UI:[{0}],Key[{1}]", pfb_name, key);
                return NULL_;
            }
            if (!_loc_map[pfb_name].ContainsKey(key))
            {
                LogManager.Error("KEY   找不到对应的文本.UI:[{0}],Key[{1}]", pfb_name, key);
                return NULL_;
            }
            return _loc_map[pfb_name][key];
        }

        public void _init_loc()
        {
            _loc_map = new Dictionary<string, Dictionary<string, string>>();
           /* Dictionary<int, LangLocObj> ui_loc = StaticData.GetDic<LangLocObj>();

            Type v = typeof(E_ViewId);
            foreach (var info in ui_loc)
            {
                LangLocObj obj = info.Value;
                //string view_id = (E_ViewId)Enum.Parse(v, obj.view_id);
                if (!_loc_map.ContainsKey(obj.view_id))
                    _loc_map[obj.view_id] = new Dictionary<string, string>();

                if (_loc_map[obj.view_id].ContainsKey(obj.text_key))
                    LogManager.Error("本地化配置文件出错UI:[{0}] Key:[{1}],描述:[{2}]", obj.view_id, obj.text_key, obj.des);
                else
                    _loc_map[obj.view_id].Add(obj.text_key, obj.loc_key);
            }*/
        }

    }
}

