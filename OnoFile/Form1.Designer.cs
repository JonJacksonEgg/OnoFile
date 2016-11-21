namespace WindowsFormsApplication1
{
    partial class MainWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.DLbox = new System.Windows.Forms.TextBox();
            this.videoBox = new System.Windows.Forms.TextBox();
            this.exitButton = new System.Windows.Forms.PictureBox();
            this.transferBtn = new System.Windows.Forms.Button();
            this.browseDLBtn = new System.Windows.Forms.Button();
            this.videoBtn = new System.Windows.Forms.Button();
            this.renameCB = new System.Windows.Forms.CheckBox();
            this.specialCB = new System.Windows.Forms.CheckBox();
            this.folderCB = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.mkvCB = new System.Windows.Forms.CheckBox();
            this.mp4CB = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.status = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.LogBtn = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.movieCB = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.helpBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.exitButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(198, 175);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(107, 133);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 1;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox2_MouseDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.label1.Font = new System.Drawing.Font("Trebuchet MS", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(48, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(240, 35);
            this.label1.TabIndex = 2;
            this.label1.Text = "Downloads Folder";
            this.label1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.label1_MouseDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.label2.Font = new System.Drawing.Font("Trebuchet MS", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(69, 311);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(178, 35);
            this.label2.TabIndex = 3;
            this.label2.Text = "Video Folder";
            this.label2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.label2_MouseDown);
            // 
            // DLbox
            // 
            this.DLbox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DLbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DLbox.Location = new System.Drawing.Point(50, 80);
            this.DLbox.Name = "DLbox";
            this.DLbox.Size = new System.Drawing.Size(225, 26);
            this.DLbox.TabIndex = 5;
            this.DLbox.TabStop = false;
            // 
            // videoBox
            // 
            this.videoBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.videoBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.videoBox.Location = new System.Drawing.Point(52, 355);
            this.videoBox.Name = "videoBox";
            this.videoBox.Size = new System.Drawing.Size(223, 26);
            this.videoBox.TabIndex = 5;
            this.videoBox.TabStop = false;
            // 
            // exitButton
            // 
            this.exitButton.Image = ((System.Drawing.Image)(resources.GetObject("exitButton.Image")));
            this.exitButton.Location = new System.Drawing.Point(316, 3);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(25, 25);
            this.exitButton.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.exitButton.TabIndex = 6;
            this.exitButton.TabStop = false;
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // transferBtn
            // 
            this.transferBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.transferBtn.Location = new System.Drawing.Point(96, 400);
            this.transferBtn.Name = "transferBtn";
            this.transferBtn.Size = new System.Drawing.Size(128, 37);
            this.transferBtn.TabIndex = 7;
            this.transferBtn.Text = "Transfer";
            this.transferBtn.UseVisualStyleBackColor = true;
            this.transferBtn.Click += new System.EventHandler(this.transferBtn_Click);
            // 
            // browseDLBtn
            // 
            this.browseDLBtn.Location = new System.Drawing.Point(275, 79);
            this.browseDLBtn.Name = "browseDLBtn";
            this.browseDLBtn.Size = new System.Drawing.Size(30, 26);
            this.browseDLBtn.TabIndex = 8;
            this.browseDLBtn.Text = "...";
            this.browseDLBtn.UseVisualStyleBackColor = true;
            this.browseDLBtn.Click += new System.EventHandler(this.browseDLBtn_Click);
            // 
            // videoBtn
            // 
            this.videoBtn.Location = new System.Drawing.Point(275, 355);
            this.videoBtn.Name = "videoBtn";
            this.videoBtn.Size = new System.Drawing.Size(30, 26);
            this.videoBtn.TabIndex = 8;
            this.videoBtn.Text = "...";
            this.videoBtn.UseVisualStyleBackColor = true;
            this.videoBtn.Click += new System.EventHandler(this.videoBtn_Click);
            // 
            // renameCB
            // 
            this.renameCB.AutoSize = true;
            this.renameCB.Checked = true;
            this.renameCB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.renameCB.Location = new System.Drawing.Point(71, 155);
            this.renameCB.Name = "renameCB";
            this.renameCB.Size = new System.Drawing.Size(147, 17);
            this.renameCB.TabIndex = 9;
            this.renameCB.Text = "Rename File (name SxEx)";
            this.renameCB.UseVisualStyleBackColor = true;
            // 
            // specialCB
            // 
            this.specialCB.AutoSize = true;
            this.specialCB.Checked = true;
            this.specialCB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.specialCB.Location = new System.Drawing.Point(71, 175);
            this.specialCB.Name = "specialCB";
            this.specialCB.Size = new System.Drawing.Size(126, 17);
            this.specialCB.TabIndex = 9;
            this.specialCB.Text = "Special in name = S0";
            this.specialCB.UseVisualStyleBackColor = true;
            // 
            // folderCB
            // 
            this.folderCB.AutoSize = true;
            this.folderCB.Checked = true;
            this.folderCB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.folderCB.Location = new System.Drawing.Point(71, 196);
            this.folderCB.Name = "folderCB";
            this.folderCB.Size = new System.Drawing.Size(94, 17);
            this.folderCB.TabIndex = 9;
            this.folderCB.Text = "Create Folders";
            this.folderCB.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(51, 240);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 16);
            this.label3.TabIndex = 10;
            this.label3.Text = "Formats:";
            // 
            // mkvCB
            // 
            this.mkvCB.AutoSize = true;
            this.mkvCB.Checked = true;
            this.mkvCB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mkvCB.Location = new System.Drawing.Point(71, 259);
            this.mkvCB.Name = "mkvCB";
            this.mkvCB.Size = new System.Drawing.Size(49, 17);
            this.mkvCB.TabIndex = 11;
            this.mkvCB.Text = ".mkv";
            this.mkvCB.UseVisualStyleBackColor = true;
            // 
            // mp4CB
            // 
            this.mp4CB.AutoSize = true;
            this.mp4CB.Checked = true;
            this.mp4CB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mp4CB.Location = new System.Drawing.Point(71, 282);
            this.mp4CB.Name = "mp4CB";
            this.mp4CB.Size = new System.Drawing.Size(49, 17);
            this.mp4CB.TabIndex = 11;
            this.mp4CB.Text = ".mp4";
            this.mp4CB.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(51, 128);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 16);
            this.label4.TabIndex = 10;
            this.label4.Text = "Options:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(130, 485);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 16);
            this.label5.TabIndex = 10;
            this.label5.Text = "Status:";
            // 
            // status
            // 
            this.status.AutoSize = true;
            this.status.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.status.Location = new System.Drawing.Point(45, 508);
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(0, 16);
            this.status.TabIndex = 10;
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(48, 449);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(227, 26);
            this.progressBar.TabIndex = 12;
            this.progressBar.Visible = false;
            // 
            // LogBtn
            // 
            this.LogBtn.Location = new System.Drawing.Point(122, 545);
            this.LogBtn.Name = "LogBtn";
            this.LogBtn.Size = new System.Drawing.Size(75, 23);
            this.LogBtn.TabIndex = 13;
            this.LogBtn.Text = "Show Log";
            this.LogBtn.UseVisualStyleBackColor = true;
            this.LogBtn.Visible = false;
            this.LogBtn.Click += new System.EventHandler(this.LogBtn_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.Location = new System.Drawing.Point(721, 59);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(211, 126);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "OnoFile";
            this.notifyIcon1.Visible = true;
            // 
            // movieCB
            // 
            this.movieCB.AutoSize = true;
            this.movieCB.Checked = true;
            this.movieCB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.movieCB.Location = new System.Drawing.Point(71, 217);
            this.movieCB.Name = "movieCB";
            this.movieCB.Size = new System.Drawing.Size(82, 17);
            this.movieCB.TabIndex = 9;
            this.movieCB.Text = "Add Movies";
            this.movieCB.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(64, 10);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "V1.2";
            // 
            // helpBtn
            // 
            this.helpBtn.Location = new System.Drawing.Point(12, 5);
            this.helpBtn.Name = "helpBtn";
            this.helpBtn.Size = new System.Drawing.Size(46, 23);
            this.helpBtn.TabIndex = 15;
            this.helpBtn.Text = "Help";
            this.helpBtn.UseVisualStyleBackColor = true;
            this.helpBtn.Click += new System.EventHandler(this.helpBtn_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(345, 577);
            this.Controls.Add(this.helpBtn);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.LogBtn);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.mp4CB);
            this.Controls.Add(this.mkvCB);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.status);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.movieCB);
            this.Controls.Add(this.folderCB);
            this.Controls.Add(this.specialCB);
            this.Controls.Add(this.renameCB);
            this.Controls.Add(this.videoBtn);
            this.Controls.Add(this.browseDLBtn);
            this.Controls.Add(this.transferBtn);
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.videoBox);
            this.Controls.Add(this.DLbox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainWindow";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.exitButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox DLbox;
        private System.Windows.Forms.TextBox videoBox;
        private System.Windows.Forms.PictureBox exitButton;
        private System.Windows.Forms.Button transferBtn;
        private System.Windows.Forms.Button browseDLBtn;
        private System.Windows.Forms.Button videoBtn;
        private System.Windows.Forms.CheckBox renameCB;
        private System.Windows.Forms.CheckBox specialCB;
        private System.Windows.Forms.CheckBox folderCB;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox mkvCB;
        private System.Windows.Forms.CheckBox mp4CB;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label status;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Button LogBtn;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.CheckBox movieCB;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button helpBtn;
    }
}

