
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
using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace Summer
{
    public class CameraRenderTexture : MonoBehaviour
    {

        #region 属性

        public Camera _camera;
        public int _sampleWidthHeight = 1024;
        public Texture2D _mainTex;
        #endregion

        #region MONO Override

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        #endregion

        #region Public

        public void RenderCamera()
        {
            RenderTexture rt = RenderTexture.GetTemporary(_sampleWidthHeight, _sampleWidthHeight, 0);
            _camera.targetTexture = rt;
            _camera.Render();
            RenderTexture.active = rt;
            
            _mainTex = new Texture2D(1024, 1024, TextureFormat.ARGB32, false);
            //Debug.Log("rt:" + rt.width + "Height:" + rt.height);
            _mainTex.ReadPixels(new Rect(0, 0, /*rt.width*/1024, /*rt.height*/1024), 0, 0);//把渲染纹理的像素给Texture2D,才能在项目里面使用
            _mainTex.Apply();//记得应用一下，不然很蛋疼
            byte[] bytes = _mainTex.EncodeToPNG();//拿到图片的byte
            File.WriteAllBytes(Application.dataPath + "/Resources/" + Time.frameCount + ".png", bytes);//写入本地
            rt.Release();

            //_camera.targetTexture = null;
            //RenderTexture.active = null;
        }

        #endregion

        #region Private Methods



        #endregion
    }
}