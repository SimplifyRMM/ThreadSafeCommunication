# ThreadSafeCommunication
Tiny lesson for those new to C Sharp (.NET WinForms) who want to be able to update a control on a form safely across threads or from other forms.

## Step 1 ─ Creating a Class
* Add a new class to your project named "Utilities.cs"
* Once inside the class, erase everything except the "Using" statements at the top.
* Copy and paste this code below your "Using" statements.
  
```csharp
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
```
## Step 2 ─ Referencing Classes and Assigning Variables
* Go back to the Code View for the form that your textBox is on.
* Scroll to the top of your code and add to the bottom of your "Using" statements:
```csharp
using Utilities;
```
* Scroll down slightly until you see InitializeComponent() and, below that, set the public textBox we created earlier to the textBox on your form. See below:
```csharp
public Form1()
{
  InitializeComponent();
  //This is the only line we're adding, the rest of this method is automatically generated by Visual Studio.
  //In the Utilities file we created, our Namespace was "Utilities" and our Public Class was called "Tools." It may seem that you'd call this code by writing "Utilities.Tools.box," we've already made our reference to "Utilities" in the "Using" statement.
  Tools.box = logBox; 
}
```
## Step 3 ─ Calling Custom Methods
* Now that we've referenced our custom class and assigned the control that we want to update, we can feel free to call the "SendUpdate" method from anywhere. Here are a few examples:
* **Button_Click Event**
```csharp
private void Button1_Click(object sender, EventArgs e)
{   
  Tools.SendUpdate("You clicked Button1!");
}
```
* **Updating from another Form (Form 2 in this example.)**
```csharp
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
    }
}
```

Please feel free to leave comments with better/more efficient/more proper ways to handle this issue! 
Thank you.