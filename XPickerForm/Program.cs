using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using XColorPicker.Core;

namespace XPickerForm
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            // 禁用 DPI 缩放
            if (Environment.OSVersion.Version.Major >= 6) SetProcessDPIAware();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var core = ColorPickerCore.Instance;
            Application.Run(new PickerForm());
        }

        [DllImport("user32.dll")]
        private static extern bool SetProcessDPIAware();
    }
}
