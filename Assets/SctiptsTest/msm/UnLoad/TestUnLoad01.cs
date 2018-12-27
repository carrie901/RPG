
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 测试
/// </summary>
public class TestUnLoad01 : MonoBehaviour
{
    public Image img;
    // Use this for initialization
    private void Start()
    {
        Resources.UnloadUnusedAssets();
        /*ResRequestInfo info = ResRequestFactory.CreateRequest<GameObject>("res_bundle/TestRes/TestPanel02");
        GameObject go = ResManager.instance.LoadPrefab(info);
        GameObjectHelper.SetParent(go, gameObject);*/

        //AssetBundle.LoadFromFile(Application.streamingAssetsPath +"/rpg/uiresources/uitexture/other/activity_yeqian_1.ab");


        /*GameObject go = LoadAsset<GameObject>("res_bundle/TestRes/TestPanel02", "TestPanel02");
        GameObject ins_go = Instantiate(go) as GameObject;
        GameObjectHelper.SetParent(ins_go, gameObject);


        LoadAsset<Sprite>("uiresources/uitexture/other/activity_yeqian_1", "activity_yeqian_1");
        LoadAsset<Sprite>("uiresources/uitexture/other/activity_yeqian_3", "activity_yeqian_3");*/
    }

    // Update is called once per frame
    private void Update()
    {
        T1();
        T2();
        T3();
    }

    public AssetBundle tt;
    //public Object[] objs;
    public Texture2D _2d;
    public T LoadAsset<T>(string asset_path, string res_name) where T : Object
    {
        AssetBundle ab = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/rpg/" + asset_path + ".ab");
        //Debug.Log("ab:" + ab.GetInstanceID());
        T t = ab.LoadAsset<T>(res_name);
        //objs = ab.LoadAllAssets();

        //Debug.Log("t:" + t.GetHashCode());
        tt = ab;
        tt.Unload(false);
        return t;
        //return null;
    }

    public bool flag1 = false;

    private void T1()
    {
        if (!flag1) return;
        flag1 = false;
        img.sprite = LoadAsset<Sprite>("uiresources/uitexture/other/activity_yeqian_1", "activity_yeqian_1");
        _2d = img.sprite.texture;
        //img.sprite = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/res_bundle/630.png");
    }

    public bool flag2 = false;

    private void T2()
    {
        if (!flag2) return;
        flag2 = false;
        Debug.Log("img.sprite:" + img.sprite.GetInstanceID() + "img.sprite.texture:" + img.sprite.texture.GetInstanceID());
        //Debug.Log("img.sprite:" + img.sprite.GetInstanceID() + "img.sprite.texture:" + img.sprite.texture.GetInstanceID());
        //Resources.UnloadAsset(img.sprite.texture);
        Resources.UnloadAsset(img.sprite.texture);
        Resources.UnloadAsset(img.sprite.associatedAlphaSplitTexture);
        Resources.UnloadAsset(img.sprite);
        img.sprite = null;
        //Resources.UnloadUnusedAssets();
        //Resources.UnloadAsset(img.sprite.texture);
        /* for (int i = 0; i < objs.Length; i++)
         {
             Resources.UnloadAsset(objs[i]);
             objs[i] = null;
         }*/
        //tt.Unload(true);

    }

    public bool flag3 = false;

    private void T3()
    {
        if (!flag3) return;
        flag3 = false;
    }
}
