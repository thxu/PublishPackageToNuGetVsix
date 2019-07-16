namespace PublishPackageToNuGet2017.Form
{
    partial class OnLinePkgListForm
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
            this.btnSearch = new System.Windows.Forms.Button();
            this.txt_PkgId = new System.Windows.Forms.TextBox();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.btn_ok = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgv_PkgList = new System.Windows.Forms.DataGridView();
            this.btn_MoveToFirst = new System.Windows.Forms.Button();
            this.btn_MoveNext = new System.Windows.Forms.Button();
            this.btn_MovePre = new System.Windows.Forms.Button();
            this.lb_CurrPage = new System.Windows.Forms.Label();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_PkgList)).BeginInit();
            this.SuspendLayout();
            // 
            // cbPackageSource
            // 
            this.cbPackageSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPackageSource.FormattingEnabled = true;
            this.cbPackageSource.Location = new System.Drawing.Point(589, 12);
            this.cbPackageSource.Name = "cbPackageSource";
            this.cbPackageSource.Size = new System.Drawing.Size(199, 20);
            this.cbPackageSource.TabIndex = 25;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(494, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 12);
            this.label2.TabIndex = 24;
            this.label2.Text = "PackageSource:";
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(275, 8);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 26;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txt_PkgId
            // 
            this.txt_PkgId.Location = new System.Drawing.Point(13, 10);
            this.txt_PkgId.Name = "txt_PkgId";
            this.txt_PkgId.Size = new System.Drawing.Size(256, 21);
            this.txt_PkgId.TabIndex = 27;
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Location = new System.Drawing.Point(603, 9);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(95, 23);
            this.btn_Cancel.TabIndex = 1;
            this.btn_Cancel.Text = "Cancel";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // btn_ok
            // 
            this.btn_ok.Location = new System.Drawing.Point(478, 9);
            this.btn_ok.Name = "btn_ok";
            this.btn_ok.Size = new System.Drawing.Size(95, 23);
            this.btn_ok.TabIndex = 0;
            this.btn_ok.Text = "OK";
            this.btn_ok.UseVisualStyleBackColor = true;
            this.btn_ok.Click += new System.EventHandler(this.btn_ok_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel1.Controls.Add(this.lb_CurrPage);
            this.panel1.Controls.Add(this.btn_MovePre);
            this.panel1.Controls.Add(this.btn_MoveNext);
            this.panel1.Controls.Add(this.btn_MoveToFirst);
            this.panel1.Controls.Add(this.btn_Cancel);
            this.panel1.Controls.Add(this.btn_ok);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 409);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 41);
            this.panel1.TabIndex = 28;
            // 
            // dgv_PkgList
            // 
            this.dgv_PkgList.AllowUserToAddRows = false;
            this.dgv_PkgList.AllowUserToDeleteRows = false;
            this.dgv_PkgList.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgv_PkgList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_PkgList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4});
            this.dgv_PkgList.Location = new System.Drawing.Point(0, 37);
            this.dgv_PkgList.MultiSelect = false;
            this.dgv_PkgList.Name = "dgv_PkgList";
            this.dgv_PkgList.ReadOnly = true;
            this.dgv_PkgList.RowHeadersVisible = false;
            this.dgv_PkgList.RowTemplate.Height = 23;
            this.dgv_PkgList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_PkgList.Size = new System.Drawing.Size(800, 375);
            this.dgv_PkgList.TabIndex = 29;
            // 
            // btn_MoveToFirst
            // 
            this.btn_MoveToFirst.BackgroundImage = global::PublishPackageToNuGet2017.Properties.Resources.MoveFirstHS;
            this.btn_MoveToFirst.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btn_MoveToFirst.Location = new System.Drawing.Point(13, 7);
            this.btn_MoveToFirst.Name = "btn_MoveToFirst";
            this.btn_MoveToFirst.Size = new System.Drawing.Size(26, 26);
            this.btn_MoveToFirst.TabIndex = 2;
            this.btn_MoveToFirst.UseVisualStyleBackColor = true;
            this.btn_MoveToFirst.Click += new System.EventHandler(this.btn_MoveToFirst_Click);
            // 
            // btn_MoveNext
            // 
            this.btn_MoveNext.BackgroundImage = global::PublishPackageToNuGet2017.Properties.Resources.MoveNextHS;
            this.btn_MoveNext.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btn_MoveNext.Location = new System.Drawing.Point(105, 7);
            this.btn_MoveNext.Name = "btn_MoveNext";
            this.btn_MoveNext.Size = new System.Drawing.Size(26, 26);
            this.btn_MoveNext.TabIndex = 3;
            this.btn_MoveNext.UseVisualStyleBackColor = true;
            this.btn_MoveNext.Click += new System.EventHandler(this.btn_MoveNext_Click);
            // 
            // btn_MovePre
            // 
            this.btn_MovePre.BackgroundImage = global::PublishPackageToNuGet2017.Properties.Resources.MovePreviousHS;
            this.btn_MovePre.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btn_MovePre.Location = new System.Drawing.Point(45, 7);
            this.btn_MovePre.Name = "btn_MovePre";
            this.btn_MovePre.Size = new System.Drawing.Size(26, 26);
            this.btn_MovePre.TabIndex = 4;
            this.btn_MovePre.UseVisualStyleBackColor = true;
            this.btn_MovePre.Click += new System.EventHandler(this.btn_MovePre_Click);
            // 
            // lb_CurrPage
            // 
            this.lb_CurrPage.AutoSize = true;
            this.lb_CurrPage.Location = new System.Drawing.Point(77, 14);
            this.lb_CurrPage.Name = "lb_CurrPage";
            this.lb_CurrPage.Size = new System.Drawing.Size(11, 12);
            this.lb_CurrPage.TabIndex = 5;
            this.lb_CurrPage.Text = "0";
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column1.HeaderText = "Id";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Column2.HeaderText = "Version";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 70;
            // 
            // Column3
            // 
            this.Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Column3.HeaderText = "Author";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 60;
            // 
            // Column4
            // 
            this.Column4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column4.HeaderText = "Describe";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // OnLinePkgListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dgv_PkgList);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.txt_PkgId);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.cbPackageSource);
            this.Controls.Add(this.label2);
            this.Name = "OnLinePkgListForm";
            this.Text = "OnLinePkgListForm";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_PkgList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbPackageSource;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txt_PkgId;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.Button btn_ok;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgv_PkgList;
        private System.Windows.Forms.Button btn_MoveToFirst;
        private System.Windows.Forms.Button btn_MovePre;
        private System.Windows.Forms.Button btn_MoveNext;
        private System.Windows.Forms.Label lb_CurrPage;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
    }
}