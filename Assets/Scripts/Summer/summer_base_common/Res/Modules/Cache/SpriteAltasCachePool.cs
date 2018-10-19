
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
    public class SpriteAltasCachePool : PoolExternalCache<string, SpriteTagPoolCacheRef>, I_Update
    {
        #region 属性

        public static SpriteAltasCachePool Instance = new SpriteAltasCachePool();
        public Dictionary<string, SpriteTagPoolCacheRef> _map = new Dictionary<string, SpriteTagPoolCacheRef>();

        #endregion

        #region Public

        private SpriteAltasCachePool()
        {
            OnRemoveValueEvent += OnRemove;
            Capacity = 2;
        }

        public void LoadSprite(SpriteAtlasLoader loader)
        {
            ResManager.instance.UnLoadSprite(loader._img);
            ResManager.instance.LoadSprite(loader._img, loader._resPath);
            Set(loader._resPath, Get(loader));
        }

        public void UnLoadSprite(SpriteAtlasLoader loader)
        {
            ResManager.instance.UnLoadSprite(loader._img);
        }

        public void OnUpdate(float dt)
        {

        }

        #endregion

        #region Private Methods

        private void OnRemove(string key)
        {
            if (_map.ContainsKey(key))
            {
                ResLoader.Instance.UnLoadRes(_map[key]._firstSpriteName);
            }
        }

        private SpriteTagPoolCacheRef Get(SpriteAtlasLoader loader)
        {
            if (_map.ContainsKey(loader._spriteTag))
                return _map[loader._spriteTag];
            else
            {
                SpriteTagPoolCacheRef info = new SpriteTagPoolCacheRef(loader._spriteTag, loader._resPath);
                _map.Add(info._spriteTag, info);
                return info;
            }
        }

        #endregion
    }
}