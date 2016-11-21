using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using WindowsFormsApplication1;
using System.Threading;
using OnoFile;
using System.Diagnostics;

namespace WindowsFormsApplication1
{
    public partial class MainWindow : Form
    {
        string LoggingInfo = "";
        string dlLoc = "";
        string vidLoc = "";

        bool rename = false;
        bool special = false;
        bool folder = false;
        bool mkv = false;
        bool mp4 = false;
        bool movie = false;
        string[] args;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            args = Environment.GetCommandLineArgs(); //Checking for UI-less args
            if (args.Length > 1 && args[1] == "-ui") //hide the ui and make it automatic
            {
                try
                {
                    Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\OnoFile\\");
                    using (StreamReader file = new System.IO.StreamReader(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\OnoFile\\" + "save.dat"))
                    {
                        dlLoc = file.ReadLine();
                        vidLoc = file.ReadLine();
                        rename = bool.Parse(file.ReadLine());
                        special = bool.Parse(file.ReadLine());
                        folder = bool.Parse(file.ReadLine());
                        mkv = bool.Parse(file.ReadLine());
                        mp4 = bool.Parse(file.ReadLine());
                        movie = bool.Parse(file.ReadLine());
                        //SAVE CHECKBOX SETTINGS

                        this.WindowState = FormWindowState.Minimized;
                        notifyIcon1.Visible = true;
                        notifyIcon1.BalloonTipText = "Starting transfers...";
                        notifyIcon1.ShowBalloonTip(500);

                        FileTransfer BeginTransfer = new FileTransfer(this); //Pass the form info to the object, a bit of a shitty way to do it I think - a lot of performance lost
                        BeginTransfer.Logic(dlLoc, vidLoc, rename, special, folder, mkv, mp4, movie); //no multi threading here, we dont need the form usable so..
                        notifyIcon1.BalloonTipText = "Transfers Complete";
                        notifyIcon1.ShowBalloonTip(500);
                        Environment.Exit(0);
                    }
                }
                catch { status.Text = "Error with save data"; }
            }

            try
            {
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\OnoFile\\");
                using (StreamReader file = new System.IO.StreamReader(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\OnoFile\\" + "save.dat"))
                {
                    DLbox.Text = file.ReadLine();
                    videoBox.Text = file.ReadLine();
                    renameCB.Checked = bool.Parse(file.ReadLine());
                    specialCB.Checked = bool.Parse(file.ReadLine());
                    folderCB.Checked = bool.Parse(file.ReadLine());
                    mkvCB.Checked = bool.Parse(file.ReadLine());
                    mp4CB.Checked = bool.Parse(file.ReadLine());
                    movie = bool.Parse(file.ReadLine());
                    //SAVE CHECKBOX SETTINGS
                }
            }
            catch { status.Text = "Error with save data"; }       
                
            
        }

        private void transferBtn_Click(object sender, EventArgs e)
        {
            //progressBar.Visible = true;
            //void Logic(string downloadLocation, string endLocation, bool rename, bool special, bool folder, bool mkv, bool mp4)

            if (renameCB.Checked == true) rename = true;
            if (specialCB.Checked == true) special = true;
            if (folderCB.Checked == true) folder = true;
            if (mkvCB.Checked == true) mkv = true;
            if (mp4CB.Checked == true) mp4 = true;

            FileTransfer BeginTransfer = new FileTransfer(this); //Pass the form info to the object, a bit of a shitty way to do it I think - a lot of performance lost
            new Thread(() => BeginTransfer.Logic(DLbox.Text, videoBox.Text, rename, special, folder, mkv, mp4, movie)).Start();
        }

        public void UpdateStatus(string statusMessage) //MULTITHREADING INVOKE STUFF - R.I.P 
        {
            try
            {
                LoggingInfo = LoggingInfo + statusMessage + "\r\n";
                if (status.InvokeRequired == true) status.Invoke((MethodInvoker)delegate { status.Text = statusMessage; });
                else status.Text = statusMessage;
            }
            catch { /*will fail if the ui is hidden*/}
        }

        public void CLOSE_BUTTON_ENABLED(bool cond)
        {
            try
            {
                if (status.InvokeRequired == true) status.Invoke((MethodInvoker)delegate { exitButton.Enabled = cond; });
                else exitButton.Enabled = cond;
            
            }
            catch { /*will fail if the ui is hidden*/}
        }

        public void UpdateProgressBar(int current, int total) //MULTITHREADING INVOKE STUFF - R.I.P
        {
            try
            {
                current--; //Since its completed
                if (status.InvokeRequired == true) status.Invoke((MethodInvoker)delegate 
                {
                    progressBar.Value = 100 - (Convert.ToInt32((double)current / (double)total * 100));
                });
                else progressBar.Value = 100 - (Convert.ToInt32((double)current / (double)total * 100));//CALC PERCENTAGE}
            }
            catch { /*will fail if the ui is hidden*/}
        }

        public void ShowLogBtn(bool cond) //MULTITHREADING INVOKE STUFF - R.I.P
        {
            try
            {
                if (status.InvokeRequired == true) status.Invoke((MethodInvoker)delegate { LogBtn.Visible = cond; });
                else LogBtn.Visible = cond;
            }
            catch { /*will fail if the ui is hidden*/}
        }

        public void pBarVis(bool boo) //MULTITHREADING INVOKE STUFF - R.I.P
        {
            try
            {
                if (status.InvokeRequired == true) status.Invoke((MethodInvoker)delegate { progressBar.Visible = boo; });
                else progressBar.Visible = boo;
            }
            catch { /*will fail if the ui is hidden*/}
        }
        #region Move Window
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        {
            MoveWindow(sender, e);
        }
        private void Form1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            MoveWindow(sender, e);
        }
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            MoveWindow(sender, e);
        }

        public void MoveWindow(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            MoveWindow(sender, e);
        }

        private void label2_MouseDown(object sender, MouseEventArgs e)
        {
            MoveWindow(sender, e);
        }
        #endregion

        private void exitButton_Click(object sender, EventArgs e)
        {
            using (StreamWriter writer = new StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\OnoFile\\" + "save.dat"))
            {
                writer.WriteLine(DLbox.Text);
                writer.WriteLine(videoBox.Text);
                if (renameCB.Checked == true) writer.WriteLine("true");
                else writer.WriteLine("false");
                if (specialCB.Checked == true) writer.WriteLine("true");
                else writer.WriteLine("false");
                if (folderCB.Checked == true) writer.WriteLine("true");
                else writer.WriteLine("false");
                if (mkvCB.Checked == true) writer.WriteLine("true");
                else writer.WriteLine("false");
                if (mp4CB.Checked == true) writer.WriteLine("true");
                else writer.WriteLine(false);
                if (movieCB.Checked == true) writer.WriteLine("true");
                else writer.WriteLine(false);
            }
            Environment.Exit(0);
        }

        #region Folder Browser Buttons
        private void browseDLBtn_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dlg = new FolderBrowserDialog())
            {
                dlg.Description = "Select a folder";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    DLbox.Text = dlg.SelectedPath;
                }
            }
        }

        private void videoBtn_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dlg = new FolderBrowserDialog())
            {
                dlg.Description = "Select a folder";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    videoBox.Text = dlg.SelectedPath;
                }
            }
        }
        #endregion

        private void LogBtn_Click(object sender, EventArgs e)
        {
            log NewLog = new log(LoggingInfo, this.Location.X, this.Location.Y);
            NewLog.ShowDialog();
        }

        private void helpBtn_Click(object sender, EventArgs e)
        {
            helpinfo HELP = new helpinfo(this.Location.X, this.Location.Y);
            HELP.ShowDialog();
        }
    }
}
