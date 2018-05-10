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
        /*public static string[] raw_anim_directory = new string[]
        {
            "Assets/Raw/Model/"
        };*/

        public static string[] raw_anim_directory = new string[]
        {
            "Assets/Raw/Animation/"
        };

        public static string character_prefab_directory = "Assets/Res/Prefab/Model/";                         // 角色Prefab
        public static string search_suffix = "*.anim";//*.FBX        查询的目标是fbx还是anim
        #region 预处理

        //[MenuItem("Tools/预处理/动画预处理")]
        public static void AllAnimationPreTreatment()
        {
            //List<string> all_animation = new List<string>(animation_all);

            // 1.找到需要处理的角色prefab
            List<string> char_prefab_path = EPathHelper.GetAssetPathList(character_prefab_directory, false, "*.prefab");
            int length = char_prefab_path.Count;
            for (int i = 0; i < length; i++)
            {
                _excute_animation(char_prefab_path[i]);
                EditorUtility.DisplayProgressBar(char_prefab_path[i], string.Format("动画预处理({0}/{1})", i + 1, length), (float)(i + 1) / length);
            }

            SavePrefab();
            EditorUtility.ClearProgressBar();
            EditorUtility.DisplayDialog("", "动画预处理完毕", "OK");
        }

        //[MenuItem("Assets/Asset Pre Process/动画预处理")]
        public static void AnimationPreTreatment()
        {
            GameObject select_go = Selection.activeGameObject;
            if (select_go == null) return;

            string path = AssetDatabase.GetAssetPath(select_go);
            if (!path.Contains(character_prefab_directory))
            {
                Debug.LogErrorFormat("请选择{0}的路径下的Prefab", character_prefab_directory);
                return;
            }

            _excute_animation(path);
            SavePrefab();
            EditorUtility.DisplayDialog("", "动画预处理完毕", "OK");
        }

        #endregion

        #region private

        public static void _excute_animation(string path)
        {
            // 2.根据指定角色的prefab路径得到动作文件夹
            string anim_folder = _find_file_folder(path);
            Debug.AssertFormat(!string.IsNullOrEmpty(anim_folder), "路径:{0},找不到对应的动作文件", path);
            if (string.IsNullOrEmpty(anim_folder)) return;
            // 3.得到动作文件
            List<AnimationClip> anim_clips = _find_animation_clip(anim_folder);
            // 4.设置资源
            _add_animation(path, anim_clips);
        }

        // 添加prefab的动画
        public static void _add_animation(string prefab_path, List<AnimationClip> anim_clips)
        {
            GameObject obj = AssetDatabase.LoadAssetAtPath<GameObject>(prefab_path);
            if (obj == null) return;

            Animator ani = obj.GetComponent<Animator>();
            bool result = (ani != null);
            Debug.AssertFormat(result, "路径:{0},Animator为空", prefab_path);
            if (!result) return;

            result = (ani.runtimeAnimatorController != null);
            Debug.AssertFormat(result, "路径:{0},Animator的runtimeAnimatorController为空", prefab_path);
            if (!result) return;

            EntityAnimationGroup ani_config = Check<EntityAnimationGroup>(obj);
            ani_config.Clear();
            for (int i = 0; i < anim_clips.Count; i++)
            {
                ani_config.AddAnims(anim_clips[i].name, anim_clips[i]);
            }
        }

        // 查找指定目录下的所有动画文件
        public static List<AnimationClip> _find_animation_clip(string folder_path)
        {
            List<AnimationClip> clips = new List<AnimationClip>();
            List<string> anims_path = EPathHelper.GetAssetPathList(folder_path, false, search_suffix);

            int length = anims_path.Count;
            for (int i = 0; i < length; i++)
            {
                AnimationClip clip = AssetDatabase.LoadAssetAtPath<AnimationClip>(anims_path[i]);
                if (clip == null) continue;
                clips.Add(clip);
                /*Object[] objs = AssetDatabase.LoadAllAssetRepresentationsAtPath(anims_path[i]);

                for (int k = 0; k < objs.Length; k++)
                {
                    if (!(objs[k] is AnimationClip)) continue;
                    clips.Add(objs[k] as AnimationClip);
                }*/
            }
            return clips;
        }

        // 根据相对路径得到名字,根据一定的规则得到原始动作文件夹
        public static string _find_file_folder(string file_path)
        {
            string npc_name = file_path.Split('_')[1].Split('.')[0];


            for (int i = 0; i < raw_anim_directory.Length; i++)
            {
                if (Directory.Exists(raw_anim_directory[i] + npc_name))
                    return raw_anim_directory[i] + npc_name;
            }

            return string.Empty;
        }

        #endregion
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


        public static string[] animation_all =
        {
            "attack_01",
            "attack_02",
            "attack_03",
            "attack_04",
            "attack_05",
            "attack_06",
            "climb_up",
            "climb_down",
            "die",
            "hit",
            "hit1",
            "hit2",
            "hit3",
            "idle",
            "KnockHit_left",
            "KnockHit_up",
            "KnockHit_right",
            "KnockHitFlow_left",
            "KnockHitFlow_right",
            "KnockHitFlow_up",
            "KnockHitDown_left",
            "KnockHitDown_right",
            "KnockHitDown_up",
            "Peerless_01",
            "run",
            "walk",
            "up_up",
            "up_left",
            "up_right",
            "skill_01",
            "skill_02",
            "skill_03",
            "skill_04",
            "skill_05",
            "skill_06",
        };
    }
}
