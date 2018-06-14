﻿using System.Collections;
using System.Collections.Generic;
using Summer;
using UnityEngine;

public class TestDataLoader : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

        /*MyScriptableObject nameInfoObj = ScriptableObject.CreateInstance<MyScriptableObject>();
        nameInfoObj.testName = "测试名字";
        nameInfoObj.name = "MyScriptableObject";
        nameInfoObj.myData.Add(new MyDataInfo(100, "myData测试"));
        nameInfoObj.myData.Add(new MyDataInfo(101, "myData3测试"));
        UnityEditor.AssetDatabase.CreateAsset(nameInfoObj, "Assets/" + nameInfoObj.name + ".asset");*/

        ConfigManager.ReadLocalConfig();

        BaseEntity entity = EntityPool.Instance.Pop(1001001);
        EntitesManager.Instance.AddEntity(entity);
        GameEventSystem.Instance.RaiseEvent(E_GLOBAL_EVT.camera_set_player, entity);
        EntitesManager.Instance.SetManual(entity);

        int count = 10;
        for (int i = 0; i < count; i++)
        {
            BaseEntity tmp = EntityPool.Instance.Pop(1001001);
            tmp.InitPosRot();
            EntitesManager.Instance.AddEntity(tmp);
        }
        /*TransformPool.Instance.Pop<PoolVfxObject>("res_bundle/prefab/vfx/Skill/eff_H_ZhaoYun_01_attack_01");
        TransformPool.Instance.Pop<PoolVfxObject>("res_bundle/prefab/vfx/Skill/eff_H_ZhaoYun_01_attack_02");
        TransformPool.Instance.Pop<PoolVfxObject>("res_bundle/prefab/vfx/Skill/eff_H_ZhaoYun_01_attack_03");
        TransformPool.Instance.Pop<PoolVfxObject>("res_bundle/prefab/vfx/Skill/eff_H_ZhaoYun_01_attack_04");
        TransformPool.Instance.Pop<PoolVfxObject>("res_bundle/prefab/vfx/Skill/eff_H_ZhaoYun_01_attack_05");
        TransformPool.Instance.Pop<PoolVfxObject>("res_bundle/prefab/vfx/Skill/eff_H_ZhaoYun_01_attack_06");
        TransformPool.Instance.Pop<PoolVfxObject>("res_bundle/prefab/vfx/Skill/eff_H_ZhaoYun_01_peerless");
        TransformPool.Instance.Pop<PoolVfxObject>("res_bundle/prefab/vfx/Skill/eff_H_ZhaoYun_01_skill_01");
        TransformPool.Instance.Pop<PoolVfxObject>("res_bundle/prefab/vfx/Skill/eff_H_ZhaoYun_01_skill_02");
        TransformPool.Instance.Pop<PoolVfxObject>("res_bundle/prefab/vfx/Skill/eff_H_ZhaoYun_01_skill_02_G");
        TransformPool.Instance.Pop<PoolVfxObject>("res_bundle/prefab/vfx/Skill/eff_H_ZhaoYun_01_skill_03");
        TransformPool.Instance.Pop<PoolVfxObject>("res_bundle/prefab/vfx/Skill/eff_H_ZhaoYun_01_skill_04");
        TransformPool.Instance.Pop<PoolVfxObject>("res_bundle/prefab/vfx/Skill/eff_H_ZhaoYun_01_skill_05");
        TransformPool.Instance.Pop<PoolVfxObject>("res_bundle/prefab/vfx/Skill/eff_H_ZhaoYun_01_skill_05_G");
        TransformPool.Instance.Pop<PoolVfxObject>("res_bundle/prefab/vfx/Skill/eff_H_ZhaoYun_01_skill_05_G_1");*/
        //GameObject go = ResManager.instance.LoadPrefab("test/Plane", E_GameResType.quanming);
    }

    // Update is called once per frame
    void Update()
    {

    }
}




