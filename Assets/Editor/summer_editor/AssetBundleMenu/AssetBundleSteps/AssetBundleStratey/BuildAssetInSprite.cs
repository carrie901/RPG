using System.Collections.Generic;
using System.IO;
using UnityEngine;
namespace SummerEditor
{
    /// <summary>
    /// 拼接UI用的Sprite图片
    /// </summary>
    public class BuildAssetInSprite : I_AssetBundleStratey
    {
        public List<BuildAssetInOneAb> _strateys = new List<BuildAssetInOneAb>();
        public BuildAssetInSprite()
        {
            _init_build();
        }

        public bool IsBundleStratey(EAssetObjectInfo info)
        {
            return false;
        }

        public void AddAssetBundleFileInfo(EAssetObjectInfo info)
        {
            Debug.AssertFormat(info.AssetPath.StartsWith(EAssetBundleConst.res_sprite_driectory), "这个Sprite");
            Debug.AssertFormat(info.BeDepCount() == 0, "Sprite的爸爸依赖必须为0");
        }

        public void SetAssetBundleName()
        {
            for (int i = _strateys.Count - 1; i >= 0; i--)
            {
                _strateys[i].SetAssetBundleName();
            }
        }

        #region

        public void _init_build()
        {
            DirectoryInfo sprite_directory_info = Directory.CreateDirectory(EAssetBundleConst.res_sprite_driectory);

            DirectoryInfo[] sprites_info = sprite_directory_info.GetDirectories();

            for (int i = sprites_info.Length - 1; i >= 0; i--)
            {
                _excute_directory(sprites_info[i]);
            }
        }

        // 处理单个目录
        public void _excute_directory(DirectoryInfo sprite_directory)
        {
            // 生成 一个目录的资源打一个包
            BuildAssetInOneAb one_ab = new BuildAssetInOneAb(sprite_directory.FullName);
            _strateys.Add(one_ab);
        }

        #endregion
    }
}
