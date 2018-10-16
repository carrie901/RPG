
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
using UnityEngine.UI;

namespace Summer
{


    public class SpriteSelectableBigLoader : MonoBehaviour
    {
        public Selectable _selectable;
        private SpriteState _newSprite = new SpriteState();
        public string _disabledSpritePath;
        

        public string _highlightedSpritePath;

        public string _pressedSpritePath;
        public bool _initComplete = false;
        void OnEnable()
        {
            _initComplete = true;
            if (!string.IsNullOrEmpty(_disabledSpritePath))
            {
                //Sprite sprite = SpritePool.Instance.LoadSprite(_disabledSpritePath);
                //_newSprite.disabledSprite = sprite;
            }

            if (!string.IsNullOrEmpty(_highlightedSpritePath))
            {
                //Sprite sprite = SpritePool.Instance.LoadSprite(_highlightedSpritePath);
                //_newSprite.highlightedSprite = sprite;
            }

            if (!string.IsNullOrEmpty(_pressedSpritePath))
            {
                //Sprite sprite = SpritePool.Instance.LoadSprite(_pressedSpritePath);
                //_newSprite.highlightedSprite = sprite;
            }
            _selectable.spriteState = _newSprite;

        }

        void OnDisable()
        {
            ResLog.Assert(_initComplete, "初始化没有完成.GameObject Name:[{0}],Res Path:[{1}]", gameObject.name, _disabledSpritePath);
            if (!_initComplete) return;
            //SpritePool.Instance.ReaycelSprite(this);
        }

        public void ReaycelSprite()
        {
            //_selectable.spriteState = new SpriteState();
            //_selectable.spriteState = new SpriteState();
            //_selectable.spriteState.disabledSprite = null;
            //_selectable.spriteState.disabledSprite = default_sprite;
            //LogManager.Error("SpriteSelectableAutoLoader: 搞不定呀搞不定");
        }
    }
}