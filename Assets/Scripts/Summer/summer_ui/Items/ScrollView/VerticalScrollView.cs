
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

    public class VerticalScrollView : ScrollView
    {
        #region override

        protected override void Init()
        {
            base.Init();
            _scrollRect.horizontal = false;
            _scrollRect.vertical = true;

            _contentTrans.pivot = new Vector2(0.5f, 1f);
            _contentTrans.anchorMax = new Vector2(0.5f, 1f);
            _contentTrans.anchorMin = new Vector2(0.5f, 1f);
            _contentTrans.anchoredPosition = Vector2.zero;
        }

        protected override float GetCellLength() { return _cellHeight + _heightGap; }

        protected override float GetContentLength() { return _contentHeight; }

        protected override float GetViewLength() { return _viewWidth; }

        protected override float GetScrollPercent() { return 1 - _nearestScrollVec.y; }

        protected override int GetCellCount() { return _widthCount; }

        protected override void ResetContentCount()
        {
            int count = _datas.Count;
            _heightCount = (count / _widthCount) + (count % _widthCount == 0 ? 0 : 1);
        }

        protected override void GetIj(int index, out int i, out int j)
        {
            i = index % _widthCount;
            j = index / _widthCount;
        }

        #endregion

        #region Private Methods



        #endregion
    }
}