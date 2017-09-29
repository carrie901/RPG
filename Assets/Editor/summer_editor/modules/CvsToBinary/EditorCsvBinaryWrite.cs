using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class EditorCsvTool
{
    public static string path_write = Application.streamingAssetsPath + "/data/";
    [MenuItem("Tool/文本工具/打包成bytes", false, 4)]
    public static void BuildAssetBundle()
    {
        StaticCnf.cnf_map.Clear();
        StaticCnfLoader.LoadAllCsvFile();
        StaticCnfLoader.WriteAllCsvBinary(path_write);
        EditorUtility.DisplayDialog("打包成二进制流结束", "头痛", "确定");
    }
}
