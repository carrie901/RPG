using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace SummerEditor
{
    public class AssetBundleAnallyzeObject
    {
        #region 属性

        public static Dictionary<Type, E_AssetType> analyze_map = new Dictionary<Type, E_AssetType>()
        {
            {typeof (Mesh), E_AssetType.mesh},
            {typeof (Material), E_AssetType.material},
            {typeof (Texture2D), E_AssetType.texture},
            {typeof (Shader), E_AssetType.shader},
            {typeof (Sprite), E_AssetType.sprite},
            {typeof (AnimatorOverrideController), E_AssetType.animator_override_controller},
            {typeof (AnimationClip), E_AssetType.animation_clip},
            {typeof (AudioClip), E_AssetType.audio_clip},
            {typeof (Font), E_AssetType.font},
            {typeof (TextAsset), E_AssetType.text_asset},
            {typeof (UnityEditor.Animations.AnimatorController), E_AssetType.animations_animator_controller},
        };

        public static Dictionary<E_AssetType, Func<Object, SerializedObject, List<KeyValuePair<string, System.Object>>>>
            fun_map = new Dictionary<E_AssetType, Func<Object, SerializedObject, List<KeyValuePair<string, object>>>>()
            {
                {E_AssetType.mesh, AnalyzeMesh},
                {E_AssetType.material, AnalyzeMaterial},
                {E_AssetType.texture, AnalyzeTexture2D},
                {E_AssetType.shader, AnalyzeShader},
                {E_AssetType.sprite, AnalyzeSprite},
                {E_AssetType.animator_override_controller, AnalyzeAnimatorOverrideController},
                {E_AssetType.animation_clip, AnalyzeAnimationClip},
                {E_AssetType.audio_clip, AnalyzeAudioClip},
                {E_AssetType.font, AnalyzeFont},
                {E_AssetType.text_asset,AnalyzeTextAsset },
                {E_AssetType.animations_animator_controller, AnalyzeAnimationsAnimatorController},
            };

        public static List<string> builtin_res = new List<string>()
        {
            "Resources/unity_builtin_extra",
            "Library/unity default resources",
        };

        #endregion

        public static E_AssetType CheckObject(Object ob, EAssetBundleFileInfo assetbundle_file_info)
        {
            if (ob == null) return E_AssetType.none;
            Type object_type = ob.GetType();
            if (!analyze_map.ContainsKey(object_type))
            {
                if (ob as Component) return E_AssetType.none;
                if (ob as ScriptableObject) return E_AssetType.none;
                if (ob as MonoScript) return E_AssetType.none;
                if (ob as GameObject) return E_AssetType.none;
                if (ob as Avatar) return E_AssetType.none;
                Debug.LogError("CheckObject:" + ob.name + "_" + object_type);
                return E_AssetType.none;
            }

            // 内建支援
            string asset_path = AssetDatabase.GetAssetPath(ob);
            if (string.IsNullOrEmpty(asset_path))
            {
                return analyze_map[object_type];
            }
            if (ob as Mesh)// 先排除掉网格的内建资源
                return E_AssetType.none;
            else
                Debug.LogError("使用了内建的资源" + asset_path + "_" + object_type + "_" + assetbundle_file_info.ab_name);
            return analyze_map[object_type];
        }

        #region Mesh 网格 

        public static List<KeyValuePair<string, System.Object>> AnalyzeMesh(Object obj,
             SerializedObject serialized_object)
        {
            var propertys = new List<KeyValuePair<string, System.Object>>();
            return propertys;
        }

        #endregion

        #region Material 材质球

        public static List<KeyValuePair<string, System.Object>> AnalyzeMaterial(Object obj,
             SerializedObject serialized_object)
        {
            var propertys = new List<KeyValuePair<string, System.Object>>();
            return propertys;
        }

        #endregion

        #region Texture 纹理检测

        public const string WIDTH = "宽度";
        public const string HIGH = "高度";
        public const string FORMAT = "格式";
        public const string MIPMAP_COUNT = "MipMap功能";
        public const string READ_WRITE = "Read/Write";
        public const string MEMORY_SIZE = "内存大小";

        public static List<KeyValuePair<string, System.Object>> AnalyzeTexture2D(Object obj,
             SerializedObject serialized_object)
        {
            Texture2D tex = obj as Texture2D;
            var propertys = new List<KeyValuePair<string, System.Object>>
            {
                new KeyValuePair<string, System.Object>(WIDTH, tex.width),
                new KeyValuePair<string, System.Object>(HIGH, tex.height),
                new KeyValuePair<string, System.Object>(FORMAT, tex.format.ToString()),
                new KeyValuePair<string, System.Object>(MIPMAP_COUNT, tex.mipmapCount > 1 ? "True" : "False")
            };

            var property = serialized_object.FindProperty("m_IsReadable");
            propertys.Add(new KeyValuePair<string, object>(READ_WRITE, property.boolValue.ToString()));

            property = serialized_object.FindProperty("m_CompleteImageSize");
            propertys.Add(new KeyValuePair<string, object>(MEMORY_SIZE, property.intValue));

            return propertys;
        }

        #endregion

        #region Shader 着色器

        public static List<KeyValuePair<string, System.Object>> AnalyzeShader(Object obj,
             SerializedObject serialized_object)
        {
            var propertys = new List<KeyValuePair<string, System.Object>>();
            return propertys;
        }

        #endregion

        #region Sprite 图集

        public static List<KeyValuePair<string, System.Object>> AnalyzeSprite(Object obj,
             SerializedObject serialized_object)
        {
            var propertys = new List<KeyValuePair<string, System.Object>>();
            return propertys;
        }

        #endregion

        #region AnimatorOverrideController 

        public static List<KeyValuePair<string, System.Object>> AnalyzeAnimatorOverrideController(Object obj,
             SerializedObject serialized_object)
        {
            var propertys = new List<KeyValuePair<string, System.Object>>();
            return propertys;
        }

        #endregion

        #region AnimationClip 动作文件

        public static List<KeyValuePair<string, System.Object>> AnalyzeAnimationClip(Object obj,
             SerializedObject serialized_object)
        {
            var propertys = new List<KeyValuePair<string, System.Object>>();
            return propertys;
        }

        #endregion

        #region AudioClip 音频

        public static List<KeyValuePair<string, System.Object>> AnalyzeAudioClip(Object obj,
             SerializedObject serialized_object)
        {
            var propertys = new List<KeyValuePair<string, System.Object>>();
            return propertys;
        }

        #endregion

        #region Font 字体

        public static List<KeyValuePair<string, System.Object>> AnalyzeFont(Object obj,
             SerializedObject serialized_object)
        {
            var propertys = new List<KeyValuePair<string, System.Object>>();
            return propertys;
        }

        #endregion TextAsset

        public static List<KeyValuePair<string, System.Object>> AnalyzeTextAsset(Object obj,
            SerializedObject serialized_object)
        {
            var propertys = new List<KeyValuePair<string, System.Object>>();
            return propertys;
        }

        #region AnimationsAnimatorController

        public static List<KeyValuePair<string, System.Object>> AnalyzeAnimationsAnimatorController(Object obj,
             SerializedObject serialized_object)
        {
            var propertys = new List<KeyValuePair<string, System.Object>>();
            return propertys;
        }

        #endregion
    }
}
