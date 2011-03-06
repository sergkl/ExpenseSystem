namespace ExpenseSystem.Client
{
    partial class fExpenseSystem
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
            this.lFirstDate = new System.Windows.Forms.Label();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.lLastDate = new System.Windows.Forms.Label();
            this.bFormReport = new System.Windows.Forms.Button();
            this.cbUsers = new System.Windows.Forms.ComboBox();
            this.lChooseUser = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lFirstDate
            // 
            this.lFirstDate.AutoSize = true;
            this.lFirstDate.Location = new System.Drawing.Point(12, 9);
            this.lFirstDate.Name = "lFirstDate";
            this.lFirstDate.Size = new System.Drawing.Size(53, 13);
            this.lFirstDate.TabIndex = 0;
            this.lFirstDate.Text = "Start date";
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.Location = new System.Drawing.Point(68, 6);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(200, 20);
            this.dtpStartDate.TabIndex = 1;
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.Location = new System.Drawing.Point(68, 32);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(200, 20);
            this.dtpEndDate.TabIndex = 3;
            // 
            // lLastDate
            // 
            this.lLastDate.AutoSize = true;
            this.lLastDate.Location = new System.Drawing.Point(12, 35);
            this.lLastDate.Name = "lLastDate";
            this.lLastDate.Size = new System.Drawing.Size(51, 13);
            this.lLastDate.TabIndex = 2;
            this.lLastDate.Text = "Last date";
            // 
            // bFormReport
            // 
            this.bFormReport.Location = new System.Drawing.Point(420, 6);
            this.bFormReport.Name = "bFormReport";
            this.bFormReport.Size = new System.Drawing.Size(105, 46);
            this.bFormReport.TabIndex = 4;
            this.bFormReport.Text = "Form report";
            this.bFormReport.UseVisualStyleBackColor = true;
            this.bFormReport.Click += new System.EventHandler(this.bFormReport_Click);
            // 
            // cbUsers
            // 
            this.cbUsers.FormattingEnabled = true;
            this.cbUsers.Location = new System.Drawing.Point(274, 31);
            this.cbUsers.Name = "cbUsers";
            this.cbUsers.Size = new System.Drawing.Size(140, 21);
            this.cbUsers.TabIndex = 5;
            // 
            // lChooseUser
            // 
            this.lChooseUser.AutoSize = true;
            this.lChooseUser.Location = new System.Drawing.Point(274, 12);
            this.lChooseUser.Name = "lChooseUser";
            this.lChooseUser.Size = new System.Drawing.Size(69, 13);
            this.lChooseUser.TabIndex = 6;
            this.lChooseUser.Text = "Choose user:";
            // 
            // fExpenseSystem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(577, 281);
            this.Controls.Add(this.lChooseUser);
            this.Controls.Add(this.cbUsers);
            this.Controls.Add(this.bFormReport);
            this.Controls.Add(this.dtpEndDate);
            this.Controls.Add(this.lLastDate);
            this.Controls.Add(this.dtpStartDate);
            this.Controls.Add(this.lFirstDate);
            this.Name = "fExpenseSystem";
            this.Text = "Expense system";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lFirstDate;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.Label lLastDate;
        private System.Windows.Forms.Button bFormReport;
        private System.Windows.Forms.ComboBox cbUsers;
        private System.Windows.Forms.Label lChooseUser;
    }
}

