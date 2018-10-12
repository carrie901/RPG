using System.Collections.Generic;
using UnityEngine;

//=============================================================================
// Author : mashao
// CreateTime : 2018-1-17 10:42:39
// FileName : BuffEffectConfig.cs
//=============================================================================

namespace Summer
{
    public class BuffEffectConfig
    {
        public static BuffEffectConfig Instance = new BuffEffectConfig();
        public const string ROOT = "Root";
        // Buff相关参数
        public const string BUFF_TEMPLATE = "BuffTemplate";

        public EdNode _rootNode;
        public ChangeModelConfig _changeModelConfig;

        private BuffEffectConfig()
        {
            string text = ResManager.instance.LoadText(ResRequestFactory.CreateRequest<TextAsset>("Config/BuffEffectConfig"));

            ResMd md = new ResMd();
            md.ParseText(text);
            _rootNode = md._root_node.GetNode(ROOT);

            _changeModelConfig = new ChangeModelConfig();
            _changeModelConfig.Parse(_rootNode.GetNode(BUFF_TEMPLATE));
        }
    }

    public class ChangeModelConfig
    {
        public const string CHANGE_MODEL_GROUP = "ChangeModelGroup";
        public const string CHANGE_MODEL = "ChangeModel";

        public Dictionary<int, ChangeModelValue> map = new Dictionary<int, ChangeModelValue>();

        public void Parse(EdNode node)
        {
            EdNode group = node.GetNode(CHANGE_MODEL_GROUP);
            List<EdNode> list = group.GetNodes(CHANGE_MODEL);

            int length = list.Count;
            for (int i = 0; i < length; i++)
            {
                ChangeModelValue value = new ChangeModelValue();
                value.Parse(list[i]);
                map.Add(value._key, value);
            }
        }

        public ChangeModelValue FindById(int key)
        {
            ChangeModelValue value;
            map.TryGetValue(key, out value);
            return value;
        }
    }

    public class ChangeModelValue
    {
        public const string KEY = "Key";
        public const string FRESNEL = "_fresnel";
        public const string COLOR = "_Color";

        public int _key;
        public float _fresnel;
        public Vector3 _color;

        public void Parse(EdNode node)
        {
            _key = node.GetAttribute(KEY).Text.ToInt();
            _fresnel = node.GetAttribute(FRESNEL).Text.ToFloat();
            _color = node.GetAttribute(COLOR).Text.ToV3();
        }
    }
}
