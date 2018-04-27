using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace SummerEditor
{
    public class FbxAnimationSeohelper
    {

        [MenuItem("Tool/Seo/Animation")]
        public static void SeoAnimation()
        {
            GameObject go = Selection.activeGameObject;
            if (go == null) return;
            FbxAnimationSeo.Seo(go);
        }
    }
}

