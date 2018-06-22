
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
using System.Collections;
using System.IO;
using System.Net.Sockets;
using Summer.Tool;
using UnityEngine;
using Object = System.Object;
namespace Summer
{
    public class NetworkManager
    {

        #region 属性

        protected Socket _socket;

        protected byte[] _receive_buffer;

        protected const int HEAD_SIZE = 4;
        protected const int HEAD_NUM = 3;

        protected const float CONNECT_TIME_OUT = 3.0f;
        protected const float REQ_TIME_OUT = 5.0f;
        protected const float KEEP_ALIVE_TIME_OUT = 10.0f;

        protected static bool _is_keep_alive;

        public bool IsConncted
        {
            get { return _socket != null && _socket.Connected; }
        }

        #endregion

        #region Public

        public NetworkManager()
        {
            Init();
        }

        public void Init()
        {
            NetworkEventSystem.Instance.RegisterHandler((int)E_NetModuleMessage.socket_connected, OnSocketConnected);
            NetworkEventSystem.Instance.RegisterHandler((int)E_NetModuleMessage.socket_disconnected, OnSocketDisconnected);
            NetworkEventSystem.Instance.RegisterHandler((int)E_NetModuleMessage.req_finish, OnReqFinish);


            NetworkEventSystem.Instance.RaiseEvent((int)E_NetModuleMessage.socket_disconnected, null);
        }

        public void CloseConnection()
        {
            if (_socket != null)
            {
                if (_socket.Connected)
                {
                    _socket.Shutdown(SocketShutdown.Both);
                    LogManager.Error("Client Close...");
                }
                _socket.Close();
            }
        }

        public void OnKeepAliveSync(uint iMessageType, object kParam)
        {
            _is_keep_alive = true;
        }

        #region connection

        public IEnumerator BeginTryConnect()
        {
            try
            {
                _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                _socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
                _socket.BeginConnect(NetworkConst.ip_address, NetworkConst.ip_port, new AsyncCallback(_finish_connection), null);
            }
            catch (Exception ex)
            {
                LogManager.Error(ex.StackTrace);
                yield break;
            }

            yield return CoroutineConst.GetWaitForSeconds(CONNECT_TIME_OUT);
            if (!_socket.Connected)
            {
                LogManager.Error("Client Connect Time Out...");
                CloseConnection();
            }
            _is_keep_alive = _socket.Connected;
        }

        #endregion

        #endregion

        #region 事件响应

        private void OnSocketConnected(Object param)
        {
            if (_receive_buffer == null)
            {
                _receive_buffer = new byte[_socket.ReceiveBufferSize];
            }

            _is_keep_alive = true;

            BeginReceivePacket();
        }

        private void OnSocketDisconnected(Object param)
        {
            StartCoroutineManager.Start(BeginTryConnect());
        }

        private void OnReqFinish(Object param)
        {
            throw new System.NotImplementedException();
        }



        #endregion

        #region ReceivePacket

        // 接受包数据
        public void BeginReceivePacket()
        {
            try
            {
                lock (_socket)
                {
                    _socket.BeginReceive(_receive_buffer, 0, _socket.ReceiveBufferSize, SocketFlags.None, new AsyncCallback(EndReceivePacket), null);
                }
            }
            catch (Exception ex)
            {
                LogManager.Error(ex.StackTrace);
            }
        }

        public void EndReceivePacket(IAsyncResult ar)
        {
            int bytes_read = -1;
            try
            {
                if (IsConncted)
                {
                    lock (_socket)
                    {
                        bytes_read = _socket.EndReceive(ar);
                    }
                }

                if (bytes_read == -1)
                {
                    CloseConnection();
                    return;
                }
            }
            catch (ObjectDisposedException) { LogManager.Error("Receive Closed"); }
            catch (Exception ex) { LogManager.Error("{0}\n{1}\n{2}", ex.Message, ex.StackTrace, ex.Source); }


            // Begin Read //
            int position = 0;

            while (position < bytes_read)
            {
                int buffer_size = NetConverter.BytesToInt(_receive_buffer, position + HEAD_SIZE * 0);
                int network_index = NetConverter.BytesToInt(_receive_buffer, position + HEAD_SIZE * 1);

                byte[] msg_id_bytes = new byte[HEAD_SIZE];
                for (int i = 0; i < HEAD_SIZE; i++)
                {
                    msg_id_bytes[i] = _receive_buffer[position + HEAD_SIZE * 2 + i];
                }
                string msg_id = BitConverter.ToString(msg_id_bytes);

                if (network_index != NetworkMessageCode.KEEP_ALIVE_SYNC)
                {
                    LogManager.Error("networkMessage : " + network_index, "msgID : " + msg_id, "bufferSize : " + buffer_size);
                }

                if (position + buffer_size > bytes_read)
                {
                    LogManager.Error("Error receive packet, packet is too long : " + buffer_size);
                    break;
                }

                int rsp_packet = 0;
                // 接包
                UnPackTool.UnPack(network_index, position + HEAD_SIZE * HEAD_NUM, buffer_size - HEAD_NUM * HEAD_SIZE, _receive_buffer);
                if (rsp_packet < 10)
                {
                    continue;
                }

                // 组成消息 法伤出去



                position += buffer_size;
            }

            Array.Clear(_receive_buffer, 0, _socket.ReceiveBufferSize);

            BeginReceivePacket();
        }

