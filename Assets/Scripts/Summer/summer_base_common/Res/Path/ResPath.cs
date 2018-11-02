
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

using System.Collections.Generic;

namespace Summer
{
    public class ResPath
    {
        public static Dictionary<PathType, BaseResPath> _resMap
            = new Dictionary<PathType, BaseResPath>(ResPathComparer.Instance)
            {
                {PathType.PANEL_PFB,PfbPath.Instance },
            };

        public static BaseResPath Get(PathType pathType)
        {
            if (_resMap.ContainsKey(pathType))
            {
                return _resMap[pathType];
            }
            return DefaultPath.Instance;
        }

        public static T Get<T>(PathType pathType) where T : BaseResPath
        {
            BaseResPath path = Get(pathType);
            return path as T;
        }

        public static string Get(PathType pathType, string path)
        {
            if (_resMap.ContainsKey(pathType))
            {
                return _resMap[pathType].Get(path);
            }
            return DefaultPath.Instance.Get(path);
        }

        public static void Add(PathType pathType, BaseResPath baseResPath)
        {
            _resMap.Add(pathType, baseResPath);
        }
    }

    public enum PathType
    {
        NONE = 0,
        /// <summary>
        /// 界面路径
        /// </summary>
        PANEL_PFB,
        /// <summary>
        /// 默认路径
        /// </summary>
        DEFAULT,
        MAX,
    }

    public class ResPathComparer : IEqualityComparer<PathType>
    {
        public static ResPathComparer Instance = new ResPathComparer();
        private ResPathComparer() { }
        public bool Equals(PathType x, PathType y)
        {
            return x == y;
        }

        public int GetHashCode(PathType obj)
        {
            return (int)obj;
        }
    }
}
