namespace QRCodeWebcam2
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
            this.components = new System.ComponentModel.Container();
            this.Label1 = new System.Windows.Forms.Label();
            this.txtQRCode = new System.Windows.Forms.TextBox();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.cboDevice = new System.Windows.Forms.ComboBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(12, 9);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(46, 13);
            this.Label1.TabIndex = 2;
            this.Label1.Text = "Camera:";
            // 
            // txtQRCode
            // 
            this.txtQRCode.Location = new System.Drawing.Point(474, 51);
            this.txtQRCode.Multiline = true;
            this.txtQRCode.Name = "txtQRCode";
            this.txtQRCode.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtQRCode.Size = new System.Drawing.Size(263, 207);
            this.txtQRCode.TabIndex = 8;
            this.txtQRCode.TextChanged += new System.EventHandler(this.txtQRCode_TextChanged);
            // 
            // pictureBox
            // 
            this.pictureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox.Location = new System.Drawing.Point(15, 51);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(435, 291);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox.TabIndex = 7;
            this.pictureBox.TabStop = false;
            this.pictureBox.Click += new System.EventHandler(this.pictureBox_Click);
            // 
            // cboDevice
            // 
            this.cboDevice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDevice.FormattingEnabled = true;
            this.cboDevice.Location = new System.Drawing.Point(73, 7);
            this.cboDevice.Name = "cboDevice";
            this.cboDevice.Size = new System.Drawing.Size(356, 21);
            this.cboDevice.TabIndex = 6;
            this.cboDevice.SelectedIndexChanged += new System.EventHandler(this.cboDevice_SelectedIndexChanged);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(474, 278);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(263, 62);
            this.btnStart.TabIndex = 5;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(749, 352);
            this.Controls.Add(this.txtQRCode);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.cboDevice);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.Label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.TextBox txtQRCode;
        internal System.Windows.Forms.PictureBox pictureBox;
        internal System.Windows.Forms.ComboBox cboDevice;
        internal System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Timer timer1;
    }
}

