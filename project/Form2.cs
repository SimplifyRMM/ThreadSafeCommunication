using System;
using System.Windows.Forms;
using Utilities;

namespace ConsoleReading
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            string user = Environment.UserDomainName.ToString() + @"\" + Environment.UserName.ToString();
            Tools.SendUpdate(user + " logged in successfully.");
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Tools.SendUpdate("Set Color Scheme: " + comboBox1.Text.ToString());
        }
    }
}
