
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


namespace Summer
{
    /// <summary>
    /// 
    /// </summary>
    public class PfbPath : BaseResPath
    {
        #region static

        public static string _rootPath = "res_bundle/prefab/ui/";
        public static PfbPath Instance = new PfbPath();

        #endregion

        #region 属性

        public string RootPanel = "RootPanel.prefab";

        #endregion

        private PfbPath() : base(PathType.PANEL_PFB) { }
        public override string Get(string path)
        {
            return _rootPath + path;
        }
    }

}

