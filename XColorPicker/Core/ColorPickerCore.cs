using System.Drawing;
using System.Windows.Forms;
using XColorPicker.Abstraction;
using XColorPicker.Component;
namespace XColorPicker.Core
{
    public class ColorPickerCore:IAltEPressProcessor
    {
        private OverlayForm overlayForm; // 用于屏幕变灰的覆盖窗体
        private bool isPicking;          // 标记是否正在取色
        private Timer updateTimer = new Timer();       // 用于更新鼠标颜色的定时器
        public ColorPickerCore()
        {
            
        }
        public void OnAltEPressed()
        {
            if (isPicking)
            {
                OnCancel(); // 如果已经在取色，重复按下 Alt+E 就取消
                return;
            }
            isPicking = true;
            overlayForm = new OverlayForm();
            overlayForm.Show();
            updateTimer = new Timer { Interval = 50 };
            updateTimer.Tick += (s, e) => UpdateMouseColor();
            updateTimer.Start();
            // 监听鼠标点击事件
            overlayForm.MouseClick += OnMouseClick;
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
            if (updateTimer != null)
            {
                updateTimer.Stop();
                updateTimer.Dispose();
                updateTimer = null;
            }

            if (overlayForm != null)
            {
                overlayForm.MouseClick -= OnMouseClick;
                overlayForm.Close();
                overlayForm.Dispose();
                overlayForm = null;
            }
        }
        private void UpdateMouseColor()
        {
            if (!isPicking) return;

            var color = GetColorAtCursor();
            overlayForm.UpdateMouseColor(color); // 显示颜色到鼠标旁
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
