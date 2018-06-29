
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
    }

    [System.Serializable]
    public class TextAttribute
    {
        public string Key;
        public string Value;
    }


    /*public class TextMd
    {
        /*public static TextNode Create(EdNode node)
        {
           /* TextNode textnode = new TextNode();
            textnode.Parse(node);
            return textnode;#2#
        }#1#
    }*/
}