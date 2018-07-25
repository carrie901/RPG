using UnityEngine;
using System.Collections;

//=============================================================================
// Author : mashao
// CreateTime : 2018-2-8 15:24:23
// FileName : ViewCnf.cs
//=============================================================================

namespace Summer
{
    /// <summary>
    /// 所有界面的配置文件信息
    /// 涉及到对应的UI,Panle/Dialog 从配置文件读取
    /// </summary>
    public class ViewConst
    {

    }

    public enum E_PanelType
    {
        panel,      // 同类只允许出现一个界面，覆盖的形式
        fxd,        // 固定窗口 
        dialog      // 模式窗口 dialog 可以出现多个，叠加
    }

    public enum E_PanelBgType
    {
        nothing,
        col,            //拥有背景碰撞框
        col_and_img,    //拥有碰撞框并且有背景图片
    }
}
