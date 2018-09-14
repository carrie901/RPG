
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

using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace SummerEditor.ProjectBuild
{

    public class BaseProjectBuild : Editor
    {
        /// <summary>
        /// 构建Apk
        /// </summary>
        public static void Build()
        {
            string[] buildScenes = GetBuildSceens();
            string path = string.Format("{0}/{1}.apk", BuildProjectConst.PackageRootPath, BuildProjectConst.PackageName);
            Debug.Log("出包路径:" + path);
            string result = BuildPipeline.BuildPlayer(buildScenes, path, BuildTarget.Android, BuildOptions.None);
            if (result.Length > 0)
                throw new Exception("Build Error:" + result);
        }

        #region private

        private static string[] GetBuildSceens()
        {
            List<string> sceneList = new List<string>();
            int length = EditorBuildSettings.scenes.Length;
            for (int i = 0; i < length; i++)
            {
                if (EditorBuildSettings.scenes[i].enabled)
                    sceneList.Add(EditorBuildSettings.scenes[i].path);
            }
            return sceneList.ToArray();
        }

        #endregion
    }

}
