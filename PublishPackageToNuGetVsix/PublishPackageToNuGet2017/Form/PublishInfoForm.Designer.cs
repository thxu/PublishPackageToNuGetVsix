namespace PublishPackageToNuGet2017.Form
{
    partial class PublishInfoForm
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
            this.btnPublish = new System.Windows.Forms.Button();
            this.txtDesc = new System.Windows.Forms.TextBox();
            this.txtOwners = new System.Windows.Forms.TextBox();
            this.txtAuthors = new System.Windows.Forms.TextBox();
            this.txtVersion = new System.Windows.Forms.TextBox();
            this.txtId = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel_PkgDependencyGroup = new System.Windows.Forms.Panel();
            this.btn_EditDependencies = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnPublish
            // 
            this.btnPublish.Location = new System.Drawing.Point(95, 278);
            this.btnPublish.Name = "btnPublish";
            this.btnPublish.Size = new System.Drawing.Size(75, 23);
            this.btnPublish.TabIndex = 45;
            this.btnPublish.Text = "Publish";
            this.btnPublish.UseVisualStyleBackColor = true;
            this.btnPublish.Click += new System.EventHandler(this.btnPublish_Click);
            // 
            // txtDesc
            // 
            this.txtDesc.Location = new System.Drawing.Point(95, 180);
            this.txtDesc.Multiline = true;
            this.txtDesc.Name = "txtDesc";
            this.txtDesc.Size = new System.Drawing.Size(484, 70);
            this.txtDesc.TabIndex = 44;
            // 
            // txtOwners
            // 
            this.txtOwners.Enabled = false;
            this.txtOwners.Location = new System.Drawing.Point(95, 138);
            this.txtOwners.Name = "txtOwners";
            this.txtOwners.Size = new System.Drawing.Size(484, 21);
            this.txtOwners.TabIndex = 43;
            // 
            // txtAuthors
            // 
            this.txtAuthors.Enabled = false;
            this.txtAuthors.Location = new System.Drawing.Point(95, 96);
            this.txtAuthors.Name = "txtAuthors";
            this.txtAuthors.Size = new System.Drawing.Size(484, 21);
            this.txtAuthors.TabIndex = 42;
            // 
            // txtVersion
            // 
            this.txtVersion.Enabled = false;
            this.txtVersion.Location = new System.Drawing.Point(95, 54);
            this.txtVersion.Name = "txtVersion";
            this.txtVersion.Size = new System.Drawing.Size(484, 21);
            this.txtVersion.TabIndex = 41;
            // 
            // txtId
            // 
            this.txtId.Enabled = false;
            this.txtId.Location = new System.Drawing.Point(95, 12);
            this.txtId.Name = "txtId";
            this.txtId.Size = new System.Drawing.Size(484, 21);
            this.txtId.TabIndex = 40;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(11, 183);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(71, 12);
            this.label6.TabIndex = 39;
            this.label6.Text = "Description";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(41, 141);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 38;
            this.label5.Text = "Owners";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(35, 99);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 12);
            this.label4.TabIndex = 37;
            this.label4.Text = "Authors";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(35, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 12);
            this.label2.TabIndex = 36;
            this.label2.Text = "Version";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(65, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 12);
            this.label1.TabIndex = 35;
            this.label1.Text = "Id";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 315);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 12);
            this.label3.TabIndex = 46;
            this.label3.Text = "Dependencies：";
            // 
            // panel_PkgDependencyGroup
            // 
            this.panel_PkgDependencyGroup.AutoScroll = true;
            this.panel_PkgDependencyGroup.AutoScrollMinSize = new System.Drawing.Size(400, 0);
            this.panel_PkgDependencyGroup.Location = new System.Drawing.Point(95, 315);
            this.panel_PkgDependencyGroup.Name = "panel_PkgDependencyGroup";
            this.panel_PkgDependencyGroup.Size = new System.Drawing.Size(484, 130);
            this.panel_PkgDependencyGroup.TabIndex = 47;
            // 
            // btn_EditDependencies
            // 
            this.btn_EditDependencies.Location = new System.Drawing.Point(176, 278);
            this.btn_EditDependencies.Name = "btn_EditDependencies";
            this.btn_EditDependencies.Size = new System.Drawing.Size(190, 23);
            this.btn_EditDependencies.TabIndex = 48;
            this.btn_EditDependencies.Text = "Edit Dependencies";
            this.btn_EditDependencies.UseVisualStyleBackColor = true;
            this.btn_EditDependencies.Click += new System.EventHandler(this.btn_EditDependencies_Click);
            // 
            // PublishInfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(591, 457);
            this.Controls.Add(this.btn_EditDependencies);
            this.Controls.Add(this.panel_PkgDependencyGroup);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnPublish);
            this.Controls.Add(this.txtDesc);
            this.Controls.Add(this.txtOwners);
            this.Controls.Add(this.txtAuthors);
            this.Controls.Add(this.txtVersion);
            this.Controls.Add(this.txtId);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "PublishInfoForm";
            this.Text = "PublishInfoForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnPublish;
        private System.Windows.Forms.TextBox txtDesc;
        private System.Windows.Forms.TextBox txtOwners;
        private System.Windows.Forms.TextBox txtAuthors;
        private System.Windows.Forms.TextBox txtVersion;
        private System.Windows.Forms.TextBox txtId;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel_PkgDependencyGroup;
        private System.Windows.Forms.Button btn_EditDependencies;
    }
}