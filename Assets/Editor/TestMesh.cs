
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
//                 佛祖保佑             永无BUG
                                             
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TestMesh  {

    [MenuItem("Assets/GetVerctorNum")]
    static void GetVerctorNum()
    {
        Object[] selectedAsset = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);
        int count = 0;
        int meshCount = 0;
        int triCount = 0;
        for (int i = 0; i < selectedAsset.Length; i++)
        {
            GameObject obj = selectedAsset[i] as GameObject;
            Renderer[] renderers = obj.GetComponentsInChildren<Renderer>();
            MeshFilter[] filters = obj.GetComponentsInChildren<MeshFilter>(true);
            if (filters != null)
            {
                for (int j = 0; j < filters.Length; j++)
                {
                    MeshFilter f = filters[j];
                    count += f.sharedMesh.vertexCount;
                    triCount += f.sharedMesh.triangles.Length / 3;
                    meshCount++;
                }
            }
        }
        Debug.LogWarning("总共Mesh=" + meshCount + "   总共顶点=" + count + "   总共三角形=" + triCount);
    }
}
