
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
using System.Linq;
using UnityEngine;

namespace Summer
{
    /// <summary>
    /// 动态图集
    /// 主要用于战斗界面的英雄头像/技能Icon/身上的Buff
    /// 通过动态图集来极少Dc
    /// </summary>
    public class TexturePacker
    {
        #region 属性

        private readonly Dictionary<string, Sprite> _spriteMap
            = new Dictionary<string, Sprite>();                                             // 生成动态图集集合
        private readonly Dictionary<string, Texture2D> _texMap =
            new Dictionary<string, Texture2D>();                                            // 等待合并的纹理，合并之后会清空
        private Texture2D _mainTex;                                                         // 合并之后的主纹理
        private int _defaultWh = 1024;                                                      // 默认的主纹理宽高
        private int _pixelsPerUnit = 100;

        #endregion

        #region Public

        public void AddTexture2D(Texture2D tex)
        {
            if (_texMap.ContainsKey(tex.name)) return;
            _texMap.Add(tex.name, tex);
        }

        public void PackerTexture()
        {
            _mainTex = new Texture2D(_defaultWh, _defaultWh, TextureFormat.ARGB32, false);
            //_mainTex.alphaIsTransparency = true;
            //_mainTex.filterMode = FilterMode.Bilinear;

            Texture2D[] packerTexs = _texMap.Values.ToArray();
            _texMap.Clear();

            Rect[] uvs = _mainTex.PackTextures(packerTexs, 0);
            _texMap.Clear();
            int length = packerTexs.Length;
            for (int i = 0; i < length; i++)
            {
                Rect rect = new Rect(uvs[i].x * _defaultWh, uvs[i].y * _defaultWh, uvs[i].width * _defaultWh, uvs[i].height * _defaultWh);
                Sprite sprite = Sprite.Create(_mainTex, rect, Vector2.zero, _pixelsPerUnit, 0, SpriteMeshType.FullRect);
                _spriteMap.Add(packerTexs[i].name, sprite);
            }

            /*Uv0 = new Vector2(rect.x, (rect.y + rect.height));
            Uv1 = new Vector2(rect.x, rect.y);
            Uv2 = new Vector2(rect.x + rect.width, rect.y);
            Uv3 = new Vector2(rect.x + rect.width, (rect.y + rect.height));*/
        }

        public Sprite GetSprite(string id)
        {
            Sprite sprite = null;
            _spriteMap.TryGetValue(id, out sprite);
            return sprite; ;
        }

        public void Dispose()
        {
            foreach (var info in _spriteMap)
            {
                Object.Destroy(info.Value);
            }
            _spriteMap.Clear();
            Resources.UnloadAsset(_mainTex);
            _mainTex = null;
        }

        #endregion

        #region Private Methods



        #endregion
    }
}