using System;
using System.Windows.Forms;

namespace Dev.ProcessMonitor.FormTest
{
    public partial class Form1 : Form
    {
        #region C'tors

        public Form1()
        {
            InitializeComponent();
        }

        #endregion

        #region Event Handling

        private void button1_Click(object sender, EventArgs e)
        {
            string filename =
                @"..\..\..\Dev.ProcessMonitor.TestTargerExe\bin\Debug\Dev.ProcessMonitor.TestTargerExe.exe";
            string arg = "";

            var starter = new ProcessStarter(filename, arg);
            starter.StandardErrorOut += starter_StandardErrorOut;
            starter.StandardOut += starter_StandardOut;
            starter.Start();
        }


        //static void starter_StandardOut(object sender, StandardOutArg e)
        //{
        //    Console.WriteLine("standout=>" + e.ProcessId + "=>" + e.OutPut);
        //}

        //static void starter_StandardErrorOut(object sender, StandardErrorArg e)
        //{
        //    Console.WriteLine("error=>" + e.ProcessId + "=>" + e.OutPut);
        //}

        private void button2_Click(object sender, EventArgs e)
        {
            string filename =
                @"..\..\..\Dev.ProcessMonitor.TestTargerExe\bin\Debug\Dev.ProcessMonitor.TestTargerExe.exe";
            string arg = "";

            var starter = new ProcessStarterSync(filename, arg);
            starter.StandardErrorOut += starter_StandardErrorOut;
            starter.StandardOut += starter_StandardOut;
            starter.Finished += starter_Finished;
            starter.StartAsync();
        }

        private void starter_Finished(object sender, EventArgs e)
        {
            MessageBox.Show("finished");
        }

        private void starter_StandardErrorOut(object sender, StandardErrorArg e)
        {
            string msg = ("error=>" + e.ProcessId + "=>" + e.OutPut);
            textBox2.AppendText(msg + "\r\n");
        }

        private void starter_StandardOut(object sender, StandardOutArg e)
        {
            string msg = ("standout=>" + e.ProcessId + "=>" + e.OutPut);
            textBox1.AppendText(msg + "\r\n");
        }

        #endregion
    }
}