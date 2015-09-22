namespace Flake.MoBa.Driver
{
    partial class frmMain
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
            this.cboAddress = new System.Windows.Forms.ComboBox();
            this.gbxDirection = new System.Windows.Forms.GroupBox();
            this.rbtBackward = new System.Windows.Forms.RadioButton();
            this.rbtForward = new System.Windows.Forms.RadioButton();
            this.trkSpeed = new System.Windows.Forms.TrackBar();
            this.cmdLight = new System.Windows.Forms.Button();
            this.lblSpeed = new System.Windows.Forms.Label();
            this.cmdBreak = new System.Windows.Forms.Button();
            this.gbxDirection.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trkSpeed)).BeginInit();
            this.SuspendLayout();
            // 
            // cboAddress
            // 
            this.cboAddress.FormattingEnabled = true;
            this.cboAddress.Location = new System.Drawing.Point(12, 12);
            this.cboAddress.Name = "cboAddress";
            this.cboAddress.Size = new System.Drawing.Size(121, 21);
            this.cboAddress.TabIndex = 0;
            this.cboAddress.Text = "32";
            // 
            // gbxDirection
            // 
            this.gbxDirection.Controls.Add(this.rbtBackward);
            this.gbxDirection.Controls.Add(this.rbtForward);
            this.gbxDirection.Location = new System.Drawing.Point(12, 39);
            this.gbxDirection.Name = "gbxDirection";
            this.gbxDirection.Size = new System.Drawing.Size(200, 100);
            this.gbxDirection.TabIndex = 1;
            this.gbxDirection.TabStop = false;
            this.gbxDirection.Text = "direction";
            // 
            // rbtBackward
            // 
            this.rbtBackward.AutoSize = true;
            this.rbtBackward.Location = new System.Drawing.Point(7, 44);
            this.rbtBackward.Name = "rbtBackward";
            this.rbtBackward.Size = new System.Drawing.Size(72, 17);
            this.rbtBackward.TabIndex = 1;
            this.rbtBackward.Text = "backward";
            this.rbtBackward.UseVisualStyleBackColor = true;
            this.rbtBackward.CheckedChanged += new System.EventHandler(this.rbtBackward_CheckedChanged);
            // 
            // rbtForward
            // 
            this.rbtForward.AutoSize = true;
            this.rbtForward.Checked = true;
            this.rbtForward.Location = new System.Drawing.Point(7, 20);
            this.rbtForward.Name = "rbtForward";
            this.rbtForward.Size = new System.Drawing.Size(60, 17);
            this.rbtForward.TabIndex = 0;
            this.rbtForward.TabStop = true;
            this.rbtForward.Text = "forward";
            this.rbtForward.UseVisualStyleBackColor = true;
            this.rbtForward.CheckedChanged += new System.EventHandler(this.rbtForward_CheckedChanged);
            // 
            // trkSpeed
            // 
            this.trkSpeed.Location = new System.Drawing.Point(280, 12);
            this.trkSpeed.Maximum = 128;
            this.trkSpeed.Name = "trkSpeed";
            this.trkSpeed.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trkSpeed.Size = new System.Drawing.Size(45, 492);
            this.trkSpeed.TabIndex = 2;
            this.trkSpeed.Scroll += new System.EventHandler(this.trkSpeed_Scroll);
            // 
            // cmdLight
            // 
            this.cmdLight.Location = new System.Drawing.Point(12, 146);
            this.cmdLight.Name = "cmdLight";
            this.cmdLight.Size = new System.Drawing.Size(75, 23);
            this.cmdLight.TabIndex = 3;
            this.cmdLight.Text = "Light";
            this.cmdLight.UseVisualStyleBackColor = true;
            this.cmdLight.Click += new System.EventHandler(this.cmdLight_Click);
            // 
            // lblSpeed
            // 
            this.lblSpeed.AutoSize = true;
            this.lblSpeed.Location = new System.Drawing.Point(185, 459);
            this.lblSpeed.Name = "lblSpeed";
            this.lblSpeed.Size = new System.Drawing.Size(13, 13);
            this.lblSpeed.TabIndex = 4;
            this.lblSpeed.Text = "0";
            // 
            // cmdBreak
            // 
            this.cmdBreak.Location = new System.Drawing.Point(12, 230);
            this.cmdBreak.Name = "cmdBreak";
            this.cmdBreak.Size = new System.Drawing.Size(75, 23);
            this.cmdBreak.TabIndex = 6;
            this.cmdBreak.Text = "Break";
            this.cmdBreak.UseVisualStyleBackColor = true;
            this.cmdBreak.Click += new System.EventHandler(this.cmdBreak_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(320, 516);
            this.Controls.Add(this.cmdBreak);
            this.Controls.Add(this.lblSpeed);
            this.Controls.Add(this.cmdLight);
            this.Controls.Add(this.trkSpeed);
            this.Controls.Add(this.gbxDirection);
            this.Controls.Add(this.cboAddress);
            this.Name = "frmMain";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.gbxDirection.ResumeLayout(false);
            this.gbxDirection.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trkSpeed)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cboAddress;
        private System.Windows.Forms.GroupBox gbxDirection;
        private System.Windows.Forms.RadioButton rbtBackward;
        private System.Windows.Forms.RadioButton rbtForward;
        private System.Windows.Forms.TrackBar trkSpeed;
        private System.Windows.Forms.Button cmdLight;
        private System.Windows.Forms.Label lblSpeed;
        private System.Windows.Forms.Button cmdBreak;
    }
}

