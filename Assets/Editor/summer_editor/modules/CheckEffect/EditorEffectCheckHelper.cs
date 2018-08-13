using System.Collections.Generic;
using System.IO;
using System.Text;
using Summer;
using UnityEditor;
using UnityEngine;
using UnityEditor.SceneManagement;

//=============================================================================
// Author : mashao
// CreateTime : 2018-1-31 17:6:45
// FileName : EditorEffectCheck.cs
//=============================================================================

namespace SummerEditor
{
    /// <summary>
    /// 检测特效引用的图片
    /// </summary>
	public class EditorEffectCheckHelper
    {
        public static EndsWithFilter name_filter = new EndsWithFilter(".png");
        // <特效名称,<图片名称，图片内存大小>>
        public static Dictionary<string, EditorCheckEffectTextureInfo> effect_map =
            new Dictionary<string, EditorCheckEffectTextureInfo>();

        #region 检测特效的纹理引用

        public static void CheckEffectTexture()
        {
            effect_map.Clear();
            // 1.查找对应的特效prefab文件
            DirectoryInfo dir_info = new DirectoryInfo(CheckEffectConst.all_effect_path);
            FileInfo[] files = dir_info.GetFiles("*.prefab", SearchOption.AllDirectories);

            int file_length = files.Length;
            for (int i = 0; i < file_length; i++)
            {
                EditorUtility.DisplayProgressBar("分析特效的贴图", files[i].Name, (float)i / file_length);
                // 2.依次分析特效文件
                AnalysisFile(files[i]);
            }
            EditorUtility.ClearProgressBar();
            // 3.把分析结果写入CSV
            WriteFile();
            EditorUtility.DisplayDialog("分析特效贴图", "1.下一步开始启动场景检测特效Dc", "Ok");
        }

        private static void AnalysisFile(FileInfo file_info)
        {
            // 1.绝对路径转换为Unity/Assets相对路径
            string file_path = EPathHelper.AbsoluteToRelativePathWithAssets(file_info.FullName);

            // 2.初始化特效的相关信息
            EditorCheckEffectTextureInfo eff_texture_info = new EditorCheckEffectTextureInfo();
            eff_texture_info.EffectName = file_info.Name.Split('.')[0];
            eff_texture_info.AssetPath = EPathHelper.AbsoluteToRelativePathWithAssets(file_info.FullName);
            // 3.找到特效依赖的文件
            Object obj = AssetDatabase.LoadAssetAtPath<Object>(file_path);
            //TODO AssetDatabase.LoadAssetAtPath和EditorUtility.CollectDependencies的差别
            //string[] deps = AssetDatabase.GetDependencies(file_path);
            Object[] deps_obj = EditorUtility.CollectDependencies(new Object[] { obj });
            // 4.过滤非纹理资源
            //List<string> dep_list = SuffixHelper.Filter(deps, name_filter);
            //eff_texture_info.AddTexList(dep_list);
            eff_texture_info.AddTexList(deps_obj);
            // 5.计算
            effect_map.Add(eff_texture_info.EffectName, eff_texture_info);
        }

        private static void WriteFile()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("特效名称,占用内存,纹理个数");
            foreach (var v in effect_map)
            {
                sb.AppendLine(v.Value.ToDes());
            }
            File.WriteAllText(CheckEffectConst.effect_texture_report_path, sb.ToString());
        }


        #endregion

        #region 检测特效的dc

        // 检测特效的dc
        public static void CheckEffectDrawCall()
        {
            Time.timeScale = 1.0f;
            EditorSceneManager.OpenScene(CheckEffectConst.effect_scene);
            EditorApplication.isPlaying = true;
        }

        #endregion

    }
}
