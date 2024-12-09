using ColorPickor;
using System;
using System.Drawing;
using System.Windows.Forms;
using XColorPicker.Abstraction;
using XPickerForm.Component;
namespace XColorPicker.Core
{
    public class ColorPickerCore : IAltEPressProcessor
    {
        private static Lazy<ColorPickerCore> _instance = new Lazy<ColorPickerCore>(() => new ColorPickerCore());
        public static ColorPickerCore Instance { get { return _instance.Value; } }

        private bool isPicking = false;

        public OverlayForm overlayForm;
        public Native native;
        private ColorPickerCore()
        {
            overlayForm = new OverlayForm();
            native = new Native();
            // 监听鼠标点击事件
            overlayForm.MouseClick += OnMouseClick;
        }
        public void OnAltEPressed()
        {
            if (isPicking)
            {
                if (isPicking)
                {
                    OnCancel(); // 如果已经在取色，重复按下 Alt+E 就取消
                    return;
                }
            }
            overlayForm.ShowWindow();
            isPicking = true;
        }
        private void OnMouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                // 左键点击，取色并复制到剪贴板
                var color = GetColorAtCursor();
                Clipboard.SetText($"#{color.R:X2}{color.G:X2}{color.B:X2}");
                OnCancel();
            }
            else if (e.Button == MouseButtons.Right)
            {
                OnCancel();
            }
        }

        public void OnCancel()
        {
            if (!isPicking) return;
            isPicking = false;
            overlayForm.HideWindow();
            GC.Collect();

        }
        private Color GetColorAtCursor()
        {
            // 获取鼠标所在位置的颜色
            var cursorPos = Cursor.Position;
            using (var bitmap = new Bitmap(1, 1))
            {
                using (var g = Graphics.FromImage(bitmap))
                {
                    g.CopyFromScreen(cursorPos, Point.Empty, new Size(1, 1));
                }
                return bitmap.GetPixel(0, 0);
            }
        }


    }
}
