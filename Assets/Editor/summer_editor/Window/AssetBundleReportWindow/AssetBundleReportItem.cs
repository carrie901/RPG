
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
    public class AssetBundleReportItem : ExtendTableItem
    {

        #region 属性

        public EAssetBundleFileInfo _info;

        #endregion

        #region Override

        public AssetBundleReportItem(int max_size, float[] titles_width, float width, float height)
            : base(max_size, titles_width, width, height)
        {
            SetData(null);
        }

        public override void SetData(object data)
        {
            base.SetData(data);
            _info = data as EAssetBundleFileInfo;
            if (_info != null)
            {

                string t_repeat_mem_size = (_info.GetRepeatMemSize() / 1024).ToString("f2"); ;
                string t_ab_size = (_info.file_ab_memory_size / 1024).ToString("f2");
                string t_ab_mem_size = (_info.GetMemorySize() / 1024).ToString("f2");
                string[] string_info = new string[]
                {
                    _info.ab_name, t_ab_size, t_ab_mem_size,
                    _info.all_depends.Count + "", _info.FindRedundance() + "",
                    _info.GetAssetCount(E_AssetType.mesh) + "",
                    _info.GetAssetCount(E_AssetType.material) + "", _info.GetAssetCount(E_AssetType.texture) + "",
                    _info.GetAssetCount(E_AssetType.shader) + "",
                    _info.GetAssetCount(E_AssetType.sprite) + "", _info.GetAssetCount(E_AssetType.animation_clip) + "",
                    _info.GetAssetCount(E_AssetType.audio_clip) + "", t_repeat_mem_size + ""
                };
                SetContent(string_info);
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