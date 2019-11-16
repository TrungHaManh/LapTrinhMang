using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using RpcHandler.Message;

namespace RpcHandler.Client
{
    // client stub
    public class ClientExport<A>
    {
        protected ClientSocket client;
        protected string ClassName;

        public Queue<Response> GetAllResponse() { return client.Responses; }

        // class_name: Tên của class cần gọi (bên server)
        public ClientExport(string ip_server, int port, string class_name)
        {
            ClassName = class_name;
            client = new ClientSocket(ip_server, port);
        }
        public void SetAddress(string server_ip, int _port)
        {
            client.SetAddress(server_ip, _port);
        }
        /// <summary>
        /// Thực hiên cuộc gọi RPC
        /// </summary>
        /// <param name="method">Tên hàm cần gọi</param>
        /// <param name="args">Tham số của hàm</param>
        public void RPC([CallerMemberName]string method = null, params object[] args)
        {
            Request request = new Request();
            request.Class = ClassName;
            request.Method = method;
            request.parameters = GetParamsMap(method, args);
            client.RPC(request);
        }
        
        private Dictionary<string, object> GetParamsMap(string method, params object[] args)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            var pr = typeof(A).GetMethod(method).GetParameters();
            for(int i=0; i<pr.Length; i++)
            {
                result.Add(pr[i].Name, args[i]);
            }
            return result;
        }
    }
}
