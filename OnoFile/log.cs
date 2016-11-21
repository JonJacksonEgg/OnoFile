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
    public partial class log : Form
    {
        int xPos;
        int yPos;
        public log(string Log, int x, int y)
        {
            InitializeComponent();
            LogBox.Text = Log; //345, 577 - ono form size
            xPos = x + 345;
            yPos = y;
        }

        private void log_Load(object sender, EventArgs e)
        {
            this.Location = new Point(xPos, yPos);

        }

        private void LogBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
