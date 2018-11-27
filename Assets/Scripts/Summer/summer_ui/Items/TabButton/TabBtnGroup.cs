
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
using Summer;
using UnityEngine;

public class TabBtnGroup : MonoBehaviour {

    #region 属性

    private List<TabBtn> _tabs = new List<TabBtn>();

    #endregion

    #region MONO Override

    // Use this for initialization
    void Start()
    {
        
    }
    
	#endregion
 
    #region Public

    public void NotifyTabOn(TabBtn btn)
    {
        // ValidateTabIsInGroup(); // 有效检测
        int length = _tabs.Count;
        for (var i = 0; i < length; i++)
        {
            if (_tabs[i] == btn)
                continue;

            _tabs[i].IsOn = false;
        }
    }

    public void UnRegisterTab(TabBtn btn)
    {
        if (_tabs.Contains(btn))
            _tabs.Remove(btn);
    }
    
    public void RegisterTab(TabBtn btn)
    {
        if (!_tabs.Contains(btn))
            _tabs.Add(btn);
    }

    #endregion

    #region Private Methods



    #endregion
}
