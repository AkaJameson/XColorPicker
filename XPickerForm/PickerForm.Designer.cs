using System.Drawing;
using System.IO;
using System;
using System.Windows.Forms;
using XColorPicker.Core;

namespace XPickerForm
{
    partial class PickerForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(0,0);
            this.Text = "XColorPicker";
            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            notifyIcon = new NotifyIcon
            {
                Icon = GetIcon(),
                Visible = true,
                Text = "ColorPicker"
            };
            contextMenuStrip = new ContextMenuStrip();
            contextMenuStrip.Items.Add("Alt+E 呼出颜色选择器");
            contextMenuStrip.Items.Add("再次Alt+E 退出选中");
            contextMenuStrip.Items.Add("退出",null,(s,e) => { notifyIcon.Visible = false; Application.Exit(); });
            notifyIcon.ContextMenuStrip = contextMenuStrip;
            notifyIcon.MouseClick += NotifyMouseLeftClick;

        }
        private void NotifyMouseLeftClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ColorPickerCore.Instance.OnAltEPressed();
            }
        }
        #endregion

        #region 自定义变量
        private NotifyIcon notifyIcon;
        private ContextMenuStrip contextMenuStrip;
        #endregion
        /// <summary>
        /// 获取程序图标
        /// </summary>
        /// <returns></returns>
        private Icon GetIcon()
        {
            try
            {
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

