using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using MsgPb;
using ProtoBuf;

namespace Summer
{
    public class UnPackTool
    {
        public static IExtensible UnPack(int network_messagecode, int start_index, int length, byte[] buffer)
        {
            IExtensible packet = null;
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

            return packet;
        }

        public static IExtensible UnPack1(ref int start_index, int length, byte[] buffer, int msg_id)
        {
            IExtensible packet = null;
            try
            {
                using (MemoryStream stream_for_proto = new MemoryStream(buffer, start_index, length))
                {
                    
                    packet = Serializer.Deserialize<RetVersion>(stream_for_proto);
                    /* switch (network_messagecode)
                     {
                         case NetworkMessageCode.KEEP_ALIVE_SYNC:
                             //序列化
                             break;
                         default:
                             LogManager.Error("No Such Packet, packet type is " + network_messagecode);
                             break;
                     }*/
                    stream_for_proto.Dispose();
                }
                start_index += length;
            }
            catch (System.Exception ex)
            {
                LogManager.Error(ex.Message + "\n " + ex.StackTrace + "\n" + ex.Source);
            }

            return packet;
        }
    }
}
