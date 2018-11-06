using System.Collections.Generic;
using System.IO;

namespace SummerEditor
{
    /// <summary>
    /// 拼接UI用的Sprite图片
    /// 下面有N个目录，每个目录
    /// </summary>
    public class BuildAssetInSprite : I_AssetBundleStratey
    {
        public List<BuildAssetInOneAb> _strateys = new List<BuildAssetInOneAb>();
        public BuildAssetInSprite()
        {
            _initBuild();
        }

        public bool IsBundleStratey(EAssetObjectInfo info)
        {
            return false;
        }

        public void SetAssetBundleName()
        {
            for (int i = _strateys.Count - 1; i >= 0; i--)
            {
                _strateys[i].SetAssetBundleName();
            }
        }

        #region private

        private void _initBuild()
        {
            DirectoryInfo spriteDirectoryInfo = Directory.CreateDirectory(EAssetBundleConst.RES_SPRITE_DRIECTORY);
            DirectoryInfo[] spritesInfo = spriteDirectoryInfo.GetDirectories();
            // 处理单个目录
            for (int i = spritesInfo.Length - 1; i >= 0; i--)
            { // 生成 一个目录的资源打一个包
                BuildAssetInOneAb oneAb = new BuildAssetInOneAb(spritesInfo[i].FullName);
                _strateys.Add(oneAb);
            }
        }
        #endregion
    }
}
