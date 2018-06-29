using System;
using System.Collections.Generic;

namespace Summer
{
    public class TargetSelectFactory
    {
        public static Dictionary<string, Type> _target_select_map = new Dictionary<string, Type>()
        {
            { "数量过滤",typeof(TargetSelectByCount) },
            { "范围过滤" ,typeof(TargetSelectByArea)},
            { "目标类型过滤",typeof(TargetSelectByType)},
            { "目标职业过滤",typeof(TargetSelectByCareer)},
            { "目标性别过滤",typeof(TargetSelectBySex)},
            { "目标流派过滤",typeof(TargetSelectByPai)}
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
