using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Summer
{
    //非运行时也触发效果  
    [ExecuteInEditMode]
    //提供一个后处理的基类，主要功能在于直接通过Inspector面板拖入shader，生成shader对应的材质  
    [RequireComponent(typeof(Camera))]
    public class CameraPostEffectBase : MonoBehaviour
    {

        //Inspector面板上直接拖入  
        public Shader shader = null;
        private Material _material = null;
        public Material CMainMaterial
        {
            get
            {
                if (_material == null)
                    _material = GenerateMaterial(shader);
                return _material;
            }
        }

        //根据shader创建用于屏幕特效的材质  
        protected Material GenerateMaterial(Shader tshader)
        {
            if (tshader == null)
                return null;
            //需要判断shader是否支持  
            if (tshader.isSupported == false)
                return null;
            Material material = new Material(tshader);
            material.hideFlags = HideFlags.DontSave;
            if (material)
                return material;
            return null;
        }

    }
}

