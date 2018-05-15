
namespace Summer
{
    public enum E_GameResType
    {
        none = 0,
        text_asset,              // 文本路径
        quanming,
        skill_icon,             // 技能 Icon表
        hero_icon,              // 英雄 icon表
        buff_icon,
        other_icon,
        buff_prefab,            // buff prefab表
        stage_icon,

        character_prefab,
        skill_prefab,
        ui_prefab,
        ui_item_prefab,
        item_icon,              // 道具 icon

        //声音
        music_bgm,              // BGM
        music_sound,            // 音效
        music_voice,            // 配乐

        // ui 特效
        ui_effect,

    }

    public enum E_AssetType
    {
        none = 0,
        script,         // .cs
        shader,         // .shader or build-in shader with name
        font,           // .ttf
        texture,        // .png, .jpg
        material,       // .mat
        animation,      // .anim
        controller,     // .controller
        fbx,            // .fbx
        textasset,      // .txt, .bytes
        prefab,         // .prefab
        unity,          // .unity
        max
    }

    public enum E_ResErrorCode
    {
        none = 0,                                       // 无
        parameter_error = 1,                            // 参数错误
        time_out = 2,                                   // 超时
        preprocess_error = 3,                           // 预处理错误

        //Load
        load_main_manifest_failed = 101,                // 载入AssetBundleManifest错误
        load_resources_mani_fest_failed = 102,          // 载入ResourcesManifest错误
        load_resources_packages_failed = 103,           // 载入ResourcesPackages错误
        load_new_main_manifest_failed = 104,            // 载入新的AssetBundleManifest错误
        load_new_resources_mani_fest_failed = 105,      // 载入新的ResourcesManifest错误


        //Find
        not_find_asset_bundle = 201,                    // 未找到有效的AssetBundle

        //Download
        invalid_url = 1001,                             // 未能识别URL服务器
        server_no_response = 1002,                      // 服务器未响应
        download_failed = 1003,                         // 下载失败
        download_main_config_file_failed = 1004,        // 主配置文件下载失败
        download_asset_bundle_failed = 1005,            // AssetBundle下载失败

        //PackageDownloader
        invalid_package_name = 2001,                    // 无效的包名
    }

}
