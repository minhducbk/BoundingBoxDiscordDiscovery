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
            this.btnRun = new System.Windows.Forms.Button();
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
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(14, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "File Name:";
            // 
            // txtFileName
            // 
            this.txtFileName.Location = new System.Drawing.Point(137, 28);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Size = new System.Drawing.Size(140, 20);
            this.txtFileName.TabIndex = 1;
            this.txtFileName.Text = "ECG_5000.txt";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(14, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "NLength:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(14, 236);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(128, 16);
            this.label3.TabIndex = 3;
            this.label3.Text = "BestSoFarDistance:";
            // 
            // txtNLength
            // 
            this.txtNLength.Location = new System.Drawing.Point(137, 62);
            this.txtNLength.Name = "txtNLength";
            this.txtNLength.Size = new System.Drawing.Size(140, 20);
            this.txtNLength.TabIndex = 4;
            this.txtNLength.Text = "40";
            // 
            // btnRun
            // 
            this.btnRun.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRun.Location = new System.Drawing.Point(335, 51);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(91, 41);
            this.btnRun.TabIndex = 5;
            this.btnRun.Text = "Run";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(14, 263);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(126, 16);
            this.label4.TabIndex = 6;
            this.label4.Text = "BestSoFarLocation:";
            // 
            // bestSoFarDisVal
            // 
            this.bestSoFarDisVal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bestSoFarDisVal.Location = new System.Drawing.Point(148, 236);
            this.bestSoFarDisVal.Name = "bestSoFarDisVal";
            this.bestSoFarDisVal.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.bestSoFarDisVal.Size = new System.Drawing.Size(129, 16);
            this.bestSoFarDisVal.TabIndex = 7;
            this.bestSoFarDisVal.Text = "0";
            this.bestSoFarDisVal.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // bestSoFarLocVal
            // 
            this.bestSoFarLocVal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bestSoFarLocVal.Location = new System.Drawing.Point(146, 263);
            this.bestSoFarLocVal.Name = "bestSoFarLocVal";
            this.bestSoFarLocVal.Size = new System.Drawing.Size(131, 16);
            this.bestSoFarLocVal.TabIndex = 8;
            this.bestSoFarLocVal.Text = "0";
            this.bestSoFarLocVal.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(14, 131);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(21, 16);
            this.label5.TabIndex = 9;
            this.label5.Text = "R:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(14, 165);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(121, 16);
            this.label6.TabIndex = 10;
            this.label6.Text = "MaxEntryPerNode:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(14, 199);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(117, 16);
            this.label7.TabIndex = 11;
            this.label7.Text = "MinEntryPerNode:";
            // 
            // txtR
            // 
            this.txtR.Location = new System.Drawing.Point(137, 130);
            this.txtR.Name = "txtR";
            this.txtR.Size = new System.Drawing.Size(139, 20);
            this.txtR.TabIndex = 12;
            this.txtR.Text = "2";
            // 
            // txtMaxEntry
            // 
            this.txtMaxEntry.Location = new System.Drawing.Point(137, 165);
            this.txtMaxEntry.Name = "txtMaxEntry";
            this.txtMaxEntry.Size = new System.Drawing.Size(140, 20);
            this.txtMaxEntry.TabIndex = 13;
            this.txtMaxEntry.Text = "25";
            // 
            // txtMinEntry
            // 
            this.txtMinEntry.Location = new System.Drawing.Point(137, 199);
            this.txtMinEntry.Name = "txtMinEntry";
            this.txtMinEntry.Size = new System.Drawing.Size(140, 20);
            this.txtMinEntry.TabIndex = 14;
            this.txtMinEntry.Text = "12";
            // 
            // txtD
            // 
            this.txtD.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtD.Location = new System.Drawing.Point(137, 95);
            this.txtD.Name = "txtD";
            this.txtD.Size = new System.Drawing.Size(139, 22);
            this.txtD.TabIndex = 15;
            this.txtD.Text = "6";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(14, 98);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(21, 16);
            this.label8.TabIndex = 16;
            this.label8.Text = "D:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(11, 289);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(129, 16);
            this.label9.TabIndex = 17;
            this.label9.Text = "Execution Time(ms):";
            // 
            // txtExeTime
            // 
            this.txtExeTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtExeTime.Location = new System.Drawing.Point(176, 289);
            this.txtExeTime.Name = "txtExeTime";
            this.txtExeTime.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtExeTime.Size = new System.Drawing.Size(100, 23);
            this.txtExeTime.TabIndex = 18;
            this.txtExeTime.Text = "0";
            // 
            // FormDiscordDiscovery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(471, 314);
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
            this.Controls.Add(this.btnRun);
            this.Controls.Add(this.txtNLength);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtFileName);
            this.Controls.Add(this.label1);
            this.Name = "FormDiscordDiscovery";
            this.Text = "Bounding Box Discord Discovery";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtFileName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtNLength;
        private System.Windows.Forms.Button btnRun;
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
    }
}

