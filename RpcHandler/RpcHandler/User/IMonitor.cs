using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpcHandler.User
{
    /// <summary>
    /// Interface chức năng hệ thống.
    /// Module client & server phải thực thi interface này để thực hiện gọi RPC
    /// </summary>
    public interface IMonitor
    {
        void SetUSB(bool enable);
        void SetFileShare(bool enable);
        void SetClipboard(bool enable);
        string Ping();
        string GetAllCurrentProcess();
        void DisableProcessWithID(int id);
        void Chat(string title, string mess);
    }
}
