
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
using Summer;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
namespace SummerEditor
{
    /// <summary>
    /// 针对UI.Prefab上的图集，自动清除他们的Sprite，然后挂载脚本
    /// </summary>
    public class UpdateUiSprite
    {

        #region 属性

        public static string BuiltPath = "Resources";

        public static string[] _prefabPath =
        {
            "Assets/Prefabs/GUI"
        }; // 指定的目录下的prefab

        public static string[] BigImagePath =
        {
            "Assets/UIResource/BigImage/LoadBg"
        }; // 指定路径的大图

        public static string[] AlatsPath =
        {

        }; // 图集纹理地址


        #endregion

        #region Public

        public static void Excute()
        {
            // 1.收集有效的.prefab
            string[] pfbGuids = AssetDatabase.FindAssets("t:prefab", _prefabPath);
            // 2.依次处理所有的prefab
            var len = pfbGuids.Length;
            for (int i = 0; i < len; i++)
            {
                string pfbPath = AssetDatabase.GUIDToAssetPath(pfbGuids[i]);
                // 3.加载所有的Image/Selectable
                ExcuteSinglePfb(pfbPath);
            }
            AssetDatabase.Refresh();
            AssetDatabase.SaveAssets();
        }

        #endregion

        #region Private Methods

        private static void ExcuteSinglePfb(string pfbPath)
        {
            AssetImporter assetImp = AssetImporter.GetAtPath(pfbPath);

            GameObject go = AssetDatabase.LoadAssetAtPath<GameObject>(pfbPath);
            Image[] imgs = go.GetComponentsInChildren<Image>(true);
            int len = imgs.Length;
            for (int i = 0; i < len; i++)
            {
                ExcuteSprite(imgs[i], pfbPath);
            }

            Selectable[] selectables = go.GetComponentsInChildren<Selectable>(true);
            len = selectables.Length;
            for (int i = 0; i < len; i++)
            {
                ExcuteSelectable(selectables[i], pfbPath);
            }
            EditorUtility.SetDirty(go);
            EditorUtility.SetDirty(assetImp);
            AssetDatabase.ImportAsset(pfbPath);
        }

        private static void ExcuteSelectable(Selectable selectable, string pfbPath)
        {
            if (selectable.transition != Selectable.Transition.SpriteSwap) return;
            // 强制纹理为图集/或者大图 不然直接提示错误
            string highlightedPath = AssetDatabase.GetAssetPath(selectable.spriteState.highlightedSprite);
            string pressedPath = AssetDatabase.GetAssetPath(selectable.spriteState.pressedSprite);
            string disabledPath = AssetDatabase.GetAssetPath(selectable.spriteState.disabledSprite);

            if (IsInAltas(highlightedPath) && IsInAltas(pressedPath) && IsInAltas(disabledPath))
            {
                ExcuteSelectableAltas(selectable, highlightedPath, pressedPath, disabledPath);
            }
            else if (IsInBigTex(highlightedPath) && IsInBigTex(pressedPath) && IsInBigTex(disabledPath))
            {
                ExcuteSelectableBig(selectable, highlightedPath, pressedPath, disabledPath);
            }
            else
            {
                Debug.LogErrorFormat("prefab:[{0}],selectable:[{1}]没有对这种类型的的策略", pfbPath, selectable.name);
            }
        }

        #region 处理Sprite 图集/大图/内置资源

        // 处理Sprite
        private static void ExcuteSprite(Image img, string pfbPath)
        {
            string imgPath = string.Empty;
            if (img.sprite != null) imgPath = AssetDatabase.GetAssetPath(img.sprite);

            // 指向的纹理是图集
            if (IsInAltas(imgPath))
            {
                ExcuteAltas(img, imgPath);
            }
            else if (IsInBigTex(imgPath))
            {
                ExcuteBigImg(img, imgPath);
            }
            else if (IsInBuilt(imgPath))
            {
                Debug.LogErrorFormat("prefab:[{0}],img:[{1}]使用内置资源:", pfbPath, img.name);
            }
            else
            {
                Debug.LogErrorFormat("prefab:[{0}],img:[{1}]没有对这种类型的Img的策略", pfbPath, img.name);
            }
        }

        // 处理Sprite中的图集
        private static void ExcuteAltas(Image img, string imgPath)
        {
            // 图集的处理方式
            SpriteAtlasLoader loader = GameObjectHelper.AddComponent<SpriteAtlasLoader>(img.gameObject);
            RemoveOtherCs(img.gameObject, TYPE_SPRITE);
            img.sprite = null;
            loader._initComplete = false;
            loader._resPath = imgPath;
            loader._spriteTag = GetSpriteTag(imgPath);
        }

