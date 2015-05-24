namespace Hmax
{
    partial class SettingsForm
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
            this.button3 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxCert = new System.Windows.Forms.TextBox();
            this.button5 = new System.Windows.Forms.Button();
            this.textBoxChar = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.statusLabel = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(116, 261);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(138, 23);
            this.button3.TabIndex = 21;
            this.button3.Text = "Define New Range";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 232);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 13);
            this.label3.TabIndex = 19;
            this.label3.Text = "Character Range";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(13, 48);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(113, 23);
            this.button2.TabIndex = 17;
            this.button2.Text = "Replace Cert.";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Enabled = false;
            this.button1.Location = new System.Drawing.Point(242, 58);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(150, 23);
            this.button1.TabIndex = 16;
            this.button1.Text = "Add (NOT IMPLEMENTED)";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // listBox1
            // 
            this.listBox1.Enabled = false;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Items.AddRange(new object[] {
            "E-Commerce",
            "Banking",
            "Social",
            "E-Mails",
            "Others"});
            this.listBox1.Location = new System.Drawing.Point(116, 12);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(120, 69);
            this.listBox1.TabIndex = 15;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "Cert Subject";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Enabled = false;
            this.label1.Location = new System.Drawing.Point(16, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Profile Groups";
            // 
            // textBoxCert
            // 
            this.textBoxCert.Location = new System.Drawing.Point(83, 13);
            this.textBoxCert.Name = "textBoxCert";
            this.textBoxCert.Size = new System.Drawing.Size(161, 20);
            this.textBoxCert.TabIndex = 23;
            this.textBoxCert.Text = "CN=Test Cert1";
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(26, 53);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(139, 21);
            this.button5.TabIndex = 24;
            this.button5.Text = "Use Smart Card";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // textBoxChar
            // 
            this.textBoxChar.Location = new System.Drawing.Point(116, 229);
            this.textBoxChar.Name = "textBoxChar";
            this.textBoxChar.Size = new System.Drawing.Size(620, 20);
            this.textBoxChar.TabIndex = 25;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.textBoxCert);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(16, 9);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(260, 92);
            this.panel1.TabIndex = 26;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(291, 45);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(43, 25);
            this.label6.TabIndex = 27;
            this.label6.Text = "OR";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(26, 14);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(139, 23);
            this.button4.TabIndex = 26;
            this.button4.Text = "Use Regular File";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 87);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 13);
            this.label4.TabIndex = 27;
            this.label4.Text = "Private Key";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(19, 297);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(717, 20);
            this.textBox1.TabIndex = 29;
            this.textBox1.Text = "Test your default password here.";
            this.textBox1.Enter += new System.EventHandler(this.textBox1_Enter);
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.statusLabel);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Location = new System.Drawing.Point(116, 87);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(620, 136);
            this.panel2.TabIndex = 30;
            // 
            // statusLabel
            // 
            this.statusLabel.AutoSize = true;
            this.statusLabel.Location = new System.Drawing.Point(71, 108);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(35, 13);
            this.statusLabel.TabIndex = 30;
            this.statusLabel.Text = "label7";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 108);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(42, 13);
            this.label5.TabIndex = 29;
            this.label5.Text = "Status:";
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel3.Controls.Add(this.button5);
            this.panel3.Controls.Add(this.button4);
            this.panel3.Location = new System.Drawing.Point(386, 9);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(200, 100);
            this.panel3.TabIndex = 28;
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(772, 343);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBoxChar);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.label1);
            this.Name = "SettingsForm";
            this.Text = "Hmax Settings";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxCert;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.TextBox textBoxChar;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel3;
    }
}