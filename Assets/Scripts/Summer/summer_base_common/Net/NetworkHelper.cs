
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


using System;

namespace Summer
{
    public class NetworkHelper
    {
        public const int ERROR_DATA = -1;
        /// <summary>
        /// 一个完整的Packet包的长度
        /// </summary>
        /// <param name="receive_buffer"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        public static int ReadBufferSize(byte[] receive_buffer, ref int position)
        {
            int buffer_size = NetConverter.BytesToInt(receive_buffer, position);
            position += NetworkConst.HEAD_SIZE;
            return buffer_size;
        }

        public static int ReadMsgId(byte[] receive_buffer, ref int position)
        {
            int result = BitConverter.ToInt32(receive_buffer, position);
            position += NetworkConst.MSG_ID_SIZE;
            return result;
        }

        /// <summary>
        /// 从receive_buffer中读取网络下标，比如同一条消息，需要知道哪一条没发送成功
        /// </summary>
        /// <param name="receive_buffer"></param>
        /// <param name="start_index"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static int ReadMsgIndex(byte[] receive_buffer, int start_index, int size = 4)
        {
            return ERROR_DATA;
        }

        /// <summary>
        /// 从receive_buffer中读取Data的长度
        /// </summary>
        /// <param name="receive_buffer"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        public static int ReadPbLength(byte[] receive_buffer, ref int position)
        {
            int result = BitConverter.ToInt32(receive_buffer, position);
            position += NetworkConst.MSG_DATA_SIZE;
            return result;
        }


        #region 测试 发送数据

        public static void ReadBuffer()
        {

        }

        #endregion

        #region 测试     接受数据

        public static byte[] int_byte = new byte[4];

        public static int WriteBuffer(int msg_id, byte[] data, byte[] buffer_size)
        {
            byte[] msg_id_bytes = WriteMsgId(msg_id);
            byte[] pb_data_bytes = WriteBufferLength(data.Length);


            byte[] packet_length = NetConverter.IntToBytes(msg_id_bytes.Length + pb_data_bytes.Length + data.Length);

            int index = 0;
            Copy(packet_length, buffer_size, ref index);
            Copy(msg_id_bytes, buffer_size, ref index);
            Copy(pb_data_bytes, buffer_size, ref index);
            Copy(data, buffer_size, ref index);
            return index;
        }

        public static byte[] WriteBufferLength(Int32 protobuffer_length)
        {
            byte[] protobuf_head = BitConverter.GetBytes(protobuffer_length);
            return protobuf_head;
        }

        public static byte[] WriteMsgId(Int32 msg_id32)
        {
            byte[] msg_head = BitConverter.GetBytes(msg_id32);
            return msg_head;
        }

        public static void WriteMsgDatga()
        {

        }

        #endregion

        public static void Copy(byte[] data, byte[] buffer_size, ref int index)
        {
            Array.Copy(data, 0, buffer_size, index, data.Length);
            index += data.Length;
        }
    }
}


