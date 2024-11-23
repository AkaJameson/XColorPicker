using System.Drawing;
using System.Windows.Forms;
using XColorPicker.Core.Abstraction;

namespace XColorPicker.Component
{
    internal class CustomLabel : Label
    {
        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                cp.ExStyle &= ~0x20; // 取消 WS_EX_TRANSPARENT 属性
                return cp;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            // 开启抗锯齿
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            // 绘制文字
            using (var textBrush = new SolidBrush(ForeColor))
            {
                e.Graphics.DrawString(Text, Font, textBrush, new PointF(0, 0));
            }
        }
    }
}
