
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

using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace SummerEditor
{
    public class AssetFormatMain 
    {
        public static Dictionary<string, List<AssetFormatRule>> rules = new Dictionary<string, List<AssetFormatRule>>();
        public static bool initFlag;
        public static void Init()
        {
            //if (initFlag) return;
            Debug.LogError("AssetFormatMain 初始化成功");
            initFlag = true;
            rules.Clear();
            //1. 导入并且初始化规则
            //2. 细分类型-->过滤文件-->设置规则
            TextAsset textAsset = AssetDatabase.LoadAssetAtPath<TextAsset>(AssetImportConst.RULE_ASSET_PATH);
            ResMd resMd = new ResMd();
            resMd.ParseText(textAsset.text);

            EdNode ruleNode = resMd._root_node.GetNode("Rule");
            List<EdNode> nodes = ruleNode.Nodes;
            int length = nodes.Count;
            for (int i = 0; i < length; i++)
            {
                EdNode node = nodes[i];
                AssetFormatRule rule = new AssetFormatRule();
                rule.SetFilter(node);
                if (!rules.ContainsKey(rule.key))
                    rules.Add(rule.key, new List<AssetFormatRule>());
                rules[rule.key].Add(rule);
            }
        }
    }
}
