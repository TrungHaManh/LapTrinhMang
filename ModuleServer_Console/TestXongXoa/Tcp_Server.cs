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
    public class Tcp_Server
    {
        private string Ip_string = "127.0.0.1";
        private int Number_port;
        public ManualResetEvent allDone = new ManualResetEvent(false);
        public void InitServer(int port)
        {
            Number_port = port;

            IPAddress address = IPAddress.Parse(Ip_string);
            TcpListener listener = new TcpListener(address, port);
            listener.Start();
            listener.BeginAcceptSocket(BeginAcceptFunc, listener);
            //listener.Stop();
        }

        private void BeginAcceptFunc(IAsyncResult ar)
        {
            allDone.Set();

            TcpListener listener = (TcpListener)ar.AsyncState;
            Socket socket = listener.EndAcceptSocket(ar);

            NetworkStream stream = new NetworkStream(socket);
            
            StreamReader reader = new StreamReader(stream);
            StreamWriter writer = new StreamWriter(stream);
            writer.AutoFlush = true;

            string name = reader.ReadLine();
            int id = int.Parse(reader.ReadLine());
            float point = float.Parse(reader.ReadLine());

            Console.WriteLine(name + "- id: " + id + "- point: " + point);

            writer.WriteLine("OK ban da duoc chap nhan!");

            stream.Close();
            socket.Close();

            listener.BeginAcceptSocket(BeginAcceptFunc, listener);
        }
    }
}
