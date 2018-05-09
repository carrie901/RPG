﻿
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace SummerEditor
{
    public class AnimationSeparationTool
    {
        public static string copy_anim_path = "Assets/Raw/Animation/";

        public static string raw_anim_directory = "Assets/Raw/Model/";


        [MenuItem("Tools/Animation/分离动画")]
        public static void AllSeparationAnimationByFbx()
        {
            List<string> dirs = new List<string>();
            EPathHelper.ScanDirectory(raw_anim_directory, dirs);

            int length = dirs.Count;
            for (int i = 0; i < length; i++)
            {
                List<AnimationClip> clips = new List<AnimationClip>();

                string asset_dir_path = GetFolderName(dirs[i]);

                string anim_folder = FindAnimsByFolder(asset_dir_path, clips);
                if (string.IsNullOrEmpty(anim_folder) || clips.Count == 0) continue;


                if (!Directory.Exists(copy_anim_path + anim_folder))
                    Directory.CreateDirectory(copy_anim_path + anim_folder);

                int clip_length = clips.Count;
                for (int k = 0; k < clip_length; k++)
                {
                    //CopyAsset(clips[k], anim_folder);
                }
            }

            AssetDatabase.Refresh();
            AssetDatabase.SaveAssets();
        }

        [MenuItem("Assets/Animation/分离动画")]
        public static void SeparationAnimationByFbx()
        {
            List<AnimationClip> clips = new List<AnimationClip>();
            string select_path = FindAnims(clips);
            if (clips.Count == 0) return;

            string anim_folder = GetFolderName(select_path);

            if (!Directory.Exists(copy_anim_path + anim_folder))
                Directory.CreateDirectory(copy_anim_path + anim_folder);
            int length = clips.Count;
            for (int i = 0; i < length; i++)
            {
                CopyAsset(clips[i], anim_folder);
            }
            AssetDatabase.Refresh();
            AssetDatabase.SaveAssets();
        }

        public static void CopyAsset(AnimationClip old_clip, string anim_folder)
        {
            AnimationClip new_clip = new AnimationClip();
            EditorUtility.CopySerialized(old_clip, new_clip);

            string copy_asset_path = copy_anim_path + anim_folder + "/" + new_clip.name + "_1.anim";
            string new_asset_path = copy_anim_path + anim_folder + "/" + new_clip.name + ".anim";


            AssetDatabase.CreateAsset(new_clip, copy_asset_path);
            File.Copy(copy_asset_path, new_asset_path, true);
            AssetDatabase.DeleteAsset(copy_asset_path);
        }

        // 根据路径得到这个文件夹的名字
        public static string GetFolderName(string path)
        {
            FileInfo file_info = new FileInfo(path);

            string dir = EPathHelper.AbsoluteToRelativePathWithAssets(file_info.DirectoryName);
            string[] results = dir.Split('/');
            string result = results[results.Length - 1];
            return result;
        }

        // 查找目录下的所有Clips
        public static string FindAnims(List<AnimationClip> clips)
        {
            Object[] objs = Selection.GetFiltered(typeof(object), SelectionMode.Assets);
            string[] guids = null;
            string anim_path = string.Empty;

            List<string> paths = new List<string>();
            int length = objs.Length;
            for (int i = 0; i < length; i++)
            {
                string path = AssetDatabase.GetAssetPath(objs[i]);
                paths.Add(path);
            }

            if (paths.Count > 0)
                guids = AssetDatabase.FindAssets(string.Format("t:{0}", typeof(AnimationClip).ToString().Replace("UnityEngine.", "")), paths.ToArray());
            else
                guids = new string[] { };

            length = guids.Length;
            for (int i = 0; i < length; i++)
            {
                string asset_path = AssetDatabase.GUIDToAssetPath(guids[i]);
                AnimationClip clip = AssetDatabase.LoadAssetAtPath<AnimationClip>(asset_path);
                if (clip == null) continue;
                anim_path = asset_path;
                clips.Add(clip);
            }

            return anim_path;
        }

        public static string FindAnimsByFolder(string floder, List<AnimationClip> clips)
        {
            string anim_path = string.Empty;
            string[] guids = null;
            List<string> paths = new List<string> { floder };
            if (paths.Count > 0)
                guids = AssetDatabase.FindAssets(string.Format("t:{0}", typeof(AnimationClip).ToString().Replace("UnityEngine.", "")), paths.ToArray());
            else
                guids = new string[] { };
            int length = guids.Length;

            for (int i = 0; i < length; i++)
            {
                string asset_path = AssetDatabase.GUIDToAssetPath(guids[i]);
                AnimationClip clip = AssetDatabase.LoadAssetAtPath<AnimationClip>(asset_path);
                if (clip == null) continue;
                anim_path = asset_path;
                clips.Add(clip);
            }
            return anim_path;
        }

    }
}

