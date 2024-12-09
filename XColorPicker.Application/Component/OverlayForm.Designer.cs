using System.Drawing;
using System.Windows.Forms;
using XColorPicker.Component;

namespace XPickerForm.Component
{
    partial class OverlayForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Text = "OverLayForm";
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            BackColor = Color.Gray;
            TopMost = true;
            ShowInTaskbar = false;
            DoubleBuffered = true;
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
        }


        #endregion
    }
}