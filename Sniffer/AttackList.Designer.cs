namespace Sniffer
{
    partial class AttackList
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_startArpSpoof = new System.Windows.Forms.Button();
            this.btn_start_scan = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView
            // 
            this.dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView.Location = new System.Drawing.Point(0, 0);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.dataGridView.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView.RowTemplate.Height = 30;
            this.dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView.Size = new System.Drawing.Size(1253, 309);
            this.dataGridView.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dataGridView);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1253, 309);
            this.panel1.TabIndex = 3;
            // 
            // btn_startArpSpoof
            // 
            this.btn_startArpSpoof.AccessibleName = "btn_Select_All";
            this.btn_startArpSpoof.Location = new System.Drawing.Point(636, 343);
            this.btn_startArpSpoof.Name = "btn_startArpSpoof";
            this.btn_startArpSpoof.Size = new System.Drawing.Size(161, 37);
            this.btn_startArpSpoof.TabIndex = 5;
            this.btn_startArpSpoof.Text = "공격 시작";
            this.btn_startArpSpoof.UseVisualStyleBackColor = true;
            this.btn_startArpSpoof.Click += new System.EventHandler(this.btn_select_Click);
            // 
            // btn_start_scan
            // 
            this.btn_start_scan.AccessibleName = "btn_Start_Scan";
            this.btn_start_scan.Location = new System.Drawing.Point(442, 343);
            this.btn_start_scan.Name = "btn_start_scan";
            this.btn_start_scan.Size = new System.Drawing.Size(161, 37);
            this.btn_start_scan.TabIndex = 6;
            this.btn_start_scan.Text = "스캔 시작";
            this.btn_start_scan.UseVisualStyleBackColor = true;
            this.btn_start_scan.Click += new System.EventHandler(this.btn_start_scan_Click);
            // 
            // AttackList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1253, 423);
            this.Controls.Add(this.btn_start_scan);
            this.Controls.Add(this.btn_startArpSpoof);
            this.Controls.Add(this.panel1);
            this.Name = "AttackList";
            this.Text = "AttackList";
            this.Load += new System.EventHandler(this.AttackList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn_startArpSpoof;
        private System.Windows.Forms.Button btn_start_scan;

    }
}