using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Dev.ProcessMonitor.FormTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var filename = @"..\..\..\Dev.ProcessMonitor.TestTargerExe\bin\Debug\Dev.ProcessMonitor.TestTargerExe.exe";
            var arg = "";

            Dev.ProcessMonitor.ProcessStarter starter = new ProcessStarter(filename, arg);
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

        private void starter_StandardOut(object sender, StandardOutArg e)
        {
            var msg = ("standout=>" + e.ProcessId + "=>" + e.OutPut);
            textBox1.AppendText(msg + "\r\n");
        }

        private void starter_StandardErrorOut(object sender, StandardErrorArg e)
        {
            var msg = ("error=>" + e.ProcessId + "=>" + e.OutPut);
            this.textBox2.AppendText(msg + "\r\n");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var filename = @"..\..\..\Dev.ProcessMonitor.TestTargerExe\bin\Debug\Dev.ProcessMonitor.TestTargerExe.exe";
            var arg = "";

            Dev.ProcessMonitor.ProcessStarterSync starter = new ProcessStarterSync(filename, arg);
            starter.StandardErrorOut += starter_StandardErrorOut;
            starter.StandardOut += starter_StandardOut;
            starter.Finished += starter_Finished;
            starter.StartAsync();
        }

        void starter_Finished(object sender, EventArgs e)
        {
            MessageBox.Show("finished");
        }
    }
}
