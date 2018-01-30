namespace SMS_Marketing
{
    partial class Services
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgvSendSMS = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dgvRequest = new System.Windows.Forms.DataGridView();
            this.Sender = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.message = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgvWaitingSMS = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusBar1 = new System.Windows.Forms.StatusBar();
            this.gboConnectionStatus = new System.Windows.Forms.GroupBox();
            this.cbxCOM = new System.Windows.Forms.ComboBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.label23 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblConnectionStatus = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSendSMS)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRequest)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWaitingSMS)).BeginInit();
            this.gboConnectionStatus.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgvSendSMS);
            this.groupBox2.Location = new System.Drawing.Point(389, 224);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(344, 278);
            this.groupBox2.TabIndex = 84;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Send SMS";
            // 
            // dgvSendSMS
            // 
            this.dgvSendSMS.AllowUserToAddRows = false;
            this.dgvSendSMS.AllowUserToDeleteRows = false;
            this.dgvSendSMS.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSendSMS.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4});
            this.dgvSendSMS.Location = new System.Drawing.Point(2, 19);
            this.dgvSendSMS.Name = "dgvSendSMS";
            this.dgvSendSMS.ReadOnly = true;
            this.dgvSendSMS.Size = new System.Drawing.Size(336, 253);
            this.dgvSendSMS.TabIndex = 0;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "SMSsender";
            this.dataGridViewTextBoxColumn3.HeaderText = "Phone Number";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 105;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "SMSmessage";
            this.dataGridViewTextBoxColumn4.HeaderText = "Message";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Width = 185;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dgvRequest);
            this.groupBox3.Location = new System.Drawing.Point(12, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(721, 206);
            this.groupBox3.TabIndex = 86;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Request SMS";
            // 
            // dgvRequest
            // 
            this.dgvRequest.AllowUserToAddRows = false;
            this.dgvRequest.AllowUserToDeleteRows = false;
            this.dgvRequest.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRequest.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Sender,
            this.message});
            this.dgvRequest.Location = new System.Drawing.Point(6, 19);
            this.dgvRequest.Name = "dgvRequest";
            this.dgvRequest.ReadOnly = true;
            this.dgvRequest.Size = new System.Drawing.Size(709, 181);
            this.dgvRequest.TabIndex = 0;
            // 
            // Sender
            // 
            this.Sender.DataPropertyName = "SMSsender";
            this.Sender.HeaderText = "Phone Number";
            this.Sender.Name = "Sender";
            this.Sender.ReadOnly = true;
            this.Sender.Width = 200;
            // 
            // message
            // 
            this.message.DataPropertyName = "SMSmessage";
            this.message.HeaderText = "Message";
            this.message.Name = "message";
            this.message.ReadOnly = true;
            this.message.Width = 462;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgvWaitingSMS);
            this.groupBox1.Location = new System.Drawing.Point(19, 224);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(364, 278);
            this.groupBox1.TabIndex = 87;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Waitting SMS";
            // 
            // dgvWaitingSMS
            // 
            this.dgvWaitingSMS.AllowUserToAddRows = false;
            this.dgvWaitingSMS.AllowUserToDeleteRows = false;
            this.dgvWaitingSMS.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvWaitingSMS.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2});
            this.dgvWaitingSMS.Location = new System.Drawing.Point(6, 19);
            this.dgvWaitingSMS.Name = "dgvWaitingSMS";
            this.dgvWaitingSMS.ReadOnly = true;
            this.dgvWaitingSMS.Size = new System.Drawing.Size(352, 253);
            this.dgvWaitingSMS.TabIndex = 0;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "SMSsender";
            this.dataGridViewTextBoxColumn1.HeaderText = "Phone Number";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 105;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "SMSmessage";
            this.dataGridViewTextBoxColumn2.HeaderText = "Message";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 200;
            // 
            // statusBar1
            // 
            this.statusBar1.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusBar1.Location = new System.Drawing.Point(0, 570);
            this.statusBar1.Name = "statusBar1";
            this.statusBar1.Size = new System.Drawing.Size(745, 20);
            this.statusBar1.TabIndex = 83;
            this.statusBar1.Text = "Message";
            // 
            // gboConnectionStatus
            // 
            this.gboConnectionStatus.BackColor = System.Drawing.Color.Transparent;
            this.gboConnectionStatus.Controls.Add(this.cbxCOM);
            this.gboConnectionStatus.Controls.Add(this.btnOK);
            this.gboConnectionStatus.Controls.Add(this.label23);
            this.gboConnectionStatus.Controls.Add(this.label1);
            this.gboConnectionStatus.Controls.Add(this.lblConnectionStatus);
            this.gboConnectionStatus.Location = new System.Drawing.Point(19, 508);
            this.gboConnectionStatus.Name = "gboConnectionStatus";
            this.gboConnectionStatus.Size = new System.Drawing.Size(714, 56);
            this.gboConnectionStatus.TabIndex = 82;
            this.gboConnectionStatus.TabStop = false;
            this.gboConnectionStatus.Text = "Connection Status";
            // 
            // cbxCOM
            // 
            this.cbxCOM.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxCOM.FormattingEnabled = true;
            this.cbxCOM.Location = new System.Drawing.Point(432, 23);
            this.cbxCOM.Name = "cbxCOM";
            this.cbxCOM.Size = new System.Drawing.Size(153, 21);
            this.cbxCOM.TabIndex = 81;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(603, 19);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(105, 25);
            this.btnOK.TabIndex = 80;
            this.btnOK.Text = "Connect Device";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(3, 25);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(100, 13);
            this.label23.TabIndex = 37;
            this.label23.Text = "Connection Status :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(351, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 36;
            this.label1.Text = "Serial Port : ";
            // 
            // lblConnectionStatus
            // 
            this.lblConnectionStatus.AutoSize = true;
            this.lblConnectionStatus.ForeColor = System.Drawing.Color.Red;
            this.lblConnectionStatus.Location = new System.Drawing.Point(102, 25);
            this.lblConnectionStatus.Name = "lblConnectionStatus";
            this.lblConnectionStatus.Size = new System.Drawing.Size(79, 13);
            this.lblConnectionStatus.TabIndex = 36;
            this.lblConnectionStatus.Text = "Not Connected";
            // 
            // Services
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(745, 590);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.statusBar1);
            this.Controls.Add(this.gboConnectionStatus);
            this.Name = "Services";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Services_FormClosing);
            this.Load += new System.EventHandler(this.Services_Load);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSendSMS)).EndInit();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRequest)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvWaitingSMS)).EndInit();
            this.gboConnectionStatus.ResumeLayout(false);
            this.gboConnectionStatus.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgvSendSMS;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView dgvRequest;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgvWaitingSMS;
        private System.Windows.Forms.StatusBar statusBar1;
        private System.Windows.Forms.GroupBox gboConnectionStatus;
        private System.Windows.Forms.ComboBox cbxCOM;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblConnectionStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Sender;
        private System.Windows.Forms.DataGridViewTextBoxColumn message;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
    }
}

