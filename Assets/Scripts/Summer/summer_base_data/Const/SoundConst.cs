using System.Collections.Generic;

namespace Summer
{

    /// <summary>
    /// ������һЩö��
    /// </summary>
    public class SoundConst
    {
        public const int BGM_MAIN = 1;                              // ��½������ս��ǰ
        public const int BGM_BATTLE = 6;                            // ս����������
        public const int IN_TO_BATTLE = 7;                          // ����ս��
        public const int LEVEL_WIN = 8;                             // ս��ʤ��
        public const int LEVEL_LOST = 9;                            // ս��ʧ��
        public const int COMMON = 10;                               // ͨ����Ч
        public const int PESSLESS_NODE = 11;                        // ��˫�ڵ�
        public const int PEERLESS_SUCCESS = 215;                    // ������˫ֵ��һ�񣬲���һ����Ч
    }

    /// <summary>
    /// ����Ч��
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