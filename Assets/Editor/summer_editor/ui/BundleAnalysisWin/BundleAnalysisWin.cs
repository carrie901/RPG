using UnityEngine;
using System.Collections;
using Summer;
using UnityEditor;

namespace SummerEditor
{
    public class BundleAnalysisWin : EditorWindow
    {
        static float t_width = 1800;
        static float t_height = 800;
        [MenuItem("Tool/Bundle分析界面")]
        public static void ShowWindown()
        {
            BundleAnalysisWin bundleManagerWin = EditorWindow.GetWindow<BundleAnalysisWin>();
            bundleManagerWin.minSize = new Vector2(t_width, t_height);
            bundleManagerWin.maxSize = new Vector2(t_width + 40, t_height + 40);
            bundleManagerWin.Show();
        }

        public EComponent _container;
        public EAssetBundleListPanel _asset_view;
        public EComponent _dep_view;

        public EAssetBundleRefPanel _asset_info_view;
        public EComponent _dep_info_view;
        public BundleAnalysisWin()
        {
            _container = new EComponent(t_width, t_height);
            _container.show_bg = false;
            _container.ResetPosition(t_width / 2, t_height / 2);
            _container.SetBg(0, 0, 0, 1);

            /* // 主资源信息列表
             _asset_view = new EAssetBundleListPanel(1000, 500);
             _container.AddComponent(_asset_view, 10, 10);
             // 单个资源的详细信息
             _asset_info_view = new EAssetBundleRefPanel();
             _container.AddComponent(_asset_info_view);
             _asset_info_view.SetPositionRight(_asset_view);*/


            EToggleBar t = new EToggleBar(50, 30, "AAA");
            _container.AddComponent(t, 30, 30);

            ELabel t1 = new ELabel(30, 20, "你好");
            _container.AddComponent(t1, 10, 10);
            /*_dep_view = new EComponent(730, 260);
            _container.AddComponent(_dep_view);
            _dep_view.SetPositionDown(_asset_view);

            _dep_info_view = new EComponent(230, 260);
            _container.AddComponent(_dep_info_view);
            _dep_info_view.SetPositionRight(_dep_view);*/

            //EToggleBar button = new EToggleBar("测试单选款", 40, 30);
            //button.ResetPositionByLeftUp(100, 200);

            //button.on_change += on_change;
            //_container.AddComponent(button);
        }

        void OnEnable()
        {
            Debug.Log("OnEnable");
        }

        void OnDisable()
        {
            Debug.Log("OnDisable");
            EAbAnalysisEvent.Instance.Clear();
        }

        void OnGUI()
        {
            if (_container != null)
            {
                _container.OnDraw(0, 0);
            }
        }

        public void on_change(EToggleBar togglebar)
        {
            EabAnalysisTool.AnalysisDirectory();
        }
    }

    public enum E_Editor_Analysis
    {
        check_asset,                //查看这个assetbundle引用的资源

    }

    public class EAbAnalysisEvent : TSingleton<EAbAnalysisEvent>
    {
        public EventSet<E_Editor_Analysis, System.Object> _event_set = new EventSet<E_Editor_Analysis, System.Object>();
        public bool RegisterHandler(E_Editor_Analysis key, EventSet<E_Editor_Analysis, System.Object>.EventHandler handler)
        {
            return _event_set.RegisterHandler(key, handler);
        }

        public bool UnRegisterHandler(E_Editor_Analysis key, EventSet<E_Editor_Analysis, System.Object>.EventHandler handler)
        {
            return _event_set.UnRegisterHandler(key, handler);
        }

        public bool RaiseEvent(E_Editor_Analysis key, System.Object param = null, bool b_delay = false)
        {
            return _event_set.RaiseEvent(key, param, b_delay);
        }

        public void Clear()
        {
            _event_set.Clear();
        }
    }


}
