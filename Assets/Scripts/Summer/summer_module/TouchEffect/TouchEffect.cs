
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
namespace Summer
{
    /// <summary>
    /// 监听屏幕点击特效
    /// </summary>
    public class TouchEffect : TSingleton<TimerManager>, I_Update
    {

        #region 属性

        protected bool _enable = false;                                 // 功能开启
        protected float _radiusOverlapping;                             // 重叠半径
        protected Vector2 _prevClickPos;

        #endregion

        public void OnUpdate(float dt)
        {
            if (!_enable) return;

#if UNITY_EDITOR
            if (Input.GetMouseButtonDown(0))
            {
                var pos = Input.mousePosition;
#else
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                Vector3 pos = Input.GetTouch(0).position;
#endif
                Vector2 clickPos = ClickScreen(pos);

                if (Vector2.SqrMagnitude(_prevClickPos - clickPos) > _radiusOverlapping)
                {

                }
                else
                {

                }

                _prevClickPos = clickPos;
            }

        }

        #region Public



        #endregion

        #region Private Methods

        public Vector2 ClickScreen(Vector3 inputPos)
        {
            return Vector2.one * 0.5f;
        }

        private bool IsTouch()
        {
            return false;
        }

        #endregion
    }

}
