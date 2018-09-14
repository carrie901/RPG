using System.Collections.Generic;

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
        public string ResName { get; set; }
        /// <summary>
        /// 资源实际的路径 存放在res_bundle下的路径（非AB路径）
        /// </summary>
        public string ResPath { get; set; }
        /// <summary>
        /// 资源的游戏类型，比如是技能icon，技能的Prefab,为了确定路径的类型
        /// </summary>
        public E_GameResType ResType { get; set; }
    }


    /// <summary>
    /// 请求信息的工场
    /// </summary>
    public class ResRequestFactory
    {
        protected static Dictionary<E_GameResType, Dictionary<string, ResRequestInfo>> _resRequestMap
            = new Dictionary<E_GameResType, Dictionary<string, ResRequestInfo>>(32, GameResTypeComparer.Instance);
        public static ResRequestInfo CreateRequest<T>(string resName, E_GameResType resType = E_GameResType.QUANMING) where T : UnityEngine.Object
        {
            Dictionary<string, ResRequestInfo> resNameMap;
            if (!_resRequestMap.TryGetValue(resType, out resNameMap))
            {
                resNameMap = new Dictionary<string, ResRequestInfo>(32);
                _resRequestMap.Add(resType, resNameMap);
            }

            ResRequestInfo resRequestInfo;
            if (!resNameMap.TryGetValue(resName, out resRequestInfo))
            {
                resRequestInfo = new ResRequestInfo();
                resRequestInfo.ResName = resName;
                resRequestInfo.ResType = resType;
                resRequestInfo.ResPath = ResPathManager.FindPath<T>(resType, resName);
                resNameMap.Add(resRequestInfo.ResName, resRequestInfo);
            }
            return resRequestInfo;
        }
    }
}

