
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
using UnityEditor;
using UnityEngine;

namespace SummerEditor
{
    public class AssetFormatRule : I_AssetRule
    {
        public string key;
        public I_AssetFilter filte;
        public I_AssetRule rule;

        public void ApplySettings<T>(AssetImporter assetImport, T obj) where T : UnityEngine.Object
        {
            if (rule != null)
                rule.ApplySettings(assetImport, obj);
        }

        public void SetFilter(EdNode node)
        {
            key = node.Name;

            EdNode filterNode = node.Nodes[0];
            Type type = Type.GetType("SummerEditor." + filterNode.Name);

            Debug.AssertFormat(type != null, "资源格式化找不到对应过滤条件:[0]", filterNode.Name);
            if (type == null) return;

            filte = Activator.CreateInstance(type) as I_AssetFilter;
            if (filte == null) return;

            filte.SetFilter(filterNode);
            EdNode ruleNode = node.Nodes[1];
            Type ruleType = Type.GetType("SummerEditor." + ruleNode.Name);

            Debug.AssertFormat(ruleType != null, "资源格式化找不到对应过格式化规则:[0]", ruleNode.Name);
            if (ruleType == null) return;
            rule = Activator.CreateInstance(ruleType) as I_AssetRule;
            Debug.Log("rule_type:" + ruleType.ToString());
        }

        public bool IsMatch<T>(AssetImporter assetImport, T t) where T : UnityEngine.Object
        {
            if (filte == null) return false;
            return filte.IsMatch<T>(assetImport, t);
        }
    }
}