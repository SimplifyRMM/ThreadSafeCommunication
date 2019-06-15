using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utilities;

namespace ConsoleReading
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            Tools.box = logBox;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Tools.SendUpdate("Application opened, awaiting user input...");
        }

        private void BackgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            Tools.SendUpdate("BackgroundWorker checking in!");
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            Tools.SendUpdate(textBox1.Text.Trim());
        }

        private void Button1_Click(object sender, EventArgs e)
        {   
            Tools.SendUpdate("BackgroundWorker starting...");

            backgroundWorker1.RunWorkerAsync();
        }

        private void BackgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Tools.SendUpdate("BackgroundWorker finished!");
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.Show();
        }
    }
}
