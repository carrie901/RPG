
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
using ProtoBuf;
using Summer.Tool;
using System.Collections.Generic;
using Object = System.Object;
namespace Summer
{
    /// <summary>
    /// TODO BUG 如果发送的数据的长度大于收的buffer的长度 会有粘包和拆包的问题
    /// </summary>
    public class SocketManager
    {

        #region 属性

        protected Socket _socket;

        protected byte[] _receive_buffer;

        protected static bool _is_keep_alive;

        public Dictionary<string, IExtensible> _msg_index_map
            = new Dictionary<string, IExtensible>();

        public bool IsConncted
        {
            get { return _socket != null && _socket.Connected; }
        }

        #endregion

        #region Public

        public SocketManager()
        {
            Init();
        }

        public void Init()
        {
            NetworkEventSystem.Instance.RegisterHandler(E_NetModule.socket_connected, OnSocketConnected);
            NetworkEventSystem.Instance.RegisterHandler(E_NetModule.socket_disconnected, OnSocketDisconnected);
            NetworkEventSystem.Instance.RegisterHandler(E_NetModule.req_finish, OnRequestFinish);


            NetworkEventSystem.Instance.RaiseEvent(E_NetModule.socket_disconnected, null);
        }

        public void CloseConnection()
        {
            if (_socket == null) return;
            if (_socket.Connected)
            {
                _socket.Shutdown(SocketShutdown.Both);
                LogManager.Error("关闭Socket连接");
            }
            _socket.Close();
            _socket = null;
        }

        public void OnKeepAliveSync(uint i_message_type, object k_param)
        {
            _is_keep_alive = true;
        }

        #endregion

        #region connection OnSocketConnected/OnSocketDisconnected/OnRequestFinish


        public IEnumerator BeginConnection()
        {
            try
            {
                _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                _socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
                _socket.BeginConnect(NetworkConst.ip_address, NetworkConst.ip_port, _finish_connect, null);
            }
            catch (Exception ex)
            {
                LogManager.Error(ex.StackTrace);
                yield break;
            }

            yield return CoroutineConst.GetWaitForSeconds(NetworkConst.CONNECT_TIME_OUT);
            if (!_socket.Connected)
            {
                LogManager.Error("Client Connect Time Out...");
                CloseConnection();
            }
            _is_keep_alive = _socket.Connected;
        }

        /// <summary>
        /// 当无法接收到心跳包的时候尝试重新连接服务器
        /// </summary>
        public IEnumerator BeginTryConnect()
        {
            LogManager.Log("尝试连接");
            //yield return null;
            while (_socket == null || !_socket.Connected || !_is_keep_alive)
            {
                NetLog.Log("开始连接");
                CloseConnection();
                yield return StartCoroutineManager.Start(BeginConnection());
            }

            /*LogManager.Log("目前没有检测心跳");
            while (_is_keep_alive)
            {
                NetLog.Log("检测_is_keep_alive:[{0}]", _is_keep_alive);
                _is_keep_alive = false;
                yield return CoroutineConst.GetWaitForSeconds(NetworkConst.KEEP_ALIVE_TIME_OUT);
            }

            NetworkEventSystem.Instance.RaiseEvent(E_NetModule.socket_disconnected, null);*/
        }

        // 连接成功，开始接受数据
        private void OnSocketConnected(Object param)
        {
            NetLog.Log("连接成功，开始接受数据");
            if (_receive_buffer == null)
            {
                _receive_buffer = new byte[_socket.ReceiveBufferSize];
            }

            _is_keep_alive = true;

            BeginReceivePacket();
        }

        // 断开连接，请求重新连接
        private void OnSocketDisconnected(Object param)
        {
            NetLog.Log("断开连接，请求重新连接");
            StartCoroutineManager.Start(BeginTryConnect());
        }

        private void OnRequestFinish(Object param)
        {
            NetLog.Log("OnRequestFinish");
        }

        private void OnKeepAlive(Object param)
        {
            
        }

        #endregion

        #region SendPacket

        public IEnumerator _enumerator_bengin_send_packet;
        public int _send_index;
        public string SendPacket<T>(int network_message, T packet, bool timeout_message = true) where T : global::ProtoBuf.IExtensible
        {
            byte[] msg_index = BitConverter.GetBytes(_send_index);

            if (timeout_message)
            {
                //ShowLoadingDialog 显示等待等待
            }
            _enumerator_bengin_send_packet = BeginSendPacket<T>(network_message, packet, timeout_message, msg_index);
            StartCoroutineManager.Start(_enumerator_bengin_send_packet);
            _send_index++;
            return BitConverter.ToString(msg_index);
        }

        private IEnumerator BeginSendPacket<T>(int network_message, T packet, bool timeout_message, byte[] msg_index_bytes) where T : global::ProtoBuf.IExtensible
        {
            // 每一条消息的的下标
            string msg_id = BitConverter.ToString(msg_index_bytes);

            lock (_msg_index_map)
            {
                _msg_index_map.Add(msg_id, packet);
            }

            NetLog.Log("发送消息 : {0} 消息下标 :{1}", network_message, msg_id);

            DoBeginSendPacket<T>(network_message, packet, msg_index_bytes);
            yield return CoroutineConst.GetWaitForSeconds(NetworkConst.REQ_TIME_OUT);

            // 等待超时时间之后，检测超时情况
            LogManager.Error("超时:[{0}]", network_message);

            // 移除相关的回调信息
            lock (_msg_index_map)
            {
                bool result = RemoveMsgIndex(msg_id);
                if (result && timeout_message)// 然后提示超时
                    NetworkEventSystem.Instance.RaiseEvent(E_NetModule.req_timeout, null);
            }
        }

