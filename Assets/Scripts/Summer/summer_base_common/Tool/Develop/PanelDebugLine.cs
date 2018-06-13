
using UnityEngine;
using UnityEngine.UI;

namespace Summer
{
    [ExecuteInEditMode]
    public class PanelDebugLine : MonoBehaviour
    {
#if UNITY_EDITOR
        protected static int now_frame;
        protected static float now_real_time;
        protected static Vector3[] four_corners = new Vector3[4];

        private void OnDrawGizmos()
        {
            // if (ConfigurationData.Instance.isShowDebugLine == 1)
            {
                Graphic[] graphic = GameObject.FindObjectsOfType<Graphic>();
                foreach (Graphic g in graphic)
                {
                    if (g.raycastTarget)
                    {
                        RectTransform rect_transform = g.transform as RectTransform;
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

}