using Summer;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;
namespace SummerEditor
{
    public class SpriteSetE
    {
        public static string ui_prefab = "Assets/TestRes/";

        public static Dictionary<string, ESpriteCnf01> _sprite_infos = new Dictionary<string, ESpriteCnf01>();

        [MenuItem("Tools/待定/添加脚本")]
        public static void CheckSprite()
        {
            List<GameObject> pfb_gos = new List<GameObject>();
            List<string> assets_prefab_path = EPathHelper.GetAssetsPath(ui_prefab, true, "*.prefab");

            // 统计类型是Spite的引用个数
            // 遍历每一个prefab 得到Image 和Button类型的脚本
            for (int i = 0; i < assets_prefab_path.Count; i++)
            {
                string pfb_path = assets_prefab_path[i];
                GameObject go = AssetDatabase.LoadAssetAtPath<GameObject>(pfb_path);
                pfb_gos.Add(go);
                UnityEngine.Object[] objs = EditorUtility.CollectDependencies(new UnityEngine.Object[] { go });

                // 分析Sprite
                for (int k = 0; k < objs.Length; k++)
                {
                    FindSprite(objs[k]);
                }
            }

            for (int i = 0; i < pfb_gos.Count; i++)
            {
                GameObject go = pfb_gos[i];
                Image[] imgs = go.GetComponentsInChildren<Image>();

                for (int k = 0; k < imgs.Length; k++)
                {
                    ExcuteSprite(imgs[k]);
                }

                Selectable[] selectables = go.GetComponentsInChildren<Selectable>();
                for (int k = 0; k < selectables.Length; k++)
                {
                    ExcuteSelectable(selectables[k]);
                }
            }

            AssetDatabase.Refresh();
            AssetDatabase.SaveAssets();

            // 如果是Image就添加脚本，并且加载她的meta文件，确定是是ui还是big
        }

        public static void FindSprite(Object obj)
        {
            if (!(obj is Sprite)) return;

            string asset_path = AssetDatabase.GetAssetPath(obj);
            bool result = string.IsNullOrEmpty(asset_path);
            Debug.AssertFormat(!result, "obj:[{0}]路径为空", obj.name);

            if (result) return;


            if (_sprite_infos.ContainsKey(asset_path))
            {
                _sprite_infos[asset_path].ref_count++;
            }
            else
            {
                ESpriteCnf01 sprite_cnf = new ESpriteCnf01();
                sprite_cnf.SetAssetPath(asset_path);
                sprite_cnf.ref_count = 1;
                _sprite_infos.Add(asset_path, sprite_cnf);
            }
        }

        public static void ExcuteSprite(Image img)
        {
            // TODO BUG 如果一个脚本引用的纹理从有tag会变成没有tag 就会有bug
            Debug.AssertFormat(img.sprite != null, "[{0}]Image的Sprite为空", img.name);
            string asset_path = AssetDatabase.GetAssetPath(img.sprite);

            ESpriteCnf01 sprite_info = _sprite_infos[asset_path];
            if (sprite_info.is_texture)
            {
                RemoveOtherCs(img.gameObject, type_big_sprite);

                SpriteBigAutoLoader loader = img.GetComponent<SpriteBigAutoLoader>();
                if (loader == null)
                    loader = img.gameObject.AddComponent<SpriteBigAutoLoader>();

                loader.img = img;
                loader.sprite_tag = sprite_info.packingtag;
                loader.res_path = EPathHelper.RemoveAssetsAndSuffixforPath(sprite_info.asset_path_without_asset);
                img.sprite = null;
            }
            else
            {
                RemoveOtherCs(img.gameObject, type_sprite);
                SpriteAutoLoader loader = img.GetComponent<SpriteAutoLoader>();
                if (loader == null)
                    loader = img.gameObject.AddComponent<SpriteAutoLoader>();

                loader.img = img;
                loader.sprite_tag = sprite_info.packingtag;
                loader.res_path = sprite_info.asset_path_without_asset;
                img.sprite = null;
            }
        }

        // 指定Image 这块可能是有问题的，需要验证
        public static void ExcuteSelectable(Selectable selectable)
        {
            if (selectable == null) return;
            //Debug.AssertFormat(img.sprite != null, "[{0}]Image的Sprite为空", img.name);
            SpriteState sprite_state = selectable.spriteState;


            SpriteSelectableAutoLoader loader = selectable.GetComponent<SpriteSelectableAutoLoader>();
            if (loader == null)
                loader = selectable.gameObject.AddComponent<SpriteSelectableAutoLoader>();

            loader._selectable = selectable;
            if (sprite_state.disabledSprite != null)
            {
                string asset_path = AssetDatabase.GetAssetPath(sprite_state.disabledSprite);
                ESpriteCnf01 sprite_info = _sprite_infos[asset_path];

                loader.disabled_sprite_path = sprite_info.asset_path_without_asset;
                loader.disabled_sprite_tag = sprite_info.packingtag;
            }


            if (sprite_state.highlightedSprite != null)
            {
                string asset_path = AssetDatabase.GetAssetPath(sprite_state.highlightedSprite);
                ESpriteCnf01 sprite_info = _sprite_infos[asset_path];

                loader.highlighted_sprite_path = sprite_info.asset_path_without_asset;
                loader.highlighted_sprite_tag = sprite_info.packingtag;
            }

            if (sprite_state.pressedSprite != null)
            {
                string asset_path = AssetDatabase.GetAssetPath(sprite_state.pressedSprite);
                ESpriteCnf01 sprite_info = _sprite_infos[asset_path];

                loader.pressed_sprite_path = sprite_info.asset_path_without_asset;
                loader.pressed_sprite_tag = sprite_info.packingtag;
            }

        }

        public static int type_big_sprite = 1;
        public static int type_sprite = 2;
        public static int type_selectable = 3; 
        public static void RemoveOtherCs(GameObject go, int type)
        {
            SpriteBigAutoLoader big_loader = go.GetComponent<SpriteBigAutoLoader>();
            SpriteAutoLoader normal_loader = go.GetComponent<SpriteAutoLoader>();
            SpriteSelectableAutoLoader selectable_loader = go.GetComponent<SpriteSelectableAutoLoader>();


            if (big_loader != null && type != type_big_sprite)
                Object.DestroyImmediate(big_loader);

            if (normal_loader != null && type != type_sprite)
                Object.DestroyImmediate(normal_loader);

            if (selectable_loader != null && type != type_selectable)
                Object.DestroyImmediate(selectable_loader);
        }
    }


    // 已经有一个重复的ESpriteCnf了
    public class ESpriteCnf01
    {
        public string asset_path_without_asset;                 // 
        public string packingtag;
        public bool is_texture;
        public int ref_count;

        public void SetAssetPath(string path)
        {
            
            TextureImporter importer = AssetImporter.GetAtPath(path) as TextureImporter;
            asset_path_without_asset = EPathHelper.RemoveAssetsAndSuffixforPath(path);
            packingtag = importer.spritePackingTag;

            if (string.IsNullOrEmpty(packingtag))
            {
                is_texture = true;
                packingtag = asset_path_without_asset;
            }
            else
            {
                is_texture = false;
            }
        }
    }
}
