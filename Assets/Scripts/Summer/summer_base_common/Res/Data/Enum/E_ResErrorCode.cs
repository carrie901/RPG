
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
                                             
using System.Collections.Generic;
using UnityEngine;

namespace Summer
{
    public enum E_ResErrorCode
    {
        NONE = 0,                                       // 无
        PARAMETER_ERROR = 1,                            // 参数错误
        TIME_OUT = 2,                                   // 超时
        PREPROCESS_ERROR = 3,                           // 预处理错误

        //Load
        LOAD_MAIN_MANIFEST_FAILED = 101,                // 载入AssetBundleManifest错误


        //Find
        NOT_FIND_ASSET_BUNDLE = 201,                    // 未找到有效的AssetBundle

        //Download
        INVALID_URL = 1001,                             // 未能识别URL服务器
        SERVER_NO_RESPONSE = 1002,                      // 服务器未响应
        DOWNLOAD_FAILED = 1003,                         // 下载失败
        DOWNLOAD_MAIN_CONFIG_FILE_FAILED = 1004,        // 主配置文件下载失败
        DOWNLOAD_ASSET_BUNDLE_FAILED = 1005,            // AssetBundle下载失败

        //PackageDownloader
        INVALID_PACKAGE_NAME = 2001,                    // 无效的包名
    }
}