using System;
//- ReSharper disable All
namespace Summer
{
    public interface I_ViewAnimation
    {
        /// <summary>
        /// 显示动画
        /// </summary>
        void EnterAnimation(Action on_complete);

        /// <summary>
        /// 隐藏动画
        /// </summary>
        void QuitAnimation(Action on_complete);

        /// <summary>
        /// 重置动画
        /// </summary>
        void ResetAnimation();
    }
}

