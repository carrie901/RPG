
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

#if UNITY_EDITOR
using UnityEditor;
#endif

public class DebugGraphicLine : MonoBehaviour
{

#if UNITY_EDITOR
    static int now_frame;
    static float now_real_time;
    static Vector3[] four_corners = new Vector3[4];
    void OnDrawGizmos()
    {
        //        return;
        // 避免大量重复绘制（实际应使场景中只有一个脚本实例）
        if (EditorApplication.isPlaying)
        {
            if (!EditorApplication.isPaused)
            {
                if (now_frame == Time.frameCount)
                    return;
                now_frame = Time.frameCount;
            }
            else
            {
                if (Time.realtimeSinceStartup - now_real_time < 0.02f)
                    return;
                now_real_time = Time.realtimeSinceStartup;
            }
        }


        {
            Graphic[] graphics = GameObject.FindObjectsOfType<Graphic>();
            foreach (Graphic g in graphics)
            {
                if (g.raycastTarget)
                {
                    RectTransform rect_transform = g.transform as RectTransform;
                    if (rect_transform == null) continue;
                    rect_transform.GetWorldCorners(four_corners);
                    Gizmos.color = Color.red;
                    for (int i = 0; i < 4; i++)
                        Gizmos.DrawLine(four_corners[i], four_corners[(i + 1) % 4]);
                }
            }
        }
    }
#endif
}
