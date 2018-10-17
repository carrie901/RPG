
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

using Object = UnityEngine.Object;

namespace Summer
{
    public interface I_ObjectInfo
    {
        /// <summary>
        /// 这个路径 是资源路径比如 res_bundle/xxx/xxx.png 也可是资源包路径StreamingAssets/xxx/xxx.ab  
        /// </summary>
        string Path { get; }

        int RefCount { get; }

        T GetAsset<T>(string resName) where T : Object;

        void UnRef(Object obj);

        void UnLoad();

        bool CheckObject(Object obj);

        //bool CheckType<T>() where T : Object;

        bool IsEmptyRef();
    }



}

