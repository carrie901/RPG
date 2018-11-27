
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
    public class HorizontalScrollView : ScrollView
    {
        #region

        protected override void Init()
        {
            base.Init();
            _scrollRect.horizontal = true;
            _scrollRect.vertical = false;

            _contentTrans.pivot = new Vector2(0f, 0.5f);
            _contentTrans.anchorMax = new Vector2(0f, 0.5f);
            _contentTrans.anchorMin = new Vector2(0f, 0.5f);
            _contentTrans.anchoredPosition = Vector2.zero;
        }

        protected override float GetCellLength() { return _cellWidth + _widthGap; }

        protected override float GetContentLength() { return _contentWidth; }

        protected override float GetViewLength() { return _viewWidth; }

        protected override float GetScrollPercent() { return _nearestScrollVec.x; }

        protected override int GetCellCount() { return _heightCount; }

        protected override void ResetContentCount()
        {
            int count = _datas.Count;
            _widthCount = (count / _heightCount) + (count % _heightCount == 0 ? 0 : 1);
        }

        protected override void GetIj(int index, out int i, out int j)
        {
            i = index / _heightCount;
            j = index % _heightCount;
        }

        #endregion

        #region Private Methods



        #endregion
    }


}
