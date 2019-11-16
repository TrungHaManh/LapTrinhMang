using System;
using System.Management;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace TestXongXoa
{
    public class UsbHandler
    {

        [DllImport("shell32")]
        static extern bool IsUserAnAdmin();
        public static Int32 ENABLE_USB_WORD = 3;
        public static Int32 DISABLE_USB_WORD = 4;
        public enum EventType
        {
            Inserted = 2,
            Removed = 3
        }
        public delegate void EventResult(bool _result);
        public EventResult MyEventResult;

        private bool isAdmin = false;
        private RegistryKey registry;
        private string Regpath = "System\\CurrentControlSet\\Services\\USBSTOR";
        public UsbHandler()
        {
            isAdmin = IsUserAnAdmin();
            if (isAdmin)
            {
                registry = Registry.LocalMachine.OpenSubKey(Regpath, true);
            }
        }
        /// <summary>
        /// Đợi sự kiện usb
        /// </summary>
        /// <param name="callbackFunction">hàm trả về của sự kiện</param>
        //public void WaitForUsb(EventResult callbackFunction)
        //{
        //    if (!isAdmin) return;
        //    MyEventResult = callbackFunction;
        //    ManagementEventWatcher watcher = new ManagementEventWatcher();
        //    WqlEventQuery query = new WqlEventQuery("SELECT * FROM Win32_VolumeChangeEvent WHERE EventType = 2 or EventType = 3");

        //    watcher.EventArrived += (s, e) =>
        //    {
        //        string driveName = e.NewEvent.Properties["DriveName"].Value.ToString();
        //        EventType eventType = (EventType)(Convert.ToInt16(e.NewEvent.Properties["EventType"].Value));

        //        string eventName = Enum.GetName(typeof(EventType), eventType);

        //        //Console.WriteLine("{0}: {1} {2}", DateTime.Now, driveName, eventName);
        //        if (eventName == "Inserted") MyEventResult.Invoke(true);
        //        else if (eventName == "Removed") MyEventResult.Invoke(false);

        //    };

        //    watcher.Query = query;
        //    watcher.Start();
        //}

        /// <summary>
        /// Bật/Tắt kết nói USB
        /// </summary>
        /// <param name="enable"></param>
        public void SetUSB(bool enable)
        {
            if (isAdmin && registry != null)
            {
                registry.SetValue("Start", enable ? ENABLE_USB_WORD : DISABLE_USB_WORD);
            }
        }
    }
}
