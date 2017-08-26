namespace BoundingBoxDiscordDiscovery
{
    partial class FormDiscordDiscovery
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtFileName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtNLength = new System.Windows.Forms.TextBox();
            this.btnRunOff = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.bestSoFarDisVal = new System.Windows.Forms.Label();
            this.bestSoFarLocVal = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtR = new System.Windows.Forms.TextBox();
            this.txtMaxEntry = new System.Windows.Forms.TextBox();
            this.txtMinEntry = new System.Windows.Forms.TextBox();
            this.txtD = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtExeTime = new System.Windows.Forms.Label();
            this.btnRunOnl = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(28, 62);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(138, 30);
            this.label1.TabIndex = 0;
            this.label1.Text = "File Name:";
            // 
            // txtFileName
            // 
            this.txtFileName.Location = new System.Drawing.Point(274, 54);
            this.txtFileName.Margin = new System.Windows.Forms.Padding(6);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Size = new System.Drawing.Size(276, 31);
            this.txtFileName.TabIndex = 1;
            this.txtFileName.Text = "ECG.txt";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(28, 127);
            this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(116, 30);
            this.label2.TabIndex = 2;
            this.label2.Text = "NLength:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(28, 454);
            this.label3.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(241, 30);
            this.label3.TabIndex = 3;
            this.label3.Text = "BestSoFarDistance:";
            // 
            // txtNLength
            // 
            this.txtNLength.Location = new System.Drawing.Point(274, 119);
            this.txtNLength.Margin = new System.Windows.Forms.Padding(6);
            this.txtNLength.Name = "txtNLength";
            this.txtNLength.Size = new System.Drawing.Size(276, 31);
            this.txtNLength.TabIndex = 4;
            this.txtNLength.Text = "40";
            // 
            // btnRunOff
            // 
            this.btnRunOff.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRunOff.Location = new System.Drawing.Point(664, 62);
            this.btnRunOff.Margin = new System.Windows.Forms.Padding(6);
            this.btnRunOff.Name = "btnRunOff";
            this.btnRunOff.Size = new System.Drawing.Size(221, 112);
            this.btnRunOff.TabIndex = 5;
            this.btnRunOff.Text = "Offline + MinDist";
            this.btnRunOff.UseVisualStyleBackColor = true;
            this.btnRunOff.Click += new System.EventHandler(this.btnRunOffline_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(28, 509);
            this.label4.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(237, 30);
            this.label4.TabIndex = 6;
            this.label4.Text = "BestSoFarLocation:";
            // 
            // bestSoFarDisVal
            // 
            this.bestSoFarDisVal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bestSoFarDisVal.Location = new System.Drawing.Point(294, 454);
            this.bestSoFarDisVal.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.bestSoFarDisVal.Name = "bestSoFarDisVal";
            this.bestSoFarDisVal.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.bestSoFarDisVal.Size = new System.Drawing.Size(258, 31);
            this.bestSoFarDisVal.TabIndex = 7;
            this.bestSoFarDisVal.Text = "0";
            this.bestSoFarDisVal.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // bestSoFarLocVal
            // 
            this.bestSoFarLocVal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bestSoFarLocVal.Location = new System.Drawing.Point(291, 506);
            this.bestSoFarLocVal.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.bestSoFarLocVal.Name = "bestSoFarLocVal";
            this.bestSoFarLocVal.Size = new System.Drawing.Size(262, 31);
            this.bestSoFarLocVal.TabIndex = 8;
            this.bestSoFarLocVal.Text = "0";
            this.bestSoFarLocVal.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(28, 252);
            this.label5.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(39, 30);
            this.label5.TabIndex = 9;
            this.label5.Text = "R:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(28, 317);
            this.label6.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(229, 30);
            this.label6.TabIndex = 10;
            this.label6.Text = "MaxEntryPerNode:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(28, 383);
            this.label7.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(223, 30);
            this.label7.TabIndex = 11;
            this.label7.Text = "MinEntryPerNode:";
            // 
            // txtR
            // 
            this.txtR.Location = new System.Drawing.Point(274, 250);
            this.txtR.Margin = new System.Windows.Forms.Padding(6);
            this.txtR.Name = "txtR";
            this.txtR.Size = new System.Drawing.Size(274, 31);
            this.txtR.TabIndex = 12;
            this.txtR.Text = "2";
            // 
            // txtMaxEntry
            // 
            this.txtMaxEntry.Location = new System.Drawing.Point(274, 317);
            this.txtMaxEntry.Margin = new System.Windows.Forms.Padding(6);
            this.txtMaxEntry.Name = "txtMaxEntry";
            this.txtMaxEntry.Size = new System.Drawing.Size(276, 31);
            this.txtMaxEntry.TabIndex = 13;
            this.txtMaxEntry.Text = "25";
            // 
            // txtMinEntry
            // 
            this.txtMinEntry.Location = new System.Drawing.Point(274, 383);
            this.txtMinEntry.Margin = new System.Windows.Forms.Padding(6);
            this.txtMinEntry.Name = "txtMinEntry";
            this.txtMinEntry.Size = new System.Drawing.Size(276, 31);
            this.txtMinEntry.TabIndex = 14;
            this.txtMinEntry.Text = "12";
            // 
            // txtD
            // 
            this.txtD.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtD.Location = new System.Drawing.Point(274, 183);
            this.txtD.Margin = new System.Windows.Forms.Padding(6);
            this.txtD.Name = "txtD";
            this.txtD.Size = new System.Drawing.Size(274, 37);
            this.txtD.TabIndex = 15;
            this.txtD.Text = "6";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(28, 188);
            this.label8.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(39, 30);
            this.label8.TabIndex = 16;
            this.label8.Text = "D:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(28, 561);
            this.label9.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(249, 30);
            this.label9.TabIndex = 17;
            this.label9.Text = "Execution Time(ms):";
            // 
            // txtExeTime
            // 
            this.txtExeTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtExeTime.Location = new System.Drawing.Point(354, 555);
            this.txtExeTime.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.txtExeTime.Name = "txtExeTime";
            this.txtExeTime.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtExeTime.Size = new System.Drawing.Size(200, 44);
            this.txtExeTime.TabIndex = 18;
            this.txtExeTime.Text = "0";
            // 
            // btnRunOnl
            // 
            this.btnRunOnl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRunOnl.Location = new System.Drawing.Point(663, 338);
            this.btnRunOnl.Name = "btnRunOnl";
            this.btnRunOnl.Size = new System.Drawing.Size(222, 99);
            this.btnRunOnl.TabIndex = 19;
            this.btnRunOnl.Text = "Run Online";
            this.btnRunOnl.UseVisualStyleBackColor = true;
            this.btnRunOnl.Click += new System.EventHandler(this.btnRunOnl_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(663, 206);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(221, 113);
            this.button1.TabIndex = 20;
            this.button1.Text = "Offline Origin";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btn_OriginalOffline_Click);
            // 
            // FormDiscordDiscovery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(942, 604);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnRunOnl);
            this.Controls.Add(this.txtExeTime);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtD);
            this.Controls.Add(this.txtMinEntry);
            this.Controls.Add(this.txtMaxEntry);
            this.Controls.Add(this.txtR);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.bestSoFarLocVal);
            this.Controls.Add(this.bestSoFarDisVal);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnRunOff);
            this.Controls.Add(this.txtNLength);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtFileName);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "FormDiscordDiscovery";
            this.Text = "Bounding Box Discord Discovery - ver 1.7 - 26 Aug 2017";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtFileName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtNLength;
        private System.Windows.Forms.Button btnRunOff;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label bestSoFarDisVal;
        private System.Windows.Forms.Label bestSoFarLocVal;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtR;
        private System.Windows.Forms.TextBox txtMaxEntry;
        private System.Windows.Forms.TextBox txtMinEntry;
        private System.Windows.Forms.TextBox txtD;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label txtExeTime;
        private System.Windows.Forms.Button btnRunOnl;
        private System.Windows.Forms.Button button1;
    }
}

