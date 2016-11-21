namespace OnoFile
{
    partial class helpinfo
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
            this.helpTB = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // helpTB
            // 
            this.helpTB.Location = new System.Drawing.Point(-2, -1);
            this.helpTB.Name = "helpTB";
            this.helpTB.Size = new System.Drawing.Size(342, 440);
            this.helpTB.TabIndex = 0;
            this.helpTB.Text = "";
            // 
            // helpinfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(340, 438);
            this.Controls.Add(this.helpTB);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "helpinfo";
            this.Text = "Help Info";
            this.Load += new System.EventHandler(this.helpinfo_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox helpTB;

    }
}