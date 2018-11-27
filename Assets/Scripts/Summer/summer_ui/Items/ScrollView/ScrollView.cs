
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
    //[RequireComponent(/*typeof(Canvas),*/ typeof(ScrollRect))]
    /// <summary>
    /// 基本功能已经写好，差一些收尾工作和一些对外提供的功能
    /// </summary>
    public abstract class ScrollView : MonoBehaviour
    {

        #region 属性

        public Action<bool> EndLeft;                                        // 到达最左边
        public Action<bool> EndRight;                                       // 到达最右边
        [SerializeField, Header("纵向的间隔")]
        protected float _widthGap;                                          // 纵向的间隔
        [SerializeField, Header("横向的间隔")]
        protected float _heightGap;                                         // 横向的间隔
        [SerializeField, Header("预设")]
        protected GameObject _prefabGo;                                     // 预设
                                                                            //[SerializeField, Header("横向滚动是X偏移量,纵向滚动是Y偏移量")]
        protected float _padding;
        protected List<ScrollCellItem> _hideGo = new List<ScrollCellItem>(8);
        protected List<ScrollCellItem> _showGo = new List<ScrollCellItem>(8);
        protected List<System.Object> _datas = new List<System.Object>(128);
        protected ScrollRect _scrollRect;                                   // 滚动Rect
        protected RectTransform _contentTrans;
        protected float _viewWidth;                                         // 实际可视区域的宽度 这个会和Mask的width会大一点
        protected float _viewHeight;                                        // 实际可视区域的高度 这个会和Mask的Height会大一点
        protected float _contentWidth;
        protected float _contentHeight;
        protected float _cellWidth;                                         // Item的宽度
        protected float _cellHeight;                                        // Item的高度
        protected int _widthCount;                                          // 一行几个
        protected int _heightCount;                                         // 一列几个
        protected int _startIndex;                                          // 显示内容起始下标
        protected int _endIndex;                                            // 显示内容结束下标
        protected GameObject _containerParent;                              // 显示的内容
        protected GameObject _poolParent;                                   // 隐藏的内容
        protected List<int> _contains = new List<int>(16);                  // 目前的包含的Index
        protected Vector2 _nearestScrollVec;

        #endregion

        #region MONO Override

        void Awake()
        {
            Init();
        }

        void OnDisable()
        {
            int length = _showGo.Count;
            for (int i = length - 1; i >= 0; i--)
            {
                Push(_showGo[i]);
            }
        }

        #endregion

        #region Public

        public void InitData<T>(List<T> datas)
        {
            _datas.Clear();
            int length = datas.Count;
            for (int i = 0; i < length; i++)
                _datas.Add(datas[i]);
            ResetContentSize();
            OnScroll(new Vector2(0, 1));
        }
        // 移动到指定位置
        public void SrollToCell(int index, float speed) { }
        //刷新当前的界面
        public void RefreshCells() { }

        #endregion

        #region virtual

        protected virtual void Init()
        {
            _scrollRect = gameObject.GetComponent<ScrollRect>();
            _scrollRect.horizontal = false;
            _scrollRect.vertical = true;
            _scrollRect.onValueChanged.AddListener(OnScroll);

            _containerParent = new GameObject("Container", typeof(RectTransform));
            _contentTrans = _containerParent.GetComponent<RectTransform>();
            _containerParent.transform.SetParent(transform, false);

            _poolParent = new GameObject("Pool", typeof(RectTransform));
            _poolParent.transform.SetParent(transform, false);
            _poolParent.SetActive(false);

            _scrollRect.viewport = gameObject.GetComponent<RectTransform>();
            _scrollRect.content = _contentTrans;

            InitPrefab();

            RectTransform viewRect = _scrollRect.GetComponent<RectTransform>();
            _viewWidth = viewRect.sizeDelta.x;
            _viewHeight = viewRect.sizeDelta.y;

            ResetContentSize();
        }

        protected abstract float GetCellLength();
        // Item内容的长度 横向滚动X,纵向滚动Y 
        protected abstract float GetContentLength();

        protected abstract float GetViewLength();
        // 对应行/列的格式
        protected abstract int GetCellCount();

        // 坐标系问题(0,1) X轴从0开始，Y轴从1开始
        protected abstract float GetScrollPercent();
        // 重置内容中的行和列的数量
        protected abstract void ResetContentCount();
        // 通过Index来得到Item对应的IJ行 I=第几列,J=第几行
        protected abstract void GetIj(int index, out int i, out int j);

        #endregion

        #region Private Methods

        private void Push(ScrollCellItem go)
        {
            _showGo.Remove(go);
            _hideGo.Add(go);
            go.transform.SetParent(_poolParent.transform);
            go.Clear();
        }

        private ScrollCellItem Pop()
        {
            ScrollCellItem reGo;
            if (_hideGo.Count > 0)
            {
                ScrollCellItem go = _hideGo[0];
                reGo = go;
                _showGo.Add(go);
                _hideGo.RemoveAt(0);
            }
            else
            {
                reGo = Create();
                _showGo.Add(reGo);
            }
            reGo.transform.SetParent(_containerParent.transform);
            return reGo;
        }

        private ScrollCellItem Create()
        {
            GameObject unit = Instantiate(_prefabGo);
            ScrollCellItem item = unit.GetComponent<ScrollCellItem>();
            item.InitCreate();
            RectTransform rect = unit.GetComponent<RectTransform>();
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            return item;
        }

        private void ResetContentSize()
        {
            Debug.Assert(_datas != null, "ScrollView Error");
            if (_datas == null) return;

            ResetContentCount();

            float contentW = _widthCount * (_cellWidth + _widthGap) - _widthGap;
            float contentH = _heightCount * (_cellHeight + _heightGap) - _heightGap;

            _contentWidth = contentW;
            _contentHeight = contentH;
            _contentTrans.sizeDelta = new Vector2(_contentWidth, _contentHeight);
        }

        private void OnScroll(Vector2 offset)
        {
            _nearestScrollVec = new Vector2(Mathf.Clamp01(offset.x), Mathf.Clamp01(offset.y));
            GetStartEndIndex(out _startIndex, out _endIndex);
            if (EndLeft != null)
                EndLeft(_startIndex <= 0);
            if (EndRight != null)
                EndRight(_endIndex >= _datas.Count);
            //Debug.Log("startIndex:" + _startIndex + "endIndex:" + _endIndex);
            UpdateCells(_startIndex, _endIndex);
        }

        // 计算哪些Index可以被看到
        protected void GetStartEndIndex(out int startIndex, out int endIndex)
        {
            //滑出可见区域高度 
            float contentOffset = (GetContentLength() - GetViewLength()) * (GetScrollPercent());
            //当前可见区域起始y坐标
            float startOffset = Math.Max(0, contentOffset);
            //当前可见区域结束y坐标
            float endOffset = Math.Min(GetContentLength(), Math.Max(0, contentOffset + GetViewLength()));

            float cellLength = GetCellLength();
            int iStart = Mathf.FloorToInt(startOffset / cellLength);
            int iEnd = Mathf.CeilToInt(endOffset / cellLength);

            startIndex = iStart * GetCellCount();
            endIndex = iEnd * GetCellCount() - 1;

            if (endIndex >= _datas.Count)
                endIndex = _datas.Count - 1;
        }

        private void UpdateCells(int startIndex, int endIndex)
        {
            int length = _showGo.Count;
            for (int i = length - 1; i >= 0; i--)
            {
                ScrollCellItem item = _showGo[i];
                int index = item.GetIndex();
                if (index >= startIndex && index <= endIndex) continue;

                _contains.Remove(index);
                Push(item);
                //Debug.Log("移除:" + index);
            }

            for (int i = startIndex; i <= endIndex; i++)
            {
                if (_contains.Contains(i)) continue;
                ScrollCellItem item = Pop();
                System.Object data = _datas[i];
                item.SetPos(GetPos(i));
                item.SetData(data, i);
                _contains.Add(i);
                //Debug.Log("添加:" + i);
            }
        }

        private Vector2 GetPos(int index)
        {
            Vector2 pos = Vector2.zero;
            int i, j;
            GetIj(index, out i, out j);

            // 物体宽度+间隔*数量-间隔=总的物体所占的宽度, 视野宽度-总物体宽度/2 =起始偏移量
            float viewOffsetX = 0;//(_contentWidth - ((_cellWidth + _widthGap) * _widthCount - _widthGap)) / 2;
            float viewOffsetY = 0;

            float x = i * (_cellWidth + _widthGap) + _cellWidth / 2;
            float y = j * (_cellHeight + _heightGap) + _cellHeight / 2;
            pos.y = -(viewOffsetY + y) + _contentHeight / 2;
            pos.x = (viewOffsetX + x) - _contentWidth / 2;

            return pos;
        }

        private void InitPrefab()
        {
            if (_prefabGo != null)
            {
                RectTransform prefabRect = _prefabGo.GetComponent<RectTransform>();
                _cellWidth = prefabRect.sizeDelta.x;
                _cellHeight = prefabRect.sizeDelta.y;
            }
            else
            {
                _cellWidth = 100;
                _cellHeight = 100;
            }

            _widthCount = Mathf.Max(1, Mathf.FloorToInt((_viewWidth + _widthGap) / (_cellWidth + _widthGap)));
            _heightCount = Mathf.Max(1, Mathf.FloorToInt((_viewHeight + _heightGap) / (_cellHeight + _heightGap)));
        }

        #endregion
    }
}