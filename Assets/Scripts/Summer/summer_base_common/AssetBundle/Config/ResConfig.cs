using System.Collections.Generic;
using UnityEngine;

//=============================================================================
// Author : mashao
// CreateTime : 2018-1-17 10:42:39
// FileName : ResConfig.cs
//=============================================================================

namespace Summer
{
    public class ResConfig
    {
        public static ResConfig Instance = new ResConfig();
        public const string ROOT = "Root";
        // Buff相关参数
        public const string BUFF_TEMPLATE = "BuffTemplate";

        public EdNode root_node;
        public ChangeModelConfig change_model_config;

        private ResConfig()
        {
            string text = ResManager.instance.LoadText(ResRequestFactory.CreateRequest<TextAsset>("Config/BuffEffectConfig"));

            ResMd md = new ResMd();
            md.ParseText(text);
            root_node = md._root_node.GetNode(ROOT);

            change_model_config = new ChangeModelConfig();
            change_model_config.Parse(root_node.GetNode(BUFF_TEMPLATE));
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
                map.Add(value.key, value);
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

        public int key;
        public float fresnel;
        public Vector3 color;

        public void Parse(EdNode node)
        {
            key = node.GetAttribute(KEY).Text.ToInt();
            fresnel = node.GetAttribute(FRESNEL).Text.ToFloat();
            color = node.GetAttribute(COLOR).Text.ToV3();
        }
    }
}
