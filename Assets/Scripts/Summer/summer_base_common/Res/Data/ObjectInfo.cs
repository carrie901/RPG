
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
using UnityEngine;

namespace Summer
{

    public class ObjectInfo : I_ObjectInfo
    {
        /// <summary>
        /// 这个路径
        ///     是资源路径比如 res_bundle/xxx/xxx.png 
        ///     //也可是资源包路径StreamingAssets/xxx/xxx.ab  
        ///     同样是ResName xxx
        /// </summary>
        public string Path { get; private set; }
        public int RefCount { get; private set; }
        public Object _object;
        public ObjectInfo(Object obj, string resPath)
        {
            _object = obj;
            Path = resPath;
            RefCount = 0;
        }
        public T GetAsset<T>(string resName) where T : Object
        {
            if (_object == null)
            {
                ResLog.Error("AssetInfo Error,Object Is Null. Path:[{0}]", Path);
                return null;
            }
            T t = _object as T;
            if (t == null)
            {
                ResLog.Error("AssetInfo Error,Object 类型不对.Path:[{0}],提取类型:[{1}],实际类型:[{2}]", Path, typeof(T), _object);
            }
            RefCount++;
            return t;
        }

        public void UnRef(Object obj)
        {
            RefCount--;
        }

        public void UnRefByPath(string resPath)
        {
            RefCount--;
        }

        public virtual void UnLoad() { }

        public bool CheckObject(Object obj)
        {
            return _object == obj;
        }

        public bool CheckType<T>() where T : Object
        {
            return _object is T;
        }

        public bool IsEmptyRef()
        {
            return RefCount <= 0;
        }
    }
}
