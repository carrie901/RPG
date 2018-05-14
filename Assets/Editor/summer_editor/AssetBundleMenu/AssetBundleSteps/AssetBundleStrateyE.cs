using System.Collections.Generic;

namespace SummerEditor
{
    /// <summary>
    /// 打包策略
    /// </summary>
    public abstract class AbStratey
    {
        protected string _directory;
        protected List<string> files;

        protected AbStratey(string path)
        {
            _directory = path;
            _init();
        }

        public void _init()
        {
            files = EPathHelper.GetAssetPathList01(_directory, true);
        }

        public abstract void BuildAsset();
    }

    /// <summary>
    /// 这个文件夹下的资源按照自己的文件名字设置Bundle Name
    /// </summary>
    public class BuildAssetInFileNameAb : AbStratey
    {
        public BuildAssetInFileNameAb(string path) : base(path) { }

        public override void BuildAsset()
        {
            int length = files.Count;
            for (int i = 0; i < length; i++)
            {
                AssetBundleSetNameE.SetAbNameByPath(files[i]);
            }
        }
    }

    /// <summary>
    /// 这个文件夹下所有资源的达成一个包
    /// </summary>
    public class BuildAssetInOneAb : AbStratey
    {
        public BuildAssetInOneAb(string path) : base(path) { }

        public override void BuildAsset()
        {
            int length = files.Count;
            for (int i = 0; i < length; i++)
            {
                AssetBundleSetNameE.SetAbNameByDirectory(files[i]);
            }
        }
    }

    /// <summary>
    /// 这个文件夹下的资源按照大小，A+B+C小于指定大小达成一个包
    /// </summary>
    public class BuildAssetInSizeAb : AbStratey
    {
        public BuildAssetInSizeAb(string path) : base(path)
        {
        }

        public override void BuildAsset()
        {
            // 脑子不好，直接粗糙算法，直接按照顺序加下去
            int length = files.Count;
            float left_size = EAssetBundleConst.ASSETBUILD_MAX_SIZE;
            string directory = EPathHelper.AbsoluteToRelativePathRemoveAssets(_directory);
            int name_index = 1;
            for (int i = 0; i < length; i++)
            {
                float file_size = EMemorySizeHelper.GetFileSize(files[i]);
                if (file_size >= EAssetBundleConst.ASSETBUILD_MAX_SIZE)
                {
                    SetAbName(files[i], directory, name_index);
                    name_index++;
                }
                else
                {
                    if (left_size >= file_size)
                    {
                        SetAbName(files[i], directory, name_index);
                        left_size = left_size - file_size;
                    }
                    else
                    {
                        name_index++;
                        left_size = EAssetBundleConst.ASSETBUILD_MAX_SIZE - file_size;

                        SetAbName(files[i], directory, name_index);
                    }
                }
            }
        }

        public void SetAbName(string file_path, string directory, int index)
        {
            AssetBundleSetNameE.SetAbNameByParam(file_path, string.Format("{0}{1:0000}", directory, index));
        }
    }

    /// <summary>
    /// 这个文件夹下的资源按照名字的相同达成一个包
    /// 特别合适英雄技能Icon
    /// </summary>
    public class BuildAssetInNameAb: AbStratey
    {
        public BuildAssetInNameAb(string path) : base(path)
        {
        }

        public override void BuildAsset()
        {
            
        }
    }
}
