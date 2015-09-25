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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.cboAddress = new System.Windows.Forms.ComboBox();
            this.gbxDirection = new System.Windows.Forms.GroupBox();
            this.rbtBackward = new System.Windows.Forms.RadioButton();
            this.rbtForward = new System.Windows.Forms.RadioButton();
            this.trkSpeed = new System.Windows.Forms.TrackBar();
            this.cmdLight = new System.Windows.Forms.Button();
            this.lblSpeed = new System.Windows.Forms.Label();
            this.cmdBreak = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.digitalComponentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.initializeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadLocomotiveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gbxDirection.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trkSpeed)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cboAddress
            // 
            resources.ApplyResources(this.cboAddress, "cboAddress");
            this.cboAddress.FormattingEnabled = true;
            this.cboAddress.Name = "cboAddress";
            // 
            // gbxDirection
            // 
            resources.ApplyResources(this.gbxDirection, "gbxDirection");
            this.gbxDirection.Controls.Add(this.rbtBackward);
            this.gbxDirection.Controls.Add(this.rbtForward);
            this.gbxDirection.Name = "gbxDirection";
            this.gbxDirection.TabStop = false;
            // 
            // rbtBackward
            // 
            resources.ApplyResources(this.rbtBackward, "rbtBackward");
            this.rbtBackward.Name = "rbtBackward";
            this.rbtBackward.UseVisualStyleBackColor = true;
            this.rbtBackward.CheckedChanged += new System.EventHandler(this.rbtForwardBackward_CheckedChanged);
            // 
            // rbtForward
            // 
            resources.ApplyResources(this.rbtForward, "rbtForward");
            this.rbtForward.Checked = true;
            this.rbtForward.Name = "rbtForward";
            this.rbtForward.TabStop = true;
            this.rbtForward.UseVisualStyleBackColor = true;
            this.rbtForward.CheckedChanged += new System.EventHandler(this.rbtForwardBackward_CheckedChanged);
            // 
            // trkSpeed
            // 
            resources.ApplyResources(this.trkSpeed, "trkSpeed");
            this.trkSpeed.Maximum = 128;
            this.trkSpeed.Name = "trkSpeed";
            this.trkSpeed.Scroll += new System.EventHandler(this.trkSpeed_Scroll);
            // 
            // cmdLight
            // 
            resources.ApplyResources(this.cmdLight, "cmdLight");
            this.cmdLight.Name = "cmdLight";
            this.cmdLight.UseVisualStyleBackColor = true;
            this.cmdLight.Click += new System.EventHandler(this.cmdLight_Click);
            // 
            // lblSpeed
            // 
            resources.ApplyResources(this.lblSpeed, "lblSpeed");
            this.lblSpeed.Name = "lblSpeed";
            // 
            // cmdBreak
            // 
            resources.ApplyResources(this.cmdBreak, "cmdBreak");
            this.cmdBreak.Name = "cmdBreak";
            this.cmdBreak.UseVisualStyleBackColor = true;
            this.cmdBreak.Click += new System.EventHandler(this.cmdBreak_Click);
            // 
            // menuStrip1
            // 
            resources.ApplyResources(this.menuStrip1, "menuStrip1");
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.digitalComponentsToolStripMenuItem});
            this.menuStrip1.Name = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            resources.ApplyResources(this.fileToolStripMenuItem, "fileToolStripMenuItem");
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            // 
            // exitToolStripMenuItem
            // 
            resources.ApplyResources(this.exitToolStripMenuItem, "exitToolStripMenuItem");
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            // 
            // digitalComponentsToolStripMenuItem
            // 
            resources.ApplyResources(this.digitalComponentsToolStripMenuItem, "digitalComponentsToolStripMenuItem");
            this.digitalComponentsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.initializeToolStripMenuItem,
            this.loadLocomotiveToolStripMenuItem});
            this.digitalComponentsToolStripMenuItem.Name = "digitalComponentsToolStripMenuItem";
            // 
            // initializeToolStripMenuItem
            // 
            resources.ApplyResources(this.initializeToolStripMenuItem, "initializeToolStripMenuItem");
            this.initializeToolStripMenuItem.Name = "initializeToolStripMenuItem";
            this.initializeToolStripMenuItem.Click += new System.EventHandler(this.initializeToolStripMenuItem_Click);
            // 
            // loadLocomotiveToolStripMenuItem
            // 
            resources.ApplyResources(this.loadLocomotiveToolStripMenuItem, "loadLocomotiveToolStripMenuItem");
            this.loadLocomotiveToolStripMenuItem.Name = "loadLocomotiveToolStripMenuItem";
            this.loadLocomotiveToolStripMenuItem.Click += new System.EventHandler(this.loadLocomotiveToolStripMenuItem_Click);
            // 
            // frmMain
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cmdBreak);
            this.Controls.Add(this.lblSpeed);
            this.Controls.Add(this.cmdLight);
            this.Controls.Add(this.trkSpeed);
            this.Controls.Add(this.gbxDirection);
            this.Controls.Add(this.cboAddress);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmMain";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.gbxDirection.ResumeLayout(false);
            this.gbxDirection.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trkSpeed)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
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
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem digitalComponentsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem initializeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadLocomotiveToolStripMenuItem;
    }
}

