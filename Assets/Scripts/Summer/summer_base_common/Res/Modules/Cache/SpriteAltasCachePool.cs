
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
    /// <summary>
    /// 常驻+最大
    /// </summary>
    public class SpriteAltasCachePool : PoolCache<string, ResRefCount>, I_Update
    {
        #region 属性

        public static SpriteAltasCachePool Instance = new SpriteAltasCachePool();
        public Dictionary<string, ResRefCount> _map = new Dictionary<string, ResRefCount>();

        #endregion

        #region Public

        private SpriteAltasCachePool()
        {
            OnRemoveValueEvent += OnRemove;
            Capacity = 2;
        }

        public void LoadSprite(SpriteAtlasLoader loader)
        {
            ResManager.instance.LoadSprite(loader._img, loader._resPath);
            Set(loader._resPath, Get(loader._resPath));
        }

        public void UnLoadSprite(SpriteAtlasLoader loader)
        {
            ResLoader.Instance.UnLoadRef(loader._img);
        }

        public void OnUpdate(float dt)
        {

        }

        #endregion

        #region Private Methods

        private void OnRemove(string key)
        {
            ResLoader.Instance.UnLoadRes(key);
        }

        private ResRefCount Get(string resPath)
        {
            if (_map.ContainsKey(resPath))
                return _map[resPath];
            else
            {
                ResRefCount info = new ResRefCount { _resPath = resPath };
                _map.Add(resPath, info);
                return info;
            }
        }

        #endregion
    }
}