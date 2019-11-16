using System.Threading;

namespace TestXongXoa
{
    public class ClipBoardHandler
    {
        public static ClipBoardHandler Instance;
        public bool Enable;
        public static void Init()
        {
            Instance = new ClipBoardHandler { Enable = true };
            Instance.Run();
        }

        public void Run()
        {
            Thread thr = new Thread(() => {
                while (true)
                {
                    if (!Enable)
                    {
                        System.Diagnostics.Process process = new System.Diagnostics.Process();
                        System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                        startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                        startInfo.FileName = "cmd.exe";
                        startInfo.Arguments = "cmd /c \"echo off | clip\"";
                        process.StartInfo = startInfo;
                        process.Start();
                    }
                    Thread.Sleep(20);
                }
            });
            thr.IsBackground = true;
            thr.Start();
        }
    }
}
