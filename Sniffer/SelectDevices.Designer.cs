namespace Sniffer
{
    partial class SelectDevices
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
            this.checkedListBox_Devices = new System.Windows.Forms.CheckedListBox();
            this.btn_select = new System.Windows.Forms.Button();
            this.btn_close = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // checkedListBox_Devices
            // 
            this.checkedListBox_Devices.Dock = System.Windows.Forms.DockStyle.Top;
            this.checkedListBox_Devices.FormattingEnabled = true;
            this.checkedListBox_Devices.Location = new System.Drawing.Point(0, 0);
            this.checkedListBox_Devices.Name = "checkedListBox_Devices";
            this.checkedListBox_Devices.Size = new System.Drawing.Size(885, 326);
            this.checkedListBox_Devices.TabIndex = 0;
            // 
            // btn_select
            // 
            this.btn_select.Location = new System.Drawing.Point(262, 345);
            this.btn_select.Name = "btn_select";
            this.btn_select.Size = new System.Drawing.Size(132, 40);
            this.btn_select.TabIndex = 1;
            this.btn_select.Text = "선택";
            this.btn_select.UseVisualStyleBackColor = true;
            this.btn_select.Click += new System.EventHandler(this.btn_start_Click);
            // 
            // btn_close
            // 
            this.btn_close.Location = new System.Drawing.Point(480, 345);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(132, 40);
            this.btn_close.TabIndex = 2;
            this.btn_close.Text = "종료";
            this.btn_close.UseVisualStyleBackColor = true;
            // 
            // SelectDevices
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(885, 397);
            this.Controls.Add(this.btn_close);
            this.Controls.Add(this.btn_select);
            this.Controls.Add(this.checkedListBox_Devices);
            this.Name = "SelectDevices";
            this.Text = "CaptureOption";
            this.Load += new System.EventHandler(this.CaptureOption_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckedListBox checkedListBox_Devices;
        private System.Windows.Forms.Button btn_select;
        private System.Windows.Forms.Button btn_close;

    }
}