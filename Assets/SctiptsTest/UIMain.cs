using UnityEngine;
using System.Collections;
using Summer;
using UnityEngine.UI;

public class UIMain : MonoBehaviour
{
    //AssetBundle assetbundle = null;
    public RawImage img;

    void Start()
    {
        CreateObject();
        //CreatImage(loadSprite("UI_dianjiang_button_huo01", "main"));
        //CreatImage(loadSprite("UI_dianjiang_button_huo02", "main1"));
        //CreatImage(loadSprite("UI_dianjiang_button_huo02"));

        //assetbundle = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/Main.assetbundle");
    }

    private void CreatImage(Sprite sprite)
    {
        GameObject go = new GameObject(sprite.name);

        go.layer = LayerMask.NameToLayer("UI");
        go.transform.parent = transform;
        go.transform.localScale = Vector3.one;
        Image image = go.AddComponent<Image>();
        image.sprite = sprite;
        image.SetNativeSize();
    }

    private Sprite loadSprite(string spriteName, string main_ui)
    {
        /*#if USE_ASSETBUNDLE

        #else
                return Resources.Load<GameObject>("Sprite/" + spriteName).GetComponent<SpriteRenderer>().sprite;
        #endif*/
        AssetBundle assetbundle = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/AssetBundle/" + main_ui + ".assetbundle");

        Object ob = assetbundle.LoadAsset(spriteName);
        return ob as Sprite;
    }

    public void CreateObject()
    {
        GameObject go = LoadGameObject();
        go.layer = LayerMask.NameToLayer("UI");
        go.transform.parent = transform;
        go.transform.localScale = Vector3.one;
    }

    public Image sp;
    public GameObject LoadGameObject()
    {
        MemoryDetector m = new MemoryDetector();
        AssetBundle assetbundle1 = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/AssetBundle/t_05/main");
        assetbundle1.LoadAllAssets();
        sp.sprite = assetbundle1.LoadAsset<Sprite>("UI_dianjiang_button_huo02");
        m.OnExcute();
        AssetBundle assetbundle = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/AssetBundle/t_05/prefab_01.assetbundle");
        //Object[] os = assetbundle.LoadAllAssets();
        Object ob = assetbundle.LoadAsset("UI_dianjiang_button_quan01");
        m.OnExcute();
        GameObject go = Instantiate(ob as GameObject);
        return go;
    }

    /*private Texture loadSprite1(string spriteName, string main_ui)
    {
        /*#if USE_ASSETBUNDLE

        #else
                return Resources.Load<GameObject>("Sprite/" + spriteName).GetComponent<SpriteRenderer>().sprite;
        #endif#1#

        if (assetbundle == null)
            assetbundle = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/AssetBundle/" + main_ui + ".assetbundle");
        Object[] gos = assetbundle.LoadAllAssets();
        Object go = assetbundle.LoadAsset(spriteName);
        Texture t = go as Texture;
        Sprite s = go as Sprite;
        t = s.texture;
        return t;
    }*/

}