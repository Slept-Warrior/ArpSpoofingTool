namespace Sniffer
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.(?)
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources that used.
        /// </summary>
        /// <param name="disposing">True if a Resource is need to be deleted, false otherwise</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Genereted Codes

        /// <summary>
        /// Generated Codes for IDE Support - Do Not Edit
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_Capture_Option = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_Arp_Attack = new System.Windows.Forms.Button();
            this.btn_HackCookie_Start = new System.Windows.Forms.Button();
            this.listView = new System.Windows.Forms.ListView();
            this.btn_reply = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_Capture_Option
            // 
            this.btn_Capture_Option.Location = new System.Drawing.Point(159, 0);
            this.btn_Capture_Option.Name = "btn_Capture_Option";
            this.btn_Capture_Option.Size = new System.Drawing.Size(37, 36);
            this.btn_Capture_Option.TabIndex = 0;
            this.btn_Capture_Option.Text = "D";
            this.btn_Capture_Option.UseVisualStyleBackColor = true;
            this.btn_Capture_Option.Click += new System.EventHandler(this.btn_capture_option_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btn_reply);
            this.panel1.Controls.Add(this.btn_Arp_Attack);
            this.panel1.Controls.Add(this.btn_HackCookie_Start);
            this.panel1.Controls.Add(this.btn_Capture_Option);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1187, 54);
            this.panel1.TabIndex = 2;
            // 
            // btn_Arp_Attack
            // 
            this.btn_Arp_Attack.Location = new System.Drawing.Point(116, 0);
            this.btn_Arp_Attack.Name = "btn_Arp_Attack";
            this.btn_Arp_Attack.Size = new System.Drawing.Size(37, 36);
            this.btn_Arp_Attack.TabIndex = 2;
            this.btn_Arp_Attack.Text = "L";
            this.btn_Arp_Attack.UseVisualStyleBackColor = true;
            this.btn_Arp_Attack.Click += new System.EventHandler(this.btn_arp_attack_Click);
            // 
            // btn_HackCookie_Start
            // 
            this.btn_HackCookie_Start.Location = new System.Drawing.Point(3, 3);
            this.btn_HackCookie_Start.Name = "btn_HackCookie_Start";
            this.btn_HackCookie_Start.Size = new System.Drawing.Size(37, 36);
            this.btn_HackCookie_Start.TabIndex = 1;
            this.btn_HackCookie_Start.Text = "S";
            this.btn_HackCookie_Start.UseVisualStyleBackColor = true;
            this.btn_HackCookie_Start.Click += new System.EventHandler(this.btn_capture_start_Click);
            // 
            // listView
            // 
            this.listView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView.FullRowSelect = true;
            this.listView.Location = new System.Drawing.Point(0, 54);
            this.listView.Name = "listView";
            this.listView.Size = new System.Drawing.Size(1187, 641);
            this.listView.TabIndex = 3;
            this.listView.UseCompatibleStateImageBehavior = false;
            this.listView.DoubleClick += new System.EventHandler(this.listView_DoubleClick);
            // 
            // btn_reply
            // 
            this.btn_reply.Location = new System.Drawing.Point(73, 0);
            this.btn_reply.Name = "btn_reply";
            this.btn_reply.Size = new System.Drawing.Size(37, 36);
            this.btn_reply.TabIndex = 3;
            this.btn_reply.Text = "R";
            this.btn_reply.UseVisualStyleBackColor = true;
            this.btn_reply.Click += new System.EventHandler(this.btn_reply_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1187, 695);
            this.Controls.Add(this.listView);
            this.Controls.Add(this.panel1);
            this.Name = "Main";
            this.Text = "Main";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_Capture_Option;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn_HackCookie_Start;
        private System.Windows.Forms.ListView listView;
        private System.Windows.Forms.Button btn_Arp_Attack;
        private System.Windows.Forms.Button btn_reply;
    }
}

