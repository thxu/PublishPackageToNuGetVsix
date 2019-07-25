namespace PublishPackageToNuGet2017.Form
{
    partial class PackageDetailsForm
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
            this.cbPackageSource = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dgv_PkgDetails = new System.Windows.Forms.DataGridView();
            this.Version = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Desc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Auth = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PublishDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.btn_ok = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_PkgDetails)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbPackageSource
            // 
            this.cbPackageSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPackageSource.FormattingEnabled = true;
            this.cbPackageSource.Location = new System.Drawing.Point(101, 8);
            this.cbPackageSource.Name = "cbPackageSource";
            this.cbPackageSource.Size = new System.Drawing.Size(199, 20);
            this.cbPackageSource.TabIndex = 27;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 12);
            this.label2.TabIndex = 26;
            this.label2.Text = "PackageSource:";
            // 
            // dgv_PkgDetails
            // 
            this.dgv_PkgDetails.AllowUserToAddRows = false;
            this.dgv_PkgDetails.AllowUserToDeleteRows = false;
            this.dgv_PkgDetails.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgv_PkgDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_PkgDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Version,
            this.Desc,
            this.Auth,
            this.PublishDate});
            this.dgv_PkgDetails.Location = new System.Drawing.Point(1, 38);
            this.dgv_PkgDetails.Name = "dgv_PkgDetails";
            this.dgv_PkgDetails.ReadOnly = true;
            this.dgv_PkgDetails.RowHeadersVisible = false;
            this.dgv_PkgDetails.RowTemplate.Height = 23;
            this.dgv_PkgDetails.Size = new System.Drawing.Size(459, 362);
            this.dgv_PkgDetails.TabIndex = 28;
            // 
            // Version
            // 
            this.Version.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Version.HeaderText = "版本";
            this.Version.Name = "Version";
            this.Version.ReadOnly = true;
            // 
            // Desc
            // 
            this.Desc.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Desc.HeaderText = "版本描述";
            this.Desc.Name = "Desc";
            this.Desc.ReadOnly = true;
            // 
            // Auth
            // 
            this.Auth.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Auth.HeaderText = "作者";
            this.Auth.Name = "Auth";
            this.Auth.ReadOnly = true;
            // 
            // PublishDate
            // 
            this.PublishDate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.PublishDate.HeaderText = "发布时间";
            this.PublishDate.Name = "PublishDate";
            this.PublishDate.ReadOnly = true;
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Location = new System.Drawing.Point(352, 9);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(95, 23);
            this.btn_Cancel.TabIndex = 30;
            this.btn_Cancel.Text = "Cancel";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            // 
            // btn_ok
            // 
            this.btn_ok.Location = new System.Drawing.Point(225, 9);
            this.btn_ok.Name = "btn_ok";
            this.btn_ok.Size = new System.Drawing.Size(95, 23);
            this.btn_ok.TabIndex = 29;
            this.btn_ok.Text = "Ok";
            this.btn_ok.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btn_ok);
            this.panel1.Controls.Add(this.btn_Cancel);
            this.panel1.Location = new System.Drawing.Point(1, 406);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(459, 41);
            this.panel1.TabIndex = 31;
            // 
            // PackageDetailsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(461, 450);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dgv_PkgDetails);
            this.Controls.Add(this.cbPackageSource);
            this.Controls.Add(this.label2);
            this.Name = "PackageDetailsForm";
            this.Text = "PackageDetailsForm";
            ((System.ComponentModel.ISupportInitialize)(this.dgv_PkgDetails)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbPackageSource;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dgv_PkgDetails;
        private System.Windows.Forms.DataGridViewTextBoxColumn Version;
        private System.Windows.Forms.DataGridViewTextBoxColumn Desc;
        private System.Windows.Forms.DataGridViewTextBoxColumn Auth;
        private System.Windows.Forms.DataGridViewTextBoxColumn PublishDate;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.Button btn_ok;
        private System.Windows.Forms.Panel panel1;
    }
}