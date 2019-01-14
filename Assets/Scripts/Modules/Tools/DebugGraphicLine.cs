
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
using UnityEngine.UI;
using System.Collections.Generic;
#if UNITY_EDITOR
//using UnityEditor;
#endif

namespace Summer
{
    public class DebugGraphicLine : MonoBehaviour
    {

#if UNITY_EDITOR
        static int NowFrame;
        static float NowRealTime;
        static readonly Vector3[] FourCorners = new Vector3[4];
        public List<GameObject> _rays;
        void OnDrawGizmos()
        {
            //        return;
            // 避免大量重复绘制（实际应使场景中只有一个脚本实例）
            /*if (EditorApplication.isPlaying)
            {
                if (!EditorApplication.isPaused)
                {
                    if (NowFrame == Time.frameCount)
                        return;
                    NowFrame = Time.frameCount;
                }
                else
                {
                    if (Time.realtimeSinceStartup - NowRealTime < 0.02f)
                        return;
                    NowRealTime = Time.realtimeSinceStartup;
                }
            }*/


            {
                Graphic[] graphics = GameObject.FindObjectsOfType<Graphic>();

                _rays.Clear();
                foreach (Graphic g in graphics)
                {
                    if (!g.raycastTarget) continue;
                    RectTransform rectTransform = g.transform as RectTransform;
                    if (rectTransform == null) continue;
                    rectTransform.GetWorldCorners(FourCorners);
                    Gizmos.color = Color.red;
                    for (int i = 0; i < 4; i++)
                        Gizmos.DrawLine(FourCorners[i], FourCorners[(i + 1) % 4]);
                    _rays.Add(g.gameObject);
                }
            }
        }
#endif
    }

}

