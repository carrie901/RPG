
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

namespace SummerEditor
{

    public class AbWin : EComponent
    {
        public ToolBarPanel _toolbar;
        public Dictionary<int, Action> _onClickAction;
        public AbWin(float width, float height) : base(width, height)
        {
        }

        public void OnSelect(EToolBar bar)
        {
            int selectIndex = bar.SelectIndex;
            if (_onClickAction.ContainsKey((selectIndex)))
                _onClickAction[selectIndex]();
        }

        public void OnBuildAssetBundle()
        {
            AssetBundleMenuE.BuildStep();
        }

        public void OnShowAbReport()
        {
            AssetBundleReportWindow.Init();
        }

        public void _init()
        {
            _toolbar = new ToolBarPanel(new List<string>() { "打包资源", "查看资源报告", "生成CRC热更新文件" }, Ew, Eh - 10);
            _toolbar.OnSelect += OnSelect;
            _onClickAction.Add(0, OnBuildAssetBundle);
            _onClickAction.Add(1, OnShowAbReport);
            AddComponent(_toolbar, 0, 5);
            //_abBuildBtn=new EButton("打包资源",150);
            //_abReportBtn=new EButton("查看Ab资源报告:");
        }
    }
}

