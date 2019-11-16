using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.NetworkInformation;

public class NetworkManager
{
    public static NetworkManager Instance;
    private ClientMonitor clientMonitor;
    private static int PORT = 9889;
    public ClientMonitor ClientMonitor { get { return clientMonitor; } }
    public static void Init(string ip_server="", int port = 9889)
    {
        Instance = new NetworkManager();
        PORT = port;
        Instance.clientMonitor = new ClientMonitor(ip_server, PORT);
        Instance.ScanIP();
    }
    public void ScanIP()
    {
        IPAddress[] ips = Dns.GetHostAddresses(Dns.GetHostName());
        string my_ip = "1.1.1.1";
        string[] sub_ip = new string[4];
        foreach(IPAddress ip in ips)
        {
            if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
            {
                my_ip = ip.ToString();
                sub_ip = my_ip.Split('.');
            }
        }
        for(int i=1; i<=255; i++)
        {
            string ip = sub_ip[0] + "." + sub_ip[1] + "." + sub_ip[2] + "." + i;
            clientMonitor.SetAddress(ip, PORT);
            clientMonitor.Ping();
        }
    }
    public void LocalScan()
    {
        clientMonitor.SetAddress("127.0.0.1", PORT);
        clientMonitor.Ping();
    }
    public void GetAllCurrentProcess(string ip, bool isLocalHost=false)
    {
        clientMonitor.SetAddress(isLocalHost ? "127.0.0.1" : ip, PORT);
        clientMonitor.GetAllCurrentProcess();
    }
    public void EnableClipboard(bool enable, string ip, bool isLocalHost= false)
    {
        clientMonitor.SetAddress(isLocalHost ? "127.0.0.1" : ip, PORT);
        clientMonitor.SetClipboard(enable);
    }
    public void EnableUSB(bool enabel, string ip, bool isLocalHost= false)
    {
        clientMonitor.SetAddress(isLocalHost ? "127.0.0.1" : ip, PORT);
        clientMonitor.SetUSB(enabel);
    }
    public void ShutdowProcess(int id, string ip, bool isLocalHost = false)
    {
        clientMonitor.SetAddress(isLocalHost ? "127.0.0.1" : ip, PORT);
        clientMonitor.DisableProcessWithID(id);
    }
    public void Chat(string title, string mess, string ip, bool isLocalHost= false)
    {
        clientMonitor.SetAddress(isLocalHost ? "127.0.0.1" : ip, PORT);
        clientMonitor.Chat(title, mess);
    }
    public void EnableShareFile(bool enable, string ip, bool isLocalHost= false)
    {
        clientMonitor.SetAddress(isLocalHost ? "127.0.0.1" : ip, PORT);
        clientMonitor.SetFileShare(enable);
    }
}
