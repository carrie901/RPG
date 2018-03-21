using UnityEngine.UI;
using System.Collections.Generic;
using Summer;
using UnityEngine;

namespace SummerEditor
{
    public class EditorItemResHelper
    { 

        #region Text

        public static void FindAllText(GameObject select_go, List<ItemRes> res_list)
        {

            res_list.Clear();
            if (select_go == null) return;
            Text[] texts = select_go.GetComponentsInChildren<Text>();


            int length = texts.Length;
            for (int i = 0; i < length; i++)
            {
                ItemRes ir = CollectTextRes(texts[i]);
                res_list.Add(ir);
            }
        }

        #endregion

        #region Res Collect

        public static ItemRes CollectImgRes(Image img)
        {
            if (img == null) return null;
            ItemRes res = new ItemImageRes();
            res.SetTarget(img.gameObject);
            res.raycast_target = img.raycastTarget;
            res._material = img.material;
            res.sound = img.gameObject.GetComponent<ButtonSound>();
            return res;
        }

        public static ItemRes CollectRawImageRes(RawImage img)
        {
            if (img == null) return null;
            ItemRes res = new ItemRawImageRes();
            res.SetTarget(img.gameObject);
            res.raycast_target = img.raycastTarget;
            res._material = img.material;
            res.sound = img.gameObject.GetComponent<ButtonSound>();
            return res;
        }

        public static ItemRes CollectTextRes(Text text)
        {
            if (text == null) return null;
            ItemRes res = new ItemTextRes();
            res.SetTarget(text.gameObject);
            res.raycast_target = text.raycastTarget;
            res._material = text.material;
            res.sound = text.gameObject.GetComponent<ButtonSound>();
            return res;
        }

        #endregion

    }
}