        // 处理Sprite中的大图
        private static void ExcuteBigImg(Image img, string imgPath)
        {
            SpriteBigLoader loader = GameObjectHelper.AddComponent<SpriteBigLoader>(img.gameObject);
            RemoveOtherCs(img.gameObject, TYPE_BIG_SPRITE);
            img.sprite = null;
            loader._isComplete = false;
            loader._resPath = imgPath;
        }

        // 图片属于纹理中
        private static bool IsInAltas(string imgPath)
        {
            int len = AlatsPath.Length;
            for (int i = 0; i < len; i++)
            {
                if (imgPath.Contains(AlatsPath[i]))
                {
                    return true;
                }
            }
            return false;
        }

        // 图片属于大图
        private static bool IsInBigTex(string imgPath)
        {
            int len = BigImagePath.Length;
            for (int i = 0; i < len; i++)
            {
                if (imgPath.Contains(BigImagePath[i]))
                {
                    return true;
                }
            }
            return false;
        }

        // 图片属于内置资源
        private static bool IsInBuilt(string imgPath)
        {
            if (imgPath.StartsWith(BuiltPath))
                return true;
            return false;
        }

        // 得到Sprite的SpriteTag
        private static string GetSpriteTag(string imgPath)
        {
            TextureImporter texImporter = AssetImporter.GetAtPath(imgPath) as TextureImporter;
            Debug.AssertFormat(texImporter != null, "指定路径:[{0}]不是纹理");

            string spriteTag = texImporter == null ? String.Empty : texImporter.spritePackingTag;
            Debug.AssertFormat(string.IsNullOrEmpty(spriteTag), "纹理:[{0}]没有设置Tag", imgPath);

            return spriteTag;
        }

        #endregion

        #region 处理Selectable

        private static void ExcuteSelectableAltas(Selectable selectable, string highlightedPath, string pressedPath, string disabledPath)
        {
            SpriteSelectableAltasLoader loader = GameObjectHelper.AddComponent<SpriteSelectableAltasLoader>(selectable.gameObject);
            RemoveOtherCs(selectable.gameObject, TYPE_SELECTABLE_SPRITE);

            SpriteState state = new SpriteState();
            state.highlightedSprite = null;
            state.pressedSprite = null;
            state.disabledSprite = null;
            selectable.spriteState = state;

            loader._initComplete = false;
            loader._highlightedSpritePath = highlightedPath;
            loader._pressedSpritePath = pressedPath;
            loader._disabledSpritePath = disabledPath;
        }
        private static void ExcuteSelectableBig(Selectable selectable, string highlightedPath, string pressedPath, string disabledPath)
        {
            SpriteSelectableBigLoader loader = GameObjectHelper.AddComponent<SpriteSelectableBigLoader>(selectable.gameObject);
            RemoveOtherCs(selectable.gameObject, TYPE_SELECTABLE_BIT);

            SpriteState state = new SpriteState();
            state.highlightedSprite = null;
            state.pressedSprite = null;
            state.disabledSprite = null;
            selectable.spriteState = state;

            loader._initComplete = false;
            loader._highlightedSpritePath = highlightedPath;
            loader._pressedSpritePath = pressedPath;
            loader._disabledSpritePath = disabledPath;
        }
        #endregion

        #region Other
        public const int TYPE_BIG_SPRITE = 1;           // 大图
        public const int TYPE_SPRITE = 2;               // 图集
        public const int TYPE_SELECTABLE_SPRITE = 3;    // 按钮
        public const int TYPE_SELECTABLE_BIT = 4;       // 按钮
        public static void RemoveOtherCs(GameObject go, int type)
        {
            SpriteBigLoader bigLoader = go.GetComponent<SpriteBigLoader>();
            SpriteAtlasLoader altasLoader = go.GetComponent<SpriteAtlasLoader>();
            SpriteSelectableAltasLoader selectableLoader = go.GetComponent<SpriteSelectableAltasLoader>();
            SpriteSelectableBigLoader selectableBigLoader = go.GetComponent<SpriteSelectableBigLoader>();

            if (bigLoader != null && type != TYPE_BIG_SPRITE)
                UnityEngine.Object.DestroyImmediate(bigLoader);

            if (altasLoader != null && type != TYPE_SPRITE)
                UnityEngine.Object.DestroyImmediate(altasLoader);

            if (selectableLoader != null && type != TYPE_SELECTABLE_SPRITE)
                UnityEngine.Object.DestroyImmediate(selectableLoader);

            if (selectableBigLoader != null && type != TYPE_SELECTABLE_BIT)
                UnityEngine.Object.DestroyImmediate(selectableBigLoader);
        }
        #endregion

        #endregion
    }
}
