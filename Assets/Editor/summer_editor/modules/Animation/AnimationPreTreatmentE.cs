using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using Summer;

namespace SummerEditor
{
    /// <summary>
    /// 角色的动画预处理，给角色添加添加对应的动画
    /// </summary>
    public class AnimationPreTreatmentE
    {

        public static string[] _rawAnimDirectory = new string[]
        {
            "Assets/Raw/Animation/"
        };

        public static string _characterPrefabDirectory = "Assets/res_bundle/prefab/model/";                // 角色Prefab
        public static string _searchSuffix = "*.anim";                                                      // *.FBX        查询的目标是fbx还是anim

        #region 预处理

        //[MenuItem("Tools/预处理/动画预处理")]
        public static void AllAnimationPreTreatment()
        {
            //List<string> all_animation = new List<string>(animation_all);

            // 1.找到需要处理的角色prefab
            List<string> charPrefabPath = EPathHelper.GetAssetsPath(_characterPrefabDirectory, false, "*.prefab");
            int length = charPrefabPath.Count;
            for (int i = 0; i < length; i++)
            {
                _excute_animation(charPrefabPath[i]);
                EditorUtility.DisplayProgressBar(charPrefabPath[i], string.Format("动画预处理({0}/{1})", i + 1, length), (float)(i + 1) / length);
            }

            SavePrefab();
            EditorUtility.ClearProgressBar();
            EditorUtility.DisplayDialog("", "动画预处理完毕", "OK");
        }

        //[MenuItem("Assets/Asset Pre Process/动画预处理")]
        public static void AnimationPreTreatment()
        {
            GameObject selectGo = Selection.activeGameObject;
            if (selectGo == null) return;

            string path = AssetDatabase.GetAssetPath(selectGo);
            if (!path.Contains(_characterPrefabDirectory))
            {
                Debug.LogErrorFormat("请选择{0}的路径下的Prefab", _characterPrefabDirectory);
                return;
            }

            _excute_animation(path);
            SavePrefab();
            EditorUtility.DisplayDialog("", "动画预处理完毕", "OK");
        }

        #endregion

        #region public 

        public static void SavePrefab()
        {

            AssetDatabase.Refresh();
            AssetDatabase.SaveAssets();
            Resources.UnloadUnusedAssets();
        }

        public static T Check<T>(GameObject go) where T : MonoBehaviour
        {
            T t = go.GetComponent<T>();
            if (t == null)
                t = go.AddComponent<T>();
            return t;
        }

        #endregion  

        #region private

        public static void _excute_animation(string path)
        {
            // 2.根据指定角色的prefab路径得到动作文件夹
            string animFolder = _find_file_folder(path);
            Debug.AssertFormat(!string.IsNullOrEmpty(animFolder), "路径:{0},找不到对应的动作文件", path);
            if (string.IsNullOrEmpty(animFolder)) return;
            // 3.得到动作文件
            List<AnimationClip> animClips = _find_animation_clip(animFolder);
            // 4.设置资源
            _add_animation(path, animClips);
        }

        // 添加prefab的动画
        public static void _add_animation(string prefabPath, List<AnimationClip> animClips)
        {
            GameObject obj = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);
            if (obj == null) return;

            Animator ani = obj.GetComponent<Animator>();
            bool result = (ani != null);
            Debug.AssertFormat(result, "路径:{0},Animator为空", prefabPath);
            if (!result) return;

            result = (ani.runtimeAnimatorController != null);
            Debug.AssertFormat(result, "路径:{0},Animator的runtimeAnimatorController为空", prefabPath);
            if (!result) return;

            EntityAnimationGroup aniConfig = Check<EntityAnimationGroup>(obj);
            aniConfig.Clear();
            for (int i = 0; i < animClips.Count; i++)
            {
                aniConfig.AddAnims(animClips[i].name, animClips[i]);
            }

            EditorUtility.SetDirty(obj);
        }

        // 查找指定目录下的所有动画文件
        public static List<AnimationClip> _find_animation_clip(string folderPath)
        {

            List<AnimationClip> clips = new List<AnimationClip>();
            List<string> animsPath = EPathHelper.GetAssetsPath(folderPath, false, _searchSuffix);

            int length = animsPath.Count;
            for (int i = 0; i < length; i++)
            {
                EditorUtility.DisplayProgressBar("加载动画文件,", animsPath[i], (1 + i) / length);
                AnimationClip clip = AssetDatabase.LoadAssetAtPath<AnimationClip>(animsPath[i]);
                if (clip == null) continue;
                clips.Add(clip);
            }
            EditorUtility.ClearProgressBar();
            return clips;
        }

        // 根据相对路径得到名字,根据一定的规则得到原始动作文件夹
        public static string _find_file_folder(string filePath)
        {
            string assetName = EPathHelper.GetName(filePath);
            string npcName = assetName.Split('_')[1].Split('.')[0];


            for (int i = 0; i < _rawAnimDirectory.Length; i++)
            {
                if (Directory.Exists(_rawAnimDirectory[i] + npcName))
                    return _rawAnimDirectory[i] + npcName;
            }

            return string.Empty;
        }

        #endregion
    }
}
