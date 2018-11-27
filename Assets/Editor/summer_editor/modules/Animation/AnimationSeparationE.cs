using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace SummerEditor
{
    /// <summary>
    /// 动画分离工具
    /// </summary>
    public class AnimationSeparationE
    {
        public static string CopyAnimPath = "Assets/Raw/Animation/"; // copy到指定的目录

        public static string RawAnimDirectory = "Assets/Raw/Model/"; // Fbx原始目录

        //[MenuItem("Tools/Animation/分离动画")]
        public static void AllSeparationAnimationByFbx()
        {
            List<string> assetPaths = EPathHelper.GetAssetsPath(RawAnimDirectory, true, "*.FBX");


            int length = assetPaths.Count;
            for (int i = 0; i < length; i++)
            {
                List<AnimationClip> clips = new List<AnimationClip>();

                string assetPath = assetPaths[i];
                string animFolder = FindAnimsByFolder(assetPath, clips);
                if (string.IsNullOrEmpty(animFolder) || clips.Count == 0) continue;
                animFolder = GetFolderName(animFolder);

                if (!Directory.Exists(CopyAnimPath + animFolder))
                    Directory.CreateDirectory(CopyAnimPath + animFolder);

                int clipLength = clips.Count;
                for (int k = 0; k < clipLength; k++)
                {
                    EditorUtility.DisplayProgressBar("复制文件", "复制" + clips[k] + "到:" + animFolder + "文件夹", (i) / (clipLength * 1.0f));
                    CopyAsset(clips[k], animFolder);
                }
            }
            EditorUtility.ClearProgressBar();
            AssetDatabase.Refresh();
            AssetDatabase.SaveAssets();

        }

        //[MenuItem("Assets/Animation/分离动画")]
        public static void SeparationAnimationByFbx()
        {
            List<AnimationClip> clips = new List<AnimationClip>();
            string selectPath = FindAnims(clips);
            if (clips.Count == 0) return;

            string animFolder = GetFolderName(selectPath);

            if (!Directory.Exists(CopyAnimPath + animFolder))
                Directory.CreateDirectory(CopyAnimPath + animFolder);
            int length = clips.Count;
            for (int i = 0; i < length; i++)
            {
                CopyAsset(clips[i], animFolder);
                EditorUtility.DisplayProgressBar(clips[i].name, string.Format("动画预处理({0}/{1})", i + 1, length), (float)(i + 1) / length);
            }
            EditorUtility.ClearProgressBar();
            EditorUtility.DisplayDialog("", "动画预处理完毕", "OK");
            AssetDatabase.Refresh();
            AssetDatabase.SaveAssets();
        }

        #region private

        public static void CopyAsset(AnimationClip oldClip, string animFolder)
        {
            AnimationClip newClip = new AnimationClip();
            EditorUtility.CopySerialized(oldClip, newClip);

            string copyAssetPath = CopyAnimPath + animFolder + "/" + newClip.name + "_tmp.anim";
            string newAssetPath = CopyAnimPath + animFolder + "/" + newClip.name + ".anim";


            AssetDatabase.CreateAsset(newClip, copyAssetPath);
            File.Copy(copyAssetPath, newAssetPath, true);
            AssetDatabase.DeleteAsset(copyAssetPath);
        }

        // 根据路径得到这个文件夹的名字
        public static string GetFolderName(string path)
        {
            FileInfo fileInfo = new FileInfo(path);
            string dir = EPathHelper.AbsoluteToRelativePathWithAssets(fileInfo.DirectoryName);
            string[] results = dir.Split('/');
            string result = results[results.Length - 1];
            return result;
        }

        // 查找目录下的所有Clips
        public static string FindAnims(List<AnimationClip> clips)
        {
            Object[] objs = Selection.GetFiltered(typeof(object), SelectionMode.Assets);
            string[] guids = null;
            string animPath = string.Empty;

            List<string> paths = new List<string>();
            int length = objs.Length;
            for (int i = 0; i < length; i++)
            {
                string path = AssetDatabase.GetAssetPath(objs[i]);
                paths.Add(path);
            }

            if (paths.Count > 0)
                guids =
                    AssetDatabase.FindAssets(
                        string.Format("t:{0}", typeof(AnimationClip).ToString().Replace("UnityEngine.", "")),
                        paths.ToArray());
            else
                guids = new string[] { };

            length = guids.Length;
            for (int i = 0; i < length; i++)
            {
                string assetPath = AssetDatabase.GUIDToAssetPath(guids[i]);
                AnimationClip clip = AssetDatabase.LoadAssetAtPath<AnimationClip>(assetPath);
                if (clip == null) continue;
                animPath = assetPath;
                clips.Add(clip);
            }

            return animPath;
        }

        // floder必须是Asset/这样的格式
        public static string FindAnimsByFolder(string floder, List<AnimationClip> clips)
        {
            string animPath = string.Empty;
            string[] guids = null;
            List<string> paths = new List<string> { floder };
            if (paths.Count > 0)
                guids =
                    AssetDatabase.FindAssets(
                        string.Format("t:{0}", typeof(AnimationClip).ToString().Replace("UnityEngine.", "")),
                        paths.ToArray());
            else
                guids = new string[] { };
            int length = guids.Length;

            for (int i = 0; i < length; i++)
            {
                string assetPath = AssetDatabase.GUIDToAssetPath(guids[i]);
                AnimationClip clip = AssetDatabase.LoadAssetAtPath<AnimationClip>(assetPath);
                if (clip == null) continue;
                animPath = assetPath;
                clips.Add(clip);
            }
            return animPath;
        }

        #endregion
    }
}

