using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OnoFile
{
    public partial class helpinfo : Form
    {
        int xPos;
        int yPos;
        public helpinfo(int x, int y)
        {
            InitializeComponent();
            //345, 577 - ono form size
            xPos = x;
            yPos = y + 50;
        }

        private void helpinfo_Load(object sender, EventArgs e)
        {
            this.Location = new Point(xPos, yPos);
            helpTB.Text = "\r\nOnoFile is used to transfer video files from one location to another," +
                            " and then rename the file into an appropriate format for the Kodi media software" +
                        " to create an organised list of your videos. OnoFile can be used without Kodi to create an organised library" +
                        ", but it is recommended.\r\n\r\n" +
                        "The naming convenion OnoFile uses is: \r\n\r\n<series-name> - S<season number>E<episode number>\r\n\r\n" +
                        "This video will be put into it's 'series-name' folder and then rename the video into this format, for example" +
                        "\r\n\r\nDoctor.Who - episode 20\r\n\r\n" +
                        "This file will be moved to the location under the 'Doctor Who' folder, then renamed to: \r\n\r\n" +
                        "Doctor Who - S01E20\r\n\r\n" +
                        "Note that OnoFile will assume that the file belongs in whatever the latest seasons folder is, so if there was a second season folder in the Doctor Who directory, then it would be named to the second season instead. " +
                        "There are options to allow the creation of a new folder if there is no 'Doctor Who' folder in the destination, there is also an option to check for special episodes (these episodes are given the season 0)." +
                        "\r\n\r\nIf you would like to run OnoFile automatically from the saved settings in the background, use the start-up arguments '-ui'.";

            helpTB.Text = helpTB.Text + "\r\n\r\n\r\n\r\n\r\n" +
                "Version Info:\r\n\r\n" +
                "1.2 - Added appropriate logic for higher episode name checking, now asks the user if the episode number is correct if greater then 25\r\n\r\n\r\n" +
                "1.1 - Added help info, also added changes to the tray icon.\r\n      - Added a checkbox for future implementation of movies.\r\n      - Finally added smart folder searching, it should now detect that 'doctor who s2' belongs in 'doctor who' folder";
        }
    }
}
