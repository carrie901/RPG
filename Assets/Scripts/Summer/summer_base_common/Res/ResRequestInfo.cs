﻿using System.Collections.Generic;

namespace Summer
{
    /// <summary>
    /// 加载请求的信息
    /// </summary>
    public class ResRequestInfo
    {
        /// <summary>
        /// 资源的名字
        /// </summary>
        public string res_name;                          
        /// <summary>
        /// 资源实际的路径 存放在res_bundle下的路径（非AB路径）
        /// </summary>
        public string res_path;                          
        /// <summary>
        /// 资源的游戏类型，比如是技能icon，技能的Prefab,为了确定路径的类型
        /// </summary>
        public E_GameResType res_type;                   
    }


    public class ResRequestFactory
    {
        // TODO 小小的gc问题， 可优化
        public static Dictionary<E_GameResType, Dictionary<string, ResRequestInfo>> res_request_map
            = new Dictionary<E_GameResType, Dictionary<string, ResRequestInfo>>(32);
        public static ResRequestInfo CreateRequest<T>(string res_name, E_GameResType res_type = E_GameResType.quanming) where T : UnityEngine.Object
        {
            Dictionary<string, ResRequestInfo> res_name_map;
            if (!res_request_map.TryGetValue(res_type, out res_name_map))
            {
                res_name_map = new Dictionary<string, ResRequestInfo>(32);
                res_request_map.Add(res_type, res_name_map);
            }

            ResRequestInfo res_request_info;
            if (!res_name_map.TryGetValue(res_name, out res_request_info))
            {
                res_request_info = new ResRequestInfo();
                res_request_info.res_name = res_name;
                res_request_info.res_type = res_type;
                res_request_info.res_path = ResPathManager.FindPath<T>(res_type, res_name);
                res_name_map.Add(res_request_info.res_name, res_request_info);
            }


            return res_request_info;
        }


    }
}

