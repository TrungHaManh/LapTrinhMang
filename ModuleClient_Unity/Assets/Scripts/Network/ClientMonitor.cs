using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RpcHandler.User;
using RpcHandler.Client;
using RpcHandler.Message;

public class ClientMonitor : ClientExport<ClientMonitor>, IMonitor
{
    public ClientMonitor(string ip_server, int port): base(ip_server, port, "TestServer") { }

    public void Chat(string title, string mess)
    {
        RPC(nameof(Chat), title, mess);
    }

    public void DisableProcessWithID(int id)
    {
        RPC(nameof(DisableProcessWithID), id);
    }

    public string GetAllCurrentProcess()
    {
        RPC(nameof(GetAllCurrentProcess));
        return null;
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
