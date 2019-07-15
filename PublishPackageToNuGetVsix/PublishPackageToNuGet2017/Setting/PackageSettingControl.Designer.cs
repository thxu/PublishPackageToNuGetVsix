﻿namespace PublishPackageToNuGet2017.Setting
{
    partial class PackageSettingControl
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.cbPackageSource = new System.Windows.Forms.ComboBox();
            this.txtPublishKey = new System.Windows.Forms.TextBox();
            this.txtAuthour = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_Refresh = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cbPackageSource
            // 
            this.cbPackageSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPackageSource.FormattingEnabled = true;
            this.cbPackageSource.Location = new System.Drawing.Point(131, 66);
            this.cbPackageSource.Name = "cbPackageSource";
            this.cbPackageSource.Size = new System.Drawing.Size(205, 20);
            this.cbPackageSource.TabIndex = 23;
            this.cbPackageSource.SelectedIndexChanged += new System.EventHandler(this.SavePackageSource);
            // 
            // txtPublishKey
            // 
            this.txtPublishKey.Location = new System.Drawing.Point(131, 101);
            this.txtPublishKey.Name = "txtPublishKey";
            this.txtPublishKey.Size = new System.Drawing.Size(205, 21);
            this.txtPublishKey.TabIndex = 22;
            this.txtPublishKey.TextChanged += new System.EventHandler(this.SavePublishKey);
            // 
            // txtAuthour
            // 
            this.txtAuthour.Location = new System.Drawing.Point(131, 32);
            this.txtAuthour.Name = "txtAuthour";
            this.txtAuthour.Size = new System.Drawing.Size(205, 21);
            this.txtAuthour.TabIndex = 21;
            this.txtAuthour.TextChanged += new System.EventHandler(this.SaveAuthour);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(36, 104);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 12);
            this.label3.TabIndex = 20;
            this.label3.Text = "PublishKey:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(36, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 12);
            this.label2.TabIndex = 19;
            this.label2.Text = "PackageSource:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(36, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 18;
            this.label1.Text = "Authour:";
            // 
            // btn_Refresh
            // 
            this.btn_Refresh.Location = new System.Drawing.Point(343, 66);
            this.btn_Refresh.Name = "btn_Refresh";
            this.btn_Refresh.Size = new System.Drawing.Size(62, 23);
            this.btn_Refresh.TabIndex = 24;
            this.btn_Refresh.Text = "Refresh";
            this.btn_Refresh.UseVisualStyleBackColor = true;
            this.btn_Refresh.Click += new System.EventHandler(this.btn_Refresh_Click);
            // 
            // PackageSettingControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btn_Refresh);
            this.Controls.Add(this.cbPackageSource);
            this.Controls.Add(this.txtPublishKey);
            this.Controls.Add(this.txtAuthour);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "PackageSettingControl";
            this.Size = new System.Drawing.Size(414, 150);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbPackageSource;
        private System.Windows.Forms.TextBox txtPublishKey;
        private System.Windows.Forms.TextBox txtAuthour;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_Refresh;
    }
}
