using UnityEngine;
using System.Collections;

//=============================================================================
// Author : mashao
// CreateTime : 2018-3-22 19:47:49
// FileName : UnityLayerConst.cs
//=============================================================================

namespace Summer
{
    public class UnityLayerConst
    {
        public static UnityLayerConst Instance = new UnityLayerConst();


        #region NavLayer

        public const string LAYER_NAV = "NavLayer";
        public int layer_nav_index = 0;
        public int layer_nav_mask = 0;

        #endregion

        #region DefaultLayer

        public const string LAYER_DEFAULT = "Default";
        public int layer_defalut_index = 0;
        public int layer_defalut_mask = 0;

        #endregion

        #region

        public const string LAYER_SCENE_OBJECT = "sceneobject";
        public int layer_sceneobject_index = 0;
        public int layer_sceneobject_mask = 0;

        #endregion

        #region


        #endregion

        public UnityLayerConst()
        {
            layer_nav_index = LayerMask.NameToLayer(LAYER_NAV);
            layer_nav_mask = 1 << layer_nav_index;

            layer_defalut_index = LayerMask.NameToLayer(LAYER_DEFAULT);
            layer_defalut_mask = 1 << layer_defalut_index;

            layer_sceneobject_index = LayerMask.NameToLayer(LAYER_SCENE_OBJECT);
            layer_sceneobject_mask = 1 << layer_sceneobject_index;
        }
    }
}
