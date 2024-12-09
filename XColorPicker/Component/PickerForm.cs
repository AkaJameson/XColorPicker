using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using XColorPicker;

namespace ColorPickor
{
    public partial class PickerForm : Form
    {
        private NotifyIcon notifyIcon;
        private ContextMenuStrip contextMenuStrip;
        public PickerForm()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            notifyIcon = new NotifyIcon
            {
                Icon = GetIcon(),
                Visible = true,
                Text = "ColorPicker"
            };
            contextMenuStrip = new ContextMenuStrip();
            contextMenuStrip.Items.Add("Alt+E 呼出颜色选择器");
            contextMenuStrip.Items.Add("再次Alt+E 退出选中");
            contextMenuStrip.Items.Add("退出", null, FormManager.Instance.ExitApplication!);
            notifyIcon.ContextMenuStrip = contextMenuStrip;
            notifyIcon.MouseClick += NotifyMouseLeftClick!;
        }

        private void NotifyMouseLeftClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                FormManager.Instance.altEPressProcessor.OnAltEPressed();
            }
        }
        #region event
        /// <summary>
        /// 窗体最小化事件
        /// </summary>
        /// <param name="e"></param>
        
        protected override void OnMinimumSizeChanged(EventArgs e)
        {
            this.ShowInTaskbar = false;
            base.OnMinimumSizeChanged(e);
        }
        #endregion
        /// <summary>
        /// 获取程序图标
        /// </summary>
        /// <returns></returns>
        private Icon GetIcon()
        {
            try{
                string iconPath = Path.Combine(Application.StartupPath, "Resources", "ColorPicker.ico");
                // 从文件中加载 ICO 文件并返回 Icon 对象
                Icon ico = new Icon(iconPath);
                return ico;
            }
            catch (Exception ex)
            {
                return SystemIcons.Application;
            }
        }
    }
}
