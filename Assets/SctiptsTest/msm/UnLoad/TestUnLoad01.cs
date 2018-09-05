using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Summer;
/// <summary>
/// 测试
/// </summary>
public class TestUnLoad01 : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

        /*ResRequestInfo info = ResRequestFactory.CreateRequest<GameObject>("res_bundle/TestRes/TestPanel02");
        GameObject go = ResManager.instance.LoadPrefab(info);
        GameObjectHelper.SetParent(go, gameObject);*/

        //AssetBundle.LoadFromFile(Application.streamingAssetsPath +"/rpg/uiresources/uitexture/other/activity_yeqian_1.ab");


        GameObject go = LoadAsset<GameObject>("res_bundle/TestRes/TestPanel02", "TestPanel02");
        GameObject ins_go = Instantiate(go) as GameObject;
        GameObjectHelper.SetParent(ins_go, gameObject);


        LoadAsset<Sprite>("uiresources/uitexture/other/activity_yeqian_1", "activity_yeqian_1");
        LoadAsset<Sprite>("uiresources/uitexture/other/activity_yeqian_3", "activity_yeqian_3");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public T LoadAsset<T>(string asset_path, string res_name) where T : Object
    {
        AssetBundle ab = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/rpg/" + asset_path + ".ab");
        T t = ab.LoadAsset<T>(res_name);
        return t;
        //return null;
    }

}
