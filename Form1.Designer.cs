namespace _9CHeadlessMonitoringAndLauncher
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelMenu = new System.Windows.Forms.Panel();
            this.iconButton8 = new FontAwesome.Sharp.IconButton();
            this.iconButton6 = new FontAwesome.Sharp.IconButton();
            this.iconButton5 = new FontAwesome.Sharp.IconButton();
            this.iconButton4 = new FontAwesome.Sharp.IconButton();
            this.iconButton3 = new FontAwesome.Sharp.IconButton();
            this.iconButton2 = new FontAwesome.Sharp.IconButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.IconPanel = new System.Windows.Forms.Panel();
            this.btnMenu = new FontAwesome.Sharp.IconButton();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panelTitleBar = new System.Windows.Forms.Panel();
            this.iconButton9 = new FontAwesome.Sharp.IconButton();
            this.label1 = new System.Windows.Forms.Label();
            this.snapshotPanel = new System.Windows.Forms.Panel();
            this.snapshotMenuActionProgressTLT = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.snapshotMenuActionLBL = new System.Windows.Forms.Label();
            this.snapshotMenuActionTLT = new System.Windows.Forms.Label();
            this.snapshotMenuFileLBL = new System.Windows.Forms.Label();
            this.snapshotMenuFileTLT = new System.Windows.Forms.Label();
            this.currentStatusTLT = new System.Windows.Forms.Label();
            this.SnapshotMenuCurrentStatusLBL = new System.Windows.Forms.Label();
            this.snapshotMenuStatusTitle = new System.Windows.Forms.Label();
            this.snapshotMenuStartBTN = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.snapshotMenuTitle = new System.Windows.Forms.Label();
            this.snapshotConfigPanel2 = new System.Windows.Forms.Panel();
            this.snapshotConfigBTN = new System.Windows.Forms.Button();
            this.snapshotConfigCurrentPathLBL = new System.Windows.Forms.Label();
            this.snapshotConfigCurrentPathTLT = new System.Windows.Forms.Label();
            this.snapshotConfigInnerTLT = new System.Windows.Forms.Label();
            this.snapshotConfigTitlePanel = new System.Windows.Forms.Panel();
            this.snapshotConfigTitleLBL = new System.Windows.Forms.Label();
            this.SnapshotStartWorker = new System.ComponentModel.BackgroundWorker();
            this.SnapshotDownloadWrkr = new System.ComponentModel.BackgroundWorker();
            this.folderBrowserDialog2 = new System.Windows.Forms.FolderBrowserDialog();
            this.panelMenu.SuspendLayout();
            this.panel1.SuspendLayout();
            this.IconPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panelTitleBar.SuspendLayout();
            this.snapshotPanel.SuspendLayout();
            this.panel2.SuspendLayout();
            this.snapshotConfigPanel2.SuspendLayout();
            this.snapshotConfigTitlePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMenu
            // 
            this.panelMenu.BackColor = System.Drawing.Color.RoyalBlue;
            this.panelMenu.Controls.Add(this.iconButton8);
            this.panelMenu.Controls.Add(this.iconButton6);
            this.panelMenu.Controls.Add(this.iconButton5);
            this.panelMenu.Controls.Add(this.iconButton4);
            this.panelMenu.Controls.Add(this.iconButton3);
            this.panelMenu.Controls.Add(this.iconButton2);
            this.panelMenu.Controls.Add(this.panel1);
            this.panelMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelMenu.Location = new System.Drawing.Point(0, 0);
            this.panelMenu.Name = "panelMenu";
            this.panelMenu.Size = new System.Drawing.Size(190, 397);
            this.panelMenu.TabIndex = 0;
            // 
            // iconButton8
            // 
            this.iconButton8.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.iconButton8.FlatAppearance.BorderSize = 0;
            this.iconButton8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconButton8.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.iconButton8.ForeColor = System.Drawing.SystemColors.Control;
            this.iconButton8.IconChar = FontAwesome.Sharp.IconChar.SignOutAlt;
            this.iconButton8.IconColor = System.Drawing.Color.White;
            this.iconButton8.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconButton8.IconSize = 30;
            this.iconButton8.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iconButton8.Location = new System.Drawing.Point(0, 348);
            this.iconButton8.Name = "iconButton8";
            this.iconButton8.Padding = new System.Windows.Forms.Padding(10, 0, 0, 10);
            this.iconButton8.Size = new System.Drawing.Size(190, 49);
            this.iconButton8.TabIndex = 7;
            this.iconButton8.Tag = "Exit";
            this.iconButton8.Text = "iconButton8";
            this.iconButton8.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.iconButton8.UseVisualStyleBackColor = true;
            this.iconButton8.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // iconButton6
            // 
            this.iconButton6.Dock = System.Windows.Forms.DockStyle.Top;
            this.iconButton6.FlatAppearance.BorderSize = 0;
            this.iconButton6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconButton6.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.iconButton6.ForeColor = System.Drawing.SystemColors.Control;
            this.iconButton6.IconChar = FontAwesome.Sharp.IconChar.CameraRotate;
            this.iconButton6.IconColor = System.Drawing.Color.White;
            this.iconButton6.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconButton6.IconSize = 30;
            this.iconButton6.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iconButton6.Location = new System.Drawing.Point(0, 281);
            this.iconButton6.Name = "iconButton6";
            this.iconButton6.Padding = new System.Windows.Forms.Padding(10, 10, 0, 0);
            this.iconButton6.Size = new System.Drawing.Size(190, 49);
            this.iconButton6.TabIndex = 5;
            this.iconButton6.Tag = "Snapshot Config";
            this.iconButton6.Text = "iconButton6";
            this.iconButton6.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.iconButton6.UseVisualStyleBackColor = true;
            this.iconButton6.Click += new System.EventHandler(this.LoadSnapshotConfigMenu);
            // 
            // iconButton5
            // 
            this.iconButton5.Dock = System.Windows.Forms.DockStyle.Top;
            this.iconButton5.FlatAppearance.BorderSize = 0;
            this.iconButton5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconButton5.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.iconButton5.ForeColor = System.Drawing.SystemColors.Control;
            this.iconButton5.IconChar = FontAwesome.Sharp.IconChar.Camera;
            this.iconButton5.IconColor = System.Drawing.Color.White;
            this.iconButton5.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconButton5.IconSize = 30;
            this.iconButton5.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iconButton5.Location = new System.Drawing.Point(0, 232);
            this.iconButton5.Name = "iconButton5";
            this.iconButton5.Padding = new System.Windows.Forms.Padding(10, 10, 0, 0);
            this.iconButton5.Size = new System.Drawing.Size(190, 49);
            this.iconButton5.TabIndex = 4;
            this.iconButton5.Tag = "Snapshot";
            this.iconButton5.Text = "iconButton5";
            this.iconButton5.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.iconButton5.UseVisualStyleBackColor = true;
            this.iconButton5.Click += new System.EventHandler(this.LoadSnapshotMenu);
            // 
            // iconButton4
            // 
            this.iconButton4.Dock = System.Windows.Forms.DockStyle.Top;
            this.iconButton4.FlatAppearance.BorderSize = 0;
            this.iconButton4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconButton4.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.iconButton4.ForeColor = System.Drawing.SystemColors.Control;
            this.iconButton4.IconChar = FontAwesome.Sharp.IconChar.Recycle;
            this.iconButton4.IconColor = System.Drawing.Color.White;
            this.iconButton4.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconButton4.IconSize = 30;
            this.iconButton4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iconButton4.Location = new System.Drawing.Point(0, 183);
            this.iconButton4.Name = "iconButton4";
            this.iconButton4.Padding = new System.Windows.Forms.Padding(10, 10, 0, 0);
            this.iconButton4.Size = new System.Drawing.Size(190, 49);
            this.iconButton4.TabIndex = 3;
            this.iconButton4.Tag = "Node Restart";
            this.iconButton4.Text = "iconButton4";
            this.iconButton4.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.iconButton4.UseVisualStyleBackColor = true;
            // 
            // iconButton3
            // 
            this.iconButton3.Dock = System.Windows.Forms.DockStyle.Top;
            this.iconButton3.FlatAppearance.BorderSize = 0;
            this.iconButton3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconButton3.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.iconButton3.ForeColor = System.Drawing.SystemColors.Control;
            this.iconButton3.IconChar = FontAwesome.Sharp.IconChar.Server;
            this.iconButton3.IconColor = System.Drawing.Color.White;
            this.iconButton3.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconButton3.IconSize = 30;
            this.iconButton3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iconButton3.Location = new System.Drawing.Point(0, 134);
            this.iconButton3.Name = "iconButton3";
            this.iconButton3.Padding = new System.Windows.Forms.Padding(10, 10, 0, 0);
            this.iconButton3.Size = new System.Drawing.Size(190, 49);
            this.iconButton3.TabIndex = 2;
            this.iconButton3.Tag = "Node Setup";
            this.iconButton3.Text = "iconButton3";
            this.iconButton3.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.iconButton3.UseVisualStyleBackColor = true;
            // 
            // iconButton2
            // 
            this.iconButton2.Dock = System.Windows.Forms.DockStyle.Top;
            this.iconButton2.FlatAppearance.BorderSize = 0;
            this.iconButton2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconButton2.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.iconButton2.ForeColor = System.Drawing.SystemColors.Control;
            this.iconButton2.IconChar = FontAwesome.Sharp.IconChar.Home;
            this.iconButton2.IconColor = System.Drawing.Color.White;
            this.iconButton2.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconButton2.IconSize = 30;
            this.iconButton2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iconButton2.Location = new System.Drawing.Point(0, 85);
            this.iconButton2.Name = "iconButton2";
            this.iconButton2.Padding = new System.Windows.Forms.Padding(10, 10, 0, 0);
            this.iconButton2.Size = new System.Drawing.Size(190, 49);
            this.iconButton2.TabIndex = 1;
            this.iconButton2.Tag = "Home";
            this.iconButton2.Text = "iconButton2";
            this.iconButton2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.iconButton2.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.IconPanel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(190, 85);
            this.panel1.TabIndex = 0;
            // 
            // IconPanel
            // 
            this.IconPanel.Controls.Add(this.btnMenu);
            this.IconPanel.Controls.Add(this.pictureBox1);
            this.IconPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.IconPanel.Location = new System.Drawing.Point(0, 0);
            this.IconPanel.Name = "IconPanel";
            this.IconPanel.Size = new System.Drawing.Size(190, 85);
            this.IconPanel.TabIndex = 1;
            // 
            // btnMenu
            // 
            this.btnMenu.FlatAppearance.BorderSize = 0;
            this.btnMenu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMenu.IconChar = FontAwesome.Sharp.IconChar.Bars;
            this.btnMenu.IconColor = System.Drawing.Color.White;
            this.btnMenu.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnMenu.IconSize = 30;
            this.btnMenu.Location = new System.Drawing.Point(115, 3);
            this.btnMenu.Name = "btnMenu";
            this.btnMenu.Size = new System.Drawing.Size(72, 44);
            this.btnMenu.TabIndex = 1;
            this.btnMenu.UseVisualStyleBackColor = true;
            this.btnMenu.Click += new System.EventHandler(this.btnMenu_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::_9CHeadlessMonitoringAndLauncher.Properties.Resources.Asset_1;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(112, 79);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // panelTitleBar
            // 
            this.panelTitleBar.BackColor = System.Drawing.Color.White;
            this.panelTitleBar.Controls.Add(this.iconButton9);
            this.panelTitleBar.Controls.Add(this.label1);
            this.panelTitleBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTitleBar.Location = new System.Drawing.Point(190, 0);
            this.panelTitleBar.Name = "panelTitleBar";
            this.panelTitleBar.Size = new System.Drawing.Size(457, 44);
            this.panelTitleBar.TabIndex = 1;
            // 
            // iconButton9
            // 
            this.iconButton9.AutoSize = true;
            this.iconButton9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(74)))), ((int)(((byte)(130)))));
            this.iconButton9.Dock = System.Windows.Forms.DockStyle.Right;
            this.iconButton9.FlatAppearance.BorderSize = 0;
            this.iconButton9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconButton9.IconChar = FontAwesome.Sharp.IconChar.X;
            this.iconButton9.IconColor = System.Drawing.Color.White;
            this.iconButton9.IconFont = FontAwesome.Sharp.IconFont.Solid;
            this.iconButton9.IconSize = 15;
            this.iconButton9.Location = new System.Drawing.Point(423, 0);
            this.iconButton9.Name = "iconButton9";
            this.iconButton9.Size = new System.Drawing.Size(34, 44);
            this.iconButton9.TabIndex = 0;
            this.iconButton9.UseVisualStyleBackColor = false;
            this.iconButton9.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Verdana", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.label1.Size = new System.Drawing.Size(534, 44);
            this.label1.TabIndex = 3;
            this.label1.Text = "Nine Chronicles Node Monitor";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.label1_MouseDown);
            // 
            // snapshotPanel
            // 
            this.snapshotPanel.BackColor = System.Drawing.Color.White;
            this.snapshotPanel.Controls.Add(this.snapshotMenuActionProgressTLT);
            this.snapshotPanel.Controls.Add(this.progressBar1);
            this.snapshotPanel.Controls.Add(this.snapshotMenuActionLBL);
            this.snapshotPanel.Controls.Add(this.snapshotMenuActionTLT);
            this.snapshotPanel.Controls.Add(this.snapshotMenuFileLBL);
            this.snapshotPanel.Controls.Add(this.snapshotMenuFileTLT);
            this.snapshotPanel.Controls.Add(this.currentStatusTLT);
            this.snapshotPanel.Controls.Add(this.SnapshotMenuCurrentStatusLBL);
            this.snapshotPanel.Controls.Add(this.snapshotMenuStatusTitle);
            this.snapshotPanel.Controls.Add(this.snapshotMenuStartBTN);
            this.snapshotPanel.Controls.Add(this.panel2);
            this.snapshotPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.snapshotPanel.Location = new System.Drawing.Point(190, 44);
            this.snapshotPanel.Name = "snapshotPanel";
            this.snapshotPanel.Size = new System.Drawing.Size(457, 353);
            this.snapshotPanel.TabIndex = 2;
            this.snapshotPanel.Visible = false;
            // 
            // snapshotMenuActionProgressTLT
            // 
            this.snapshotMenuActionProgressTLT.AutoSize = true;
            this.snapshotMenuActionProgressTLT.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.snapshotMenuActionProgressTLT.Location = new System.Drawing.Point(167, 223);
            this.snapshotMenuActionProgressTLT.Name = "snapshotMenuActionProgressTLT";
            this.snapshotMenuActionProgressTLT.Size = new System.Drawing.Size(105, 14);
            this.snapshotMenuActionProgressTLT.TabIndex = 10;
            this.snapshotMenuActionProgressTLT.Text = "Action Progress";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(157, 252);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(133, 23);
            this.progressBar1.TabIndex = 9;
            // 
            // snapshotMenuActionLBL
            // 
            this.snapshotMenuActionLBL.AutoSize = true;
            this.snapshotMenuActionLBL.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.snapshotMenuActionLBL.Location = new System.Drawing.Point(205, 188);
            this.snapshotMenuActionLBL.Name = "snapshotMenuActionLBL";
            this.snapshotMenuActionLBL.Size = new System.Drawing.Size(40, 14);
            this.snapshotMenuActionLBL.TabIndex = 8;
            this.snapshotMenuActionLBL.Text = "None";
            // 
            // snapshotMenuActionTLT
            // 
            this.snapshotMenuActionTLT.AutoSize = true;
            this.snapshotMenuActionTLT.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.snapshotMenuActionTLT.Location = new System.Drawing.Point(160, 188);
            this.snapshotMenuActionTLT.Name = "snapshotMenuActionTLT";
            this.snapshotMenuActionTLT.Size = new System.Drawing.Size(50, 14);
            this.snapshotMenuActionTLT.TabIndex = 7;
            this.snapshotMenuActionTLT.Text = "Action:";
            // 
            // snapshotMenuFileLBL
            // 
            this.snapshotMenuFileLBL.AutoSize = true;
            this.snapshotMenuFileLBL.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.snapshotMenuFileLBL.Location = new System.Drawing.Point(187, 156);
            this.snapshotMenuFileLBL.Name = "snapshotMenuFileLBL";
            this.snapshotMenuFileLBL.Size = new System.Drawing.Size(40, 14);
            this.snapshotMenuFileLBL.TabIndex = 6;
            this.snapshotMenuFileLBL.Text = "None";
            // 
            // snapshotMenuFileTLT
            // 
            this.snapshotMenuFileTLT.AutoSize = true;
            this.snapshotMenuFileTLT.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.snapshotMenuFileTLT.Location = new System.Drawing.Point(160, 156);
            this.snapshotMenuFileTLT.Name = "snapshotMenuFileTLT";
            this.snapshotMenuFileTLT.Size = new System.Drawing.Size(33, 14);
            this.snapshotMenuFileTLT.TabIndex = 5;
            this.snapshotMenuFileTLT.Text = "File:";
            // 
            // currentStatusTLT
            // 
            this.currentStatusTLT.AutoSize = true;
            this.currentStatusTLT.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.currentStatusTLT.Location = new System.Drawing.Point(157, 125);
            this.currentStatusTLT.Name = "currentStatusTLT";
            this.currentStatusTLT.Size = new System.Drawing.Size(105, 14);
            this.currentStatusTLT.TabIndex = 4;
            this.currentStatusTLT.Text = "Current Status:";
            // 
            // SnapshotMenuCurrentStatusLBL
            // 
            this.SnapshotMenuCurrentStatusLBL.AutoSize = true;
            this.SnapshotMenuCurrentStatusLBL.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.SnapshotMenuCurrentStatusLBL.Location = new System.Drawing.Point(259, 125);
            this.SnapshotMenuCurrentStatusLBL.Name = "SnapshotMenuCurrentStatusLBL";
            this.SnapshotMenuCurrentStatusLBL.Size = new System.Drawing.Size(31, 14);
            this.SnapshotMenuCurrentStatusLBL.TabIndex = 3;
            this.SnapshotMenuCurrentStatusLBL.Text = "Idle";
            // 
            // snapshotMenuStatusTitle
            // 
            this.snapshotMenuStatusTitle.AutoSize = true;
            this.snapshotMenuStatusTitle.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.snapshotMenuStatusTitle.Location = new System.Drawing.Point(148, 90);
            this.snapshotMenuStatusTitle.Name = "snapshotMenuStatusTitle";
            this.snapshotMenuStatusTitle.Size = new System.Drawing.Size(151, 18);
            this.snapshotMenuStatusTitle.TabIndex = 2;
            this.snapshotMenuStatusTitle.Text = "Snapshot Status";
            this.snapshotMenuStatusTitle.Click += new System.EventHandler(this.snapshotMenuStatusTitle_Click);
            // 
            // snapshotMenuStartBTN
            // 
            this.snapshotMenuStartBTN.Location = new System.Drawing.Point(187, 281);
            this.snapshotMenuStartBTN.Name = "snapshotMenuStartBTN";
            this.snapshotMenuStartBTN.Size = new System.Drawing.Size(75, 23);
            this.snapshotMenuStartBTN.TabIndex = 1;
            this.snapshotMenuStartBTN.Text = "Start";
            this.snapshotMenuStartBTN.UseVisualStyleBackColor = true;
            this.snapshotMenuStartBTN.Click += new System.EventHandler(this.snapshotMenuStartSnapshot);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.snapshotMenuTitle);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(457, 56);
            this.panel2.TabIndex = 0;
            // 
            // snapshotMenuTitle
            // 
            this.snapshotMenuTitle.AutoSize = true;
            this.snapshotMenuTitle.Font = new System.Drawing.Font("Verdana", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.snapshotMenuTitle.Location = new System.Drawing.Point(135, 16);
            this.snapshotMenuTitle.Name = "snapshotMenuTitle";
            this.snapshotMenuTitle.Size = new System.Drawing.Size(183, 25);
            this.snapshotMenuTitle.TabIndex = 0;
            this.snapshotMenuTitle.Text = "Snapshot Menu";
            // 
            // snapshotConfigPanel2
            // 
            this.snapshotConfigPanel2.BackColor = System.Drawing.Color.White;
            this.snapshotConfigPanel2.Controls.Add(this.snapshotConfigBTN);
            this.snapshotConfigPanel2.Controls.Add(this.snapshotConfigCurrentPathLBL);
            this.snapshotConfigPanel2.Controls.Add(this.snapshotConfigCurrentPathTLT);
            this.snapshotConfigPanel2.Controls.Add(this.snapshotConfigInnerTLT);
            this.snapshotConfigPanel2.Controls.Add(this.snapshotConfigTitlePanel);
            this.snapshotConfigPanel2.Location = new System.Drawing.Point(625, 54);
            this.snapshotConfigPanel2.Name = "snapshotConfigPanel2";
            this.snapshotConfigPanel2.Size = new System.Drawing.Size(562, 375);
            this.snapshotConfigPanel2.TabIndex = 3;
            this.snapshotConfigPanel2.Visible = false;
            // 
            // snapshotConfigBTN
            // 
            this.snapshotConfigBTN.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.snapshotConfigBTN.Location = new System.Drawing.Point(80, 153);
            this.snapshotConfigBTN.Name = "snapshotConfigBTN";
            this.snapshotConfigBTN.Size = new System.Drawing.Size(75, 23);
            this.snapshotConfigBTN.TabIndex = 4;
            this.snapshotConfigBTN.Text = "Change";
            this.snapshotConfigBTN.UseVisualStyleBackColor = true;
            this.snapshotConfigBTN.Click += new System.EventHandler(this.snapshotPathBTN_Click);
            // 
            // snapshotConfigCurrentPathLBL
            // 
            this.snapshotConfigCurrentPathLBL.AutoSize = true;
            this.snapshotConfigCurrentPathLBL.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.snapshotConfigCurrentPathLBL.Location = new System.Drawing.Point(176, 125);
            this.snapshotConfigCurrentPathLBL.Name = "snapshotConfigCurrentPathLBL";
            this.snapshotConfigCurrentPathLBL.Size = new System.Drawing.Size(40, 14);
            this.snapshotConfigCurrentPathLBL.TabIndex = 3;
            this.snapshotConfigCurrentPathLBL.Text = "None";
            // 
            // snapshotConfigCurrentPathTLT
            // 
            this.snapshotConfigCurrentPathTLT.AutoSize = true;
            this.snapshotConfigCurrentPathTLT.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.snapshotConfigCurrentPathTLT.Location = new System.Drawing.Point(80, 125);
            this.snapshotConfigCurrentPathTLT.Name = "snapshotConfigCurrentPathTLT";
            this.snapshotConfigCurrentPathTLT.Size = new System.Drawing.Size(93, 14);
            this.snapshotConfigCurrentPathTLT.TabIndex = 2;
            this.snapshotConfigCurrentPathTLT.Text = "Current Path:";
            // 
            // snapshotConfigInnerTLT
            // 
            this.snapshotConfigInnerTLT.AutoSize = true;
            this.snapshotConfigInnerTLT.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.snapshotConfigInnerTLT.Location = new System.Drawing.Point(80, 90);
            this.snapshotConfigInnerTLT.Name = "snapshotConfigInnerTLT";
            this.snapshotConfigInnerTLT.Size = new System.Drawing.Size(136, 18);
            this.snapshotConfigInnerTLT.TabIndex = 1;
            this.snapshotConfigInnerTLT.Text = "Snapshot Path";
            // 
            // snapshotConfigTitlePanel
            // 
            this.snapshotConfigTitlePanel.Controls.Add(this.snapshotConfigTitleLBL);
            this.snapshotConfigTitlePanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.snapshotConfigTitlePanel.Location = new System.Drawing.Point(0, 0);
            this.snapshotConfigTitlePanel.Name = "snapshotConfigTitlePanel";
            this.snapshotConfigTitlePanel.Size = new System.Drawing.Size(562, 59);
            this.snapshotConfigTitlePanel.TabIndex = 0;
            // 
            // snapshotConfigTitleLBL
            // 
            this.snapshotConfigTitleLBL.AutoSize = true;
            this.snapshotConfigTitleLBL.Font = new System.Drawing.Font("Verdana", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.snapshotConfigTitleLBL.Location = new System.Drawing.Point(188, 16);
            this.snapshotConfigTitleLBL.Name = "snapshotConfigTitleLBL";
            this.snapshotConfigTitleLBL.Size = new System.Drawing.Size(194, 25);
            this.snapshotConfigTitleLBL.TabIndex = 0;
            this.snapshotConfigTitleLBL.Text = "Snapshot Config";
            // 
            // SnapshotStartWorker
            // 
            this.SnapshotStartWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.SnapshotStartWorker_DoWork);
            // 
            // SnapshotDownloadWrkr
            // 
            this.SnapshotDownloadWrkr.DoWork += new System.ComponentModel.DoWorkEventHandler(this.SnapshotDownloadWorker);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(647, 397);
            this.Controls.Add(this.snapshotConfigPanel2);
            this.Controls.Add(this.snapshotPanel);
            this.Controls.Add(this.panelTitleBar);
            this.Controls.Add(this.panelMenu);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.panelMenu.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.IconPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panelTitleBar.ResumeLayout(false);
            this.panelTitleBar.PerformLayout();
            this.snapshotPanel.ResumeLayout(false);
            this.snapshotPanel.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.snapshotConfigPanel2.ResumeLayout(false);
            this.snapshotConfigPanel2.PerformLayout();
            this.snapshotConfigTitlePanel.ResumeLayout(false);
            this.snapshotConfigTitlePanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Panel panelMenu;
        private Panel panelTitleBar;
        private Panel homePanel;
        private FolderBrowserDialog folderBrowserDialog1;
        private FontAwesome.Sharp.IconButton iconButton2;
        private Panel panel1;
        private FontAwesome.Sharp.IconButton btnMenu;
        private PictureBox pictureBox1;
        private FontAwesome.Sharp.IconButton iconButton8;
        private FontAwesome.Sharp.IconButton iconButton6;
        private FontAwesome.Sharp.IconButton iconButton5;
        private FontAwesome.Sharp.IconButton iconButton4;
        private FontAwesome.Sharp.IconButton iconButton3;
        private Label label1;
        private FontAwesome.Sharp.IconButton iconButton9;
        private Label snapshotMenuTitle;
        private Panel IconPanel;
        private Panel snapshotConfigPanel;
        private Panel snapshotTitlePanel;
        private Label snapshotConfigTitle;
        private Label snapshotPathLBL;
        private Button snapshotPathBTN;
        private Label snapshotCurrentPathLBL;
        private Label currentPathTLBL;
        private Panel snapshotPanel;
        private Panel panel2;
        private Button snapshotMenuStartBTN;
        private Label snapshotMenuActionProgressTLT;
        private ProgressBar progressBar1;
        private Label snapshotMenuActionLBL;
        private Label snapshotMenuActionTLT;
        private Label snapshotMenuFileLBL;
        private Label snapshotMenuFileTLT;
        private Label currentStatusTLT;
        private Label SnapshotMenuCurrentStatusLBL;
        private Label snapshotMenuStatusTitle;
        private System.ComponentModel.BackgroundWorker SnapshotStartWorker;
        private System.ComponentModel.BackgroundWorker SnapshotDownloadWrkr;
        private Panel snapshotConfigPanel2;
        private Label snapshotConfigInnerTLT;
        private Panel snapshotConfigTitlePanel;
        private Label snapshotConfigTitleLBL;
        private Button snapshotConfigBTN;
        private Label snapshotConfigCurrentPathLBL;
        private Label snapshotConfigCurrentPathTLT;
        private FolderBrowserDialog folderBrowserDialog2;
    }
}