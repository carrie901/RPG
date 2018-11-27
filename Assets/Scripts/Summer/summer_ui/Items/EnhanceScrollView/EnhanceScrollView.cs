
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
    public class EnhanceScrollView : MonoBehaviour
    {
        #region 属性

        /// <summary>
        /// Control the "depth"'s curve(In 3d version just the Z value, in 2D UI you can use the depth(NGUI))
        /// NOTE:
        /// 1. In NGUI set the widget's depth may cause performance problem
        /// 2. If you use 3D UI just set the Item's Z position
        /// </summary>
        public AnimationCurve _depthCurve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(0.5f, 1), new Keyframe(1, 0));
        public AnimationCurve _scaleCurve;                      // Control the item's scale curve     
        public AnimationCurve _positionCurve;                   // Control the position curve
        public bool IsVertical = false;
        // 
        private int _startCenterIndex;                          // The start center index


        [SerializeField]
        private float _cellWidth = 10f;                         // Offset width between item

        public float CellWith
        {
            get { return _cellWidth; }
            set { _cellWidth = value; }
        }

        private float _totalHorizontalWidth = 500.0f;
        private float _yFixedPositionValue = 46.0f;             // vertical fixed position value 

        // Lerp duration
        public float lerpDuration = 0.2f;
        private float _mCurrentDuration = 0.0f;
        private int _mCenterIndex = 0;
        public bool _enableLerpTween = true;

        // center and preCentered item
        private EnhanceItem _curCenterItem;
        private EnhanceItem _preCenterItem;

        // if we can change the target item
        private bool _canChangeItem = true;
        private float _dFactor = 0.2f;

        // originHorizontalValue Lerp to horizontalTargetValue
        private float _originHorizontalValue = 0.1f;
        public float _curHorizontalValue = 0.5f;

        // "depth" factor (2d widget depth or 3d Z value)
        private int _depthFactor = 5;

        // targets enhance item in scroll view
        public List<EnhanceItem> _listEnhanceItems;
        // sort to get right index
        private List<EnhanceItem> _listSortedItems = new List<EnhanceItem>();

        public GameObject _itemObj;

        [Header("最多显示3个,噩梦关卡专用")]
        [SerializeField]
        private bool IsShowThree = false;
        public float _factor = 0.001f;
        private RectTransform _rectTrans;

        #endregion

        void Awake()
        {
            _rectTrans = GetComponent<RectTransform>();
        }

        void Update()
        {
            if (!_enableLerpTween) return;
            TweenViewToTarget();
        }

        public void InitData<T>(List<T> dataList)
        {
            LogManager.Assert(_itemObj != null, "预设不能为空");
            if (_itemObj == null) return;
            if (_listEnhanceItems.Count <= 0)
            {
                int num = dataList.Count;
                for (int i = 0; i < num; i++)
                {
                    GameObject go = Instantiate(_itemObj);
                    go.transform.SetParent(transform, false);
                    EnhanceItem item = go.GetComponent<EnhanceItem>();

                    item.EnhanceScrollView = this;
                    item.ListIndex = i;
                    item.ListCount = num;
                    _listEnhanceItems.Add(item);

                    if (IsVertical)
                        item.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, -90));
                }
            }

            for (int i = 0; i < _listEnhanceItems.Count; i++)
            {
                _listEnhanceItems[i].SetData(dataList[i]);
            }
            InitView();
        }

        public void SetCenterIndex(int index)
        {
            _startCenterIndex = index;
        }

        public void DisableLerpTween()
        {
            _enableLerpTween = false;
        }

        public void SetHorizontalTargetItemIndex(int targetIndex)
        {
            if (targetIndex < 0 || targetIndex >= _listEnhanceItems.Count) return;
            SetHorizontalTargetItemIndex(_listEnhanceItems[targetIndex]);
        }

        public void SetHorizontalTargetItemIndex(EnhanceItem selectItem)
        {
            if (!_canChangeItem)
                return;

            if (_curCenterItem == selectItem)
                return;

            _canChangeItem = false;
            _preCenterItem = _curCenterItem;
            _curCenterItem = selectItem;

            // calculate the direction of moving
            float centerXValue = _positionCurve.Evaluate(0.5f) * _totalHorizontalWidth;
            bool isRight = selectItem.transform.localPosition.x > centerXValue;

            // calculate the offset * dFactor
            int moveIndexCount = GetMoveCurveFactorCount(_preCenterItem, selectItem);
            float dvalue = isRight ? -_dFactor * moveIndexCount : _dFactor * moveIndexCount;
            float originValue = _curHorizontalValue;
            LerpTweenToTarget(originValue, _curHorizontalValue + dvalue, true);
        }

        // On Drag Move
        public void OnDragEnhanceViewMove(Vector2 delta)
        {
            // In developing
            if (IsVertical)
            {
                if (Mathf.Abs(delta.y) > 0.0f)
                {
                    _curHorizontalValue += delta.y * _factor;
                    LerpTweenToTarget(0.0f, _curHorizontalValue, false);
                }
            }
            else
            {
                if (Mathf.Abs(delta.x) > 0.0f)
                {
                    _curHorizontalValue += delta.x * _factor;
                    LerpTweenToTarget(0.0f, _curHorizontalValue, false);
                }
            }
        }

        // On Drag End
        public void OnDragEnhanceViewEnd()
        {
            // find closed item to be centered
            int closestIndex = 0;
            float value = (_curHorizontalValue - (int)_curHorizontalValue);
            float min = float.MaxValue;
            float tmp = 0.5f * (_curHorizontalValue < 0 ? -1 : 1);
            for (int i = 0; i < _listEnhanceItems.Count; i++)
            {
                float dis = Mathf.Abs(Mathf.Abs(value) - Mathf.Abs((tmp - _listEnhanceItems[i].CenterOffSet)));
                if (dis < min)
                {
                    closestIndex = i;
                    min = dis;
                }
            }
            _originHorizontalValue = _curHorizontalValue;
            float target = ((int)_curHorizontalValue + (tmp - _listEnhanceItems[closestIndex].CenterOffSet));
            _preCenterItem = _curCenterItem;
            _curCenterItem = _listEnhanceItems[closestIndex];
            LerpTweenToTarget(_originHorizontalValue, target, true);
            _canChangeItem = false;
        }

        public void Dispose()
        {
            for (int i = 0; i < _listEnhanceItems.Count; i++)
            {
                Destroy(_listEnhanceItems[i].gameObject);
                _listEnhanceItems[i] = null;
            }
            _listEnhanceItems.Clear();
        }

        #region private 

        private void InitView()
        {
            _canChangeItem = true;
            int count = _listEnhanceItems.Count;
            if (count <= 0)
                return;
            _dFactor = (Mathf.RoundToInt((1f / count) * 10000f)) * 0.0001f;
            _mCenterIndex = count / 2;
            if (count % 2 == 0)
                _mCenterIndex = count / 2 - 1;
            int index = 0;
            for (int i = count - 1; i >= 0; i--)
            {
                _listEnhanceItems[i].CurveOffSetIndex = i;
                if (count != 2)
                    _listEnhanceItems[i].CenterOffSet = _dFactor * (_mCenterIndex - index);
                else//只有2个item时,初始化时第二个item会显示在左边，是因为AnimationCurve的取值区间是[0,1],导致偏移量为1时取值0，[0,1)不知道怎么实现╮(╯▽╰)╭
                {   //当前的曲线的走向看噩梦关卡/日常活动里的PositionCurve，改成其他曲线时这里可能有坑...
                    _listEnhanceItems[1].CenterOffSet = 0.49f;
                    _listEnhanceItems[0].CenterOffSet = 0.01f;
                }
                _listEnhanceItems[i].SetSelectState(false);
                GameObject obj = _listEnhanceItems[i].gameObject;


                UDragEnhanceView script = obj.GetComponent<UDragEnhanceView>() ?? obj.AddComponent<UDragEnhanceView>();
                if (script != null)
                    script.SetScrollView(this);
                index++;
            }

            // set the center item with startCenterIndex
            if (_startCenterIndex < 0 || _startCenterIndex >= count)
            {
                LogManager.Error("## startCenterIndex < 0 || startCenterIndex >= listEnhanceItems.Count  out of index ##");
                _startCenterIndex = _mCenterIndex;

            }

            // sorted items
            _listSortedItems = new List<EnhanceItem>(_listEnhanceItems.ToArray());
            _totalHorizontalWidth = _cellWidth * count;
            _curCenterItem = _listEnhanceItems[_startCenterIndex];
            _curHorizontalValue = 0.5f - _curCenterItem.CenterOffSet;
            LerpTweenToTarget(0f, _curHorizontalValue, false);

            if (IsVertical)
            {
                _rectTrans.anchoredPosition = new Vector2(0, -_totalHorizontalWidth / 2);
                _rectTrans.localRotation = Quaternion.Euler(new Vector3(0, 0, 90));

            }
            else
            {
                _rectTrans.anchoredPosition = new Vector2(-_totalHorizontalWidth / 2, 0);
                _rectTrans.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
            }
        }

        private void TweenViewToTarget()
        {
            _mCurrentDuration += Time.deltaTime;
            if (_mCurrentDuration > lerpDuration)
                _mCurrentDuration = lerpDuration;

            float percent = _mCurrentDuration / lerpDuration;
            float value = Mathf.Lerp(_originHorizontalValue, _curHorizontalValue, percent);
            UpdateEnhanceScrollView(value);
            if (_mCurrentDuration >= lerpDuration)
            {
                _canChangeItem = true;
                _enableLerpTween = false;
                OnTweenOver();
            }
        }

        private void LerpTweenToTarget(float originValue, float targetValue, bool needTween = false)
        {
            if (!needTween)
            {
                SortEnhanceItem();
                _originHorizontalValue = targetValue;
                UpdateEnhanceScrollView(targetValue);
                OnTweenOver();
            }
            else
            {
                _originHorizontalValue = originValue;
                _curHorizontalValue = targetValue;
                _mCurrentDuration = 0.0f;
            }
            _enableLerpTween = needTween;
        }

        private void OnTweenOver()
        {
            if (_preCenterItem != null)
            {
                _preCenterItem.SetSelectState(false);

            }
            if (_curCenterItem != null)
            {
                _curCenterItem.SetSelectState(true);
            }
        }

        // Update EnhanceItem state with curve fTime value
        private void UpdateEnhanceScrollView(float fValue)
        {
            for (int i = 0; i < _listEnhanceItems.Count; i++)
            {
                EnhanceItem itemScript = _listEnhanceItems[i];
                float xValue = GetXPosValue(fValue, itemScript.CenterOffSet);
                float scaleValue = GetScaleValue(fValue, itemScript.CenterOffSet);
                float depthCurveValue = _depthCurve.Evaluate(fValue + itemScript.CenterOffSet);
                itemScript.UpdateScrollViewItems(xValue, depthCurveValue, _depthFactor, _listEnhanceItems.Count,
                    _yFixedPositionValue, scaleValue);
            }
            // 只处理了有4个的情况
            if (IsShowThree)
            {
                if (_listEnhanceItems.Count > 3)
                {
                    _listEnhanceItems.Sort((a, b) => a.SiblingIndex - b.SiblingIndex);
                    for (int i = 0; i < _listEnhanceItems.Count; i++)
                    {
                        if (i == 0)
                            _listEnhanceItems[i].gameObject.SetActive(false);
                        else
                            _listEnhanceItems[i].gameObject.SetActive(true);
                    }
                }
            }
        }

        // sort item with X so we can know how much distance we need to move the timeLine(curve time line)
        private int SortPosition(EnhanceItem a, EnhanceItem b) { return a.transform.localPosition.x.CompareTo(b.transform.localPosition.x); }
        private void SortEnhanceItem()
        {
            _listSortedItems.Sort(SortPosition);
            for (int i = _listSortedItems.Count - 1; i >= 0; i--)
                _listSortedItems[i].RealIndex = i;
        }

        #region Get Scale/Position/Factor

        // Get the evaluate value to set item's scale
        private float GetScaleValue(float sliderValue, float added)
        {
            float scaleValue = _scaleCurve.Evaluate(sliderValue + added);
            return scaleValue;
        }

        // Get the X value set the Item's position
        private float GetXPosValue(float sliderValue, float added)
        {
            float evaluateValue = _positionCurve.Evaluate(sliderValue + added) * _totalHorizontalWidth;
            return evaluateValue;
        }

        private int GetMoveCurveFactorCount(EnhanceItem preCenterItem, EnhanceItem newCenterItem)
        {
            SortEnhanceItem();
            int factorCount = Mathf.Abs(newCenterItem.RealIndex) - Mathf.Abs(preCenterItem.RealIndex);
            return Mathf.Abs(factorCount);
        }


        #endregion

        #endregion
    }
}