using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Utilities
{
    public class Tools
    {
        //Creates a public control that can be referenced from anywhere within this project.
        public static TextBox box;
        //Creates a method to update the output window across threads (accepts a string argument)
        public static void SendUpdate(string update)
        {
            try
            {
                //Adds the current time in brackets before the log entry so that it looks nice.
                string logEntry = "[" + DateTime.Now.ToLongTimeString() + "] ─ " + update.Trim() + Environment.NewLine;

                //Appending strings to a textBox in C# makes the textBox automatically scroll to the bottom (most recent entry.)
                //If the logBox is on another thread, invoke this method and append the text
                if (box.InvokeRequired)
                {
                    box.Invoke(new MethodInvoker(delegate { box.AppendText(logEntry); }));
                }

                //If the sender is on the same thread, update the control normally
                else
                {
                    box.AppendText(logEntry);
                }
            }

            //If there's an error, catch it and display an error message.
            //After the user clicks OK, return back to the form.
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
    }
}
