using System.Windows.Forms;
using XColorPicker.Core;

namespace ColorPickor
{
    public partial class Native : NativeWindow
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
                    ColorPickerCore.Instance.OnAltEPressed();
                }
            }
            base.WndProc(ref m);
        }
    }
}
