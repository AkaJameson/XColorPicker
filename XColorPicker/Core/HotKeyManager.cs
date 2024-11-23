using System;
using System.Runtime.InteropServices;
namespace XColorPicker.Core
{
    public class HotKeyManager
    {
        // 注册全局快捷键
        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

        // 注销全局快捷键
        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        public static event EventHandler HotKeyPressed;

        private const uint MOD_ALT = 0x0001; // Alt 键修饰符

        private const uint VK_E = 0x45; // E 键虚拟键码

        private const int HOTKEY_ID = 9000128; // 全局快捷键 ID
        public static int HotKeyId { get { return HOTKEY_ID; } }

        private static IntPtr? _formHandle;
        /// <summary>
        /// 注册 Alt+E 全局快捷键
        /// </summary>
        /// <param name="formHandle"></param>
        /// <exception cref="Exception"></exception>
        public static void RegisterAltEKey(IntPtr formHandle)
        {
            
            if(!RegisterHotKey(formHandle, HOTKEY_ID, MOD_ALT, VK_E))
            {
                throw new Exception("注册全局快捷键失败！");
            }
            _formHandle = formHandle;
        }
        /// <summary>
        /// 注销 Alt+E 全局快捷键
        /// </summary>
        /// <exception cref="Exception"></exception>
        public static void UnregisterAltEKey()
        {
            if (_formHandle == null)
            {
                return;
            }
            if (!UnregisterHotKey(_formHandle.Value, HOTKEY_ID))
            {
                throw new Exception("注销全局快捷键失败！");
            }
        }

    }
}
