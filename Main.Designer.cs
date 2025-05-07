namespace FoxOrganizer
{
    partial class Main
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            txtFolderPath = new TextBox();
            label2 = new Label();
            cmbSortBy = new ComboBox();
            btnOrganize = new Button();
            btnBrowse = new Button();
            lstLog = new ListBox();
            label3 = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 19);
            label1.Name = "label1";
            label1.Size = new Size(46, 15);
            label1.TabIndex = 0;
            label1.Text = "Folder: ";
            // 
            // txtFolderPath
            // 
            txtFolderPath.BorderStyle = BorderStyle.FixedSingle;
            txtFolderPath.Location = new Point(69, 16);
            txtFolderPath.Name = "txtFolderPath";
            txtFolderPath.ReadOnly = true;
            txtFolderPath.Size = new Size(328, 23);
            txtFolderPath.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 64);
            label2.Name = "label2";
            label2.Size = new Size(50, 15);
            label2.TabIndex = 2;
            label2.Text = "Sort By: ";
            // 
            // cmbSortBy
            // 
            cmbSortBy.FormattingEnabled = true;
            cmbSortBy.Location = new Point(69, 56);
            cmbSortBy.Name = "cmbSortBy";
            cmbSortBy.Size = new Size(324, 23);
            cmbSortBy.TabIndex = 3;
            // 
            // btnOrganize
            // 
            btnOrganize.Location = new Point(201, 95);
            btnOrganize.Name = "btnOrganize";
            btnOrganize.Size = new Size(132, 38);
            btnOrganize.TabIndex = 4;
            btnOrganize.Text = "Organize Files";
            btnOrganize.UseVisualStyleBackColor = true;
            btnOrganize.Click += btnOrganize_Click;
            // 
            // btnBrowse
            // 
            btnBrowse.Location = new Point(403, 15);
            btnBrowse.Name = "btnBrowse";
            btnBrowse.Size = new Size(75, 23);
            btnBrowse.TabIndex = 5;
            btnBrowse.Text = "Browse";
            btnBrowse.UseVisualStyleBackColor = true;
            btnBrowse.Click += btnBrowse_Click;
            // 
            // lstLog
            // 
            lstLog.FormattingEnabled = true;
            lstLog.Location = new Point(12, 148);
            lstLog.Name = "lstLog";
            lstLog.Size = new Size(466, 79);
            lstLog.TabIndex = 6;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 130);
            label3.Name = "label3";
            label3.Size = new Size(33, 15);
            label3.TabIndex = 7;
            label3.Text = "Log: ";
            // 
            // Main
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(490, 245);
            Controls.Add(label3);
            Controls.Add(lstLog);
            Controls.Add(btnBrowse);
            Controls.Add(btnOrganize);
            Controls.Add(cmbSortBy);
            Controls.Add(label2);
            Controls.Add(txtFolderPath);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "Main";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Fox Organizer";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox txtFolderPath;
        private Label label2;
        private ComboBox cmbSortBy;
        private Button btnOrganize;
        private Button btnBrowse;
        private ListBox lstLog;
        private Label label3;
    }
}
