
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

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Summer
{
    public class ScrollCellItem : MonoBehaviour, IPointerClickHandler
    {

        #region 属性

        protected int _index;
        protected RectTransform _tran;

        #endregion

        #region MONO Override

        void Awake()
        {
            _tran = gameObject.GetComponent<RectTransform>();
        }

        #endregion

        #region Public

        public void SetPos(Vector2 pos)
        {
            _tran.anchoredPosition = pos;
        }

        public virtual void SetData(System.Object data, int index)
        {
            _index = index;
#if UNITY_EDITOR
            gameObject.name = string.Format("{0:00}", index);
#endif
        }

        public int GetIndex()
        {
            return _index;
        }

        #endregion

        #region override

        public virtual void InitCreate()
        {

        }

        public virtual void Clear()
        {

        }

        public virtual void OnPointerClick(PointerEventData eventData)
        {

        }
        #endregion
    }
}