namespace Sniffer
{
    partial class Main
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다.
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_capture_option = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_capture_start = new System.Windows.Forms.Button();
            this.listView = new System.Windows.Forms.ListView();
            this.btn_arp_attack = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_capture_option
            // 
            this.btn_capture_option.Location = new System.Drawing.Point(159, 0);
            this.btn_capture_option.Name = "btn_capture_option";
            this.btn_capture_option.Size = new System.Drawing.Size(37, 36);
            this.btn_capture_option.TabIndex = 0;
            this.btn_capture_option.Text = "D";
            this.btn_capture_option.UseVisualStyleBackColor = true;
            this.btn_capture_option.Click += new System.EventHandler(this.btn_capture_option_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btn_arp_attack);
            this.panel1.Controls.Add(this.btn_capture_start);
            this.panel1.Controls.Add(this.btn_capture_option);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1187, 54);
            this.panel1.TabIndex = 2;
            // 
            // btn_capture_start
            // 
            this.btn_capture_start.Location = new System.Drawing.Point(3, 3);
            this.btn_capture_start.Name = "btn_capture_start";
            this.btn_capture_start.Size = new System.Drawing.Size(37, 36);
            this.btn_capture_start.TabIndex = 1;
            this.btn_capture_start.Text = "S";
            this.btn_capture_start.UseVisualStyleBackColor = true;
            this.btn_capture_start.Click += new System.EventHandler(this.btn_capture_start_Click);
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
            // btn_arp_attack
            // 
            this.btn_arp_attack.Location = new System.Drawing.Point(116, 0);
            this.btn_arp_attack.Name = "btn_arp_attack";
            this.btn_arp_attack.Size = new System.Drawing.Size(37, 36);
            this.btn_arp_attack.TabIndex = 2;
            this.btn_arp_attack.Text = "L";
            this.btn_arp_attack.UseVisualStyleBackColor = true;
            this.btn_arp_attack.Click += new System.EventHandler(this.btn_arp_attack_Click);
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

        private System.Windows.Forms.Button btn_capture_option;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn_capture_start;
        private System.Windows.Forms.ListView listView;
        private System.Windows.Forms.Button btn_arp_attack;
    }
}

