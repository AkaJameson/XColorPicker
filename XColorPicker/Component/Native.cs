using System;
using System.Windows.Forms;
using XColorPicker;
using XColorPicker.Core;
using XColorPicker.Core.Abstraction;

namespace ColorPickor
{
    public partial class Native : NativeWindow, IApplicationExit
    {
        public Native()
        {
            CreateHandle(new CreateParams());
            HotKeyManager.RegisterAltEKey(this.Handle);
        }
        protected override void WndProc(ref Message m)
        {
            const int WM_HOTKEY = 0x0312;
            if (m.Msg == WM_HOTKEY)
            {
                int id = m.WParam.ToInt32();
                if (id == HotKeyManager.HotKeyId)
                {
                    FormManager.Instance.altEPressProcessor.OnAltEPressed();
                }
            }
            base.WndProc(ref m);
        }
        public void Exit(object sender, EventArgs e)
        {
            HotKeyManager.UnregisterAltEKey();
            DestroyHandle();
        }
    }
}
