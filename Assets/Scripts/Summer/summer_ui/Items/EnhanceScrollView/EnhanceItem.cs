
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
    public class EnhanceItem : MonoBehaviour
    {
        // Start index
        private int _curveOffSetIndex;
        public int CurveOffSetIndex
        {
            get { return _curveOffSetIndex; }
            set { _curveOffSetIndex = value; }
        }

        // Runtime real index(Be calculated in runtime)
        private int _curRealIndex = 0;
        public int RealIndex
        {
            get { return _curRealIndex; }
            set { _curRealIndex = value; }
        }

        // Curve center offset 
        private float dCurveCenterOffset = 0.0f;
        public float CenterOffSet
        {
            get { return dCurveCenterOffset; }
            set { dCurveCenterOffset = value; }
        }
        private Transform mTrs;

        private EnhanceScrollView enhanceScrollView;
        public EnhanceScrollView EnhanceScrollView
        {
            set { enhanceScrollView = value; }
        }

        /// <summary>
        /// 列表中的index
        /// </summary>
        public int ListIndex { set; get; }

        public int ListCount { get; set; }

        public int SiblingIndex { get; set; }

        void Awake()
        {
            mTrs = transform;
            //this.GetComponent<Button>().onClick.AddListener(OnClickEnhanceItem);
            // UIEventListener.Get(this.gameObject).onClick = OnClickEnhanceItem ;
            OnAwake();
        }

        void Start()
        {
            OnStart();
        }

        // Update Item's status
        // 1. position
        // 2. scale
        // 3. "depth" is 2D or z Position in 3D to set the front and back item
        public void UpdateScrollViewItems(
            float xValue,
            float depthCurveValue,
            int depthFactor,
            float itemCount,
            float yValue,
            float scaleValue)
        {
            Vector3 targetPos = Vector3.one;
            Vector3 targetScale = Vector3.one;
            // position
            targetPos.x = xValue;
            targetPos.y = yValue;
            mTrs.localPosition = targetPos;

            // Set the "depth" of item
            // targetPos.z = depthValue;
            SetItemDepth(depthCurveValue, depthFactor, itemCount);
            // scale
            targetScale.x = targetScale.y = scaleValue;
            mTrs.localScale = targetScale;
        }

        protected virtual void OnClickEnhanceItem()
        {
            enhanceScrollView.SetHorizontalTargetItemIndex(this);
        }

        protected virtual void OnStart()
        {
        }

        protected virtual void OnAwake()
        {
        }

        protected virtual void SetItemDepth(float depthCurveValue, int depthFactor, float itemCount)
        {
            int newDepth = (int)(depthCurveValue * itemCount);
            SiblingIndex = newDepth;
            transform.SetSiblingIndex(newDepth);
        }

        // Set the item center state
        public virtual void SetSelectState(bool isCenter)
        {

        }

        public virtual void SetData(object data)
        {

        }
    }
}
