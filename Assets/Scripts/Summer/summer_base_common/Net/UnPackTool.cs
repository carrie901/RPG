using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Summer
{
    public class UnPackTool
    {
        public static void UnPack(int network_messagecode, int start_index, int length, byte[] buffer)
        {
            try
            {
                using (MemoryStream stream_for_proto = new MemoryStream(buffer, start_index, length))
                {
                    switch (network_messagecode)
                    {
                        case NetworkMessageCode.KEEP_ALIVE_SYNC:
                            //序列化
                            break;
                        default:
                            LogManager.Error("No Such Packet, packet type is " + network_messagecode);
                            break;
                    }
                }
            }
            catch (System.Exception ex)
            {
                LogManager.Error(ex.Message + "\n " + ex.StackTrace + "\n" + ex.Source);
            }

        }
    }
}