        private void DoBeginSendPacket<T>(int network_message, T packet, byte[] msg_id) where T : global::ProtoBuf.IExtensible
        {
            try
            {
                // TODO 优化
                byte[] send_buffer = new byte[_socket.SendBufferSize];

                MemoryStream stream_for_proto = new MemoryStream();
                ProtobufHelper.Serialize1(stream_for_proto, packet);
                /*int buffer_size = HEAD_SIZE * HEAD_NUM + (int)stream_for_proto.Length;

                byte[] buffer_size_bytes = NetConverter.IntToBytes(buffer_size);
                byte[] network_message_bytes = NetConverter.IntToBytes(/*(int)networkMessage#1#2);

                Array.Copy(buffer_size_bytes, 0, send_buffer, HEAD_SIZE * 0, HEAD_SIZE);
                Array.Copy(network_message_bytes, 0, send_buffer, HEAD_SIZE * 1, HEAD_SIZE);
                Array.Copy(msg_id, 0, send_buffer, HEAD_SIZE * 2, HEAD_SIZE);
                Array.Copy(stream_for_proto.ToArray(), 0, send_buffer, HEAD_SIZE * HEAD_NUM, stream_for_proto.Length);*/
                int buffer_size = NetworkHelper.WriteBuffer(network_message, stream_for_proto.ToArray(), send_buffer);
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

        private void EndSendPacket(IAsyncResult ar)
        {
            try
            {
                lock (_socket) { _socket.EndSend(ar); }
            }
            catch (Exception ex) { NetLog.Error(ex.Message); }
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
                    _socket.BeginReceive(_receive_buffer, 0, _socket.ReceiveBufferSize,
                        SocketFlags.None, EndReceivePacket, null);
                }
            }
            catch (Exception ex) { NetLog.Error(ex.StackTrace); }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ar"></param>
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
            catch (ObjectDisposedException) { NetLog.Error("Receive Closed"); }
            catch (Exception ex) { NetLog.Error("{0}\n{1}\n{2}", ex.Message, ex.StackTrace, ex.Source); }

            NetLog.Assert(bytes_read < NetworkConst.MAX_BUFF_SIZE, "接收到的包太长了");
            // Begin Read //
            int position = 0;

            // MsgID : 4 | PacketLength ： 4 |PACKET ： dynamic
            while (position < bytes_read)
            {
                int buffer_size = NetworkHelper.ReadBufferSize(_receive_buffer, ref position/*position + HEAD_SIZE * 0*/);

                int msg_id = NetworkHelper.ReadMsgId(_receive_buffer, ref position);
                int msg_data_size = NetworkHelper.ReadPbLength(_receive_buffer, ref position);
                // TODO 确认心跳线程
                // KEEP_ALIVE_SYNC

                // 保证整个Buffer长度
                if (position + msg_data_size > bytes_read)
                {
                    LogManager.Error("Error receive packet, packet is too long : " + buffer_size);
                    break;
                }

                // 接包;
                IExtensible rsp_packet = UnPackTool.UnPack1(ref position, msg_data_size, _receive_buffer, msg_id);
                if (rsp_packet == null)
                    continue;


                // 确认某一个消息已经Req_Finish
                RaiseMessage();
                // 
                position += msg_data_size;
            }

            Array.Clear(_receive_buffer, 0, _socket.ReceiveBufferSize);

            BeginReceivePacket();
        }

        #endregion

        #region Private Methods

        public void _finish_connect(IAsyncResult ar)
        {
            if (_socket == null) return;
            lock (_socket)
                _socket.EndConnect(ar);

            LogManager.Assert(_socket.Connected, "Socket BeginConnect finish.but Socket Connected is false");
            if (!_socket.Connected) return;
            NetworkEventSystem.Instance.RaiseEvent(E_NetModule.socket_connected, null);
        }

        public bool RemoveMsgIndex(string msg_index)
        {
            lock (_msg_index_map)
            {
                if (_msg_index_map.ContainsKey(msg_index)) return false;
                _msg_index_map.Remove(msg_index);

                NetworkEventSystem.Instance.RaiseEvent(E_NetModule.req_finish, null);
                return true;
            }
        }

        public void RaiseMessage()
        {

        }



        #endregion
    }

    /// <summary>
    /// 1.ClosedCallBack 
    /// 2.ConnectedCallBack
    /// 3.ReceivedCallBack
    /// 4.DisconnectedCallback
    /// </summary>
    public interface I_Network
    {
        bool IsConnected { get; }

        void Close();

        void Connected();

        void Received();

        void Send();

    }

    /// <summary>
    /// 1客户端每隔一个时间间隔发生一个探测包给服务器
    /// 2客户端发包时启动一个超时定时器
    /// 3服务器端接收到检测包，应该回应一个包
    /// 4如果客户机收到服务器的应答包，则说明服务器正常，删除超时定时器
    /// 5如果客户端的超时定时器超时，依然没有收到应答包，则说明服务器挂了
    /// </summary>
    public interface I_HeartBeat
    {

    }

}