
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


namespace Summer
{

    [System.Serializable]
    public class TextNode
    {
        public string Name;
        public List<TextAttribute> AttributeList;
        public List<TextNode> NodeList;

        public TextNode() { }

        public TextNode(string name) { Name = name; }
        public TextAttribute AddAttribute(string key, string text)
        {
            if (AttributeList == null)
                AttributeList = new List<TextAttribute>();
            TextAttribute attribute = new TextAttribute
            {
                Key = key,
                Value = text
            };
            AttributeList.Add(attribute);
            return attribute;
        }

        public TextNode AddNode(string name)
        {
            if (NodeList == null)
                NodeList = new List<TextNode>();
            TextNode node = new TextNode { Name = name };
            NodeList.Add(node);
            return node;
        }

        public TextNode AddNode(TextNode node)
        {
            if (NodeList == null)
                NodeList = new List<TextNode>();
            NodeList.Add(node);
            return node;
        }

        public string GetAttribute(string key)
        {
            if (AttributeList == null) return string.Empty;
            for (int i = 0; i < AttributeList.Count; i++)
            {
                if (AttributeList[i].Key == key)
                    return AttributeList[i].Value;
            }

            return string.Empty;
        }

        public TextNode GetNode(string key)
        {
            if (NodeList == null) return null;
            for (int i = 0; i < AttributeList.Count; i++)
            {
                if (NodeList[i].Name == key)
                    return NodeList[i];
            }
            return null;
        }
    }

    [System.Serializable]
    public class TextAttribute
    {
        public string Key;
        public string Value;
    }
}