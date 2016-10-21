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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridView_Host_List = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_Start_ArpSpoof = new System.Windows.Forms.Button();
            this.btn_Start_Scan = new System.Windows.Forms.Button();
            this.dataGridView_Attack_List = new System.Windows.Forms.DataGridView();
            this.btn_Move = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Host_List)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Attack_List)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView_Host_List
            // 
            this.dataGridView_Host_List.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView_Host_List.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllHeaders;
            this.dataGridView_Host_List.Location = new System.Drawing.Point(23, 22);
            this.dataGridView_Host_List.Name = "dataGridView_Host_List";
            this.dataGridView_Host_List.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.dataGridView_Host_List.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView_Host_List.RowTemplate.Height = 30;
            this.dataGridView_Host_List.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView_Host_List.Size = new System.Drawing.Size(587, 315);
            this.dataGridView_Host_List.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btn_Move);
            this.panel1.Controls.Add(this.dataGridView_Host_List);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(665, 337);
            this.panel1.TabIndex = 3;
            // 
            // btn_Start_ArpSpoof
            // 
            this.btn_Start_ArpSpoof.AccessibleName = "btn_Select_All";
            this.btn_Start_ArpSpoof.Location = new System.Drawing.Point(647, 403);
            this.btn_Start_ArpSpoof.Name = "btn_Start_ArpSpoof";
            this.btn_Start_ArpSpoof.Size = new System.Drawing.Size(161, 37);
            this.btn_Start_ArpSpoof.TabIndex = 5;
            this.btn_Start_ArpSpoof.Text = "Start Attack";
            this.btn_Start_ArpSpoof.UseVisualStyleBackColor = true;
            this.btn_Start_ArpSpoof.Click += new System.EventHandler(this.btn_select_Click);
            // 
            // btn_Start_Scan
            // 
            this.btn_Start_Scan.AccessibleName = "btn_Start_Scan";
            this.btn_Start_Scan.Location = new System.Drawing.Point(449, 403);
            this.btn_Start_Scan.Name = "btn_Start_Scan";
            this.btn_Start_Scan.Size = new System.Drawing.Size(161, 37);
            this.btn_Start_Scan.TabIndex = 6;
            this.btn_Start_Scan.Text = "Scan";
            this.btn_Start_Scan.UseVisualStyleBackColor = true;
            this.btn_Start_Scan.Click += new System.EventHandler(this.btn_start_scan_Click);
            // 
            // dataGridView_Attack_List
            // 
            this.dataGridView_Attack_List.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView_Attack_List.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllHeaders;
            this.dataGridView_Attack_List.Location = new System.Drawing.Point(671, 22);
            this.dataGridView_Attack_List.Name = "dataGridView_Attack_List";
            this.dataGridView_Attack_List.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            this.dataGridView_Attack_List.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView_Attack_List.RowTemplate.Height = 30;
            this.dataGridView_Attack_List.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView_Attack_List.Size = new System.Drawing.Size(584, 315);
            this.dataGridView_Attack_List.TabIndex = 3;
            // 
            // btn_Move
            // 
            this.btn_Move.Location = new System.Drawing.Point(616, 153);
            this.btn_Move.Name = "btn_Move";
            this.btn_Move.Size = new System.Drawing.Size(48, 43);
            this.btn_Move.TabIndex = 3;
            this.btn_Move.Text = "->";
            this.btn_Move.UseVisualStyleBackColor = true;
            this.btn_Move.Click += new System.EventHandler(this.btn_Move_Click);
            // 
            // AttackList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1301, 452);
            this.Controls.Add(this.dataGridView_Attack_List);
            this.Controls.Add(this.btn_Start_Scan);
            this.Controls.Add(this.btn_Start_ArpSpoof);
            this.Controls.Add(this.panel1);
            this.Name = "AttackList";
            this.Text = "AttackList";
            this.Load += new System.EventHandler(this.AttackList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Host_List)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Attack_List)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView_Host_List;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn_Start_ArpSpoof;
        private System.Windows.Forms.Button btn_Start_Scan;
        private System.Windows.Forms.DataGridView dataGridView_Attack_List;
        private System.Windows.Forms.Button btn_Move;

    }
}
