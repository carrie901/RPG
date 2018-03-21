using System;

//=============================================================================
// Author : mashao
// CreateTime : 2018-1-4 14:55:45
// FileName : UguiHelper.cs
//=============================================================================
namespace Summer
{

    public static class EventActionFuncHelper
    {
        
    }

    //=============================================================================
    // 关于一些Event和Action还有Func的处理的处理
    //=============================================================================
    public static class EventActionFuncExtension
    {
        #region Func Extension

        public static T InvokeGracefully<T>(this Func<T> self_func)
        {
            if (null != self_func)
            {
                return self_func();
            }
            return default(T);
        }

        #endregion

        #region Action

        /// <summary>
        /// Call action
        /// </summary>
        public static bool InvokeGracefully(this Action self_action)
        {
            if (null != self_action)
            {
                self_action();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Call action
        /// </summary>
        public static bool InvokeGracefully<T>(this Action<T> self_action, T t)
        {
            if (null != self_action)
            {
                self_action(t);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Call action
        /// </summary>
        public static bool InvokeGracefully<T, K>(this Action<T, K> self_action, T t, K k)
        {
            if (null != self_action)
            {
                self_action(t, k);
                return true;
            }
            return false;
        }



        #endregion

        #region Event Extension

        /// <summary>
        /// Call delegate 效率有一定的影响
        /// </summary>
        /*public static bool InvokeGracefully(this Delegate self_action, params object[] args)
        {
            if (null != self_action)
            {
                self_action.InvokeGracefully(args);
                return true;
            }
            return false;
        }*/


        #endregion
    }
}

