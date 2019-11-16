using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Threading;

namespace TestXongXoa
{
    public class Tcp_Client
    {
        private string Ip_string = "127.0.0.1";
        private int Number_port;
        public ManualResetEvent allDone = new ManualResetEvent(false);

        public void InitClient(string ip_server, int port)
        {
            Ip_string = ip_server;
            Number_port = port;

            TcpClient tcpClient = new TcpClient();
            tcpClient.BeginConnect(ip_server, port, RequestFunc, tcpClient);
        }

        private void RequestFunc(IAsyncResult ar)
        {
            TcpClient tcpClient = (TcpClient)ar.AsyncState;
            tcpClient.EndConnect(ar);

            NetworkStream stream = tcpClient.GetStream();
            StreamWriter writer = new StreamWriter(stream);
            StreamReader reader = new StreamReader(stream);
            writer.AutoFlush = true;

            writer.WriteLine("Ha Manh Trung");
            writer.WriteLine(369);
            writer.WriteLine(9.5f);

            string text = reader.ReadLine();
            Console.WriteLine(text);

            
            stream.Close();
            tcpClient.Close();
        }
    }
}
