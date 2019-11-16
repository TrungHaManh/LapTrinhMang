using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RpcHandler.Client;
using RpcHandler.User;

namespace TestXongXoa
{
    public class TestClient : ClientExport<TestClient>, IMonitor
    {
        public TestClient(string ip_server, int port) : base(ip_server, port, "TestServer")
        {

        }

        public void Chat(string title, string mess)
        {
            throw new NotImplementedException();
        }

        public void DisableProcessWithID(int id)
        {
            throw new NotImplementedException();
        }

        public string GetAllCurrentProcess()
        {
            RPC(nameof(GetAllCurrentProcess));
            return "";
        }

        public string Ping()
        {
            RPC(nameof(Ping));
            return "";
        }

        public void SetClipboard(bool enable)
        {
            RPC(nameof(SetClipboard), enable);
        }

        public void SetFileShare(bool enable)
        {
            RPC(nameof(SetFileShare), enable);
        }

        public void SetUSB(bool enable)
        {
            RPC(nameof(SetUSB), enable);
        }
    }
}
