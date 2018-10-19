
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

    public class SpriteTagPoolCacheRef : I_PoolCacheRef
    {
        public string _spriteTag;
        public string _firstSpriteName;
        public Dictionary<string, int> _spriteNames = new Dictionary<string, int>(64);

        public SpriteTagPoolCacheRef(string tag, string firstName)
        {
            ResLog.Assert(!(string.IsNullOrEmpty(tag) || string.IsNullOrEmpty(firstName)),
                "初始化SpriteTagPoolCacheRef失败.Tag:[{0}],firstName[{1}]", tag, firstName);
            _spriteTag = tag;
            _firstSpriteName = firstName;
        }

        /*public void AddSprite(SpriteAtlasLoader sprite)
        {
            ResLog.Assert(sprite._spriteTag == _spriteTag,
                "SpriteTagPoolCacheRe AddSprite Fail.Tag:[{0}], Add Sprite Tag:[1]", _spriteTag, sprite._spriteTag);
            /*if (_spriteNames.ContainsKey(sprite._resPath))
            {
                _spriteNames.ad
            }#1#
        }*/

        public int GetRefCount()
        {
            int refCount = ResLoader.Instance.GetRefCount(_firstSpriteName);
            return refCount;
        }
    }
}

