using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using RpcHandler.Message;

namespace RpcHandler.Server
{
    public class ServerSocket
    {
        private TcpListener tcpListener = null;
        private Socket socket = null;
        private NetworkStream stream = null;
        private BinaryFormatter binaryFormatter = new BinaryFormatter();
        private ServerImport serverImport;
        public ServerSocket(int port, ServerImport _serverImport)
        {
            tcpListener = new TcpListener(IPAddress.Any, port);
            serverImport = _serverImport;
        }

        public void Run()
        {
            this.Start();
            this.Accept();
        }

        private void Start() { tcpListener.Start(); }
        private void Accept()
        {
            tcpListener.BeginAcceptSocket((ar) =>
            {
                this.socket = tcpListener.EndAcceptSocket(ar);
                this.stream = new NetworkStream(this.socket);
                Request request = this.Receive() as Request;
                Response response = serverImport.Run(request);
                this.Send(response);
                this.stream.Close();
                this.CloseConnect();
                this.Accept();

            }, tcpListener);
        }
        private void CloseConnect() { socket.Close(); }

        /// <summary>
        /// Gửi đối tượng bằng cách ghi đối tượng xuống NetworkStream của socket
        /// </summary>
        /// <param name="ob">Đối tượng cần ghi</param>
        private void Send(object ob) { binaryFormatter.Serialize(stream, ob); }

        /// <summary>
        /// Nhận đối tượng từ NetworkStream của socket
        /// </summary>
        /// <returns>Đối tượng nhận được</returns>
        private object Receive() { return binaryFormatter.Deserialize(stream); }
    }
}