        #endregion

        #region SendPacket

        public string SendPacket<T>(NetworkMessageCode network_message, T packet, bool timeout_message)
        {
            byte[] msg_id_bytes = BitConverter.GetBytes(UnityEngine.Random.value);

            if (timeout_message)
            {
                //ShowLoadingDialog 显示等待等待
            }
            StartCoroutineManager.Start(BeginSendPacket<T>(network_message, packet, timeout_message, msg_id_bytes));
            return BitConverter.ToString(msg_id_bytes);
        }

        private IEnumerator BeginSendPacket<T>(NetworkMessageCode networkMessage, T packet, bool timeoutMessage, byte[] msgIDBytes)
        {
            string msg_id = BitConverter.ToString(msgIDBytes);

            /*lock (_msgIDDict)
            {
                _msgIDDict.Add(BitConverter.ToString(msgIDBytes), packet);
            }*/

            LogManager.Log("Send : {0} msgID :{1}" + networkMessage, msg_id);

            DoBeginSendPacket<T>(networkMessage, packet, msgIDBytes);
            yield return new WaitForSeconds(REQ_TIME_OUT);

            // 等待超时时间之后，检测超时情况
        }

        /// <summary>
        /// 协议格式：
        /// SIZE ： 4 | TYPE ： 4 | MsgID : 4 | PACKET ： dynamic
        /// </summary>
        /// <typeparam name="T">向服务器发送的packet的类型</typeparam>
        /// <param name="network_message">向服务器发送的请求的类型</param>
        /// <param name="packet">向服务器发送的packet</param>
        private void DoBeginSendPacket<T>(NetworkMessageCode network_message, T packet, byte[] msgID)
        {
            try
            {
                byte[] send_buffer = new byte[_socket.SendBufferSize];

                MemoryStream stream_for_proto = new MemoryStream();
                //Serializer.Serialize<T>(streamForProto, packet);
                int buffer_size = HEAD_SIZE * HEAD_NUM + (int)stream_for_proto.Length;

                byte[] buffer_size_bytes = NetConverter.IntToBytes(buffer_size);
                byte[] network_message_bytes = NetConverter.IntToBytes(/*(int)networkMessage*/2);

                Array.Copy(buffer_size_bytes, 0, send_buffer, HEAD_SIZE * 0, HEAD_SIZE);
                Array.Copy(network_message_bytes, 0, send_buffer, HEAD_SIZE * 1, HEAD_SIZE);
                Array.Copy(msgID, 0, send_buffer, HEAD_SIZE * 2, HEAD_SIZE);
                Array.Copy(stream_for_proto.ToArray(), 0, send_buffer, HEAD_SIZE * HEAD_NUM, stream_for_proto.Length);
                lock (_socket)
                {
                    if (_socket != null && _socket.Connected)
                    {
                        _socket.BeginSend(send_buffer, 0, buffer_size, SocketFlags.None, new AsyncCallback(EndSendPacket), null);
                    }
                }
                stream_for_proto.Dispose();
                stream_for_proto = null;
            }
            catch (ObjectDisposedException) { LogManager.Error("Send Closed"); }
            catch (Exception ex) { LogManager.Error(ex.Message); }
        }

        private void DoBeginSendPacket(NetworkMessageCode network_message, byte[] msgID)
        {
            try
            {
                byte[] send_buffer = new byte[HEAD_SIZE * HEAD_NUM];

                byte[] buffer_size_bytes = NetConverter.IntToBytes(HEAD_SIZE * HEAD_NUM);
                byte[] network_message_bytes = NetConverter.IntToBytes(/*(int)networkMessage*/2);

                Array.Copy(buffer_size_bytes, 0, send_buffer, HEAD_SIZE * 0, HEAD_SIZE);
                Array.Copy(network_message_bytes, 0, send_buffer, HEAD_SIZE * 1, HEAD_SIZE);
                Array.Copy(msgID, 0, send_buffer, HEAD_SIZE * 2, HEAD_SIZE);

                lock (_socket)
                {
                    _socket.BeginSend(send_buffer, 0, HEAD_SIZE * HEAD_NUM, SocketFlags.None, new AsyncCallback(EndSendPacket), null);
                }
            }
            catch (Exception ex) { LogManager.Error(ex.Message); }
        }

        private void EndSendPacket(IAsyncResult ar)
        {
            //int bytesSend = 0;
            try
            {
                lock (_socket)
                {
                    _socket.EndSend(ar);
                }
            }
            catch (Exception ex)
            {
                LogManager.Error(ex.Message);
            }
        }


        #endregion

        #region Private Methods

        public void _finish_connection(IAsyncResult ar)
        {
            if (_socket == null) return;
            _socket.EndConnect(ar);

            if (_socket.Connected)
            {
                //NetworkEventSystem.Instance.DispatchMessageAsync((uint)EModelMessage.SOCKET_CONNECTED, null);
            }
        }


        #endregion
    }
}