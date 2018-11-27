
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

namespace Summer
{
    /// <summary>
    /// 页签Toogle
    /// </summary>
    public class TabBtn : MonoBehaviour, IPointerClickHandler
    {

        #region 属性

        public const string NORMAL = "Normal";
        public const string SELECT = "Select";
        public delegate void VoidDelegate(GameObject obj);          // 只需传入目标监听UI对象即可
        public event VoidDelegate OnClicked;
        public delegate bool BoolDelegate(GameObject obj);          // 前置响应
        public event BoolDelegate PreClicked;

        [Header("正常状态,默认寻找Name=Normal的GameObject")]
        public GameObject _normal;                                  // 正常状态
        [Header("选中状态,默认寻找Name=Select的GameObject")]
        public GameObject _select;                                  // 选中态
        [Header("默认选中状态")]
        public bool _selectFalg;
        [Header("所属集团")]
        public TabBtnGroup _group;                                  // 所属集团

        public bool IsOn
        {
            get { return _selectFalg; }
            set
            {
                _selectFalg = value;
                ResetState();
            }
        }
        #endregion

        #region MONO Override

        void Awake()
        {
            Init();
        }

        #endregion

        #region Public

        public void OnPointerClick(PointerEventData eventData)
        {
            if (PreClicked != null && PreClicked(gameObject))
                return;

            if (OnClicked != null)
                OnClicked(gameObject);
            _selectFalg = !_selectFalg;
            ResetState();
            if (_group != null)
                _group.NotifyTabOn(this);
        }

        #endregion

        #region Private Methods

        private void Init()
        {
            if (_normal == null)
            {
                _normal = gameObject.transform.Find(NORMAL).gameObject;
            }
            if (_select == null)
            {
                _select = gameObject.transform.Find(SELECT).gameObject;
            }
            ResetState();
            if (_group != null)
                _group.RegisterTab(this);
        }

        private void ResetState()
        {
            if (_normal != null && _select != null)
            {
                _normal.SetActive(!_selectFalg);
                _select.SetActive(_selectFalg);
            }
        }
        #endregion
    }

}

