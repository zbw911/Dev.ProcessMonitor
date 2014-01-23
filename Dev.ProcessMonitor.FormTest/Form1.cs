// ***********************************************************************************
//  Created by zbw911 
//  创建于：2014年01月22日 15:52
//  
//  修改于：2014年01月22日 18:35
//  文件名：Dev.ProcessMonitor/Dev.ProcessMonitor.FormTest/Form1.cs
//  
//  如果有更好的建议或意见请邮件至 zbw911#gmail.com
// ***********************************************************************************
using System;
using System.Configuration;
using System.Threading;
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

            var starter = new ProcessStarterAsyn(filename, arg);
            starter.StandardErrorOut += starter_StandardErrorOut;
            starter.StandardOut += starter_StandardOut;
            starter.StartAsyn();
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


            _processStarterSync = new ProcessStarterSync(filename, arg);
            _processStarterSync.StandardErrorOut += starter_StandardErrorOut;
            _processStarterSync.StandardOut += starter_StandardOut;
            _processStarterSync.Finished += starter_Finished;
            _processStarterSync.StartAsync();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            _processStarterSync.CancelEncodeAsync();
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

        private void button4_Click(object sender, EventArgs e)
        {
            Monitor m = new Monitor(true);
            m.Start();
        }
    }
}