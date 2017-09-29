using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class CopyAnimationFbxTool
{
    [MenuItem("Tool/Animation/从Fbx中剥离动画文件")]
    public static void CopyClip()
    {
        CopyAnimationFbx.CopyClip();
    }
    [MenuItem("Tool/Animation/从Fbx中剥离动画文件1")]
    public static void CopyClip1()
    {

        string targetPath = Application.dataPath + "/AnimationClip";
        if (!Directory.Exists(targetPath))
        {
            Directory.CreateDirectory(targetPath);
        }
        Object[] SelectionAsset = Selection.GetFiltered(typeof(Object), SelectionMode.Unfiltered);
        Debug.Log(SelectionAsset.Length);
        foreach (Object Asset in SelectionAsset)
        {
            AnimationClip newClip = new AnimationClip();
            EditorUtility.CopySerialized(Asset, newClip);
            AssetDatabase.CreateAsset(newClip, "Assets/AnimationClip/" + Asset.name + ".anim");
        }
        AssetDatabase.Refresh();
    }
}

public class CopyAnimationFbx
{
    public static void CopyClip()
    {
        string path = "D:\\github\\RPG\\Assets\\Raw\\Model\\Zhaoyun\\Animation\\H_ZhaoYun_01_low@attack_01.FBX";

        var datas = AssetDatabase.LoadAssetAtPath(path, typeof(UnityEngine.Object));
        Debug.Log("datas:" + datas);
        /*if (datas.Length == 0)
        {
            Debug.Log(string.Format("Can't find clip in {0}", path));
            return;
        }

        foreach (var data in datas)
        {
            if (!(data is AnimationClip))
                continue;
            AnimationClip fbx_clip = data as AnimationClip;
            AnimationClip new_clip = new AnimationClip();
            AssetDatabase.CreateAsset(new_clip, "Assets/AnimationClip/" + fbx_clip.name + ".anim");
        }*/

        //AnimationClip fbx_clip = AssetDatabase.LoadAssetAtPath(path, typeof(AnimationClip)) as AnimationClip;



        /*string targetPath = Application.dataPath + "/AnimationClip";
        if (!Directory.Exists(targetPath))
        {
            Directory.CreateDirectory(targetPath);
        }
        Object[] SelectionAsset = Selection.GetFiltered(typeof (Object), SelectionMode.Unfiltered);
        Debug.Log(SelectionAsset.Length);
        foreach (Object Asset in SelectionAsset)
        {
            AnimationClip newClip = new AnimationClip();
            EditorUtility.CopySerialized(Asset, newClip);
            AssetDatabase.CreateAsset(newClip, "Assets/AnimationClip/" + Asset.name + ".anim");
        }
        AssetDatabase.Refresh();*/
    }

}