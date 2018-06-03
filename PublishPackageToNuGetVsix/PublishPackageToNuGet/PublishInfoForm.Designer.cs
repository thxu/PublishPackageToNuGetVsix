namespace PublishPackageToNuGet
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
            this.SuspendLayout();
            // 
            // btnPublish
            // 
            this.btnPublish.Location = new System.Drawing.Point(98, 285);
            this.btnPublish.Name = "btnPublish";
            this.btnPublish.Size = new System.Drawing.Size(75, 23);
            this.btnPublish.TabIndex = 34;
            this.btnPublish.Text = "Publish";
            this.btnPublish.UseVisualStyleBackColor = true;
            this.btnPublish.Click += new System.EventHandler(this.btnPublish_Click);
            // 
            // txtDesc
            // 
            this.txtDesc.Location = new System.Drawing.Point(98, 180);
            this.txtDesc.Multiline = true;
            this.txtDesc.Name = "txtDesc";
            this.txtDesc.Size = new System.Drawing.Size(208, 70);
            this.txtDesc.TabIndex = 33;
            // 
            // txtOwners
            // 
            this.txtOwners.Enabled = false;
            this.txtOwners.Location = new System.Drawing.Point(98, 138);
            this.txtOwners.Name = "txtOwners";
            this.txtOwners.Size = new System.Drawing.Size(208, 21);
            this.txtOwners.TabIndex = 32;
            // 
            // txtAuthors
            // 
            this.txtAuthors.Enabled = false;
            this.txtAuthors.Location = new System.Drawing.Point(98, 96);
            this.txtAuthors.Name = "txtAuthors";
            this.txtAuthors.Size = new System.Drawing.Size(208, 21);
            this.txtAuthors.TabIndex = 31;
            // 
            // txtVersion
            // 
            this.txtVersion.Enabled = false;
            this.txtVersion.Location = new System.Drawing.Point(98, 54);
            this.txtVersion.Name = "txtVersion";
            this.txtVersion.Size = new System.Drawing.Size(208, 21);
            this.txtVersion.TabIndex = 30;
            // 
            // txtId
            // 
            this.txtId.Enabled = false;
            this.txtId.Location = new System.Drawing.Point(98, 12);
            this.txtId.Name = "txtId";
            this.txtId.Size = new System.Drawing.Size(208, 21);
            this.txtId.TabIndex = 29;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(14, 183);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(71, 12);
            this.label6.TabIndex = 28;
            this.label6.Text = "Description";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(44, 141);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 27;
            this.label5.Text = "Owners";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(38, 99);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 12);
            this.label4.TabIndex = 26;
            this.label4.Text = "Authors";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(38, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 12);
            this.label2.TabIndex = 25;
            this.label2.Text = "Version";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(68, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 12);
            this.label1.TabIndex = 24;
            this.label1.Text = "Id";
            // 
            // PublishInfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(338, 326);
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
    }
}