using System.Collections;
using System.Collections.Generic;
using Summer;
using UnityEngine;

public class TestCalRef : MonoBehaviour
{

    public List<AssetInfo> asset_infos = new List<AssetInfo>();
    public bool flag = false;

    public bool unload_flag = false;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (flag)
        {
            flag = false;
            _excute();
        }


        if (unload_flag)
        {
            unload_flag = false;
            _excute_unload();
        }
    }

    public void _excute()
    {
        asset_infos.Clear();
        asset_infos.AddRange(ResLoader.instance._map_res.Values);


    }

    public void _excute_unload()
    {
        SpritePool.Instance._big_sprite_pool_cache.SetDefaultCapacity();
    }
}

