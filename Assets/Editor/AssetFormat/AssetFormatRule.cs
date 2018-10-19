
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
using UnityEngine.Assertions.Must;

namespace SummerEditor
{
    public class AssetFormatRule
    {
        public I_AssetFilter _filter;
        public I_AssetRule _rule;

        public string FilterPath;
        public string FilterRule;
        public string FormatRule;

        public void ApplySettings<T>(T t, string assetPath) where T : UnityEngine.Object
        {
            TextureImporter tImporter = AssetImporter.GetAtPath(assetPath) as TextureImporter;
            if (tImporter == null) return;
            if (_rule != null) _rule.ApplySettings(tImporter, t);
        }

        public bool IsMatch<T>(T t, string assetPath) where T : UnityEngine.Object
        {
            if (!AssetImportHelper.IsMath(assetPath, FilterPath)) return false;
            if (_filter == null) return false;
            return _filter.IsMatch<T>(t);
        }

        public static AssetFormatRule CreateFormatRule(EdNode node)
        {
            try
            {
                AssetFormatRule rule = new AssetFormatRule();
                rule.FilterPath = node.GetAttribute("FilterPath").ToStr();
                EdAttribute filterPathattribute = node.GetAttribute("FilterRule");
                if (filterPathattribute != null)
                {
                    rule.FilterRule = filterPathattribute.ToStr();
                    Type filterType = Type.GetType("SummerEditor." + rule.FilterRule);
                    Debug.AssertFormat(filterType != null, "找不到对应的规则文件:[{0}]", rule.FilterRule);
                    rule._filter = Activator.CreateInstance(filterType) as I_AssetFilter;
                }
                rule.FormatRule = node.GetAttribute("FormatRule").ToStr();
                Type ruleType = Type.GetType("SummerEditor." + rule.FormatRule);
                Debug.AssertFormat(ruleType != null, "找不到对应的格式化文件:[{0}]", rule.FormatRule);
                rule._rule = Activator.CreateInstance(ruleType) as I_AssetRule;
                Debug.AssertFormat(rule._rule != null, "格式规则不存在:[{0}]", rule.FormatRule);
                return rule;
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
                return null;
            }
        }


    }
}