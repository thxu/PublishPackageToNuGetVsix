namespace PublishPackageToNuGet2017.Form
{
    partial class PackageDependenciesForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.btn_ok = new System.Windows.Forms.Button();
            this.groupBox_groups = new System.Windows.Forms.GroupBox();
            this.btn_DelGroup = new System.Windows.Forms.Button();
            this.btn_AddGroup = new System.Windows.Forms.Button();
            this.listView_GroupList = new System.Windows.Forms.ListView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dg_PkgList = new System.Windows.Forms.DataGridView();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Version = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Op = new System.Windows.Forms.DataGridViewLinkColumn();
            this.btn_OpenOnLinePkgListForm = new System.Windows.Forms.Button();
            this.btn_AddPkg = new System.Windows.Forms.Button();
            this.txtPkgVersion = new System.Windows.Forms.TextBox();
            this.txtPkgId = new System.Windows.Forms.TextBox();
            this.txtTargetFramework = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.groupBox_groups.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg_PkgList)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel1.Controls.Add(this.btn_Cancel);
            this.panel1.Controls.Add(this.btn_ok);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 414);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(712, 41);
            this.panel1.TabIndex = 0;
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Location = new System.Drawing.Point(603, 9);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(95, 23);
            this.btn_Cancel.TabIndex = 1;
            this.btn_Cancel.Text = "Cancel";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            // 
            // btn_ok
            // 
            this.btn_ok.Location = new System.Drawing.Point(478, 9);
            this.btn_ok.Name = "btn_ok";
            this.btn_ok.Size = new System.Drawing.Size(95, 23);
            this.btn_ok.TabIndex = 0;
            this.btn_ok.Text = "OK";
            this.btn_ok.UseVisualStyleBackColor = true;
            // 
            // groupBox_groups
            // 
            this.groupBox_groups.Controls.Add(this.btn_DelGroup);
            this.groupBox_groups.Controls.Add(this.btn_AddGroup);
            this.groupBox_groups.Controls.Add(this.listView_GroupList);
            this.groupBox_groups.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox_groups.Location = new System.Drawing.Point(13, 12);
            this.groupBox_groups.Name = "groupBox_groups";
            this.groupBox_groups.Size = new System.Drawing.Size(134, 396);
            this.groupBox_groups.TabIndex = 7;
            this.groupBox_groups.TabStop = false;
            this.groupBox_groups.Text = "Groups";
            // 
            // btn_DelGroup
            // 
            this.btn_DelGroup.BackgroundImage = global::PublishPackageToNuGet2017.Properties.Resources.Delete;
            this.btn_DelGroup.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btn_DelGroup.Location = new System.Drawing.Point(101, 14);
            this.btn_DelGroup.Name = "btn_DelGroup";
            this.btn_DelGroup.Size = new System.Drawing.Size(26, 26);
            this.btn_DelGroup.TabIndex = 3;
            this.btn_DelGroup.UseVisualStyleBackColor = true;
            this.btn_DelGroup.Click += new System.EventHandler(this.btn_DelGroup_Click);
            // 
            // btn_AddGroup
            // 
            this.btn_AddGroup.BackgroundImage = global::PublishPackageToNuGet2017.Properties.Resources.Add;
            this.btn_AddGroup.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btn_AddGroup.Location = new System.Drawing.Point(70, 14);
            this.btn_AddGroup.Name = "btn_AddGroup";
            this.btn_AddGroup.Size = new System.Drawing.Size(26, 26);
            this.btn_AddGroup.TabIndex = 2;
            this.btn_AddGroup.UseVisualStyleBackColor = true;
            this.btn_AddGroup.Click += new System.EventHandler(this.btn_AddGroup_Click);
            // 
            // listView_GroupList
            // 
            this.listView_GroupList.HideSelection = false;
            this.listView_GroupList.Location = new System.Drawing.Point(6, 41);
            this.listView_GroupList.Name = "listView_GroupList";
            this.listView_GroupList.Size = new System.Drawing.Size(121, 349);
            this.listView_GroupList.TabIndex = 0;
            this.listView_GroupList.UseCompatibleStateImageBehavior = false;
            this.listView_GroupList.SelectedIndexChanged += new System.EventHandler(this.listView_GroupList_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dg_PkgList);
            this.groupBox1.Controls.Add(this.btn_OpenOnLinePkgListForm);
            this.groupBox1.Controls.Add(this.btn_AddPkg);
            this.groupBox1.Controls.Add(this.txtPkgVersion);
            this.groupBox1.Controls.Add(this.txtPkgId);
            this.groupBox1.Controls.Add(this.txtTargetFramework);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(153, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(559, 396);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Group details";
            // 
            // dg_PkgList
            // 
            this.dg_PkgList.AllowUserToAddRows = false;
            this.dg_PkgList.AllowUserToDeleteRows = false;
            this.dg_PkgList.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dg_PkgList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg_PkgList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.Version,
            this.Op});
            this.dg_PkgList.Location = new System.Drawing.Point(18, 41);
            this.dg_PkgList.Name = "dg_PkgList";
            this.dg_PkgList.ReadOnly = true;
            this.dg_PkgList.RowHeadersVisible = false;
            this.dg_PkgList.RowTemplate.Height = 23;
            this.dg_PkgList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dg_PkgList.Size = new System.Drawing.Size(536, 317);
            this.dg_PkgList.TabIndex = 14;
            this.dg_PkgList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dg_PkgList_CellContentClick);
            // 
            // Id
            // 
            this.Id.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Id.HeaderText = "Id";
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            // 
            // Version
            // 
            this.Version.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Version.HeaderText = "Version";
            this.Version.Name = "Version";
            this.Version.ReadOnly = true;
            this.Version.Width = 150;
            // 
            // Op
            // 
            this.Op.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Op.HeaderText = "";
            this.Op.Name = "Op";
            this.Op.ReadOnly = true;
            this.Op.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Op.Width = 90;
            // 
            // btn_OpenOnLinePkgListForm
            // 
            this.btn_OpenOnLinePkgListForm.BackgroundImage = global::PublishPackageToNuGet2017.Properties.Resources.Properties;
            this.btn_OpenOnLinePkgListForm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btn_OpenOnLinePkgListForm.Location = new System.Drawing.Point(16, 364);
            this.btn_OpenOnLinePkgListForm.Name = "btn_OpenOnLinePkgListForm";
            this.btn_OpenOnLinePkgListForm.Size = new System.Drawing.Size(26, 26);
            this.btn_OpenOnLinePkgListForm.TabIndex = 4;
            this.btn_OpenOnLinePkgListForm.UseVisualStyleBackColor = true;
            this.btn_OpenOnLinePkgListForm.Click += new System.EventHandler(this.btn_OpenOnLinePkgListForm_Click);
            // 
            // btn_AddPkg
            // 
            this.btn_AddPkg.BackgroundImage = global::PublishPackageToNuGet2017.Properties.Resources.Add;
            this.btn_AddPkg.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btn_AddPkg.Location = new System.Drawing.Point(526, 364);
            this.btn_AddPkg.Name = "btn_AddPkg";
            this.btn_AddPkg.Size = new System.Drawing.Size(26, 26);
            this.btn_AddPkg.TabIndex = 4;
            this.btn_AddPkg.UseVisualStyleBackColor = true;
            this.btn_AddPkg.Click += new System.EventHandler(this.btn_AddPkg_Click);
            // 
            // txtPkgVersion
            // 
            this.txtPkgVersion.Location = new System.Drawing.Point(239, 367);
            this.txtPkgVersion.Name = "txtPkgVersion";
            this.txtPkgVersion.Size = new System.Drawing.Size(279, 21);
            this.txtPkgVersion.TabIndex = 13;
            // 
            // txtPkgId
            // 
            this.txtPkgId.Location = new System.Drawing.Point(50, 367);
            this.txtPkgId.Name = "txtPkgId";
            this.txtPkgId.Size = new System.Drawing.Size(181, 21);
            this.txtPkgId.TabIndex = 12;
            // 
            // txtTargetFramework
            // 
            this.txtTargetFramework.Location = new System.Drawing.Point(139, 14);
            this.txtTargetFramework.Name = "txtTargetFramework";
            this.txtTargetFramework.Size = new System.Drawing.Size(415, 21);
            this.txtTargetFramework.TabIndex = 8;
            this.txtTargetFramework.Text = "如：netstandard2.0，net45等";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(16, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(117, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "Target framework";
            // 
            // PackageDependenciesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(712, 455);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox_groups);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "PackageDependenciesForm";
            this.Text = "PackageDependenciesForm";
            this.panel1.ResumeLayout(false);
            this.groupBox_groups.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg_PkgList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn_ok;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.GroupBox groupBox_groups;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtTargetFramework;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListView listView_GroupList;
        private System.Windows.Forms.TextBox txtPkgId;
        private System.Windows.Forms.TextBox txtPkgVersion;
        private System.Windows.Forms.Button btn_DelGroup;
        private System.Windows.Forms.Button btn_AddGroup;
        private System.Windows.Forms.Button btn_OpenOnLinePkgListForm;
        private System.Windows.Forms.Button btn_AddPkg;
        private System.Windows.Forms.DataGridView dg_PkgList;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn Version;
        private System.Windows.Forms.DataGridViewLinkColumn Op;
    }
}