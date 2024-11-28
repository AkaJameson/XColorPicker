using System;
using System.Drawing;
using System.Windows.Forms;

namespace XColorPicker.Component
{
    public class OverlayForm : Form
    {
        private CustomLabel colorLabel;
        private Bitmap screenSnapshot; // 屏幕快照
        private PictureBox magnifierBox;
        private int magnificationFactor = 2; // 放大倍数
        private int magnifierSize = 50;    // 放大镜的大小
        private int crosshairSize = 50;     // 十字大小
        private Color crosshairColor = Color.Yellow; // 十字颜色
        public OverlayForm()
        {
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            BackColor = Color.Gray;
            TopMost = true;
            ShowInTaskbar = false;
            DoubleBuffered = true;                   // 双缓冲，避免闪烁
            GetScreenSnapshot();
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            colorLabel = new CustomLabel
            {
                AutoSize = true,
                ForeColor = Color.Black,
                Font = new Font("Arial", 8),
                BorderStyle = BorderStyle.None,
                BackColor = Color.Transparent,   // 背景透明
                Location = new Point(10, 10)
            };
            magnifierBox = new PictureBox
            {
                Size = new Size(magnifierSize * magnificationFactor, magnifierSize * magnificationFactor),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.Transparent
            };
            Controls.Add(colorLabel);
            Controls.Add(magnifierBox);
            this.Cursor = Cursors.Cross;
        }

        private void RefreshMagnifier()
        {
            // 获取鼠标位置
            Point cursorPosition = Cursor.Position;

            // 计算放大镜区域
            Rectangle captureRect = new Rectangle(
                cursorPosition.X - magnifierSize / 2,
                cursorPosition.Y - magnifierSize / 2,
                magnifierSize,
                magnifierSize
            );

            // 捕获鼠标附近的屏幕区域
            Bitmap capturedBitmap = new Bitmap(captureRect.Width, captureRect.Height);
            using (Graphics g = Graphics.FromImage(capturedBitmap))
            {
                g.CopyFromScreen(captureRect.Location, Point.Empty, captureRect.Size);
            }

            // 将捕获的图像放大并显示
            Bitmap magnifiedBitmap = new Bitmap(capturedBitmap, capturedBitmap.Width * magnificationFactor, capturedBitmap.Height * magnificationFactor);

            // 在放大镜图像上绘制十字
            using (Graphics g = Graphics.FromImage(magnifiedBitmap))
            {
                g.DrawLine(new Pen(crosshairColor, 1),
                           new Point(magnifiedBitmap.Width / 2 - crosshairSize, magnifiedBitmap.Height / 2),
                           new Point(magnifiedBitmap.Width / 2 + crosshairSize, magnifiedBitmap.Height / 2));

                g.DrawLine(new Pen(crosshairColor, 1),
                           new Point(magnifiedBitmap.Width / 2, magnifiedBitmap.Height / 2 - crosshairSize),
                           new Point(magnifiedBitmap.Width / 2, magnifiedBitmap.Height / 2 + crosshairSize));
            }

            magnifierBox.Image?.Dispose();
            magnifierBox.Image = magnifiedBitmap;

            capturedBitmap.Dispose();
        }

        public void UpdateMouseColor(Color color)
        {
            colorLabel.Text = $"#{color.R:X2}{color.G:X2}{color.B:X2}";
            colorLabel.Location = new Point(Cursor.Position.X+40, Cursor.Position.Y -25);
            // 更新放大镜内容
            RefreshMagnifier();
            // 更新放大镜位置
            magnifierBox.Location = new Point(Cursor.Position.X + 40, Cursor.Position.Y);
        }
        private void GetScreenSnapshot()
        {
            // 获取屏幕快照
            var screenBounds = Screen.PrimaryScreen.Bounds;
            screenSnapshot = new Bitmap(screenBounds.Width, screenBounds.Height);
            using (var g = Graphics.FromImage(screenSnapshot))
            {
                g.CopyFromScreen(screenBounds.Location, Point.Empty, screenBounds.Size);
            }
        }
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            if (screenSnapshot != null)
            {
                e.Graphics.DrawImage(screenSnapshot, Point.Empty);
            }
            else
            {
                base.OnPaintBackground(e);
            }
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            screenSnapshot?.Dispose();
            magnifierBox.Image?.Dispose();
            base.OnFormClosed(e);
        }
    }
}
