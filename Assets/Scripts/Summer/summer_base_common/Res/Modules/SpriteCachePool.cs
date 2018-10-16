
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

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Summer
{
    /// <summary>
    /// 常驻+最大
    /// </summary>
    public class SpriteCachePool : I_SpriteLoad, I_Update
    {

        #region 属性

        public static SpriteCachePool Instance = new SpriteCachePool();

        #endregion

        #region Public

        private SpriteCachePool()
        {
            
        }

        #endregion

        #region Private Methods



        #endregion

        #region Override

        public Sprite LoadSprite(string resPath)
        {
            throw new NotImplementedException();
        }

        public void LoadSpriteAsync(Image img, string resPath, Action<Sprite> complete = null)
        {
            throw new NotImplementedException();
        }

        public void OnUpdate(float dt)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}