﻿namespace susdulukripto
{
    partial class Form1
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
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.lsbMode = new System.Windows.Forms.ComboBox();
            this.VigenereMode = new System.Windows.Forms.ComboBox();
            this.button3 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.openAviDialog = new System.Windows.Forms.OpenFileDialog();
            this.openMessageDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveAviDialog = new System.Windows.Forms.SaveFileDialog();
            this.PSNR = new System.Windows.Forms.Button();
            this.PSNRlabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBox2
            // 
            this.textBox2.ForeColor = System.Drawing.SystemColors.GrayText;
            this.textBox2.Location = new System.Drawing.Point(93, 26);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(286, 20);
            this.textBox2.TabIndex = 4;
            this.textBox2.Text = "key max 25 karakter";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 24);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "Load AVI";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.AutoSize = true;
            this.button2.Location = new System.Drawing.Point(12, 100);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(87, 23);
            this.button2.TabIndex = 6;
            this.button2.Text = "Load Message";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // lsbMode
            // 
            this.lsbMode.DisplayMember = "1 LSB";
            this.lsbMode.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lsbMode.Items.AddRange(new object[] {
            "1 LSB",
            "2 LSB"});
            this.lsbMode.Location = new System.Drawing.Point(12, 66);
            this.lsbMode.Name = "lsbMode";
            this.lsbMode.Size = new System.Drawing.Size(121, 21);
            this.lsbMode.TabIndex = 7;
            this.lsbMode.Text = "1 LSB";
            // 
            // VigenereMode
            // 
            this.VigenereMode.FormattingEnabled = true;
            this.VigenereMode.Items.AddRange(new object[] {
            "Use Vigenere",
            "Without Vigenere"});
            this.VigenereMode.Location = new System.Drawing.Point(139, 66);
            this.VigenereMode.Name = "VigenereMode";
            this.VigenereMode.Size = new System.Drawing.Size(121, 21);
            this.VigenereMode.TabIndex = 8;
            this.VigenereMode.Text = "Without Vigenere";
            // 
            // button3
            // 
            this.button3.AutoSize = true;
            this.button3.Location = new System.Drawing.Point(243, 100);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(89, 23);
            this.button3.TabIndex = 9;
            this.button3.Text = "Check Payload";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Location = new System.Drawing.Point(12, 140);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(320, 240);
            this.panel1.TabIndex = 10;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(12, 386);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 11;
            this.button4.Text = "Play";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(93, 386);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 12;
            this.button5.Text = "Pause";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(174, 386);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(75, 23);
            this.button6.TabIndex = 13;
            this.button6.Text = "Stop";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(338, 299);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(75, 23);
            this.button7.TabIndex = 14;
            this.button7.Text = "Hide";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(338, 328);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(75, 23);
            this.button8.TabIndex = 15;
            this.button8.Text = "Extract";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // openAviDialog
            // 
            this.openAviDialog.FileName = "defaultFileName";
            this.openAviDialog.Title = "Load AVI";
            // 
            // openMessageDialog
            // 
            this.openMessageDialog.FileName = "openFileDialog1";
            // 
            // PSNR
            // 
            this.PSNR.Location = new System.Drawing.Point(338, 357);
            this.PSNR.Name = "PSNR";
            this.PSNR.Size = new System.Drawing.Size(75, 23);
            this.PSNR.TabIndex = 16;
            this.PSNR.Text = "PSNR";
            this.PSNR.UseVisualStyleBackColor = true;
            this.PSNR.Click += new System.EventHandler(this.PSNR_Click);
            // 
            // PSNRlabel
            // 
            this.PSNRlabel.AutoSize = true;
            this.PSNRlabel.Location = new System.Drawing.Point(297, 391);
            this.PSNRlabel.Name = "PSNRlabel";
            this.PSNRlabel.Size = new System.Drawing.Size(0, 13);
            this.PSNRlabel.TabIndex = 17;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(419, 417);
            this.Controls.Add(this.PSNRlabel);
            this.Controls.Add(this.PSNR);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.VigenereMode);
            this.Controls.Add(this.lsbMode);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ComboBox lsbMode;
        private System.Windows.Forms.ComboBox VigenereMode;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.OpenFileDialog openAviDialog;
        private System.Windows.Forms.OpenFileDialog openMessageDialog;
        private System.Windows.Forms.SaveFileDialog saveAviDialog;
        private System.Windows.Forms.Button PSNR;
        private System.Windows.Forms.Label PSNRlabel;
    }
}

