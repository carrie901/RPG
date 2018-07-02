
//
//                            _ooOoo_
//                           o8888888o
//                           88" . "88
//                           (| -_- |)
//                           O\  =  /O
//                        ____/`---'\____
//                      .'  \\|     |//  `.
//                     /  \\|||  :  |||//  \
//                    /  _||||| -:- |||||-  \
//                    |   | \\\  -  /// |   |
//                    | \_|  ''\---/''  |   |
//                    \  .-\__  `-`  ___/-. /
//                  ___`. .'  /--.--\  `. . __
//               ."" '<  `.___\_<|>_/___.'  >'"".
//              | | :  `- \`.;`\ _ /`;.`/ - ` : | |
//              \  \ `-.   \_ __\ /__ _/   .-` /  /
//         ======`-.____`-.___\_____/___.-`____.-'======
//                            `=---='
//        ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
//                 			 佛祖 保佑             

using System;
using System.Collections.Generic;
using UnityEngine;

namespace Summer
{

    #region 属性配置参数

    /// <summary>
    /// 属性参数配置
    /// </summary>
    public class EffectAttributeParam : I_EffectParam
    {
        public E_EntityAttributeType entity_attribute_type;
        public List<EffValueData> values;

        public string value_text = string.Empty;            // 描述文本

        public void ParseParam(TextNode text_node)
        {
            string attribute_string = text_node.GetAttribute(EffectTemplateInfo.ATTRIBUTETYPE);
            entity_attribute_type = (E_EntityAttributeType)Enum.Parse(typeof(E_EntityAttributeType), attribute_string);

            List<TextNode> nodes = text_node.GetNodes(EffectTemplateInfo.GROUP);
            values = new List<EffValueData>(nodes.Count);

            int length = nodes.Count;
            for (int i = 0; i < length; i++)
            {
                EffValueData param = new EffValueData();
                param.ParseParam(nodes[i]);
                values.Add(param);
            }
        }

        public void Clear()
        {
            for (int i = 0; i < values.Count; i++)
                values[i].Clear();
            values.Clear();
            values = null;
        }

        public string GetValueText()
        {
            return value_text;
        }
    }

    #endregion


    #region 数值配置参数

    #endregion

    #region 单个数值

    public class EffValueData : I_EffectParam
    {
        public E_DataUpdateType data_type;                  // 固定值,百分比
        public int value;                                   // 数据

        public string value_text;
        public void ParseParam(TextNode text_node)
        {
            string data_update_string = text_node.GetAttribute(EffectTemplateInfo.DATAUPDATETYPE);
            string value_string = text_node.GetAttribute(EffectTemplateInfo.VALUE);

            data_type = (E_DataUpdateType)Enum.Parse(typeof(E_DataUpdateType), data_update_string);
            value = int.Parse(value_string);
        }

        public void Clear()
        {
            
        }

        public string GetValueText()
        {
            if (data_type == E_DataUpdateType.multiply_plus)
            {
                if (value > 0)
                    value_text = string.Format("+{0}%", Mathf.Abs(value));
                else if (value < 0)
                    value_text = string.Format("-{0}%", Mathf.Abs(value));
            }
            else if (data_type == E_DataUpdateType.plus)
            {
                if (value > 0)
                    value_text = string.Format("+{0}", Mathf.Abs(value));
                else if (value < 0)
                    value_text = string.Format("-{0}", Mathf.Abs(value));
            }

            return value_text;
        }
    }

    #endregion
}