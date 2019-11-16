using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using RpcHandler.Message;

namespace RpcHandler.Client
{
    public class ClientSocket
    {
        private BinaryFormatter binaryFomatter = new BinaryFormatter();
        private string address;
        private int port;
        private Queue<Response> responses = new Queue<Response>();

        public Queue<Response> Responses { get { return responses; } }
        public ClientSocket(string server_ip, int _port)
        {           
            address = server_ip;
            port = _port;
        }
        public void SetAddress(string server_ip, int _port)
        {
            address = server_ip;
            port = _port;
        }
        public void RPC(Request request)
        {
            this.Connect(request);       
        }

        private void Connect(Request request)
        {
            ObjectState state = new ObjectState { mClient = new TcpClient(), mRequest = request };
            state.mClient.BeginConnect(address, port, new AsyncCallback(BeginConnectCallbackFunc), state);
        }

        private void BeginConnectCallbackFunc(IAsyncResult ar)
        {
            ObjectState state = (ObjectState)ar.AsyncState;
            try { state.mClient.EndConnect(ar); }
            catch(SocketException e)
            {
                return;
            }
            this.Send(state.mClient, state.mRequest);
            Response response = this.Receive(state.mClient) as Response;
            lock (responses)
            {
                responses.Enqueue(response);
            }
            this.Close(state.mClient);
        }

        /// <summary>
        /// ghi tuần tự đối tượng xuống networkstream của tcpClient
        /// </summary>
        /// <param name="ob">Đối tượng cần ghi</param>
        private void Send(TcpClient tcpClient, object ob) { binaryFomatter.Serialize(tcpClient.GetStream(), ob); }

        /// <summary>
        /// Đọc 1 đối tượng từ NetworkStream Của tcpClient
        /// </summary>
        /// <returns>Trả về đối tượng vừa đọc được</returns>
        private object Receive(TcpClient tcpClient) { return binaryFomatter.Deserialize(tcpClient.GetStream()); }

        private void Close(TcpClient tcpClient) { tcpClient.Close(); }

        public class ObjectState
        {
            public TcpClient mClient;
            public Request mRequest;
        }
    }
}
