using System.Drawing;
using System.Windows.Forms;
using XColorPicker.Component;

namespace XPickerForm.Component
{
    public partial class OverlayForm : Form
    {
        private CustomLabel colorLabel;
        private Bitmap screenSnapshot; // 屏幕快照
        private PictureBox magnifierBox;
        private int magnificationFactor = 2; // 放大倍数
        private int magnifierSize = 50;    // 放大镜的大小
        private int crosshairSize = 50;     // 十字大小
        private Color crosshairColor = Color.Yellow; // 十字颜色
        private Timer timer = new Timer { Interval = 50 }; 
        public OverlayForm()
        {
            InitializeComponent();
            timer.Tick += (s, e) =>
            {
                UpdateMouse(GetColorAtCursor());
            };
            HideWindow();
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
        private void UpdateMouse(Color color)
        {
            colorLabel.Text = $"#{color.R:X2}{color.G:X2}{color.B:X2}";
            colorLabel.Location = new Point(Cursor.Position.X + 40, Cursor.Position.Y - 25);
            // 更新放大镜内容
            RefreshMagnifier();
            // 更新放大镜位置
            magnifierBox.Location = new Point(Cursor.Position.X + 40, Cursor.Position.Y);
        }
        public void HideWindow()
        {
            // 隐藏放大镜
            magnifierBox.Visible = false;
            // 隐藏十字
            this.Cursor = Cursors.Default;
            screenSnapshot?.Dispose();
            screenSnapshot = null;
            timer.Stop();
            // 隐藏窗口
            this.Hide();
        }
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            if (screenSnapshot != null)
            {
                // 绘制屏幕快照作为窗体背景
                e.Graphics.DrawImage(screenSnapshot, Point.Empty);
            }
            else
            {
                base.OnPaintBackground(e);
            }
        }

        public void ShowWindow()
        {
            this.Cursor = Cursors.Cross;
            // 显示放大镜
            magnifierBox.Visible = true;
            GetScreenSnapshot();
            this.Show();
            //强制重绘
            this.Invalidate();
            timer.Start();
        }
    }
}
