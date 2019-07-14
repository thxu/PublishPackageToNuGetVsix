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
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("Id");
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("Version");
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem("Operation");
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.btn_ok = new System.Windows.Forms.Button();
            this.groupBox_groups = new System.Windows.Forms.GroupBox();
            this.listView1 = new System.Windows.Forms.ListView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.listView2 = new System.Windows.Forms.ListView();
            this.ourButton3 = new PublishPackageToNuGet2017.Form.OurButton();
            this.ourButton2 = new PublishPackageToNuGet2017.Form.OurButton();
            this.ourButton1 = new PublishPackageToNuGet2017.Form.OurButton();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.ourButton4 = new PublishPackageToNuGet2017.Form.OurButton();
            this.panel1.SuspendLayout();
            this.groupBox_groups.SuspendLayout();
            this.groupBox1.SuspendLayout();
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
            this.groupBox_groups.Controls.Add(this.ourButton2);
            this.groupBox_groups.Controls.Add(this.ourButton1);
            this.groupBox_groups.Controls.Add(this.listView1);
            this.groupBox_groups.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox_groups.Location = new System.Drawing.Point(13, 12);
            this.groupBox_groups.Name = "groupBox_groups";
            this.groupBox_groups.Size = new System.Drawing.Size(134, 396);
            this.groupBox_groups.TabIndex = 7;
            this.groupBox_groups.TabStop = false;
            this.groupBox_groups.Text = "Groups";
            // 
            // listView1
            // 
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(6, 41);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(121, 349);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ourButton4);
            this.groupBox1.Controls.Add(this.textBox3);
            this.groupBox1.Controls.Add(this.textBox2);
            this.groupBox1.Controls.Add(this.ourButton3);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.listView2);
            this.groupBox1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(153, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(645, 396);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Group details";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(139, 14);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(415, 21);
            this.textBox1.TabIndex = 8;
            this.textBox1.Text = "如：netstandard2.0，net45等";
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
            // listView2
            // 
            this.listView2.HideSelection = false;
            this.listView2.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3});
            this.listView2.Location = new System.Drawing.Point(16, 41);
            this.listView2.Name = "listView2";
            this.listView2.Size = new System.Drawing.Size(538, 318);
            this.listView2.TabIndex = 6;
            this.listView2.UseCompatibleStateImageBehavior = false;
            // 
            // ourButton3
            // 
            this.ourButton3.CheckState = true;
            this.ourButton3.ColorMouseDown = System.Drawing.Color.White;
            this.ourButton3.ColorMouseIn = System.Drawing.Color.White;
            this.ourButton3.Exclusion = true;
            this.ourButton3.Image = global::PublishPackageToNuGet2017.Properties.Resources.Properties;
            this.ourButton3.IntervalBetweenTextAndBorder = 2;
            this.ourButton3.IntervalBetweenTextAndImage = 2;
            this.ourButton3.Location = new System.Drawing.Point(8, 367);
            this.ourButton3.Name = "ourButton3";
            this.ourButton3.RoundCorner = false;
            this.ourButton3.Size = new System.Drawing.Size(30, 23);
            this.ourButton3.TabIndex = 11;
            this.ourButton3.TextPosition = PublishPackageToNuGet2017.Form.eTextPosition.Bottom;
            this.ourButton3.WithArrow = true;
            this.ourButton3.WithBorder = true;
            // 
            // ourButton2
            // 
            this.ourButton2.CheckState = true;
            this.ourButton2.ColorMouseDown = System.Drawing.Color.White;
            this.ourButton2.ColorMouseIn = System.Drawing.Color.White;
            this.ourButton2.Exclusion = true;
            this.ourButton2.Image = global::PublishPackageToNuGet2017.Properties.Resources.Delete;
            this.ourButton2.IntervalBetweenTextAndBorder = 2;
            this.ourButton2.IntervalBetweenTextAndImage = 2;
            this.ourButton2.Location = new System.Drawing.Point(97, 14);
            this.ourButton2.Name = "ourButton2";
            this.ourButton2.RoundCorner = false;
            this.ourButton2.Size = new System.Drawing.Size(30, 23);
            this.ourButton2.TabIndex = 10;
            this.ourButton2.TextPosition = PublishPackageToNuGet2017.Form.eTextPosition.Bottom;
            this.ourButton2.WithArrow = true;
            this.ourButton2.WithBorder = true;
            // 
            // ourButton1
            // 
            this.ourButton1.CheckState = true;
            this.ourButton1.ColorMouseDown = System.Drawing.Color.White;
            this.ourButton1.ColorMouseIn = System.Drawing.Color.White;
            this.ourButton1.Exclusion = true;
            this.ourButton1.Image = global::PublishPackageToNuGet2017.Properties.Resources.Add;
            this.ourButton1.IntervalBetweenTextAndBorder = 2;
            this.ourButton1.IntervalBetweenTextAndImage = 2;
            this.ourButton1.Location = new System.Drawing.Point(61, 14);
            this.ourButton1.Name = "ourButton1";
            this.ourButton1.RoundCorner = false;
            this.ourButton1.Size = new System.Drawing.Size(30, 23);
            this.ourButton1.TabIndex = 9;
            this.ourButton1.TextPosition = PublishPackageToNuGet2017.Form.eTextPosition.Bottom;
            this.ourButton1.WithArrow = true;
            this.ourButton1.WithBorder = true;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(52, 369);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(181, 21);
            this.textBox2.TabIndex = 12;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(239, 369);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(279, 21);
            this.textBox3.TabIndex = 13;
            // 
            // ourButton4
            // 
            this.ourButton4.CheckState = true;
            this.ourButton4.ColorMouseDown = System.Drawing.Color.White;
            this.ourButton4.ColorMouseIn = System.Drawing.Color.White;
            this.ourButton4.Exclusion = true;
            this.ourButton4.Image = global::PublishPackageToNuGet2017.Properties.Resources.Add;
            this.ourButton4.IntervalBetweenTextAndBorder = 2;
            this.ourButton4.IntervalBetweenTextAndImage = 2;
            this.ourButton4.Location = new System.Drawing.Point(524, 367);
            this.ourButton4.Name = "ourButton4";
            this.ourButton4.RoundCorner = false;
            this.ourButton4.Size = new System.Drawing.Size(30, 23);
            this.ourButton4.TabIndex = 11;
            this.ourButton4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ourButton4.TextPosition = PublishPackageToNuGet2017.Form.eTextPosition.Bottom;
            this.ourButton4.WithArrow = true;
            this.ourButton4.WithBorder = true;
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
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn_ok;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.GroupBox groupBox_groups;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListView listView2;
        private System.Windows.Forms.ListView listView1;
        private OurButton ourButton1;
        private OurButton ourButton2;
        private OurButton ourButton3;
        private System.Windows.Forms.TextBox textBox2;
        private OurButton ourButton4;
        private System.Windows.Forms.TextBox textBox3;
    }
}