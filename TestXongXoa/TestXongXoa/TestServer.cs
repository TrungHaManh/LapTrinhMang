using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RpcHandler;

namespace TestXongXoa
{
    public class TestServer : RpcHandler.User.IMonitor
    {
        public const string DisableDiscoveryCmd = "cmd /c \"netsh advfirewall firewall set rule group=\"Network Discovery\" new enable=No\"";
        public const string EnableDiscoveryCmd =  "cmd /c \"netsh advfirewall firewall set rule group=\"Network Discovery\" new enable=Yes\"";
        public void Chat(string title, string mess)
        {
            NotifyIcon notifyIcon = new NotifyIcon();
            notifyIcon.Icon = SystemIcons.WinLogo;
            notifyIcon.BalloonTipIcon = ToolTipIcon.Info;
            notifyIcon.BalloonTipText = mess;
            notifyIcon.BalloonTipTitle = title;
            notifyIcon.Visible = true;
            notifyIcon.ShowBalloonTip(100);
        }

        public void DisableProcessWithID(int id)
        {
            try
            {
                Process process = Process.GetProcessById(id);
                process.CloseMainWindow();
                process.WaitForExit();
            }
            catch (ArgumentException e)
            {
                return;
            }
        }

        public string GetAllCurrentProcess()
        {
            Dictionary<int, string> result = new Dictionary<int, string>();
            Process[] processes = Process.GetProcesses();
            foreach (Process process in processes)
            {
                if (process.MainWindowTitle.Length > 0)
                    result.Add(process.Id, process.ProcessName);
            }
            return MiniJSON.Json.Serialize(result);
        }

        public string Ping()
        {
            string name = Dns.GetHostName();
            string ip_string = "?.?.?.?";
            IPAddress[] ips = Dns.GetHostAddresses(name);
            foreach (IPAddress ip in ips)
            {
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    ip_string = ip.ToString();
            }
            return ip_string + "/" + name;
        }

        public void SetClipboard(bool enable)
        {
            ClipBoardHandler.Instance.Enable = enable;
            Chat("THÔNG BÁO", enable ? "Đã bật chức năng clipboard!" : "Đã tắt chức năng clipboard!");
        }

        public void SetFileShare(bool enable)
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = enable ? EnableDiscoveryCmd : DisableDiscoveryCmd;
            startInfo.Verb = "runas";
            process.StartInfo = startInfo;
            process.Start();

            Chat("THÔNG BÁO", enable ? "Đã bật chức năng share file!" : "Đã tắt chức năng share file!");
        }

        public void SetUSB(bool enable)
        {
            UsbHandler usb = new UsbHandler();
            usb.SetUSB(enable);
            Chat("THÔNG BÁO", enable ? "Đã bật kết nối USB!" : "Đã tắt kết nối USB!");
        }
    }
}
