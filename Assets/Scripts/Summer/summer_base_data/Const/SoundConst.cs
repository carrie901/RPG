using System.Collections.Generic;

namespace Summer
{

    /// <summary>
    /// 声音的一些枚举
    /// </summary>
    public class SoundConst
    {
        public const int BGM_MAIN = 1;                              // 登陆到进入战斗前
        public const int BGM_BATTLE = 6;                            // 战斗背景音乐
        public const int IN_TO_BATTLE = 7;                          // 进入战斗
        public const int LEVEL_WIN = 8;                             // 战斗胜利
        public const int LEVEL_LOST = 9;                            // 战斗失败
        public const int COMMON = 10;                               // 通用音效
        public const int PESSLESS_NODE = 11;                        // 无双节点
        public const int PEERLESS_SUCCESS = 215;                    // 主角无双值满一格，播放一个特效
    }

    /// <summary>
    /// 声音效果
    /// </summary>
    /*public enum E_SoundEffect
    {
        none,
        fade_in,
        fade_out,
    }*/

    /*public class SoundDic
    {
        public static SoundDic Instance = new SoundDic();
        public Dictionary<int, int> _map = new Dictionary<int, int>();

        public int GetSoundId(int key)
        {
            return 0;
        }
    }*/
}