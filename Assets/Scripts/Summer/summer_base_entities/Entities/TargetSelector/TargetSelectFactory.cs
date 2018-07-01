using System;
using System.Collections.Generic;

namespace Summer
{
    public class TargetSelectFactory
    {
        public static Dictionary<string, Type> _target_select_map = new Dictionary<string, Type>()
        {
            { "Count",typeof(TargetSelectByCount) },
            { "Area" ,typeof(TargetSelectByArea)},
            { "TargetType",typeof(TargetSelectByType)},
            { "TargetCareer",typeof(TargetSelectByCareer)},
            { "TargetSex",typeof(TargetSelectBySex)},
            { "TargetPai",typeof(TargetSelectByPai)}
        };


        public static I_TargetSelector Create(TextNode node)
        {
            string type_name = node.Name;
            Type type;
            _target_select_map.TryGetValue(type_name, out type);
            if (type == null)
            {
                LogManager.Error("找不到对应的类型", type_name);
                return null;
            }
            I_TargetSelector target_select = Activator.CreateInstance(type) as I_TargetSelector;
            if (target_select != null)
                target_select.Init(node);
            else
                LogManager.Log("找不到对应的效果[{0}]", type);

            return target_select;
        }
    }
}
