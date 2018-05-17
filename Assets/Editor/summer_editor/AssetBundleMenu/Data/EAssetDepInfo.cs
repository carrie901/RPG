
namespace SummerEditor
{
    public class EAssetDepInfo : EAssetInfo
    {
        
        public EAssetDepInfo(string path) : base(path)
        {
        }
        public override bool IsMainAsset()
        {
            return false;
        }
    }
}
