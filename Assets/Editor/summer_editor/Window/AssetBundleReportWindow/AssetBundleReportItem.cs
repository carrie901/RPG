
//
//                            _ooOoo_
//                           o8888888o
//                           88" . "88
//                           (| -_- |)
//                           O\  =  /O
//                        ____/`---'\____
//                      .'  \\|     |//  `.
//                     /  \\|||  :  |||//  \
//                    /  _||||| -:- |||||-  \
//                    |   | \\\  -  /// |   |
//                    | \_|  ''\---/''  |   |
//                    \  .-\__  `-`  ___/-. /
//                  ___`. .'  /--.--\  `. . __
//               ."" '<  `.___\_<|>_/___.'  >'"".
//              | | :  `- \`.;`\ _ /`;.`/ - ` : | |
//              \  \ `-.   \_ __\ /__ _/   .-` /  /
//         ======`-.____`-.___\_____/___.-`____.-'======
//                            `=---='
//        ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
//                 			 佛祖 保佑             


namespace SummerEditor
{
    /// <summary>
    /// 老的我想弃用了
    /// </summary>
    public class AssetBundleReportItem : ExtendTableItem
    {

        #region 属性

        public EAssetBundleFileInfo _info;

        #endregion

        #region Override

        public AssetBundleReportItem(int maxSize, float[] titlesWidth, float width, float height)
            : base(maxSize, titlesWidth, width, height)
        {
            SetData(null);
        }

        public override void SetData(object data)
        {
            base.SetData(data);
            _info = data as EAssetBundleFileInfo;
            if (_info != null)
            {

                string tRepeatMemSize = _info.GetRepeatMemSize()+"";
                string tAbSize = _info.FileAbMemorySize + "";
                string tAbMemSize = _info.GetMemorySize() + "";
                string[] stringInfo = new string[]
                {
                    _info.AbName, tAbSize, tAbMemSize,
                    _info._allDepends.Count + "", _info.FindRedundance() + "",
                    _info.GetAssetCount(E_AssetType.MESH) + "",
                    _info.GetAssetCount(E_AssetType.MATERIAL) + "", _info.GetAssetCount(E_AssetType.TEXTURE) + "",
                    _info.GetAssetCount(E_AssetType.SHADER) + "",
                    _info.GetAssetCount(E_AssetType.SPRITE) + "", _info.GetAssetCount(E_AssetType.ANIMATION_CLIP) + "",
                    _info.GetAssetCount(E_AssetType.AUDIO_CLIP) + "", tRepeatMemSize + ""
                };
                SetContent(stringInfo);
            }

        }

        #endregion

        #region Public

        public override void _on_draw()
        {
            base._on_draw();
        }

        #endregion

        #region Private Methods



        #endregion


    }
}